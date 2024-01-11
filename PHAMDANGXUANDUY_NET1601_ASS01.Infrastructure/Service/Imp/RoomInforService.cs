using AutoMapper;
using PHAMDANGXUANDUY_NET1601_ASS01.Domain.Entity;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Request;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Response;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.IUnitofwork;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Service.Imp
{
    public class RoomInforService : IRoomInforService
    {
        private readonly IUnitOfwork _unitOfwork;
        private readonly IMapper _mapper;

        public RoomInforService(IUnitOfwork unitOfwork, IMapper mapper)
        {
            _unitOfwork = unitOfwork;
            _mapper = mapper;
        }

        public async Task<RoomInformation> Add(CreateRoomInfor createRoomInfor)
        {
            var roominfor = _mapper.Map<RoomInformation>(createRoomInfor);
            roominfor.RoomStatus = 1;
            await _unitOfwork.RoomInforRepository.Add(roominfor);
            await _unitOfwork.Commit();
            return roominfor;
        }

        public async Task<List<ResponseRoomInfor>> Getall()
        {
            return _mapper.Map<List<ResponseRoomInfor>>(await _unitOfwork.RoomInforRepository.GetRoom());
        }

        public async Task<List<ResponseRoomInfor>> GetallCustomer()
        {
            return _mapper.Map<List<ResponseRoomInfor>>(await _unitOfwork.RoomInforRepository.GetRoomCustomer());
        }

        public async Task<ResponseRoomInfor> GetById(int id)
        {
            var response = await _unitOfwork.RoomInforRepository.GetById(id);
            return _mapper.Map<ResponseRoomInfor>(response);
        }

        public async Task<ResponseRoomInfor> Remove(int id)
        {
            var check = await _unitOfwork.RoomInforRepository.GetByRoomId(id);
            check.RoomStatus = 0;
            await _unitOfwork.RoomInforRepository.Update(check);
            await _unitOfwork.Commit();
            return _mapper.Map<ResponseRoomInfor>(check);
        }

        public async Task<ResponseRoomInfor> Update(int id, UpdateRoomInfor createRoomInfor)
        {
            var check = await _unitOfwork.RoomInforRepository.GetByRoomId(id);
            var update = _mapper.Map(createRoomInfor, check);
            //await _unitOfwork.BookingDetailRepository.CheckRoomInfoBookingExist(check.RoomId);
            update.RoomStatus = 1;
            await _unitOfwork.RoomInforRepository.Update(update);
            await _unitOfwork.Commit();
            return _mapper.Map<ResponseRoomInfor>(update);
        }
    }
}
