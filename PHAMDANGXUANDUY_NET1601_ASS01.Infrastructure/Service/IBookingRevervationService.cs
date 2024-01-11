using PHAMDANGXUANDUY_NET1601_ASS01.Domain.Entity;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Request;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Response;


namespace PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Service
{
    public interface IBookingRevervationService
    {
        Task<List<ResponseBookingRevervation>> GetAll();
        Task<ResponseBookingRevervation> Add(CreateBookingReservation createBookingReservation);
        Task<ResponseBookingRevervation> GetById(int id);
        Task<ResponseBookingRevervation> UpdateCalculate(int id, byte status);
        Task<List<ResponseBookingRevervation>> SearchDate(DateTime dateTime);

    }
}
