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
    public class ProductDataAdderServie : IProductDataAddService
    {
        private readonly ApplicationDbContext _db;
        private readonly IProductCategoryGetterService _categoryService;
        public ProductDataAdderServie(ApplicationDbContext db, IProductCategoryGetterService productCategoryService)
        {
            _db = db;
            _categoryService = productCategoryService;
        }

        private ProductDataResponse ConvertProductDataToProductDataResponse(ProductData productData)
        {
            ProductDataResponse productDataResponse = productData.ToProductDataResponse();

            productDataResponse.ProductCategory = _categoryService.GetProductCategoryByCategoryID(productData.CategoryID)?.CategoryName;

            return productDataResponse;
        }

        public ProductDataResponse AddProduct(ProductDataAddRequest productDataAddRequest)
        {
            if (productDataAddRequest == null)
                throw new ArgumentNullException(nameof(productDataAddRequest));

            if(productDataAddRequest.ProductName == null)
            {
                throw new ArgumentException("ProductName Can't be blank");
            }

            ValidationHelper.ModelValidation(productDataAddRequest);

            ProductData productData = productDataAddRequest.ToProductData();

            _db.productData.Add(productData);

           return ConvertProductDataToProductDataResponse(productData);

        }
    }
}
