

namespace PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Response
{
    public class ResponseRoomType
    {
        public int RoomTypeId { get; set; }
        public string RoomTypeName { get; set; } = null!;
        public string? TypeDescription { get; set; }
        public string? TypeNote { get; set; }
    }
}
