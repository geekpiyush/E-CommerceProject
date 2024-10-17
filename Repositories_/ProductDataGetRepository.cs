using Entities;
using Entities.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories_
{
    public class ProductDataGetRepository : IProductDataGetterRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductDataGetRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<List<ProductData>> GetAllProduct()
        {
            return await _db.productData.Include("ProductCategory").ToListAsync();
        }

        public async Task<List<ProductData>> GetFilterdProduct(Expression<Func<ProductData, bool>> predicate)
        {
           return await _db.productData.Include("ProductCategory")
                .Where(predicate)
                .ToListAsync();
        }

        public async  Task<ProductData?> GetProductByProductID(int productID)
        {
           return await _db.productData.FirstOrDefaultAsync(temp=> temp.ProductID == productID);
        }
    }
}
