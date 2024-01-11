

namespace PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Request
{
    public class CreateRoomType
    {
        public string RoomTypeName { get; set; } = null!;
        public string? TypeDescription { get; set; }
        public string? TypeNote { get; set; }
    }
}
