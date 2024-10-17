using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class OrderRequest
    {
        public int ProductID { get; set; }

        [Required(ErrorMessage ="FirstName Can't be blank")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName Can't be blank")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Address Can't be blank")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City Can't be blank")]
        public string City { get; set; }

        [Required(ErrorMessage = "Country Can't be blank")]
        public string Country { get; set; }

        [Required(ErrorMessage = "PostalCode Can't be blank")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Quantity Can't be blank")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "TotalPrice Can't be blank")]
        public double TotalPrice { get; set; }
    }
}
