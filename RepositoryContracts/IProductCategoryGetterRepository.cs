using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IProductCategoryGetterRepository
    {
        Task<List<ProductCategory>> GetProductCategories();
        Task<ProductCategory?> GetProductCategoryByCategoryID(int productCategoryID);

        Task<ProductCategory?> GetProductCategoryByCategoryName(string categoryName);
    }
}
