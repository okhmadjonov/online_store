using Microsoft.AspNetCore.Identity;


namespace OS.Domain.Models
{
    public class Role : IdentityRole<Guid>
    {
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
