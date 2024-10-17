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
        Task<List<ProductDataResponse>> GetAllProduct();

        Task<ProductDataResponse?> GetProductByProductID(int productID);

        Task<List<ProductDataResponse>> GetFilterdProduct(string? searchBy,string? searchString);

        List<ProductDataResponse> GetSortedProduct(List<ProductDataResponse> allProduct, string sortBy, SortOrderOptions sortOrder);


    }
}
