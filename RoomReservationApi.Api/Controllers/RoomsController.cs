using Microsoft.AspNetCore.Mvc;
using RoomReservationApi.Application.DTOs;
using RoomReservationApi.Application.Interfaces.Services;

namespace RoomReservationApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoomDto dto)
        {
            var response = await _roomService.CreateAsync(dto);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var response = await _roomService.GetAllAsync(page, pageSize);
            return Ok(response);
        }

        [HttpGet("with-reservations")]
        public async Task<IActionResult> GetAllWithReservations(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? status = null)   // null = todas, o Pending/Confirmed/Cancelled
        {
            var response = await _roomService.GetAllWithReservationsAsync(page, pageSize, status);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _roomService.GetByIdAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRoomDto dto)
        {
            var response = await _roomService.UpdateAsync(id, dto);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _roomService.DeleteAsync(id);
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}