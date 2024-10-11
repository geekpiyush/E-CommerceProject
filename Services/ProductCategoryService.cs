using Entities;
using Entities.DatabaseContext;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly ApplicationDbContext _db;
        public ProductCategoryService(ApplicationDbContext db)
        {
                _db = db;
        }
        public ProductCategoryResponse AddProductCategory(ProductCategoryAddRequest? productCategoryAddRequest)
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
            if (_db.ProductCategory.Where(temp => temp.CategoryName == productCategoryAddRequest.CategoryName).Count() >0)
            {
                throw new ArgumentException("Given CategoryName Already Exist");
            }

            ProductCategory productCategory = productCategoryAddRequest.ToProductCategory();

            _db.ProductCategory.Add(productCategory);

            return productCategory.ToProductCategoryResponse();
        }
    }
}
