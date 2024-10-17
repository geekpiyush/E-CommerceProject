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
    public class ProductDataUpdateRepository : IProductDataUpdateRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductDataUpdateRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ProductData> UpdateProduct(ProductData productData)
        {
            ProductData? matchingProduct = await _db.productData.FirstOrDefaultAsync(temp=>temp.ProductID == productData.ProductID);

            if(matchingProduct == null)
            {
                return productData;
            }

            matchingProduct.ProductName = productData.ProductName;
            matchingProduct.Price = productData.Price;
            matchingProduct.Quantity = productData.Quantity;
            matchingProduct.ProductImagePath = productData.ProductImagePath;
            matchingProduct.ProductDescription = productData.ProductDescription;

           await _db.SaveChangesAsync();

            return matchingProduct;
        }
    }
}
