using Entities;
using Entities.DatabaseContext;
using RepositoryContracts;
using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductDataDeleteService : IProductDataDeleteService
    {
        private readonly IProductDataDeleteRepository _dataDeleteRepository;
        private readonly IProductDataGetterRepository _dataGetterRepository;

        public ProductDataDeleteService(IProductDataDeleteRepository productDataDeleteRepository, IProductDataGetterRepository dataGetterRepository)
        {
            _dataDeleteRepository = productDataDeleteRepository;
            _dataGetterRepository = dataGetterRepository;
        }
        public async Task<bool> DeleteProductByProductID(int productID)
        {
            if(productID == null)
            {
                throw new ArgumentNullException(nameof(productID));
            }

            ProductData? matchingProductData = await _dataGetterRepository.GetProductByProductID(productID);

            if(matchingProductData == null)
            {
                throw new ArgumentException("Given productID not exist");
            }

            await _dataDeleteRepository.DeleteProductDataByProductID(productID);
            return true;
        }
    }
}
