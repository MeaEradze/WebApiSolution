﻿using Data;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class CustomerService
    {
        private readonly AppDbContext _context;

        public CustomerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
            => await _context.Customers.Include(c => c.Orders).ToListAsync();

        public async Task<Customer?> GetCustomerByIdAsync(int id)
            => await _context.Customers.Include(c => c.Orders).FirstOrDefaultAsync(c => c.Id == id);

        public async Task AddCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }
    }
}
