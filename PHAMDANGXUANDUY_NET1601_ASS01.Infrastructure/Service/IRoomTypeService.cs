using PHAMDANGXUANDUY_NET1601_ASS01.Domain.Entity;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Request;


namespace PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Service
{
    public interface IRoomTypeService
    {
        Task<List<RoomType>> GetRoomTypeAsync();
        Task<RoomType> Add(CreateRoomType createRoomType);
        Task<RoomType> GetById(int id);
        Task<RoomType> Update(int id,CreateRoomType updateRoomType);
    }
}
