using Entities;
using Entities.DatabaseContext;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories_
{
    public class OrderGetRepository : IOrderGetRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderGetRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Orders?> GetOrderByOrderID(int? orderID)
        {
            return await _db.Orders.FindAsync(orderID);
        }
    }
}
