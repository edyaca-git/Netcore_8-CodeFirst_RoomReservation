using Microsoft.AspNetCore.Mvc;
using RoomReservationApi.Application.DTOs;
using RoomReservationApi.Application.Interfaces.Services;

namespace RoomReservationApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReservationDto dto)
        {
            var response = await _reservationService.CreateAsync(dto);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] int? roomId = null,
            [FromQuery] string? status = null,
            [FromQuery] string? reservedBy = null,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null,
            [FromQuery] string? sortBy = "startTime",
            [FromQuery] string? sortOrder = "asc")
        {
            var response = await _reservationService.GetAllAsync(page, pageSize, roomId, status, reservedBy, startDate, endDate, sortBy, sortOrder);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _reservationService.GetByIdAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateReservationDto dto)
        {
            var response = await _reservationService.UpdateAsync(id, dto);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPatch("{id}/cancel")]
        public async Task<IActionResult> Cancel(int id)
        {
            var response = await _reservationService.CancelAsync(id);
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}