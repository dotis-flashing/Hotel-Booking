using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Request
{
    public class UpdateRoomInfor
    {
        public string RoomNumber { get; set; } = null!;
        public string? RoomDetailDescription { get; set; }
        public int? RoomMaxCapacity { get; set; }
        public decimal? RoomPricePerDay { get; set; }
    }
}
