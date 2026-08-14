using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OS.Application.interfaces;
using OS.Domain.Models;
using OS.Domain.Models.Base;

namespace OS.Persistence
{
    public class OSDbContext : IdentityDbContext<User, Role, Guid>, IOSDbContext
    {
        private readonly ICurrentUserService? _currentUserService;

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

        public OSDbContext(DbContextOptions<OSDbContext> options, ICurrentUserService? currentUserService = null)
            : base(options)
        {
            _currentUserService = currentUserService;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<DefaultTable>())
            {
                var userId = _currentUserService?.UserId;
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt ??= DateTime.UtcNow;
                        if (userId.HasValue && userId.Value != Guid.Empty)
                        {
                            entry.Entity.CreatedById ??= userId.Value;
                        }
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        if (userId.HasValue && userId.Value != Guid.Empty)
                        {
                            entry.Entity.UpdatedById = userId.Value;
                        }
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

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
