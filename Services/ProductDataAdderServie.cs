using Entities;
using Entities.DatabaseContext;
using RepositoryContracts;
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
        private readonly IProductDataAdderRepository _dataAdderRepository;
        public ProductDataAdderServie(IProductDataAdderRepository dataAdderRepository )
        { 
            _dataAdderRepository = dataAdderRepository;
     
        }


        public async Task<ProductDataResponse> AddProduct(ProductDataAddRequest productDataAddRequest)
        {
            if (productDataAddRequest == null)
                throw new ArgumentNullException(nameof(productDataAddRequest));

            if(productDataAddRequest.ProductName == null)
            {
                throw new ArgumentException("ProductName Can't be blank");
            }

            ValidationHelper.ModelValidation(productDataAddRequest);

            if(productDataAddRequest.ProductImage != null && productDataAddRequest.ProductImage.Length>0)
            {
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "productImages");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);  
                }

                string fileName = $"{Guid.NewGuid()}_{productDataAddRequest.ProductImage.FileName}";
                string filePath = Path.Combine(folderPath, fileName);

                using(var fileStream = new FileStream(filePath,FileMode.Create))
                {
                    productDataAddRequest.ProductImage.CopyTo(fileStream);
                }

                productDataAddRequest.ProductImagePath = $"/images/productImages/{fileName}";
            }


            ProductData productData = productDataAddRequest.ToProductData();

            await _dataAdderRepository.AddProduct(productData);
           

           return productData.ToProductDataResponse();

        }
    }
}
