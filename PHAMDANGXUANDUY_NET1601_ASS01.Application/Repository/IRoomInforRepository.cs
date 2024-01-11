using PHAMDANGXUANDUY_NET1601_ASS01.Application.IGeneric;
using PHAMDANGXUANDUY_NET1601_ASS01.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHAMDANGXUANDUY_NET1601_ASS01.Application.Repository
{
    public interface IRoomInforRepository :IGenericRepository<RoomInformation>
    {
        Task<RoomInformation> GetByRoomId(int id);
        Task<List<RoomInformation>> GetRoom();
        Task<List<RoomInformation>> GetRoomCustomer();
        Task<RoomInformation> CheckRoomIdByStatusActive(int id);

    }
}
