using Microsoft.AspNetCore.Mvc;
using PHAMDANGXUANDUY_NET1601_ASS01.Domain.Entity;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Common.Model.Request;
using PHAMDANGXUANDUY_NET1601_ASS01.Infrastructure.Service;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoomTypesController : ControllerBase
    {
        private readonly IRoomTypeService _roomTypeService;

        public RoomTypesController(IRoomTypeService roomTypeService)
        {
            _roomTypeService = roomTypeService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomType>>> GetRoomTypes()
        {
            try
            {
                return Ok(await _roomTypeService.GetRoomTypeAsync());
            }
            catch (Exception ex)
            {
                return BadRequest($"Internal Server Error: {ex.Message}");

            }
        }

        [HttpGet]
        public async Task<ActionResult<RoomType>> GetRoomType(int id)
        {
            try
            {
                return Ok(await _roomTypeService.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest($"Internal Server Error: {ex.Message}");

            }
        }

        [HttpPut]
        public async Task<IActionResult> PutRoomType(int id, CreateRoomType roomType)
        {
            try
            {
                return Ok(await _roomTypeService.Update(id, roomType));
            }
            catch (Exception ex)
            {
                return BadRequest($"Internal Server Error: {ex.Message}");

            }
        }


        [HttpPost]
        public async Task<ActionResult<RoomType>> PostRoomType(CreateRoomType roomType)
        {
            try
            {
                return Ok(await _roomTypeService.Add(roomType));
            }
            catch (Exception ex)
            {
                return BadRequest($"Internal Server Error: {ex.Message}");

            }
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteRoomType(int id)
        //{
        //    if (_context.RoomTypes == null)
        //    {
        //        return NotFound();
        //    }
        //    var roomType = await _context.RoomTypes.FindAsync(id);
        //    if (roomType == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.RoomTypes.Remove(roomType);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}


    }
}
