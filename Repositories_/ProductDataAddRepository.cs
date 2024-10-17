using Entities;
using Entities.DatabaseContext;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories_
{
    public class ProductDataAddRepository : IProductDataAdderRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductDataAddRepository(ApplicationDbContext db)
        {
            _db = db;   
        }
        public async Task<ProductData> AddProduct(ProductData productData)
        {
            _db.productData.Add(productData);

            await _db.SaveChangesAsync();

            return productData;
        }
    }
}
