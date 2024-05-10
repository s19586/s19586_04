using System.Data;
using System.Data.SqlClient;
using WebApplication2.Models;

namespace WebApplication2.Repositories;

public interface IOrderRepository
{
        Task<Order> GetOrderAsync(int orderId);
        Task<int> CreateOrderAsync(Order order);
        Task UpdateOrderFulfilledDateAsync(int orderId, DateTime fulfilledAt);
}

public class OrderRepository : IOrderRepository
{
        private readonly List<Order> _orders = new List<Order>();

        public Task<Order> GetOrderAsync(int orderId)
        {
            // Simulate retrieving an order by ID from the in-memory list
            var order = _orders.FirstOrDefault(o => o.Id == orderId);
            return Task.FromResult(order);
        }

        public async Task<int> CreateOrderAsync(Order order)
        {
            // Simulate generating a unique order ID and add the order to the list
            order.Id = GenerateUniqueId();
            _orders.Add(order);
            await Task.Delay(100); // Simulate async delay (e.g., database operation)
            return order.Id;
        }

        private int GenerateUniqueId()
        {
            // Simulate generating a unique ID (could be replaced with a more robust method)
            return _orders.Count + 1;
        }

        public Task UpdateOrderFulfilledDateAsync(int orderId, DateTime fulfilledAt)
        {
            // Simulate updating the fulfilled date of an order
            var order = _orders.FirstOrDefault(o => o.Id == orderId);
            if (order != null)
            {
                order.FulfilledAt = fulfilledAt;
            }
            return Task.CompletedTask;
        }
}