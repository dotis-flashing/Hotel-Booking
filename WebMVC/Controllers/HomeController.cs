using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.RulesetToEditorconfig;
using Microsoft.Extensions.Options;
using PHAMDANGXUANDUY_NET1601_ASS01.Domain.Entity;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Request;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Response;
using System;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _client = null;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        public IActionResult Index()
        {
            return View();
        }
        //public IActionResult Login()
        //{
        //    return View();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string email, string password)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var req = new { email, password };
                    string strData = JsonSerializer.Serialize(req);
                    var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await _client.PostAsync($"https://localhost:7143/api/Customers/LoginAdmin?email={email}&password={password}", contentData);

                    if (response.IsSuccessStatusCode)
                    {
                        var convert = await response.Content.ReadAsStringAsync();
                        bool convertSuccess = Convert.ToBoolean(convert);
                        if (convertSuccess)
                        {
                            HttpContext.Session.SetString("ADMIN", email);
                            return Redirect("/Home/HomePage");

                        }
                        else
                        {
                            return await CheckLoginCustomerAsync(email, password);
                        }
                    }
                    else
                    {
                        string errorMessage = await response.Content.ReadAsStringAsync();
                        ModelState.AddModelError(string.Empty, errorMessage); // Add error to ModelState
                        return View();
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message); // Add exception message to ModelState
                return View();
            }
        }
        public IActionResult HomePage()
        {
            var admin = HttpContext.Session.GetString("ADMIN");
            if (admin != null)
            {
                ViewBag.ADMIN = admin;
                return View();
            }
            var customerInfo = HttpContext.Session.GetString("CUSTOMER");
            var customer = JsonSerializer.Deserialize<ResponseCustomer>(customerInfo);
            if (customer != null)
            {
                ViewBag.CustomerInfo = customer.CustomerId;
                return View();

            }
            return View();

        }
        private async Task<IActionResult> CheckLoginCustomerAsync(string email, string password)
        {
            var req = new { email, password };
            string strData = JsonSerializer.Serialize(req);
            var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync($"https://localhost:7143/api/Customers/LoginCustomer?email={email}&password={password}", contentData);

            if (response.IsSuccessStatusCode)
            {

                var converted = await response.Content.ReadAsStringAsync();
                Console.WriteLine("JSON Response: " + converted);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var customer = JsonSerializer.Deserialize<ResponseCustomer>(converted, options);

                var customerJson = JsonSerializer.Serialize(customer);
                Console.WriteLine("Response: " + customer);
                Console.WriteLine("ResponseTest: " + customerJson);

                HttpContext.Session.SetString("CUSTOMER", customerJson);
                //ViewBag.CustomerInfo = customer;
                return Redirect("/Home/HomePage");
            }
            else
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, errorMessage); // Add error to ModelState
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("ADMIN");
            HttpContext.Session.Remove("CUSTOMER");
            //HttpContext.Session.Clear();
            return Redirect("/");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
