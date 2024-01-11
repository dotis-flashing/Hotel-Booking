using AutoMapper;
using PHAMDANGXUANDUY_NET1601_ASS01.Domain.Entity;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Request;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Response;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.IUnitofwork;

namespace PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Service.Imp
{
    public class BookingRevervationService : IBookingRevervationService
    {
        private readonly IUnitOfwork _unitOfwork;
        private readonly IMapper _mapper;

        public BookingRevervationService(IUnitOfwork unitOfwork, IMapper mapper)
        {
            _unitOfwork = unitOfwork;
            _mapper = mapper;
        }

        public async Task<ResponseBookingRevervation> Add(CreateBookingReservation createBookingReservation)
        {
            var booking = _mapper.Map<BookingReservation>(createBookingReservation);
            booking.BookingStatus = 0;
            booking.BookingDate = DateTime.Now;
            booking.TotalPrice = 0;

            await _unitOfwork.CustomerRepository.GetById(booking.CustomerId);

            var uniqueRoomIds = new HashSet<int>();

            foreach (var bookingDetail in booking.BookingDetails)
            {
                var bookingdetail = _mapper.Map<BookingDetail>(bookingDetail);

                if (bookingdetail.EndDate <= bookingdetail.StartDate)
                {
                    throw new Exception("EndDate không được bé hơn StartDate");
                }

                if (!uniqueRoomIds.Add(bookingdetail.RoomId))
                {
                    throw new Exception($"Duplicate RoomID {bookingdetail.RoomId} found in the booking details.");
                }
                await _unitOfwork.RoomInforRepository.CheckRoomIdByStatusActive(bookingdetail.RoomId);
                var numberdate = bookingdetail.EndDate - bookingdetail.StartDate;
                var checkRoomId = await _unitOfwork.RoomInforRepository.GetByRoomId(bookingdetail.RoomId);
                bookingdetail.ActualPrice = checkRoomId.RoomPricePerDay * (decimal)numberdate.TotalDays;
                booking.TotalPrice += bookingdetail.ActualPrice;

                await _unitOfwork.BookingDetailRepository.Add(bookingdetail);
            }

            await _unitOfwork.BookingReservationRepository.Add(booking);
            await _unitOfwork.Commit();

            return _mapper.Map<ResponseBookingRevervation>(booking);
        }


        public async Task<List<ResponseBookingRevervation>> GetAll()
        {
            return _mapper.Map<List<ResponseBookingRevervation>>(await _unitOfwork.BookingReservationRepository.GetReservation());
        }

        public async Task<ResponseBookingRevervation> GetById(int id)
        {

            var response = await _unitOfwork.BookingReservationRepository.GetByIdd(id);
            return _mapper.Map<ResponseBookingRevervation>(response);
        }

        public async Task<List<ResponseBookingRevervation>> SearchDate(DateTime dateTime)
        {

            if (dateTime == DateTime.MinValue)
            {
                var allBookings = await _unitOfwork.BookingReservationRepository.GetReservation();

                return _mapper.Map<List<ResponseBookingRevervation>>(allBookings);
            }

            var dateFilteredBookings = await _unitOfwork.BookingReservationRepository.SearchDate(dateTime);
            return _mapper.Map<List<ResponseBookingRevervation>>(dateFilteredBookings);

        }

        public async Task<ResponseBookingRevervation> UpdateCalculate(int id, byte status)
        {
            var booking = await _unitOfwork.BookingReservationRepository.GetById(id);
            if (booking.BookingStatus == status)
            {
                throw new Exception($"Already exist {status}");
            }
            if (booking.BookingStatus == 1)
            {
                throw new Exception("Paid");
            }
            booking.BookingStatus = status;
            await _unitOfwork.BookingReservationRepository.Update(booking);
            await _unitOfwork.Commit();
            return _mapper.Map<ResponseBookingRevervation>(booking);
        }
    }
}
