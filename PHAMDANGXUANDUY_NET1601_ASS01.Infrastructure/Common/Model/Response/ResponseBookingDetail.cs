using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Response
{
    public class ResponseBookingDetail
    {
        public int BookingReservationId { get; set; }
        public int RoomId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal? ActualPrice { get; set; }
    }
}
