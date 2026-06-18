using RoomReservationApi.Application.DTOs;
using RoomReservationApi.Shared.Common;
using RoomReservationApi.Shared.Entities;

namespace RoomReservationApi.Application.Interfaces.Services
{
    public interface IRoomService
    {
        Task<ApiResponse<RoomDto>> CreateAsync(CreateRoomDto dto);

        Task<ApiResponse<PagedResult<RoomDto>>> GetAllAsync(int page = 1, int pageSize = 10);

        Task<ApiResponse<PagedResult<RoomWithReservationsDto>>> GetAllWithReservationsAsync(
            int page = 1,
            int pageSize = 10,
            string? status = null);   // null = todas, o "Pending", "Confirmed", "Cancelled"

        Task<ApiResponse<RoomDto>> GetByIdAsync(int id);

        Task<ApiResponse<RoomDto>> UpdateAsync(int id, UpdateRoomDto dto);

        Task<ApiResponse<bool>> DeleteAsync(int id);
    }
}