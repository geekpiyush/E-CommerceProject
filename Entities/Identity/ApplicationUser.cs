using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? CustomerName {  get; set; }

        public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();
    }
}
