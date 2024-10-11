using Entities;
using Entities.DatabaseContext;
using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductDataDeleteService : IProductDataDeleteService
    {
        private readonly ApplicationDbContext _db;

        public ProductDataDeleteService(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool DeleteProductByProductID(int productID)
        {
            if(productID == null)
            {
                throw new ArgumentNullException(nameof(productID));
            }

            ProductData? matchingProductData = _db.productData.FirstOrDefault(temp=>temp.ProductID == productID);

            if(matchingProductData == null)
            {
                throw new ArgumentException("Given productID not exist");
            }

            _db.productData.Remove(matchingProductData);

            return true;
        }
    }
}
