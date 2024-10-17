using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class OrderResponse
    {

            public string OrderID { get; set; }
            public DateTime OrderDate { get; set; }
            public string? ProductName { get; set; }
            public double? TotalPrice { get; set; }
            public string? OrderStatus { get; set; }
            public string Currency { get; set; }     
            public string RazorpayKey { get; set; }   
            public int ProductID { get; set; }        

    }

}
