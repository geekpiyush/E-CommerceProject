using Entities;
using Entities.DatabaseContext;
using RepositoryContracts;

namespace Repositories_
{
    public class ProductCategoryAddRepository : IProductCategoryAdderRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductCategoryAddRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<ProductCategory> AddProductCategory(ProductCategory productCategory)
        {
            _db.ProductCategory.Add(productCategory);

            await _db.SaveChangesAsync();

            return productCategory;
        }
    }
}
