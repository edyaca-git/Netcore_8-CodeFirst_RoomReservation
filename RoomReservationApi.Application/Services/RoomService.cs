using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RoomReservationApi.Application.DTOs;
using RoomReservationApi.Application.Interfaces.Services;
using RoomReservationApi.Domain.Entities;
using RoomReservationApi.Domain.Interfaces.Repositories;
using RoomReservationApi.Shared.Common;
using RoomReservationApi.Shared.Entities;

namespace RoomReservationApi.Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;
        private readonly IServiceProvider _serviceProvider;   // ← Agregado

        public RoomService(
            IRoomRepository roomRepository,
            IMapper mapper,
            IServiceProvider serviceProvider)   // ← Cambiado
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
            _serviceProvider = serviceProvider;
        }

        public async Task<ApiResponse<RoomDto>> CreateAsync(CreateRoomDto dto)
        {
            var validator = _serviceProvider.GetRequiredService<IValidator<CreateRoomDto>>();
            var validation = await validator.ValidateAsync(dto);

            if (!validation.IsValid)
                return ApiResponse<RoomDto>.ErrorResponse("Validación fallida",
                    validation.Errors.Select(e => e.ErrorMessage).ToList());

            var room = _mapper.Map<Room>(dto);
            await _roomRepository.AddAsync(room);
            var roomDto = _mapper.Map<RoomDto>(room);

            return ApiResponse<RoomDto>.SuccessResponse(roomDto, "Sala creada exitosamente");
        }

        public async Task<ApiResponse<PagedResult<RoomDto>>> GetAllAsync(int page = 1, int pageSize = 10)
        {
            // No se resuelve ningún validador aquí (GET)
            var rooms = await _roomRepository.GetAllAsync();
            var total = rooms.Count();
            var pagedRooms = rooms.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var dtos = _mapper.Map<List<RoomDto>>(pagedRooms);

            var pagedResult = new PagedResult<RoomDto>(dtos, total, page, pageSize);
            return ApiResponse<PagedResult<RoomDto>>.SuccessResponse(pagedResult);
        }

        public async Task<ApiResponse<PagedResult<RoomWithReservationsDto>>> GetAllWithReservationsAsync(
            int page = 1, int pageSize = 10, string? status = null)
        {
            var rooms = await _roomRepository.GetAllAsync();

            var roomDtos = new List<RoomWithReservationsDto>();

            foreach (var room in rooms)
            {
                var reservationsQuery = room.Reservations.AsQueryable();

                if (!string.IsNullOrEmpty(status))
                {
                    reservationsQuery = reservationsQuery.Where(r => r.Status == status);
                }

                var filteredReservations = reservationsQuery
                    .OrderBy(r => r.StartTime)
                    .ToList();

                var roomDto = new RoomWithReservationsDto
                {
                    Id = room.Id,
                    Name = room.Name,
                    Capacity = room.Capacity,
                    IsActive = room.IsActive,
                    CreatedAt = room.CreatedAt,
                    Reservations = _mapper.Map<List<ReservationDto>>(filteredReservations)
                };

                roomDtos.Add(roomDto);
            }

            var total = roomDtos.Count;
            var pagedRooms = roomDtos
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var pagedResult = new PagedResult<RoomWithReservationsDto>(pagedRooms, total, page, pageSize);

            return ApiResponse<PagedResult<RoomWithReservationsDto>>.SuccessResponse(pagedResult);
        }

        public async Task<ApiResponse<RoomDto>> GetByIdAsync(int id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null)
                return ApiResponse<RoomDto>.ErrorResponse("Sala no encontrada", new List<string> { "El ID proporcionado no existe" });

            var dto = _mapper.Map<RoomDto>(room);
            return ApiResponse<RoomDto>.SuccessResponse(dto);
        }

        public async Task<ApiResponse<RoomDto>> UpdateAsync(int id, UpdateRoomDto dto)
        {
            var validator = _serviceProvider.GetRequiredService<IValidator<UpdateRoomDto>>();
            var validation = await validator.ValidateAsync(dto);

            if (!validation.IsValid)
                return ApiResponse<RoomDto>.ErrorResponse("Validación fallida",
                    validation.Errors.Select(e => e.ErrorMessage).ToList());

            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null)
                return ApiResponse<RoomDto>.ErrorResponse("Sala no encontrada");

            _mapper.Map(dto, room);
            _roomRepository.Update(room);

            var updatedDto = _mapper.Map<RoomDto>(room);
            return ApiResponse<RoomDto>.SuccessResponse(updatedDto, "Sala actualizada exitosamente");
        }

        public async Task<ApiResponse<bool>> DeleteAsync(int id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null)
                return ApiResponse<bool>.ErrorResponse("Sala no encontrada");

            if (await _roomRepository.HasActiveFutureReservationsAsync(id))
                return ApiResponse<bool>.ErrorResponse("No se puede eliminar una sala con reservaciones futuras activas");

            _roomRepository.Delete(room);
            return ApiResponse<bool>.SuccessResponse(true, "Sala eliminada exitosamente");
        }
    }
}