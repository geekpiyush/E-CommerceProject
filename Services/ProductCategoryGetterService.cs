using Entities;
using Entities.DatabaseContext;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductCategoryGetterService : IProductCategoryGetterService
    {
        private readonly IProductCategoryGetterRepository _categoryGetterRepository;

        public ProductCategoryGetterService(IProductCategoryGetterRepository productCategoryGetterRepository)
        {
            _categoryGetterRepository = productCategoryGetterRepository;
            
        }
        public async Task<List<ProductCategoryResponse>>GetAllProductCategories()
        {
            return (await _categoryGetterRepository.GetProductCategories()).Select(temp => temp.ToProductCategoryResponse()).ToList();
        }

        public async Task<ProductCategoryResponse?> GetProductCategoryByCategoryID(int productCategoryID)
        {
           if(productCategoryID == null)
            {
                return null;
            }
            ProductCategory? productCategory = await _categoryGetterRepository.GetProductCategoryByCategoryID(productCategoryID);

            if(productCategory == null)
            {
                return null;
            }
            return productCategory.ToProductCategoryResponse();
        }
    }
}
