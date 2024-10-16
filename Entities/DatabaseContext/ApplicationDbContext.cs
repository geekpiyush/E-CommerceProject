using Entities.ENUM;
using Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Entities.DatabaseContext
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser,ApplicationUserRole,Guid>
    {
        public ApplicationDbContext(DbContextOptions options):base(options) 
        {
            
        }

       public DbSet<ProductData> productData { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ProductData>().ToTable("ProductData");
            modelBuilder.Entity<ProductCategory>().ToTable("ProductCategory");

            //seed data

            modelBuilder.Entity<ProductData>().HasData(new ProductData
            { ProductID = 11001, CategoryID = 9001, ProductName = "Chanel No. 5", Quantity = "250ml", Price = 347.86,ProductDescription = "For a gentleman who wears his heart on his sleeve and knows self-love is the greatest virtue" }
            , new ProductData { ProductID = 11002, CategoryID = 9002, ProductName = "Dior Sauvage", Quantity = "150ml", Price = 1446.72 },
            new ProductData { ProductID = 11003, CategoryID = 9002, ProductName = "Tom Ford Black Orchid", Quantity = "200ml", Price = 2420.24 },
            new ProductData { ProductID = 11004, CategoryID = 9003, ProductName = "Gucci Bloom", Quantity = "100ml", Price = 1087.0 },
            new ProductData { ProductID = 11005, CategoryID = 9002, ProductName = "Yves Saint Laurent Libre", Quantity = "250ml", Price = 347.86 },
            new ProductData { ProductID = 11006, CategoryID = 9001, ProductName = "Blush Suede", Quantity = "250ml", Price = 347.86 },
            new ProductData { ProductID = 11007, CategoryID = 9003, ProductName = "Versace Eros", Quantity = "250ml", Price = 347.86 },
            new ProductData { ProductID = 11008, CategoryID = 9002, ProductName = "Paco Rabanne", Quantity = "250ml", Price = 347.86 });

            //string ProductJson = File.ReadAllText("Products.json");
            //List<Product> Products = JsonSerializer.Deserialize<List<Product>>(ProductJson);
            //foreach (Product product in Products)
            //{
            //    modelBuilder.Entity<Product>().HasData(product);
            //}

            //seed data

            modelBuilder.Entity<ProductCategory>().HasData(new ProductCategory { CategoryID = 9001, CategoryName = "Male" }, new ProductCategory { CategoryID = 9002, CategoryName = "Female" }, new ProductCategory { CategoryID = 9003, CategoryName = "Luxury" });

            //Table Relation

            modelBuilder.Entity<ProductData>(entity =>
                {
                    entity.HasOne<ProductCategory>(c => c.ProductCategory)
                    .WithMany(p => p.Product)
                    .HasForeignKey(p => p.CategoryID);
                });

        }
    }
}
