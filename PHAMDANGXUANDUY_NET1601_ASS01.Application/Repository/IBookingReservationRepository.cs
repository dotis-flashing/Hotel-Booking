using PHAMDANGXUANDUY_NET1601_ASS01.Application.IGeneric;
using PHAMDANGXUANDUY_NET1601_ASS01.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHAMDANGXUANDUY_NET1601_ASS01.Application.Repository
{
    public interface IBookingReservationRepository :IGenericRepository<BookingReservation>
    {
        Task<BookingReservation> GetByIdd(int id);
        Task<List<BookingReservation>> GetReservation();
        Task<List<BookingReservation>> GetByCustomer(int customerId);
        Task<List<BookingReservation>> GetByCustomerAndDate(int customerId,DateTime dateTime);
        Task<List<BookingReservation>> SearchDate(DateTime startDate);
    }
}
