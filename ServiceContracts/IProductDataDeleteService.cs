using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IProductDataDeleteService
    {
        Task<bool> DeleteProductByProductID(int productID);   
    }
}
