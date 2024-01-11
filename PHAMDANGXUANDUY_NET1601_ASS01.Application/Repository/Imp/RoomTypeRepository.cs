using PHAMDANGXUANDUY_NET1601_ASS01.Application.IGeneric.Imp;
using PHAMDANGXUANDUY_NET1601_ASS01.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHAMDANGXUANDUY_NET1601_ASS01.Application.Repository.Imp
{
    public class RoomTypeRepository : GenericReository<RoomType>, IRoomTypeRepository
    {
        public RoomTypeRepository(FUMiniHotelManagementContext context) : base(context)
        {
        }
    }
}
