using PHAMDANGXUANDUY_NET1601_ASS01.Application.IGeneric;
using PHAMDANGXUANDUY_NET1601_ASS01.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHAMDANGXUANDUY_NET1601_ASS01.Application.Repository
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<Customer> CheckEmailCustomer(string email);
        Task<Customer> Login(string email,string password);
        Task<Customer> GetbyIdd(int id);
    }
}
