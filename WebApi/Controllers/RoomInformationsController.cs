using Microsoft.AspNetCore.Mvc;
using PHAMDANGXUANDUY_NET1601_ASS01.Domain.Entity;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Request;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Response;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Service;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Service.Imp;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoomInformationsController : ControllerBase
    {
        private readonly IRoomInforService _roomInforService;

        public RoomInformationsController(IRoomInforService roomInforService)
        {
            _roomInforService = roomInforService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseRoomInfor>>> GetRoomInformations()
        {
            try
            {
                return Ok(await _roomInforService.Getall());
            }
            catch (Exception ex)
            {
                return BadRequest($"Internal Server Error: {ex.Message}");

            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseRoomInfor>>> GetRoomInforforCustomer()
        {
            try
            {
                return Ok(await _roomInforService.GetallCustomer());
            }
            catch (Exception ex)
            {
                return BadRequest($"Internal Server Error: {ex.Message}");

            }
        }
        [HttpGet]
        public async Task<ActionResult<ResponseRoomInfor>> GetRoomInformation(int id)
        {
            try
            {
                return Ok(await _roomInforService.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest($"Internal Server Error: {ex.Message}");

            }
        }

        [HttpPatch]
        public async Task<ActionResult<ResponseRoomInfor>> PutRoomInformation(int id, UpdateRoomInfor roomInformation)
        {
            try
            {
                return Ok(await _roomInforService.Update(id, roomInformation));
            }
            catch (Exception ex)
            {
                return BadRequest($"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResponseRoomInfor>> PostRoomInformation(CreateRoomInfor roomInformation)
        {
            try
            {
                return Ok(await _roomInforService.Add(roomInformation));
            }
            catch (Exception ex)
            {
                return BadRequest($"Internal Server Error: {ex.Message}");

            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRoomInformation(int id)
        {
            try
            {
                return Ok(await _roomInforService.Remove(id));
            }
            catch (Exception ex)
            {
                return BadRequest($"Internal Server Error: {ex.Message}");

            }
        }


    }
}
