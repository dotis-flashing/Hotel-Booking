using Microsoft.EntityFrameworkCore;
using PHAMDANGXUANDUY_NET1601_ASS01.Application.IGeneric.Imp;
using PHAMDANGXUANDUY_NET1601_ASS01.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHAMDANGXUANDUY_NET1601_ASS01.Application.Repository.Imp
{
    public class RoomInformationRepository : GenericReository<RoomInformation>, IRoomInforRepository
    {
        public RoomInformationRepository(FUMiniHotelManagementContext context) : base(context)
        {
        }

        public async Task<RoomInformation> CheckRoomIdByStatusActive(int id)
        {
            var roomInformation = await _context.Set<RoomInformation>().FirstOrDefaultAsync(c => c.RoomId == id && c.RoomStatus == 1);
            if (roomInformation == null)
            {
                throw new Exception("khong dc active");
            }
            return roomInformation;
        }

        public async Task<RoomInformation> GetByRoomId(int id)
        {
            var roomInformation = await _context.Set<RoomInformation>().FirstOrDefaultAsync(c => c.RoomId == id);
            if (roomInformation == null)
            {
                throw new Exception("khong tim thay");
            }
            return roomInformation;
        }

        public async Task<List<RoomInformation>> GetRoom()
        {
            return await _context.Set<RoomInformation>().Include(c => c.RoomType).ToListAsync();
        }

        public async Task<List<RoomInformation>> GetRoomCustomer()
        {
            return await _context.Set<RoomInformation>().Include(c => c.RoomType).Where(c => c.RoomStatus == 1).ToListAsync();
        }
    }
}
