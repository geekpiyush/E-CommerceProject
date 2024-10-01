using E_Commerce_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Project.DatabaseContext
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().ToTable("Products");

            //seedData

            modelBuilder.Entity<Product>().HasData(new Product() { ProductID= Guid.Parse("90CC9D44-14E4-48BF-92D1-4505B94E5E56"),ProductName = "TestProduct",Price=725.00,ProductCategory = "TestCategory",Quantity = 750.00 });
        }
    }

}
