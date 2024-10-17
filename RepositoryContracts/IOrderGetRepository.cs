using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IOrderGetRepository
    {
        Task<Orders?>GetOrderByOrderID(int? orderID);
    }
}
