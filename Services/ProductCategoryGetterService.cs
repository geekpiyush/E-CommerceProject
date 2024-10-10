using Entities;
using Entities.DatabaseContext;
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
        private readonly ApplicationDbContext _db;

        public ProductCategoryGetterService(ApplicationDbContext db)
        {
            _db = db;
            
        }
        public List<ProductCategoryResponse> GetAllProductCategories()
        {
            return _db.ProductCategory.Select(temp => temp.ToProductCategoryResponse()).ToList();
        }

        public ProductCategoryResponse? GetProductCategoryByCategoryID(int productCategoryID)
        {
           if(productCategoryID == null)
            {
                return null;
            }
             ProductCategory? productCategory = _db.ProductCategory.FirstOrDefault(temp => temp.CategoryID == productCategoryID);

            if(productCategory == null)
            {
                return null;
            }
            return productCategory.ToProductCategoryResponse();
        }
    }
}
