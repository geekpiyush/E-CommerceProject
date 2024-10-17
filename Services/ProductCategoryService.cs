using Entities;
using Entities.DatabaseContext;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryAdderRepository _categoryAdderRepository;
        private readonly IProductCategoryGetterRepository _categoryGetterRepository;

        public ProductCategoryService(IProductCategoryAdderRepository productCategoryAdderRepository, IProductCategoryGetterRepository productCategoryGetterRepository)
        {
                _categoryAdderRepository = productCategoryAdderRepository;
            _categoryGetterRepository = productCategoryGetterRepository;
        }

        public async Task<ProductCategoryResponse> AddProductCategory(ProductCategoryAddRequest? productCategoryAddRequest)
        {
            if(productCategoryAddRequest == null)
            {
                throw new ArgumentNullException(nameof(productCategoryAddRequest));
            }

            if(productCategoryAddRequest.CategoryName == null)
            {
                throw new ArgumentException(nameof(productCategoryAddRequest.CategoryName));
            }

            //check duplicate CategoryName
            if (await _categoryGetterRepository.GetProductCategoryByCategoryName(productCategoryAddRequest.CategoryName) != null)
            {
                throw new ArgumentException("Given CategoryName Already Exist");
            }

            ProductCategory productCategory = productCategoryAddRequest.ToProductCategory();

            await _categoryAdderRepository.AddProductCategory(productCategory);

            return productCategory.ToProductCategoryResponse();
        }

    }
}
