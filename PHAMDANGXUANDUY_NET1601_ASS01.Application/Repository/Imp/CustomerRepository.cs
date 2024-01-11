using Microsoft.EntityFrameworkCore;
using PHAMDANGXUANDUY_NET1601_ASS01.Application.IGeneric.Imp;
using PHAMDANGXUANDUY_NET1601_ASS01.Domain.Entity;

namespace PHAMDANGXUANDUY_NET1601_ASS01.Application.Repository.Imp
{
    public class CustomerRepository : GenericReository<Customer>, ICustomerRepository
    {
        public CustomerRepository(FUMiniHotelManagementContext context) : base(context)
        {
        }

        public async Task<Customer> CheckEmailCustomer(string email)
        {
            var customer = await _context.Set<Customer>().FirstOrDefaultAsync(c => c.EmailAddress.ToLower().Equals(email.ToLower()));
            if (customer != null)
            {
                throw new Exception("Exist");

            }
            return customer;
        }

        public async Task<Customer> GetbyIdd(int id)
        {
            var customer = await _context.Set<Customer>().FirstOrDefaultAsync(c => c.CustomerId == id);
            if (customer == null)
            {
                throw new Exception("not found");

            }
            return customer;
        }

        public async Task<Customer> Login(string email, string password)
        {
            var customer = await _context.Set<Customer>()
                .FirstOrDefaultAsync(c => c.EmailAddress.Equals(email) && c.Password.Equals(password));
            if (customer == null)
            {
                throw new Exception("not found");

            }
            return customer;
        }
    }
}
