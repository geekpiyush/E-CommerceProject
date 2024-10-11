using Entities;
using Entities.DatabaseContext;
using Entities.ENUM;
using ServiceContracts;
using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductDataGetterService : IProductDataGetterService
    {
        private readonly ApplicationDbContext _db;
        public ProductDataGetterService(ApplicationDbContext db)
        {
            _db = db;
        }
        public List<ProductDataResponse> GetAllProduct()
        {
            return _db.productData.Select(temp=>temp.ToProductDataResponse()).ToList();
        }

        public List<ProductDataResponse> GetFilterdProduct(string? searchBy, string? searchString)
        {
            List<ProductDataResponse> allProduct =  GetAllProduct();
            List<ProductDataResponse> matchingProduct = allProduct;
            if(searchBy == null || searchString == null)
            {
                return matchingProduct;
            }

            switch(searchBy)
            {
                case nameof(ProductData.ProductName):
                    matchingProduct = allProduct.Where(temp => (!string.IsNullOrEmpty(temp.ProductName) ? temp.ProductName.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;


                default: matchingProduct = allProduct;
                    break;

            }
            return matchingProduct;
        }

        public ProductDataResponse GetProductByProductID(int productID)
        {
            if (productID == null)
                return null;

            ProductData? productData = _db.productData.FirstOrDefault(temp=> temp.ProductID == productID);

            if(productData == null)
            {
                return null;
            }

            return productData.ToProductDataResponse();
        }

        public List<ProductDataResponse> GetSortedProduct(List<ProductDataResponse> allProduct, string sortBy, SortOrderOptions sortOrder)
        {
            if(sortBy == null)
            {
                return allProduct;
            }
            List<ProductDataResponse> sortedProducts = (sortBy, sortOrder)
            switch
            {
                (nameof(ProductDataResponse.ProductName), SortOrderOptions.ASC)
                => allProduct.OrderBy(temp => temp.ProductName, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(ProductDataResponse.ProductName), SortOrderOptions.DESC)
                => allProduct.OrderByDescending(temp => temp.ProductName, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(ProductDataResponse.CategoryID), SortOrderOptions.ASC)
             => allProduct.OrderBy(temp => temp.ProductCategory, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(ProductDataResponse.CategoryID), SortOrderOptions.DESC)
                => allProduct.OrderByDescending(temp => temp.ProductCategory, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(ProductDataResponse.ProductID), SortOrderOptions.ASC)
           => allProduct.OrderBy(temp => temp.ProductID).ToList(),

                (nameof(ProductDataResponse.ProductID), SortOrderOptions.DESC)
                => allProduct.OrderByDescending(temp => temp.ProductID).ToList(),

                (nameof(ProductDataResponse.Price), SortOrderOptions.ASC)
           => allProduct.OrderBy(temp => temp.Price).ToList(),

                (nameof(ProductDataResponse.Price), SortOrderOptions.DESC)
                => allProduct.OrderByDescending(temp => temp.Price).ToList(),

                (nameof(ProductDataResponse.Quantity), SortOrderOptions.ASC)
       => allProduct.OrderBy(temp => temp.Quantity).ToList(),

                (nameof(ProductDataResponse.Quantity), SortOrderOptions.DESC)
                => allProduct.OrderByDescending(temp => temp.Quantity).ToList(),

               _ => allProduct
            };

            return sortedProducts;
        }
    }
}
