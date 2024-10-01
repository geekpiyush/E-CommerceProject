using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Project.Models
{
    public class Product
    {
        [Required(ErrorMessage ="Product ID Can't be blank")]
        [Key]
        public Guid ProductID { get; set; }
        public string ProductName { get; set; }

        public double Price { get; set; }

        public string ProductCategory { get; set; }

        //<select>
        public double Quantity { get; set; }

        


    }
}
