using PHAMDANGXUANDUY_NET1601_ASS01.Application.IGeneric;
using PHAMDANGXUANDUY_NET1601_ASS01.Domain.Entity;

namespace PHAMDANGXUANDUY_NET1601_ASS01.Application.Repository
{
    public interface IBookingDetailRepository : IGenericRepository<BookingDetail>
    {
        Task<List<BookingDetail>> CheckRoomInfoBookingExist(int id);
    }
}
