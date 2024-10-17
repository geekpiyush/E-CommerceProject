using Azure.Core;
using Entities;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderGetRepository _orderGetRepository;
        private readonly IOrderAddRepository _orderAddRepository;
        private readonly IProductDataGetterRepository _productDataGetterRepository;

        public OrderService(IOrderGetRepository orderGetRepository, IOrderAddRepository orderAddRepository, IProductDataGetterRepository productDataGetterRepository)
        {
            _orderAddRepository = orderAddRepository;
            _orderGetRepository = orderGetRepository;
            _productDataGetterRepository = productDataGetterRepository;
        }

        public async Task<OrderResponse> PlaceOrder(OrderRequest orderRequest)
        {
            var product = await _productDataGetterRepository.GetProductByProductID(orderRequest.ProductID);

            if(product == null)
            {
                throw new ArgumentException("Product Not Found");
            }

            var order = new Orders
            {
                ProductID = orderRequest.ProductID,
                FirstName = orderRequest.FirstName,
                LastName = orderRequest.LastName,
                Address = orderRequest.Address,
                City = orderRequest.City,
                Country = orderRequest.Country,
                PostalCode = orderRequest.PostalCode,
                Quantity = orderRequest.Quantity,
                TotalPrice = orderRequest.Quantity * product.Price
            };

              var newOrder = await _orderAddRepository.AddOrder(order);

            return new OrderResponse
            {
                OrderID = newOrder.OrderID,
                OrderDate = newOrder.OrderDate,
                ProductName = product.ProductName,
                TotalPrice = newOrder.TotalPrice,
                OrderStatus = newOrder.OrderStatus
            };
        }
    }
}
