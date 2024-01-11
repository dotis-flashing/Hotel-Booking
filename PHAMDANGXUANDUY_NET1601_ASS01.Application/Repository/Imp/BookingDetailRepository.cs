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
    public class BookingDetailRepository : GenericReository<BookingDetail>, IBookingDetailRepository
    {
        public BookingDetailRepository(FUMiniHotelManagementContext context) : base(context)
        {
        }

        public async Task<List<BookingDetail>> CheckRoomInfoBookingExist(int id)
        {
            var check = await _context.Set<BookingDetail>().Where(c => c.RoomId.Equals(id)).ToListAsync();
            if (check != null)
            {
                throw new Exception("Can't Update RoomId -- Create new Room");
            }
            return check;
        }
    }
}
