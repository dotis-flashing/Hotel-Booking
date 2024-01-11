using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PHAMDANGXUANDUY_NET1601_ASS01.Domain.Entity;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Request;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Response;

namespace WebMVC.Controllers
{
    public class RoomInformationsController : Controller
    {
        private readonly HttpClient client = null;
        //private string CustommerApiUrl = "";
        public RoomInformationsController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }
        private async Task<IActionResult> checkRole()
        {
            try
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
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }
        }

        // GET: RoomInformations
        public async Task<IActionResult> Index()
        {
            await checkRole();
            HttpResponseMessage response = await client.GetAsync("https://localhost:7143/api/RoomInformations/GetRoomInformations");
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<RoomInformation> listProducts = JsonSerializer.Deserialize<List<RoomInformation>>(strData, options);
            return View(listProducts);
        }

        //https://localhost:7143/api/RoomInformations/GetRoomInformation?id=4
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                await checkRole();

                HttpResponseMessage response = await client.GetAsync($"https://localhost:7143/api/RoomInformations/GetRoomInformation?id={id}");

                if (response.IsSuccessStatusCode)
                {
                    string strData = await response.Content.ReadAsStringAsync();

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var room = JsonSerializer.Deserialize<ResponseRoomInfor>(strData, options);

                    return View(room);
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

        // GET: RoomInformations/Create
        public async Task<IActionResult> CreateAsync()
        {
            await checkRole();

            HttpResponseMessage response = await client.GetAsync("https://localhost:7143/api/RoomTypes/GetRoomTypes");

            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<RoomType> listProducts = JsonSerializer.Deserialize<List<RoomType>>(strData, options);
            try
            {
                ViewData["RoomTypeId"] = new SelectList(listProducts, "RoomTypeId", "RoomTypeName");
                return View();
            }
            catch (Exception ex)
            {
                ViewData["RoomTypeId"] = new SelectList(listProducts, "RoomTypeId", "RoomTypeName");

                return View("Error", ex.Message);

            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRoomInfor roomInformation)
        {
            await checkRole();

            HttpResponseMessage responses = await client.GetAsync("https://localhost:7143/api/RoomTypes/GetRoomTypes");

            string strDataa = await responses.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<RoomType> listProducts = JsonSerializer.Deserialize<List<RoomType>>(strDataa, options);
            ViewData["RoomTypeId"] = new SelectList(listProducts, "RoomTypeId", "RoomTypeName");
            try
            {
                if (ModelState.IsValid)
                {
                    string strData = JsonSerializer.Serialize(roomInformation);
                    var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync("https://localhost:7143/api/RoomInformations/PostRoomInformation", contentData);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        string errorMessage = await response.Content.ReadAsStringAsync();
                        ModelState.AddModelError(string.Empty, errorMessage); // Add error to ModelState
                        return View(roomInformation);
                    }
                }
                return View(roomInformation);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message); // Add exception message to ModelState
                return View(roomInformation);
            }
        }

        // GET: RoomInformations/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            await checkRole();

            HttpResponseMessage responses = await client.GetAsync("https://localhost:7143/api/RoomTypes/GetRoomTypes");

            string strDataa = await responses.Content.ReadAsStringAsync();

            var optionss = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<RoomType> listProducts = JsonSerializer.Deserialize<List<RoomType>>(strDataa, optionss);
            ViewData["RoomTypeId"] = new SelectList(listProducts, "RoomTypeId", "RoomTypeName");
            try
            {
                HttpResponseMessage response = await client.GetAsync($"https://localhost:7143/api/RoomInformations/GetRoomInformation?id={id}");

                if (response.IsSuccessStatusCode)
                {
                    string strData = await response.Content.ReadAsStringAsync();

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var room = JsonSerializer.Deserialize<ResponseRoomInfor>(strData, options);

                    return View(room);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ResponseRoomInfor roomInformation)
        {
            await checkRole();

            HttpResponseMessage responses = await client.GetAsync("https://localhost:7143/api/RoomTypes/GetRoomTypes");

            string strDataa = await responses.Content.ReadAsStringAsync();

            var optionss = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<RoomType> listProducts = JsonSerializer.Deserialize<List<RoomType>>(strDataa, optionss);
            ViewData["RoomTypeId"] = new SelectList(listProducts, "RoomTypeId", "RoomTypeName");
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(roomInformation);
                }

                string strData = JsonSerializer.Serialize(roomInformation);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PatchAsync($"https://localhost:7143/api/RoomInformations/PutRoomInformation?id={id}", contentData);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, errorMessage); // Add server-side error to ModelState
                    return View(roomInformation);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message); // Add exception message to ModelState
                return View(roomInformation);
            }
        }

        //https://localhost:44309/api/RoomInformations/DeleteRoomInformation?id=2


        // GET: RoomInformations/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await checkRole();

                HttpResponseMessage response = await client.GetAsync($"https://localhost:7143/api/RoomInformations/GetRoomInformation?id={id}");

                if (response.IsSuccessStatusCode)
                {
                    string strData = await response.Content.ReadAsStringAsync();

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var room = JsonSerializer.Deserialize<ResponseRoomInfor>(strData, options);

                    return View(room);
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await checkRole();

                if (!ModelState.IsValid)
                {
                    return View(id);
                }

                HttpResponseMessage response = await client.DeleteAsync($"https://localhost:7143/api/RoomInformations/DeleteRoomInformation?id={id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, errorMessage);
                    return View(id);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(id);
            }
        }


    }
}
