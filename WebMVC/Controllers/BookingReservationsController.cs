using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PHAMDANGXUANDUY_NET1601_ASS01.Domain.Entity;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Response;

namespace WebMVC.Controllers
{
    public class BookingReservationsController : Controller
    {
        private readonly FUMiniHotelManagementContext _context;
        private readonly HttpClient client = null;

        public BookingReservationsController()
        {
            _context = new FUMiniHotelManagementContext();
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        public async Task<IActionResult> Index(DateTime? searchDate, int? customerId)
        {
            var checkrole = await checkRole();
            string apiUrl = "https://localhost:7143/api/BookingReservations/";

            if (checkrole == true)
            {
                apiUrl += $"SearchDate";

                if (searchDate.HasValue)
                {
                    apiUrl += $"?date={searchDate}";
                    //await Console.Out.WriteLineAsync(searchDate.Value.ToString());
                }

                HttpResponseMessage response = await client.GetAsync(apiUrl);

                string strData = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                List<ResponseBookingRevervation> listProducts = JsonSerializer.Deserialize<List<ResponseBookingRevervation>>(strData, options);
                return View(listProducts);
            }
            else if (checkrole == false)
            {
                apiUrl += $"GetBookingReservationByCustomerAndDate?id={customerId}";
                if (searchDate.HasValue)
                {
                    apiUrl += $"&?date={searchDate}";
                }

                HttpResponseMessage response = await client.GetAsync(apiUrl);

                string strData = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                List<ResponseBookingRevervation> listProducts = JsonSerializer.Deserialize<List<ResponseBookingRevervation>>(strData, options);
                return View(listProducts);
            }
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                await checkRole();

                HttpResponseMessage response = await client.GetAsync($"https://localhost:7143/api/BookingReservations/GetBookingReservation?id={id}");

                if (response.IsSuccessStatusCode)
                {
                    string strData = await response.Content.ReadAsStringAsync();

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var room = JsonSerializer.Deserialize<ResponseBookingRevervation>(strData, options);

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
        public async Task<IActionResult> CreateAsync()
        {
            try
            {
                await LoadCustomersAndRooms();
                //await checkRole();

                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<JsonResult> Create([FromBody] CreateBookingReservation bookingReservation)
        //{
        //    try
        //    {
        //        await LoadCustomersAndRooms();

        //        //if (ModelState.IsValid)
        //        //{
        //        //    string strDatas = JsonSerializer.Serialize(bookingReservation);
        //        //    var contentData = new StringContent(strDatas, System.Text.Encoding.UTF8, "application/json");

        //        //    HttpResponseMessage responsed = await client.PostAsync("https://localhost:7143/api/BookingReservations/PostBookingReservation", contentData);

        //        //    if (responsed.IsSuccessStatusCode)
        //        //    {
        //        //        return Json(new { success = true, redirectUrl = Url.Action("Index", "BookingReservations") });
        //        //    }
        //        //    else
        //        //    {
        //        //        string errorMessage = await responsed.Content.ReadAsStringAsync();
        //        //        ModelState.AddModelError(string.Empty, errorMessage);
        //        //        return Json(new { success = false, errorMessage = errorMessage });
        //        //    }
        //        //}
        //        //return Json(new { success = false, errorMessage = "Invalid ModelState" });

        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError(string.Empty, ex.Message);
        //        return Json(new { success = false, errorMessage = ex.Message });
        //    }
        //}


        private async Task LoadCustomersAndRooms()
        {

            await checkRole();
            HttpResponseMessage customerResponse = await client.GetAsync("https://localhost:7143/api/Customers/GetCustomers");
            string customerData = await customerResponse.Content.ReadAsStringAsync();

            var customerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<Customer> customers = JsonSerializer.Deserialize<List<Customer>>(customerData, customerOptions);
            ViewData["CustomerId"] = new SelectList(customers, "CustomerId", "EmailAddress");

            HttpResponseMessage roomResponse = await client.GetAsync("https://localhost:7143/api/RoomInformations/GetRoomInforforCustomer");
            string roomData = await roomResponse.Content.ReadAsStringAsync();

            var roomOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<RoomInformation> rooms = JsonSerializer.Deserialize<List<RoomInformation>>(roomData, roomOptions);
            ViewData["RoomId"] = new SelectList(rooms.Select(c => new
            {
                Text = $"MaxCapacity: {c.RoomMaxCapacity} - Description: {c.RoomDetailDescription} - RoomPricePerDay: {c.RoomPricePerDay}",
                Value = c.RoomId // Replace with the actual property from your RoomInformation class
            }), "Value", "Text");
        }



        private async Task<bool> checkRole()
        {

            var admin = HttpContext.Session.GetString("ADMIN");
            if (admin != null)
            {
                ViewBag.ADMIN = admin;
                return true;
            }
            var customerInfo = HttpContext.Session.GetString("CUSTOMER");
            var customer = JsonSerializer.Deserialize<ResponseCustomer>(customerInfo);
            if (customer != null)
            {
                ViewBag.CustomerInfo = customer.CustomerId;
                return false;
            }
            return false;
        }


        // GET: BookingReservations/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"https://localhost:7143/api/BookingReservations/GetBookingReservation?id={id}");

                if (response.IsSuccessStatusCode)
                {
                    string strData = await response.Content.ReadAsStringAsync();

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var room = JsonSerializer.Deserialize<ResponseBookingRevervation>(strData, options);

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
        //api/BookingReservations/Payment?id=11&status=1

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ResponseBookingRevervation reservation)
        {
            try
            {
                await checkRole();

                if (!ModelState.IsValid)
                {
                    return View(reservation);
                }

                string strData = JsonSerializer.Serialize(reservation);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync($"https://localhost:7143/api/BookingReservations/Payment?id={id}&status={1}", contentData);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"API Error: {errorMessage}");
                    ModelState.AddModelError(string.Empty, errorMessage);
                    return View(reservation);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(reservation);
            }

        }

        // GET: BookingReservations/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.BookingReservations == null)
        //    {
        //        return NotFound();
        //    }

        //    var bookingReservation = await _context.BookingReservations
        //        .Include(b => b.Customer)
        //        .FirstOrDefaultAsync(m => m.BookingReservationId == id);
        //    if (bookingReservation == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(bookingReservation);
        //}

        //// POST: BookingReservations/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.BookingReservations == null)
        //    {
        //        return Problem("Entity set 'FUMiniHotelManagementContext.BookingReservations'  is null.");
        //    }
        //    var bookingReservation = await _context.BookingReservations.FindAsync(id);
        //    if (bookingReservation != null)
        //    {
        //        _context.BookingReservations.Remove(bookingReservation);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool BookingReservationExists(int id)
        //{
        //    return (_context.BookingReservations?.Any(e => e.BookingReservationId == id)).GetValueOrDefault();
        //}
    }
}
