using PHAMDANGXUANDUY_NET1601_ASS01.Domain.Entity;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Request;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Service
{
    public interface ICustomerService
    {
        Task<Customer> Add(CreateCustomer create);
        Task<List<Customer>> GetAll();
        Task<Customer> GetById(int id);
        Task<ResponseCustomer> Update(int id,CreateCustomer customer);
        Task<ResponseCustomer> UpdateT(int id, UpdateCustomer customer);
        Task<bool> Login(string email,string password);
        Task<ResponseCustomer> LoginCustomer(string email,string password);
    }
}
