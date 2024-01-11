using PHAMDANGXUANDUY_NET1601_ASS01.Application.Repository;

namespace PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.IUnitofwork
{
    public interface IUnitOfwork
    {
        Task Commit();
        ICustomerRepository CustomerRepository { get; }
        IBookingDetailRepository BookingDetailRepository { get; }
        IBookingReservationRepository BookingReservationRepository { get; }
        IRoomTypeRepository RoomTypeRepository { get; }
        IRoomInforRepository RoomInforRepository { get; }
    }
}
