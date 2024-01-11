using Microsoft.AspNetCore.Mvc;
using PHAMDANGXUANDUY_NET1601_ASS01.Domain.Entity;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Request;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Response;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Service;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Service.Imp;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;

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

        [HttpPut]
        public async Task<ActionResult<ResponseBookingRevervation>> Payment(int id, byte status)
        {
            return Ok(await _reservationService.UpdateCalculate(id, status));

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

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteBookingReservation(int id)
        //{
        //    if (_context.BookingReservations == null)
        //    {
        //        return NotFound();
        //    }
        //    var bookingReservation = await _context.BookingReservations.FindAsync(id);
        //    if (bookingReservation == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.BookingReservations.Remove(bookingReservation);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}


    }
}
