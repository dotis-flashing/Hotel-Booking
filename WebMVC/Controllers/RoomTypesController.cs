using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using PHAMDANGXUANDUY_NET1601_ASS01.Domain.Entity;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Request;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Response;

namespace WebMVC.Controllers
{
    public class RoomTypesController : Controller
    {
        private readonly HttpClient client = null;

        public RoomTypesController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            //CustommerApiUrl = "https://localhost:7143/api/RoomTypes/GetRoomTypes";

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

        // GET: RoomTypes
        public async Task<IActionResult> Index()
        {
            await checkRole();
            HttpResponseMessage response = await client.GetAsync("https://localhost:7143/api/RoomTypes/GetRoomTypes");
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<RoomType> listProducts = JsonSerializer.Deserialize<List<RoomType>>(strData, options);
            return View(listProducts);
        }

        // GET: RoomTypes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                await checkRole();

                HttpResponseMessage response = await client.GetAsync($"https://localhost:7143/api/RoomTypes/GetRoomType?id={id}");

                if (response.IsSuccessStatusCode)
                {
                    string strData = await response.Content.ReadAsStringAsync();

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var room = JsonSerializer.Deserialize<RoomType>(strData, options);


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

        // GET: RoomTypes/Create
        public IActionResult Create()
        {
             checkRole();

            return View();
        }

        // POST: RoomTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRoomType roomType)
        {
            try
            {
                await checkRole();

                if (ModelState.IsValid)
                {
                    string strData = JsonSerializer.Serialize(roomType);
                    var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync("https://localhost:7143/api/RoomTypes/PostRoomType", contentData);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        string errorMessage = await response.Content.ReadAsStringAsync();
                        ModelState.AddModelError(string.Empty, errorMessage); // Add error to ModelState
                        return View(roomType);
                    }
                }
                return View(roomType);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message); // Add exception message to ModelState
                return View(roomType);
            }
        }

        // GET: RoomTypes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                await checkRole();

                HttpResponseMessage response = await client.GetAsync($"https://localhost:7143/api/RoomTypes/GetRoomType?id={id}");

                if (response.IsSuccessStatusCode)
                {
                    string strData = await response.Content.ReadAsStringAsync();

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var room = JsonSerializer.Deserialize<RoomType>(strData, options);


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

        // POST: RoomTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //https://localhost:44309/api/RoomTypes/PutRoomType?id=1

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RoomType roomType)
        {
            try
            {
                await checkRole();

                if (!ModelState.IsValid)
                {
                    return View(roomType);
                }

                string strData = JsonSerializer.Serialize(roomType);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync($"https://localhost:7143/api/RoomTypes/PutRoomType?id={id}", contentData);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, errorMessage); // Add server-side error to ModelState
                    return View(roomType);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message); // Add exception message to ModelState
                return View(roomType);
            }
        }

        // GET: RoomTypes/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.RoomTypes == null)
        //    {
        //        return NotFound();
        //    }

        //    var roomType = await _context.RoomTypes
        //        .FirstOrDefaultAsync(m => m.RoomTypeId == id);
        //    if (roomType == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(roomType);
        //}

        // POST: RoomTypes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.RoomTypes == null)
        //    {
        //        return Problem("Entity set 'FUMiniHotelManagementContext.RoomTypes'  is null.");
        //    }
        //    var roomType = await _context.RoomTypes.FindAsync(id);
        //    if (roomType != null)
        //    {
        //        _context.RoomTypes.Remove(roomType);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}


    }
}
