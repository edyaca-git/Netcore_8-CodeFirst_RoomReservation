using RoomReservationApi.Application.DTOs;
using RoomReservationApi.Shared.Common;
using RoomReservationApi.Shared.Entities;

namespace RoomReservationApi.Application.Interfaces.Services
{
    public interface IReservationService
    {
        Task<ApiResponse<ReservationDto>> CreateAsync(CreateReservationDto dto);

        Task<ApiResponse<PagedResult<ReservationDto>>> GetAllAsync(
            int page = 1,
            int pageSize = 10,
            int? roomId = null,
            string? status = null,
            string? reservedBy = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string? sortBy = "startTime",
            string? sortOrder = "asc");

        Task<ApiResponse<ReservationDto>> GetByIdAsync(int id);

        Task<ApiResponse<ReservationDto>> UpdateAsync(int id, UpdateReservationDto dto);

        Task<ApiResponse<bool>> CancelAsync(int id);
    }
}