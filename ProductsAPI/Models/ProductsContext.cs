using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProductsAPI.Models
{
    public class ProductsContext:IdentityDbContext<AppUser,AppRole,int>
    {
        public ProductsContext(DbContextOptions<ProductsContext> options):base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Products>().HasData(new Products{ IsActive = true , Price = 60000 , ProductId = 1 , ProductName = "Iphone 13" });
            modelBuilder.Entity<Products>().HasData(new Products{ IsActive = true , Price = 65000 , ProductId = 2 , ProductName = "Iphone 14" });
            modelBuilder.Entity<Products>().HasData(new Products{ IsActive = true , Price = 70000 , ProductId = 3 , ProductName = "Iphone 15" });
            modelBuilder.Entity<Products>().HasData(new Products{ IsActive = true , Price = 75000 , ProductId = 4 , ProductName = "Iphone 16" });
        }
        public DbSet<Products> Products { get; set; }
    }
}