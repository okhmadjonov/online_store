using Microsoft.AspNetCore.Identity;



namespace OS.Domain.Models
{
    public class User : IdentityUser<Guid>
    {
        public Guid DefaultRoleId { get; set; }
        public Role? DefaultRole { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public bool IsActive { get; set; }
        public string? PhotoUrl { get; set; }
        public Language? Language { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
        public Region? Region { get; set; }
        public bool? IsMale { get; set; }/* Male - Erkak, Female - Ayol */
        public DateTime? CreatedAt { get; set; }
        public bool IsOnline { get; set; }
    }
}
