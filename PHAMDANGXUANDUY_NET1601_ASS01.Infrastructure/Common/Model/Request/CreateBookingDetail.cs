

using System.ComponentModel.DataAnnotations;

namespace PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Request
{
    public class CreateBookingDetail
    {
        [Required]
        public int RoomId { get; set; }
        [DataType(DataType.Date)]
        [Required]

        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [Required]

        public DateTime EndDate { get; set; }
        //public decimal? ActualPrice { get; set; }
    }
}
