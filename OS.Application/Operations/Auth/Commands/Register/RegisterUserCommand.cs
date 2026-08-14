using MediatR;
using Microsoft.AspNetCore.Identity;
using OS.Application.Common.Exceptions;
using OS.Application.Common.Utilities;
using OS.Application.Operations.Auth.Dtos;
using OS.Domain;
using OS.Domain.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace OS.Application.Operations.Auth.Commands.Register
{
    public class RegisterUserCommand : IRequest<AuthResponseDto>
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AuthResponseDto>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly TokenManager _tokenManager;

        public RegisterUserCommandHandler(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            TokenManager tokenManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenManager = tokenManager;
        }

        public async Task<AuthResponseDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existingByEmail = await _userManager.FindByEmailAsync(request.Email);
            if (existingByEmail != null)
            {
                throw new ConflictException($"Foydalanuvchi elektron pochtasi '{request.Email}' allaqachon mavjud.");
            }

            var existingByName = await _userManager.FindByNameAsync(request.UserName);
            if (existingByName != null)
            {
                throw new ConflictException($"Foydalanuvchi nomi '{request.UserName}' allaqachon mavjud.");
            }

            // Public self-registration ALWAYS assigns the default User role to prevent privilege escalation
            var targetRoleName = Roles.User;
            var role = await _roleManager.FindByNameAsync(targetRoleName)
                ?? throw new KeyNotFoundException($"Standart '{targetRoleName}' roli topilmadi.");

            var user = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber ?? string.Empty,
                DefaultRole = role,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            var createResult = await _userManager.CreateAsync(user, request.Password);
            if (!createResult.Succeeded)
            {
                var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Foydalanuvchini ro'yxatdan o'tkazib bo'lmadi: {errors}");
            }

            await _userManager.AddToRoleAsync(user, role.Name!);

            var roles = (await _userManager.GetRolesAsync(user)).ToList();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var userRole in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var jwtToken = _tokenManager.CreateToken(claims);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            var refreshTokenString = _tokenManager.GenerateRefreshToken();

            user.RefreshToken = refreshTokenString;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            return new AuthResponseDto
            {
                TokenType = "Bearer",
                AccessToken = tokenString,
                ExpiresIn = (int)Math.Max(0, (jwtToken.ValidTo - DateTime.UtcNow).TotalSeconds),
                RefreshToken = refreshTokenString,
                RefreshTokenExpiration = user.RefreshTokenExpiryTime,
                User = new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName ?? string.Empty,
                    Email = user.Email ?? string.Empty,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhotoUrl = user.PhotoUrl,
                    IsActive = user.IsActive,
                    Role = role.Name ?? roles.FirstOrDefault() ?? string.Empty,
                    Roles = roles
                }
            };
        }
    }
}
