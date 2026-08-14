using MediatR;
using Microsoft.AspNetCore.Identity;
using OS.Application.Common.Utilities;
using OS.Application.Operations.Auth.Dtos;
using OS.Domain.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace OS.Application.Operations.Auth.Commands.Login
{
    public class LoginUserCommand : IRequest<AuthResponseDto>
    {
        public string UserNameOrEmail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthResponseDto>
    {
        private readonly UserManager<User> _userManager;
        private readonly TokenManager _tokenManager;

        public LoginUserCommandHandler(UserManager<User> userManager, TokenManager tokenManager)
        {
            _userManager = userManager;
            _tokenManager = tokenManager;
        }

        public async Task<AuthResponseDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserNameOrEmail)
                ?? await _userManager.FindByEmailAsync(request.UserNameOrEmail);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Foydalanuvchi nomi yoki parol noto'g'ri.");
            }

            if (!user.IsActive)
            {
                throw new UnauthorizedAccessException("Foydalanuvchi hisobi faol emas.");
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!passwordValid)
            {
                throw new UnauthorizedAccessException("Foydalanuvchi nomi yoki parol noto'g'ri.");
            }

            var roles = (await _userManager.GetRolesAsync(user)).ToList();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var jwtToken = _tokenManager.CreateToken(claims);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            var refreshTokenString = _tokenManager.GenerateRefreshToken();

            user.RefreshToken = refreshTokenString;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            user.LastLoginAt = DateTime.UtcNow;
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
                    Role = user.DefaultRole?.Name ?? roles.FirstOrDefault() ?? string.Empty,
                    Roles = roles
                }
            };
        }
    }
}
