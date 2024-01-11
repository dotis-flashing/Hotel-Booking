namespace PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Response;

public class ResponseBookingRevervation
{
    public int CustomerId { get; set; }
    public int BookingReservationId { get; set; }

    public List<ResponseBookingDetail> BookingDetails { get; set; }
    public DateTime? BookingDate { get; set; }
    public decimal? TotalPrice { get; set; }
    public byte? BookingStatus { get; set; }


}
