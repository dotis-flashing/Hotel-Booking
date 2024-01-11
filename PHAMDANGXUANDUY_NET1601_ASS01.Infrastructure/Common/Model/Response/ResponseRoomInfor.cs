

namespace PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Response
{
    public class ResponseRoomInfor
    {
        public int RoomId { get; set; }
        public string RoomNumber { get; set; } = null!;
        public string? RoomDetailDescription { get; set; }
        public int? RoomMaxCapacity { get; set; }
        public int RoomTypeId { get; set; }
        public byte? RoomStatus { get; set; }
        public decimal? RoomPricePerDay { get; set; }
    }
}
