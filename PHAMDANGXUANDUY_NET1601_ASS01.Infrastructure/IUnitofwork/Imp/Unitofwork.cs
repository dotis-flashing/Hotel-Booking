using PHAMDANGXUANDUY_NET1601_ASS01.Application.Repository;
using PHAMDANGXUANDUY_NET1601_ASS01.Application.Repository.Imp;
using PHAMDANGXUANDUY_NET1601_ASS01.Domain.Entity;


namespace PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.IUnitofwork.Imp
{
    public class Unitofwork : IUnitOfwork
    {
        private readonly FUMiniHotelManagementContext _context;
        private readonly ICustomerRepository _customerRepository;
        private readonly IBookingDetailRepository _bookingDetailRepository;
        private readonly IBookingReservationRepository _bookingReservationRepository;
        private readonly IRoomInforRepository _roomInforRepository;
        private readonly IRoomTypeRepository _roomTypeRepository;

        public Unitofwork()
        {
            _context = new FUMiniHotelManagementContext();
            _customerRepository = new CustomerRepository(_context);
            _bookingDetailRepository = new BookingDetailRepository(_context);
            _bookingReservationRepository=new BookingRevervationRepository(_context);
            _roomInforRepository = new RoomInformationRepository(_context);
            _roomTypeRepository = new RoomTypeRepository(_context);
        }

        public ICustomerRepository CustomerRepository => _customerRepository;

        public IBookingDetailRepository BookingDetailRepository => _bookingDetailRepository;

        public IBookingReservationRepository BookingReservationRepository => _bookingReservationRepository;

        public IRoomTypeRepository RoomTypeRepository => _roomTypeRepository;

        public IRoomInforRepository RoomInforRepository => _roomInforRepository;

        public async Task Commit()
        {
            _context.SaveChanges();
        }
    }
}
