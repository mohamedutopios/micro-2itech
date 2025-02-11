using CustomerService.Data;
using CustomerService.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerContext _context;

        public CustomerRepository(CustomerContext customerContext)
        {
            _context = customerContext;
        }


        public async Task AddAsync(Customer customer)
        {
            await _context.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
           return await _context.Customers.ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);
           await _context.SaveChangesAsync();
        }
    }
}
