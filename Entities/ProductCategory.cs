using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ProductCategory
    {
        [Key] 
        public Guid CategoryID { get; set; }
        public string CategoryName { get; set; }

    }
}
