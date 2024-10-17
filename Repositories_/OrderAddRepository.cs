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
    public class OrderAddRepository : IOrderAddRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderAddRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Orders> AddOrder(Orders orders)
        {
            _db.Orders.Add(orders);
            await _db.SaveChangesAsync();
            return orders;
        }
    }
}
