using AutoMapper;
using Microsoft.Extensions.Configuration;
using PHAMDANGXUANDUY_NET1601_ASS01.Domain.Entity;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Request;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Response;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.IUnitofwork;


namespace PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Service.Imp
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfwork _unitofwork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CustomerService(IUnitOfwork unitofwork, IMapper mapper, IConfiguration configuration)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<Customer> Add(CreateCustomer create)
        {
            var customer = _mapper.Map<Customer>(create);
            customer.CustomerStatus = 1;
            await _unitofwork.CustomerRepository.CheckEmailCustomer(create.EmailAddress);
            await _unitofwork.CustomerRepository.Add(customer);
            await _unitofwork.Commit();
            return customer;
        }

        public async Task<List<Customer>> GetAll()
        {
            return await _unitofwork.CustomerRepository.GetAll();
        }

        public async Task<Customer> GetById(int id)
        {
            return await _unitofwork.CustomerRepository.GetById(id);
        }

        public async Task<bool> Login(string email, string password)
        {
            var adminEmail = _configuration["Admin:email"];
            var adminPassword = _configuration["Admin:password"];
            if (adminEmail.Equals(email) && adminPassword.Equals(password))
            {
                return true;
            }
            return false;

        }

        public async Task<ResponseCustomer> LoginCustomer(string email, string password)
        {
            return _mapper.Map<ResponseCustomer>(await _unitofwork.CustomerRepository.Login(email, password));
        }

        public async Task<ResponseCustomer> Update(int id, CreateCustomer customer)
        {
            var check = await _unitofwork.CustomerRepository.GetById(id);
            var update = _mapper.Map(customer, check);
            await _unitofwork.CustomerRepository.CheckEmailCustomer(update.EmailAddress);
            await _unitofwork.CustomerRepository.Update(update);
            await _unitofwork.Commit();
            return _mapper.Map<ResponseCustomer>(check);
        }

        public async Task<ResponseCustomer> UpdateT(int id, UpdateCustomer customer)
        {
            var check = await _unitofwork.CustomerRepository.GetById(id);
            var update = _mapper.Map(customer, check);
            //await _unitofwork.CustomerRepository.CheckEmailCustomer(update.EmailAddress);
            await _unitofwork.CustomerRepository.Update(update);
            await _unitofwork.Commit();
            return _mapper.Map<ResponseCustomer>(update);
        }
    }
}
