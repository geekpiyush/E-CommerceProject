using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class ProductCategoryAddRequest
    {
        public string? CategoryName {  get; set; }


        public ProductCategory ToProductCategory()
        {
            return new ProductCategory() { CategoryName = CategoryName };
        }
    }


}
