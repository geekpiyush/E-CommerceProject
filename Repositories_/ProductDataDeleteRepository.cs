using Entities.DatabaseContext;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories_
{
    public class ProductDataDeleteRepository : IProductDataDeleteRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductDataDeleteRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> DeleteProductDataByProductID(int productID)
        {
            _db.productData.RemoveRange(_db.productData.Where(temp => temp.ProductID == productID));

            int rowDeleted = await _db.SaveChangesAsync();

            return rowDeleted > 0;
        }
    }
}
