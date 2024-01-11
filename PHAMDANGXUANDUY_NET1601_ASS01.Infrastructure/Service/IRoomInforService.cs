using PHAMDANGXUANDUY_NET1601_ASS01.Domain.Entity;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Request;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Response;


namespace PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Service
{
    public interface IRoomInforService
    {
        Task<RoomInformation> Add(CreateRoomInfor createRoomInfor);
        Task<List<ResponseRoomInfor>> Getall();
        Task<List<ResponseRoomInfor>> GetallCustomer();
        Task<ResponseRoomInfor> GetById(int id);
        Task<ResponseRoomInfor> Update(int id, UpdateRoomInfor createRoomInfor);
        Task<ResponseRoomInfor> Remove(int id);
    }
}
