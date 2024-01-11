
namespace PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Request
{
    public class CreateBookingReservation
    {
        public int CustomerId { get; set; }

        //public decimal? TotalPrice { get; set; }
        public List<CreateBookingDetail> CreateBookingDetailList { get; set; }

    }
}
