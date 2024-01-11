using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PHAMDANGXUANDUY_NET1601_ASS01.Domain.Entity
{
    public partial class RoomInformation
    {
        public RoomInformation()
        {
            BookingDetails = new HashSet<BookingDetail>();
        }

        public int RoomId { get; set; }
        public string RoomNumber { get; set; } = null!;
        public string? RoomDetailDescription { get; set; }
        public int? RoomMaxCapacity { get; set; }
        public int RoomTypeId { get; set; }
        public byte? RoomStatus { get; set; }
        public decimal? RoomPricePerDay { get; set; }

        public virtual RoomType RoomType { get; set; } = null!;
        public virtual ICollection<BookingDetail> BookingDetails { get; set; }
    }
}
