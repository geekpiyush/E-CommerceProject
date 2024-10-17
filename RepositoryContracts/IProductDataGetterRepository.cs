using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IProductDataGetterRepository
    {
        Task<List<ProductData>>GetAllProduct();

        Task<ProductData?> GetProductByProductID(int productID);

        Task<List<ProductData>> GetFilterdProduct(Expression<Func<ProductData, bool>> predicate);
    }
}
