using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class PaymentPageViewModel
    {
        public ProductDataResponse Product { get; set; } // Product details
        public OrderResponse Order { get; set; } // Order details
    }
}
