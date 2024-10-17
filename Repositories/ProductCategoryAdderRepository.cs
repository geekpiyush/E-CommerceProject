using Entities;
using Entities.DatabaseContext;
using RepositoryContracts;

namespace Repositories
{
    public class ProductCategoryAdderRepository : IProductCategoryAdderRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductCategoryAdderRepository(ApplicationDbContext db)
        {
            _db = db;   
        }
        public Task<ProductCategory> AddProductCategory(ProductCategory productCategory)
        {
            _db.ProductCategory.Add(productCategory);
        }
    }
}
