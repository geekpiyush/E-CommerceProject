using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IOrderAddRepository
    {
        Task<Orders>AddOrder(Orders orders);
    }
}
