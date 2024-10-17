using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Orders
    {
        [Key]
        public string OrderID { get; set; }

        public int ProductID { get; set; }

        [ForeignKey("ProductID")] 
        public ProductData? ProductData { get; set; }

        [Required(ErrorMessage ="First Name Can't Be Blank")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name Can't Be Blank")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Address Name Can't Be Blank")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "City Name Can't Be Blank")]
        public string? City { get; set; }

        [Required(ErrorMessage = "Country Name Can't Be Blank")]
        public string? Country { get; set; }

        [Required(ErrorMessage = "PostalCode Name Can't Be Blank")]
        public string? PostalCode { get; set; }

        [Required(ErrorMessage = "Quantity Name Can't Be Blank")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "TotalPrice Name Can't Be Blank")]
        public double TotalPrice { get; set; }

        [Required(ErrorMessage = "OrderDate Name Can't Be Blank")]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "OrderStatus Name Can't Be Blank")]
        public string? OrderStatus { get; set; } = "Pending"; // Example status
    }

}
