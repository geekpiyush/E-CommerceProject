using Entities.ENUM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DatabaseContext
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options) 
        {
            
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().ToTable("Products");

            modelBuilder.Entity<Product>().HasData(new Product() { ProductID = Guid.Parse("A6DCC6F8-8707-4234-852B-67A4E6B84300"), ProductCategory = "Base",ProductName = "TestProduct" ,Price=299,Quantity=100});
        }
    }
}
