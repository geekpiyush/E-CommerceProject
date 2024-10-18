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
            public int Quantity { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string Country { get; set; }
            public string PostalCode { get; set; }
        public string ProductDescription { get; set; }
        public string ProductImagePath { get; set; }
        public double Price { get; set; }

    }

}
