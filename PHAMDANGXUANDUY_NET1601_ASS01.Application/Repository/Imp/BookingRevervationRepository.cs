using Microsoft.EntityFrameworkCore;
using PHAMDANGXUANDUY_NET1601_ASS01.Application.IGeneric.Imp;
using PHAMDANGXUANDUY_NET1601_ASS01.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace PHAMDANGXUANDUY_NET1601_ASS01.Application.Repository.Imp
{
    public class BookingRevervationRepository : GenericReository<BookingReservation>, IBookingReservationRepository
    {
        public BookingRevervationRepository(FUMiniHotelManagementContext context) : base(context)
        {
        }

        public async Task<BookingReservation> GetByIdd(int id)
        {
            var check =
                await _context.Set<BookingReservation>()
                .Include(c => c.BookingDetails)
                .FirstOrDefaultAsync(c => c.BookingReservationId == id);

            if (check == null)
            {
                throw new Exception("khong tim thay");
            }
            return check;
        }

        public async Task<List<BookingReservation>> GetReservation()
        {
            return await _context.Set<BookingReservation>()
                .Include(c => c.BookingDetails)
                .OrderByDescending(c => c.BookingDate)
                .ToListAsync();
        }

        public async Task<List<BookingReservation>> SearchDate(DateTime startDate)
        {

            //if (startDate == DateTime.MinValue)
            //{
            //    return await _context.Set<BookingReservation>()
            //     .Include(c => c.BookingDetails)
            //     .ToListAsync();
            //}
            //else
            //{
                var date = await _context.Set<BookingReservation>()
                .Include(c => c.BookingDetails)
                .Where(c => c.BookingDate.Equals(startDate))
                .OrderByDescending(c=>c.BookingDate)
                .ToListAsync();
                return date;
            //}
        }
    }
}