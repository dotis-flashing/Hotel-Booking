using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using PHAMDANGXUANDUY_NET1601_ASS01.Domain.Entity;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Request;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Response;

namespace WebMVC.Controllers
{
    public class CustomersController : Controller
    {
        private readonly HttpClient client = null;
        private string CustommerApiUrl = "";
        public CustomersController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            CustommerApiUrl = "http://localhost:5087/api/Customers/GetCustomers";

        }
        private async Task<bool> checkRole()
        {

            var admin = HttpContext.Session.GetString("ADMIN");
            if (admin != null)
            {
                ViewBag.ADMIN = admin;
                return (true);
            }
            var customerInfo = HttpContext.Session.GetString("CUSTOMER");
            var customer = JsonSerializer.Deserialize<ResponseCustomer>(customerInfo);
            if (customer != null)
            {
                ViewBag.CustomerInfo = customer.CustomerId;
                return (false);
            }
            return false;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            await checkRole();
            //if (customer != null)
            //{
            HttpResponseMessage response = await client.GetAsync(CustommerApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<Customer> listProducts = JsonSerializer.Deserialize<List<Customer>>(strData, options);
            return View(listProducts);
            //}

            //return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                await checkRole();
                //var customerId= Http
                HttpResponseMessage response = await client.GetAsync($"https://localhost:7143/api/Customers/GetCustomer?id={id}");

                if (response.IsSuccessStatusCode)
                {
                    string strData = await response.Content.ReadAsStringAsync();

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var customer = JsonSerializer.Deserialize<ResponseCustomer>(strData, options);


                    return View(customer);
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    return View("Error", errorMessage);
                }
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCustomer customer)
        {
            try
            {
                var check = await checkRole();
                if (ModelState.IsValid)
                {
                    string strData = JsonSerializer.Serialize(customer);
                    var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync("https://localhost:7143/api/Customers/PostCustomer", contentData);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        string errorMessage = await response.Content.ReadAsStringAsync();
                        ModelState.AddModelError(string.Empty, errorMessage); // Add error to ModelState
                        return View(customer);
                    }
                }
                return View(customer);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message); // Add exception message to ModelState
                return View(customer);
            }

        }



        // GET: RoomTypes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                await checkRole();
                HttpResponseMessage response = await client.GetAsync($"https://localhost:7143/api/Customers/GetCustomer?id={id}");

                if (response.IsSuccessStatusCode)
                {
                    string strData = await response.Content.ReadAsStringAsync();

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var customer = JsonSerializer.Deserialize<ResponseCustomer>(strData, options);
                    return View(customer);
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, errorMessage); // Add server-side error to ModelState
                    return View(id);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message); // Add exception message to ModelState
                return View(id);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ResponseCustomer createCustomer)
        {
            try
            {
                var check = await checkRole();
                if (!ModelState.IsValid)
                {
                    return View(createCustomer);
                }

                string strData = JsonSerializer.Serialize(createCustomer);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PatchAsync($"https://localhost:7143/api/Customers/PutCustomer?id={id}", contentData);

                if (response.IsSuccessStatusCode)
                {
                    if (check == false)
                    {
                        return RedirectToAction("HomePage", "Home");
                    }
                    else if (check == true)
                    {
                        return RedirectToAction("Index");
                    }
                    return View();
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, errorMessage); // Add server-side error to ModelState
                    return View(createCustomer);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message); // Add exception message to ModelState
                return View(createCustomer);
            }
        }


        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await checkRole();
                HttpResponseMessage response = await client.GetAsync($"https://localhost:7143/api/Customers/GetCustomer?id={id}");

                if (response.IsSuccessStatusCode)
                {
                    string strData = await response.Content.ReadAsStringAsync();

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var customer = JsonSerializer.Deserialize<ResponseCustomer>(strData, options);


                    return View(customer);
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    return View("Error", errorMessage);
                }
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }
        }

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Customers == null)
        //    {
        //        return Problem("Entity set 'FUMiniHotelManagementContext.Customers'  is null.");
        //    }
        //    var customer = await _context.Customers.FindAsync(id);
        //    if (customer != null)
        //    {
        //        _context.Customers.Remove(customer);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

    }
}
