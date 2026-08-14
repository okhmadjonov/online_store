using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OS.Application.interfaces;
using OS.Domain.Models;


namespace OS.Persistence
{
    public class OSDbContext : IdentityDbContext<User, Role, Guid>, IOSDbContext
    {


        public override DbSet<User> Users { get; set; }
        public override DbSet<Role> Roles { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductCategoryTranslate> ProductCategoryTranslates { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductTranslate> ProductTranslates { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<RegionTranslate> RegionTranslates { get; set; }
        public DbSet<UserProduct> UserProducts { get; set; }
      



        public OSDbContext(DbContextOptions<OSDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
           
            base.OnModelCreating(builder);

            builder.Entity<User>()
             .HasOne(r => r.DefaultRole)
             .WithMany(b => b.Users)
             .HasForeignKey(r => r.DefaultRoleId);
        }
    }
}
