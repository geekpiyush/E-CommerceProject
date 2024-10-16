using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class ProductDataResponse
    {
    
        public int ProductID { get; set; }

        public int CategoryID { get; set; }
        public string ProductName { get; set; }

        public double Price { get; set; }

        public string? ProductCategory { get; set; }

        public string Quantity { get; set; }

        public string? ProductImagePath { get; set; }

        public string? ProductDescription {  get; set; }


        public override string ToString()
        {
            return $"Product ID: {ProductID},CategoryID {CategoryID}, Product Name: {ProductName},Price: {Price},Quantity {Quantity}, Product Category {ProductCategory},Product Image Path {ProductImagePath}";
        }

        public ProductDataUpdateRequest ToProductUpdateRequest()
        {
            return new ProductDataUpdateRequest() { ProductName = ProductName, ProductImagePath = ProductImagePath, ProductID = ProductID, CategoryID = CategoryID, Quantity = Quantity, Price = Price };
        }
    }

    public static class ProductDataExtension
    {
        public static ProductDataResponse ToProductDataResponse(this ProductData productData)
        {
            return new ProductDataResponse() { CategoryID = productData.CategoryID, ProductName = productData.ProductName, Price = productData.Price,ProductID = productData.ProductID, Quantity = productData.Quantity,ProductImagePath =  productData.ProductImagePath,ProductDescription = productData.ProductDescription};
        }
    }
}
