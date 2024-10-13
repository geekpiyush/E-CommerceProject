using Entities;
using Entities.ENUM;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class ProductDataAddRequest
    {
        [Required(ErrorMessage ="Product Name Can't be Blank")]
        public string? ProductName { get; set; }

        [Required(ErrorMessage = "CategoryID Can't be Blank")]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Price Can't be Blank")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Quantity Can't be Blank")]
        public ProductQuantityOptions? Quantity { get; set; }

        public IFormFile? ProductImage { get; set; }
        public string? ProductImagePath { get; set; }


        public ProductData ToProductData ()
        {
            return new ProductData() { ProductName = ProductName, CategoryID = CategoryID, Price = Price, Quantity = Quantity.ToString(), ProductImagePath = ProductImagePath };
        }
    }

}
