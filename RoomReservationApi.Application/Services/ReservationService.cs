using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RoomReservationApi.Application.DTOs;
using RoomReservationApi.Application.Interfaces.Services;
using RoomReservationApi.Domain.Entities;
using RoomReservationApi.Domain.Enums;
using RoomReservationApi.Domain.Interfaces.Repositories;
using RoomReservationApi.Shared.Common;
using RoomReservationApi.Shared.Entities;

namespace RoomReservationApi.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;
        private readonly IServiceProvider _serviceProvider;   // ← Nuevo

        public ReservationService(
            IReservationRepository reservationRepository,
            IRoomRepository roomRepository,
            IMapper mapper,
            IServiceProvider serviceProvider)   // ← Cambiado
        {
            _reservationRepository = reservationRepository;
            _roomRepository = roomRepository;
            _mapper = mapper;
            _serviceProvider = serviceProvider;
        }

        public async Task<ApiResponse<ReservationDto>> CreateAsync(CreateReservationDto dto)
        {
            var validator = _serviceProvider.GetRequiredService<IValidator<CreateReservationDto>>();
            var validation = await validator.ValidateAsync(dto);

            if (!validation.IsValid)
                return ApiResponse<ReservationDto>.ErrorResponse("Validación fallida",
                    validation.Errors.Select(e => e.ErrorMessage).ToList());

            // Resto del método sin cambios...
            var room = await _roomRepository.GetByIdAsync(dto.RoomId);
            if (room == null || !room.IsActive)
                return ApiResponse<ReservationDto>.ErrorResponse("La sala no existe o está inactiva");

            bool hasOverlap = await _reservationRepository.HasOverlapAsync(dto.RoomId, dto.StartTime, dto.EndTime);
            if (hasOverlap)
                return ApiResponse<ReservationDto>.ErrorResponse("Existe traslape de horario con otra reservación activa");

            var reservation = _mapper.Map<Reservation>(dto);
            reservation.Status = ReservationStatus.Pending;

            await _reservationRepository.AddAsync(reservation);
            var reservationDto = _mapper.Map<ReservationDto>(reservation);

            return ApiResponse<ReservationDto>.SuccessResponse(reservationDto, "Reservación creada exitosamente");
        }

        public async Task<ApiResponse<PagedResult<ReservationDto>>> GetAllAsync(
            int page = 1, int pageSize = 10, int? roomId = null, string? status = null,
            string? reservedBy = null, DateTime? startDate = null, DateTime? endDate = null,
            string? sortBy = "startTime", string? sortOrder = "asc")
        {
            // NO se resuelve ningún validador aquí
            var reservations = await _reservationRepository.GetAllFilteredAsync(roomId, status, reservedBy, startDate, endDate, sortBy, sortOrder);
            var total = reservations.Count();
            var pageSizeLimited = Math.Min(pageSize, 50);
            var paged = reservations.Skip((page - 1) * pageSizeLimited).Take(pageSizeLimited).ToList();
            var dtos = _mapper.Map<List<ReservationDto>>(paged);

            var pagedResult = new PagedResult<ReservationDto>(dtos, total, page, pageSizeLimited);
            return ApiResponse<PagedResult<ReservationDto>>.SuccessResponse(pagedResult);
        }

        // UpdateAsync también debe usar resolución bajo demanda (similar a CreateAsync)
        public async Task<ApiResponse<ReservationDto>> UpdateAsync(int id, UpdateReservationDto dto)
        {
            var validator = _serviceProvider.GetRequiredService<IValidator<UpdateReservationDto>>();
            var validation = await validator.ValidateAsync(dto);

            if (!validation.IsValid)
                return ApiResponse<ReservationDto>.ErrorResponse("Validación fallida", validation.Errors.Select(e => e.ErrorMessage).ToList());

            // ... resto del método igual
            var reservation = await _reservationRepository.GetByIdAsync(id);
            if (reservation == null)
                return ApiResponse<ReservationDto>.ErrorResponse("Reservación no encontrada");

            if (reservation.Status == ReservationStatus.Cancelled)
                return ApiResponse<ReservationDto>.ErrorResponse("No se puede modificar una reservación cancelada");

            bool hasOverlap = await _reservationRepository.HasOverlapAsync(reservation.RoomId, dto.StartTime, dto.EndTime, id);
            if (hasOverlap)
                return ApiResponse<ReservationDto>.ErrorResponse("Existe traslape de horario");

            _mapper.Map(dto, reservation);
            _reservationRepository.Update(reservation);

            var updatedDto = _mapper.Map<ReservationDto>(reservation);
            return ApiResponse<ReservationDto>.SuccessResponse(updatedDto, "Reservación actualizada exitosamente");
        }

        // GetByIdAsync y CancelAsync se mantienen igual (sin validadores)
        public async Task<ApiResponse<ReservationDto>> GetByIdAsync(int id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);
            if (reservation == null)
                return ApiResponse<ReservationDto>.ErrorResponse("Reservación no encontrada");

            var dto = _mapper.Map<ReservationDto>(reservation);
            return ApiResponse<ReservationDto>.SuccessResponse(dto);
        }

        public async Task<ApiResponse<bool>> CancelAsync(int id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);
            if (reservation == null)
                return ApiResponse<bool>.ErrorResponse("Reservación no encontrada");

            if (reservation.Status == ReservationStatus.Cancelled)
                return ApiResponse<bool>.ErrorResponse("La reservación ya está cancelada");

            reservation.Status = ReservationStatus.Cancelled;
            _reservationRepository.Update(reservation);

            return ApiResponse<bool>.SuccessResponse(true, "Reservación cancelada exitosamente");
        }
    }
}