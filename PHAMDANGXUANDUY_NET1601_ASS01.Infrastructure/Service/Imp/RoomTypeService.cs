using AutoMapper;
using PHAMDANGXUANDUY_NET1601_ASS01.Domain.Entity;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Request;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.IUnitofwork;

namespace PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Service.Imp
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IUnitOfwork _unitOfwork;
        private readonly IMapper _mapper;

        public RoomTypeService(IUnitOfwork unitOfwork, IMapper mapper)
        {
            _unitOfwork = unitOfwork;
            _mapper = mapper;
        }

        public async Task<RoomType> Add(CreateRoomType createRoomType)
        {
            var roomtype = _mapper.Map<RoomType>(createRoomType);
            await _unitOfwork.RoomTypeRepository.Add(roomtype);
            await _unitOfwork.Commit();
            return roomtype;
        }

        public async Task<RoomType> GetById(int id)
        {
            return _mapper.Map<RoomType>(await _unitOfwork.RoomTypeRepository.GetById(id));
        }

        public async Task<List<RoomType>> GetRoomTypeAsync()
        {
            return _mapper.Map<List<RoomType>>(await _unitOfwork.RoomTypeRepository.GetAll());
        }

        public async Task<RoomType> Update(int id, CreateRoomType updateRoomType)
        {
            var roomtype = await _unitOfwork.RoomTypeRepository.GetById(id);
            var update = _mapper.Map(updateRoomType, roomtype);
            await _unitOfwork.RoomTypeRepository.Update(update);
            await _unitOfwork.Commit();
            return update;
        }
    }
}
