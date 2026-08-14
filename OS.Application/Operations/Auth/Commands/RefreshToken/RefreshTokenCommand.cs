using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using OS.Application.Common.Utilities;
using OS.Application.Operations.Auth.Dtos;
using OS.Domain.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace OS.Application.Operations.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommand : IRequest<AuthResponseDto>
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }

    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResponseDto>
    {
        private readonly UserManager<User> _userManager;
        private readonly TokenManager _tokenManager;

        public RefreshTokenCommandHandler(UserManager<User> userManager, TokenManager tokenManager)
        {
            _userManager = userManager;
            _tokenManager = tokenManager;
        }

        public async Task<AuthResponseDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var principal = _tokenManager.GetPrincipalFromExpiredToken(request.AccessToken);
            if (principal == null)
            {
                throw new SecurityTokenException("Yaroqsiz token.");
            }

            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                throw new SecurityTokenException("Token tarkibida foydalanuvchi ma'lumotlari topilmadi.");
            }

            var user = await _userManager.FindByIdAsync(userIdClaim);
            if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                throw new SecurityTokenException("Refresh token yaroqsiz yoki muddati o'tgan.");
            }

            var roles = (await _userManager.GetRolesAsync(user)).ToList();

            var newClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in roles)
            {
                newClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var newJwtToken = _tokenManager.CreateToken(newClaims);
            var newAccessTokenString = new JwtSecurityTokenHandler().WriteToken(newJwtToken);
            var newRefreshTokenString = _tokenManager.GenerateRefreshToken();

            user.RefreshToken = newRefreshTokenString;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            return new AuthResponseDto
            {
                AccessToken = newAccessTokenString,
                RefreshToken = newRefreshTokenString,
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
