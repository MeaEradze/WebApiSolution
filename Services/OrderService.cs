using Data;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    namespace Services
    {
        public class OrderService
        {
            private readonly AppDbContext _context;

            public OrderService(AppDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Order>> GetAllOrdersAsync() => await _context.Orders.ToListAsync();

            public async Task<Order?> GetOrderByIdAsync(int id) => await _context.Orders.FindAsync(id);

            public async Task AddOrderAsync(Order order)
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateOrderAsync(Order order)
            {
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteOrderAsync(int id)
            {
                var order = await _context.Orders.FindAsync(id);
                if (order != null)
                {
                    _context.Orders.Remove(order);
                    await _context.SaveChangesAsync();
                }
            }

            public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId)
                => await _context.Orders.Where(o => o.CustomerId == customerId).ToListAsync();

            public async Task<decimal> GetTotalPriceForCustomerAsync(int customerId)
                => await _context.Orders.Where(o => o.CustomerId == customerId).SumAsync(o => o.Price);

            public async Task<IEnumerable<dynamic>> GetTotalPricePerCustomerAsync()
                => await _context.Customers
                    .Select(c => new
                    {
                        CustomerName = c.Name,
                        TotalPrice = c.Orders.Sum(o => o.Price)
                    }).ToListAsync();
        }
    }
}
