using Infrastructure.Common.Model.Request;
using Microsoft.AspNetCore.Mvc;
using PHAMDANGXUANDUY_NET1601_ASS01.Domain.Entity;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Request;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Response;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Service;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookingReservationsController : ControllerBase
    {
        private readonly IBookingRevervationService _reservationService;

        public BookingReservationsController(IBookingRevervationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseBookingRevervation>>> GetBookingReservations()
        {
            return Ok(await _reservationService.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> GetBookingReservation(int id)
        {
            return Ok(await _reservationService.GetById(id));

        }
        [HttpGet]
        public async Task<IActionResult> GetBookingReservationByCustomerAndDate(int id, [DataType(DataType.Date)] DateTime date)
        {
            return Ok(await _reservationService.GetByCustomer(id, date));

        }
        [HttpGet]
        public async Task<ActionResult<List<ResponseBookingRevervation>>> SearchDate([DataType(DataType.Date)] DateTime date)
        {
            try
            {
                return Ok(await _reservationService.SearchDate(date));
            }
            catch (Exception ex)
            {
                return BadRequest($"Internal Server Error: {ex.Message}");

            }
        }

        [HttpPatch]
        public async Task<ActionResult<ResponseBookingRevervation>> Payment(int id, UpdateBookingRevervation responseBookingRevervation)
        {
            try
            {
                return Ok(await _reservationService.UpdateCalculate(id, responseBookingRevervation));
            }
            catch (Exception ex)
            {
                return BadRequest($"Internal Server Error: {ex.Message}");

            }
        }

        [HttpPost]
        public async Task<ActionResult<ResponseBookingRevervation>> PostBookingReservation(CreateBookingReservation bookingReservation)
        {
            try
            {
                return Ok(await _reservationService.Add(bookingReservation));

            }
            catch (Exception ex)
            {
                return BadRequest($"Internal Server Error: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBookingReservation(int id)
        {
            try
            {
                return Ok(await _reservationService.Delete(id));

            }
            catch (Exception ex)
            {
                return BadRequest($"Internal Server Error: {ex.Message}");
            }
        }


    }
}
