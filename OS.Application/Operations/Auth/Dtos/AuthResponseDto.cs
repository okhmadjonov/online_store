namespace OS.Application.Operations.Auth.Dtos
{
    public class AuthResponseDto
    {
        public string TokenType { get; set; } = "Bearer";
        public string AccessToken { get; set; } = string.Empty;
        public int ExpiresIn { get; set; } = 3600;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpiration { get; set; }
        public UserDto User { get; set; } = new();
    }
}
