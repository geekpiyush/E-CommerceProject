using Entities;
using Entities.DatabaseContext;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductDataUpdateService : IProductDataUpdateService
    {
        private readonly ApplicationDbContext _db;
        private readonly IProductDataGetterService _productService;
        public ProductDataUpdateService(IProductDataGetterService productService,ApplicationDbContext db)
        {
            _productService = productService;
            _db = db;
        }
        public ProductDataResponse UpdateProductData(ProductDataUpdateRequest? productDataUpdateRequest)
        {
           if(productDataUpdateRequest == null)
            {
                throw new ArgumentNullException(nameof(productDataUpdateRequest));
            }

            ValidationHelper.ModelValidation(productDataUpdateRequest);

            //find matching product
            ProductData? matchingProductData = _db.productData.FirstOrDefault(temp => temp.ProductID == productDataUpdateRequest.ProductID);

            if(matchingProductData == null)
            {
                throw new ArgumentException("Given id is not exist");
            }

            matchingProductData.ProductName = productDataUpdateRequest.ProductName;
            matchingProductData.Price = productDataUpdateRequest.Price;
            matchingProductData.ProductImagePath = productDataUpdateRequest.ProductImagePath;
            matchingProductData.Quantity = productDataUpdateRequest.Quantity;
            
            return matchingProductData.ToProductDataResponse();
        }
    }
}
