using Entities;
using Entities.DatabaseContext;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductDataUpdateService : IProductDataUpdateService
    {
        private readonly IProductDataUpdateRepository _dataUpdateRepository;
        private readonly IProductDataGetterRepository _dataGetterRepository;
        public ProductDataUpdateService(IProductDataGetterRepository productDataGetterRepository,IProductDataUpdateRepository productDataUpdateRepository)
        {
            _dataGetterRepository = productDataGetterRepository;
            _dataUpdateRepository = productDataUpdateRepository;
        }
        public async Task<ProductDataResponse> UpdateProductData(ProductDataUpdateRequest? productDataUpdateRequest)
        {
           if(productDataUpdateRequest == null)
            {
                throw new ArgumentNullException(nameof(productDataUpdateRequest));
            }

            ValidationHelper.ModelValidation(productDataUpdateRequest);

            //find matching product
            ProductData? matchingProductData = await _dataGetterRepository.GetProductByProductID(productDataUpdateRequest.ProductID);

            if(matchingProductData == null)
            {
                throw new ArgumentException("Given id is not exist");
            }

            if (productDataUpdateRequest.ProductImage != null && productDataUpdateRequest.ProductImage.Length > 0)
            {
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "productImages");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string fileName = $"{Guid.NewGuid()}_{productDataUpdateRequest.ProductImage.FileName}";
                string filePath = Path.Combine(folderPath, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    productDataUpdateRequest.ProductImage.CopyTo(fileStream);
                }

                productDataUpdateRequest.ProductImagePath = $"/images/productImages/{fileName}";
            }
            matchingProductData.ProductName = productDataUpdateRequest.ProductName;
            matchingProductData.Price = productDataUpdateRequest.Price;
            matchingProductData.ProductImagePath = productDataUpdateRequest.ProductImagePath;
            matchingProductData.Quantity = productDataUpdateRequest.Quantity;
            matchingProductData.ProductDescription = productDataUpdateRequest.ProductDescription;
            
             await _dataUpdateRepository.UpdateProduct(matchingProductData);
            return matchingProductData.ToProductDataResponse();
        }
    }
}
