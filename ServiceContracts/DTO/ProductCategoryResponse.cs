using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class ProductCategoryResponse
    {
        public int CategoryID {  get; set; }
        public string? CategoryName { get; set; }
    }

    public static class ProductCategoryExtension
    {
        public static ProductCategoryResponse ToProductCategoryResponse(this ProductCategory productCategory)
        {
            return new ProductCategoryResponse() { CategoryID = productCategory.CategoryID, CategoryName = productCategory.CategoryName };
        }
    }
}
