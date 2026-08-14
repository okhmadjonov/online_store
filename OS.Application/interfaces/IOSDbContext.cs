using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using OS.Domain.Models;

namespace OS.Application.interfaces
{
    public interface IOSDbContext
    {



        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductCategoryTranslate> ProductCategoryTranslates { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductTranslate> ProductTranslates { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<RegionTranslate> RegionTranslates { get; set; }
        public DbSet<UserProduct> UserProducts { get; set; }



        public DatabaseFacade Database { get; }


        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
