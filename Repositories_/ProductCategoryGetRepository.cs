using Entities;
using Entities.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories_
{
    public class ProductCategoryGetRepository : IProductCategoryGetterRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductCategoryGetRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<List<ProductCategory>> GetProductCategories()
        {
            return await _db.ProductCategory.ToListAsync();
        }

        public async Task<ProductCategory?> GetProductCategoryByCategoryID(int productCategoryID)
        {
            return await _db.ProductCategory.FirstOrDefaultAsync(temp => temp.CategoryID == productCategoryID);
        }

        public async Task<ProductCategory?> GetProductCategoryByCategoryName(string categoryName)
        {
            return await _db.ProductCategory.FirstOrDefaultAsync(temp=>temp.CategoryName == categoryName);
        }
    }
}
