using Microsoft.AspNetCore.Mvc;
using PHAMDANGXUANDUY_NET1601_ASS01.Domain.Entity;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Request;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Response;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Service;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return Ok(await _customerService.GetAll());
        }

        // GET: api/Customers/5
        [HttpGet]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            return Ok(await _customerService.GetById(id));
        }

        [HttpPatch]
        public async Task<ActionResult<Customer>> PutCustomer(int id, UpdateCustomer customer)
        {
            try
            {

                return Ok(await _customerService.UpdateT(id, customer));
            }
            catch (Exception ex)
            {
                return BadRequest($"Internal Server Error: {ex.Message}");

            }
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(CreateCustomer customer)
        {
            try
            {
                return Ok(await _customerService.Add(customer));
            }
            catch (Exception ex)
            {
                return BadRequest($"Internal Server Error: {ex.Message}");

            }
        }
        [HttpPost]
        public async Task<ActionResult<bool>> LoginAdmin(string email, string password)
        {
            try
            {
                return Ok(await _customerService.Login(email, password));
            }
            catch (Exception ex)
            {
                return BadRequest($"Internal Server Error: {ex.Message}");

            }
        }
        [HttpPost]
        public async Task<ActionResult<ResponseCustomer>> LoginCustomer(string email, string password)
        {
            try
            {
                return Ok(await _customerService.LoginCustomer(email, password));
            }
            catch (Exception ex)
            {
                return BadRequest($"Internal Server Error: {ex.Message}");

            }
        }

        //// DELETE: api/Customers/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCustomer(int id)
        //{
        //    if (_context.Customers == null)
        //    {
        //        return NotFound();
        //    }
        //    var customer = await _context.Customers.FindAsync(id);
        //    if (customer == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Customers.Remove(customer);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}


    }
}
