using Entities.ENUM;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class ProductData
    {
        [Required(ErrorMessage ="Product ID Can't be blank")]
        [Key]
        public int ProductID { get; set; }

        public int CategoryID { get; set; }
        public string ProductName { get; set; }

        public double Price { get; set; }

        [ForeignKey("CategoryID")]
        public ProductCategory? ProductCategory  { get; set; }

        //<select>
        public string? Quantity { get; set; }

        public string? ProductImagePath { get; set; }

    }
}
