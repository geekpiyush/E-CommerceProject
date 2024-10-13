using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ServiceContracts.DTO
{
    public class ProductDataUpdateRequest
    {
        [Required(ErrorMessage = "Product ID Can't be blank")]
        public int ProductID {  get; set; }

        [Required(ErrorMessage = "Product ID Can't be blank")]
        public int CategoryID {  get; set; }

        [Required(ErrorMessage ="ProductName can't be blank")]
        public string? ProductName { get; set; }

        [Required(ErrorMessage = "Price can't be blank")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Quantity can't be blank")]
        public string? Quantity { get; set; }

        [Required(ErrorMessage = "ProductImagePath can't be blank")]

        public IFormFile? ProductImage { get; set; }

        public string? ProductImagePath { get; set; }

        public ProductData ToProductData()
        {
            return new ProductData() { CategoryID = CategoryID, Price = Price, ProductID = ProductID, Quantity = Quantity, ProductImagePath = ProductImagePath, ProductName = ProductName };
        }
    }
}
