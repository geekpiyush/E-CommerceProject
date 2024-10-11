using Entities.ENUM;
using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IProductDataGetterService
    {
        List<ProductDataResponse> GetAllProduct();

        ProductDataResponse GetProductByProductID(int productID);

        List<ProductDataResponse> GetFilterdProduct(string? searchBy,string? searchString);

        List<ProductDataResponse> GetSortedProduct(List<ProductDataResponse> allProduct, string sortBy, SortOrderOptions sortOrder);

    }
}
