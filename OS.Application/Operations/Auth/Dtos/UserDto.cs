namespace OS.Application.Operations.Auth.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhotoUrl { get; set; }
        public bool IsActive { get; set; }
        public string Role { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new();
    }
}
