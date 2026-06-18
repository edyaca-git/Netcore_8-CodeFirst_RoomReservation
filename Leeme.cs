```
Name: RoomReservationApi
Limpieza de Código (Code Cleanup)Puedes configurar la limpieza para aplicarla en toda tu solución con un solo clic:Ve 
al menú superior y selecciona Analyze > Code Cleanup > Run Code Cleanup (Profile 1 o 2).

COMANDO DOS

# Abrir en la raíz del proyecto: C:\Projects\Git\NetDataFirst\NetCodeFirstFarm\
C:\>tree /F /A > tree.txt

C:\Projects\Git\NetDataFirst\NetCodeFirstFarm\
├── `NetCodeFirstFarm.sln`
│
├── RoomReservationApi.Api/
│   ├── Properties/
│   │   └── `launchSettings.json
│   ├── Controllers/
│   │   ├── `ReservationsController.cs` (inspected)  — class: `ReservationsController`
│   │   └── `RoomsController.cs` (inspected)         — class: `RoomsController`
│   ├── `appsettings.json`
│   ├── `Program.cs` (inspected)
│
├── RoomReservationApi.Application/
│   ├── DTOs/
│   │   ├── `CreateReservationDto.cs` (inspected)    — class: `CreateReservationDto`
│   │   ├── `CreateRoomDto.cs` (inspected)           — class: `CreateRoomDto`
│   │   ├── `ReservationDto.cs` (inspected)          — class: `ReservationDto`
│   │   ├── `RoomDto.cs` (inspected)                 — class: `RoomDto`
│   │   ├── `RoomWithReservationsDto.cs` (inspected) — class: `RoomWithReservationsDto`
│   │   ├── `UpdateReservationDto.cs` (inspected)    — class: `UpdateReservationDto`
│   │   └── `UpdateRoomDto.cs` (inspected)           — class: `UpdateRoomDto`
│   │   └── User/
│   │       ├── `CreateUserDto.cs` (inspected)       — class: `CreateUserDto`
│   │       ├── `UpdateUserDto.cs` (inspected)       — class: `UpdateUserDto`
│   │       └── `UserDto.cs` (inspected)             — class: `UserDto`
│   ├── Interfaces/
│   │   └── Services/
│   │       ├── `IReservationService.cs` (inspected) — interface: `IReservationService`
│   │       └── `IRoomService.cs` (inspected)        — interface: `IRoomService`
│   ├── Mappings/
│   │   └── `MappingProfile.cs` (inspected)          — class: `MappingProfile` (AutoMapper)
│   ├── Services/
│   │   ├── `ReservationService.cs` (inspected)      — class: `ReservationService`
│   │   └── `RoomService.cs` (inspected)             — class: `RoomService`
│   ├── Validators/
│   │   ├── `CreateReservationValidator.cs` (inspected) — class: `CreateReservationValidator`
│   │   ├── `CreateRoomValidator.cs` (inspected)        — class: `CreateRoomValidator`
│   │   ├── `UpdateReservationValidator.cs` (inspected) — class: `UpdateReservationValidator`
│   │   └── `UpdateRoomValidator.cs` (inspected)        — class: `UpdateRoomValidator`
│
├── RoomReservationApi.Domain/
│   ├── Entities/
│   │   ├── `Reservation.cs` (inspected) — class: `Reservation`
│   │   ├── `Room.cs` (inspected)        — class: `Room`
│   │   └── `User.cs` (inspected)        — class: `User`
│   ├── Enums/
│   │   └── `ReservationStatus.cs` (inspected) — static class: `ReservationStatus`
│   ├── Exceptions/
│   │   ├── `BusinessException.cs` (inspected) — internal class: `BusinessException`
│   │   └── `NotFoundException.cs` (inspected) — internal class: `NotFoundException`
│   ├── Interfaces/
│   │   └── Repositories/
│   │       ├── `IReservationRepository.cs` (inspected) — interface: `IReservationRepository`
│   │       └── `IRoomRepository.cs` (inspected)        — interface: `IRoomRepository`
│
├── RoomReservationApi.Infrastructure/
│   ├── Data/
│   │   ├── `ApplicationDbContext.cs` (inspected) — class: `ApplicationDbContext`
│   │   └── Configurations/
│   │       ├── `ReservationConfiguration.cs` (inspected) — class: `ReservationConfiguration` (IEntityTypeConfiguration<Reservation>)
│   │       ├── `RoomConfiguration.cs` (inspected)        — class: `RoomConfiguration` (IEntityTypeConfiguration<Room>)
│   │       └── `UserConfiguration.cs` (inspected)        — class: `UserConfiguration` (IEntityTypeConfiguration<User>)
│   ├── Extensions/
│   │   └── `ServiceExtensions.cs` (inspected) — static class: `ServiceExtensions` (AddInfrastructure extension)
│   ├── Migrations/
│   │   ├── `20260613212843_InitialCreate.cs` (inspected)           — partial class: `InitialCreate : Migration`
│   │   ├── `20260616152336_AddUserEntity.cs` (inspected)           — partial class: `AddUserEntity : Migration`
│   │   └── `ApplicationDbContextModelSnapshot.cs` (inspected)      — class: `ApplicationDbContextModelSnapshot : ModelSnapshot`
│   ├── Repositories/
│   │   ├── `ReservationRepository.cs` (inspected) — class: `ReservationRepository`
│   │   └── `RoomRepository.cs` (inspected)        — class: `RoomRepository`
│
└── RoomReservationApi.Shared/
    ├── Common/
    │   └── `PagedResult.cs` (inspected) — class: `PagedResult<T>`
    ├── DTOs/              (vacío en el árbol proporcionado)
    ├── Entities/
    │   └── `ApiResponse.cs` (inspected) — class: `ApiResponse<T>`


PARTE 1: CREACIÓN DE LA ESTRUCTURA DE PROYECTOS (Onion Architecture separada)
Paso 1.1: Crear la Solution en Visual Studio 2022

Abre Visual Studio 2022.
Selecciona "Create a new project".
Busca y selecciona "Blank Solution".
Name: RoomReservationApi
Elige una ubicación (ej: C:\Projects\RoomReservationApi).
Click "Create".

Paso 1.2: Crear proyecto Domain

Click derecho en la solution → Add → New Project.
Busca "Class Library" → .NET 8.0.
Name: RoomReservationApi.Domain
Location: dentro de la carpeta de la solution.
Click Create.
Elimina el archivo Class1.cs.

Paso 1.3: Crear proyecto Application

Click derecho en la solution → Add → New Project.
Class Library → .NET 8.0.
Name: RoomReservationApi.Application
Elimina Class1.cs.

Paso 1.4: Crear proyecto Infrastructure

Click derecho en la solution → Add → New Project.
Class Library → .NET 8.0.
Name: RoomReservationApi.Infrastructure
Elimina Class1.cs.

Paso 1.5: Crear proyecto API (Presentation)

Click derecho en la solution → Add → New Project.
Busca "ASP.NET Core Web API" → .NET 8.0.
Name: RoomReservationApi.Api
Desmarca "Use controllers" si aparece (usaremos controllers pero configuraremos después).
Click Create.

Paso 1.6: Crear proyecto Shared (opcional pero recomendado)

Class Library → .NET 8.0.
Name: RoomReservationApi.Shared
Elimina Class1.cs.

Paso 1.7: Agregar referencias entre proyectos (Onion flow)

RoomReservationApi.Api referencia a: Application, Infrastructure, Shared
RoomReservationApi.Application referencia a: Domain, Shared
RoomReservationApi.Infrastructure referencia a: Domain, Application, Shared
RoomReservationApi.Domain NO referencia a nada más (solo Shared si es necesario).

PARTE 2: INSTALACIÓN DE PAQUETES NuGet
Abre Package Manager Console (Tools → NuGet Package Manager → Package Manager Console).

En proyecto Infrastructure:
Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 8.0.24
Install-Package Microsoft.EntityFrameworkCore.Tools -Version 8.0.24
Install-Package Microsoft.EntityFrameworkCore.Design -Version 8.0.24

En proyecto Application:
Install-Package FluentValidation -Version 12.1.1
Install-Package FluentValidation.DependencyInjectionExtensions -Version 12.1.1
Install-Package AutoMapper.Extensions.Microsoft.DependencyInjection -Version 12.0.0
Install-Package Microsoft.Extensions.DependencyInjection.Abstractions -Version 8.0.0


En proyecto Api:
PowerShellInstall-Package Swashbuckle.AspNetCore
Install-Package Microsoft.EntityFrameworkCore.Design

Install-Package Swashbuckle.AspNetCore -Version 6.6.2
Install-Package Microsoft.EntityFrameworkCore.Design -Version 8.0.24
Install-Package Microsoft.AspNetCore.Mvc.NewtonsoftJson -Version 8.0.14 -ProjectName RoomReservationApi.Api


PARTE 3: CREACIÓN DE ENTIDADES Y DDD (Domain Layer)
Paso 3.1: En RoomReservationApi.Domain crear carpetas
Entities
Enums
Exceptions
Interfaces (dentro: Repositories)

Paso 3.2: Crear Room.cs
C#// RoomReservationApi.Domain/Entities/Room.cs
using System.ComponentModel.DataAnnotations;

namespace RoomReservationApi.Domain.Entities;

public class Room
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int Capacity { get; set; }

    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}

Paso 3.3: Crear Reservation.cs
C#// RoomReservationApi.Domain/Entities/Reservation.cs
namespace RoomReservationApi.Domain.Entities;

public class Reservation
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public string ReservedBy { get; set; } = string.Empty;
    public string Purpose { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Status { get; set; } = "Pending"; // Pending, Confirmed, Cancelled
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Room Room { get; set; } = null!;
}

Estructura de carpetas en RoomReservationApi.Shared

En el proyecto RoomReservationApi.Shared crea las siguientes carpetas:
Entities
DTOs
Common (para paginación y helpers)

namespace RoomReservationApi.Shared.Entities;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string? Message { get; set; }
    public List<string>? Errors { get; set; }

    // Constructor vacío para deserialización
    public ApiResponse() { }

    public static ApiResponse<T> SuccessResponse(T data, string message = "Operación realizada con éxito")
    {
        return new ApiResponse<T>
        {
            Success = true,
            Data = data,
            Message = message
        };
    }

    public static ApiResponse<T> ErrorResponse(string message, List<string>? errors = null)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Message = message,
            Errors = errors ?? new List<string>()
        };
    }
}

Crear PagedResult.cs (para paginación)

Click derecho en carpeta Common → Add → Class
Nombre: PagedResult.cs

namespace RoomReservationApi.Shared.Common;

public class PagedResult<T>
{
    public List<T> Data { get; set; } = new();
    public int Total { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling(Total / (double)PageSize);

    public PagedResult() { }

    public PagedResult(List<T> data, int total, int page, int pageSize)
    {
        Data = data;
        Total = total;
        Page = page;
        PageSize = pageSize;
    }
}

CREACIÓN DE DTOS EN LA CAPA APPLICATION
Paso 5.1: Estructura de carpetas en RoomReservationApi.Application

Crea las siguientes carpetas:
DTOs
Validators
Interfaces (para Services)
Services
Mappings


Paso 5.2: Crear DTOs para Room
Crear RoomDto.cs

En carpeta DTOs → Add → Class → RoomDto.cs

C#
namespace RoomReservationApi.Application.DTOs;

public class RoomDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}

Crear CreateRoomDto.cs
namespace RoomReservationApi.Application.DTOs;

public class CreateRoomDto
{
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }
}


Crear UpdateRoomDto.cs y UpdateReservationDto.cs (en Application/DTOs)
UpdateRoomDto.cs
namespace RoomReservationApi.Application.DTOs;

public class UpdateRoomDto
{
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public bool IsActive { get; set; }
}

UpdateReservationDto.cs
namespace RoomReservationApi.Application.DTOs;

public class UpdateReservationDto
{
    public string ReservedBy { get; set; } = string.Empty;
    public string Purpose { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}

ReservationDto.cs (en Application/DTOs)

namespace RoomReservationApi.Application.DTOs;

public class ReservationDto
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public string ReservedBy { get; set; } = string.Empty;
    public string Purpose { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

CreateReservationDto.cs

namespace RoomReservationApi.Application.DTOs;

public class CreateReservationDto
{
    public int RoomId { get; set; }
    public string ReservedBy { get; set; } = string.Empty;
    public string Purpose { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}

Crear IRoomService.cs y IReservationService.cs completos (Application/Interfaces/Services/)
IRoomService.cs (completo)
using RoomReservationApi.Application.DTOs;
using RoomReservationApi.Shared.Common;
using RoomReservationApi.Shared.Entities;

namespace RoomReservationApi.Application.Interfaces.Services;

public interface IRoomService
{
    Task<ApiResponse<RoomDto>> CreateAsync(CreateRoomDto dto);
    Task<ApiResponse<PagedResult<RoomDto>>> GetAllAsync(int page = 1, int pageSize = 10);
    Task<ApiResponse<RoomDto>> GetByIdAsync(int id);
    Task<ApiResponse<RoomDto>> UpdateAsync(int id, UpdateRoomDto dto);
    Task<ApiResponse<bool>> DeleteAsync(int id);
}

IReservationService.cs (completo)

using RoomReservationApi.Application.DTOs;
using RoomReservationApi.Shared.Common;
using RoomReservationApi.Shared.Entities;

namespace RoomReservationApi.Application.Interfaces.Services;

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

Crear carpeta Services en Application

Crea carpeta Services al mismo nivel que Interfaces.

Paso 7.3: Crear RoomService.cs

Click derecho en carpeta Services → Add → Class → RoomService.cs

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

 
Crear IReservationRepository.cs (Domain Layer)
Crear la interfaz IReservationRepository
RoomReservationApi.Domain.Interfaces → Repositories.
IReservationRepository.cs
using RoomReservationApi.Domain.Entities;

namespace RoomReservationApi.Domain.Interfaces.Repositories;

public interface IReservationRepository
{
    Task<Reservation?> GetByIdAsync(int id);
    Task<IEnumerable<Reservation>> GetAllAsync();
    Task<IEnumerable<Reservation>> GetAllFilteredAsync(
        int? roomId = null,
        string? status = null,
        string? reservedBy = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        string? sortBy = "startTime",
        string? sortOrder = "asc");
    Task AddAsync(Reservation reservation);
    void Update(Reservation reservation);
    void Delete(Reservation reservation);
    
    /// <summary>
    /// Verifica si existe traslape de horario con reservaciones activas (no Cancelled)
    /// </summary>
    Task<bool> HasOverlapAsync(int roomId, DateTime startTime, DateTime endTime, int? excludeReservationId = null);

    /// <summary>
    /// Verifica si hay reservaciones futuras activas en una sala
    /// </summary>
    Task<bool> HasActiveFutureReservationsAsync(int roomId);
}


Crear ReservationService.cs COMPLETO (con todas las reglas de negocio)
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
 
MAPPINGS (AutoMapper)
Crear MappingProfile.cs
En carpeta Mappings del proyecto Application → Add → Class → MappingProfile.cs
using AutoMapper;
using RoomReservationApi.Application.DTOs;
using RoomReservationApi.Domain.Entities;

namespace RoomReservationApi.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateRoomDto, Room>();
        CreateMap<Room, RoomDto>();
        CreateMap<UpdateRoomDto, Room>().ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<CreateReservationDto, Reservation>();
        CreateMap<Reservation, ReservationDto>();
        CreateMap<UpdateReservationDto, Reservation>().ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}



VALIDATORS COMPLETOS (Application Layer)
CreateRoomValidator.cs (Application/Validators/)

using FluentValidation;
using RoomReservationApi.Application.DTOs;

namespace RoomReservationApi.Application.Validators;

public class CreateRoomValidator : AbstractValidator<CreateRoomDto>
{
    public CreateRoomValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre de la sala es obligatorio")
            .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres");

        RuleFor(x => x.Capacity)
            .GreaterThan(0).WithMessage("La capacidad debe ser mayor a 0");
    }
}


Paso 8.1: Crear UpdateRoomValidator.cs (Application/Validators/)

using FluentValidation;
using RoomReservationApi.Application.DTOs;

namespace RoomReservationApi.Application.Validators;

public class UpdateRoomValidator : AbstractValidator<UpdateRoomDto>
{
    public UpdateRoomValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre es obligatorio")
            .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres");

        RuleFor(x => x.Capacity)
            .GreaterThan(0).WithMessage("La capacidad debe ser mayor a 0");
    }
}

CreateReservationValidator.cs

using FluentValidation;
using RoomReservationApi.Application.DTOs;

namespace RoomReservationApi.Application.Validators;

public class CreateReservationValidator : AbstractValidator<CreateReservationDto>
{
    public CreateReservationValidator()
    {
        RuleFor(x => x.RoomId)
            .GreaterThan(0).WithMessage("El ID de la sala es obligatorio");

        RuleFor(x => x.ReservedBy)
            .NotEmpty().WithMessage("El responsable (ReservedBy) es obligatorio");

        RuleFor(x => x.Purpose)
            .NotEmpty().WithMessage("El propósito es obligatorio");

        RuleFor(x => x.StartTime)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("La fecha y hora de inicio debe ser mayor a la fecha actual");

        RuleFor(x => x.EndTime)
            .GreaterThan(x => x.StartTime)
            .WithMessage("La fecha y hora de fin debe ser posterior a la de inicio");
    }
}


Paso 8.2: Crear UpdateReservationValidator.cs
using FluentValidation;
using RoomReservationApi.Application.DTOs;

namespace RoomReservationApi.Application.Validators;

public class UpdateReservationValidator : AbstractValidator<UpdateReservationDto>
{
    public UpdateReservationValidator()
    {
        RuleFor(x => x.ReservedBy)
            .NotEmpty().WithMessage("El responsable (ReservedBy) es obligatorio");

        RuleFor(x => x.Purpose)
            .NotEmpty().WithMessage("El propósito es obligatorio");

        RuleFor(x => x.StartTime)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("La fecha y hora de inicio debe ser mayor a la fecha actual");

        RuleFor(x => x.EndTime)
            .GreaterThan(x => x.StartTime)
            .WithMessage("La fecha y hora de fin debe ser posterior a la de inicio");
    }
}

PARTE 9: REPOSITORIOS EN INFRASTRUCTURE (Implementaciones completas)
Paso 9.1: Crear RoomRepository.cs (Infrastructure/Repositories/)

using Microsoft.EntityFrameworkCore;
using RoomReservationApi.Domain.Entities;
using RoomReservationApi.Domain.Interfaces.Repositories;
using RoomReservationApi.Infrastructure.Data;

namespace RoomReservationApi.Infrastructure.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly ApplicationDbContext _context;

    public RoomRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Room?> GetByIdAsync(int id)
    {
        return await _context.Rooms
            .Include(r => r.Reservations)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<Room>> GetAllAsync()
    {
        return await _context.Rooms
            .Include(r => r.Reservations)
            .ToListAsync();
    }

    public async Task AddAsync(Room room)
    {
        await _context.Rooms.AddAsync(room);
        await _context.SaveChangesAsync();
    }

    public void Update(Room room)
    {
        _context.Rooms.Update(room);
        _context.SaveChanges();
    }

    public void Delete(Room room)
    {
        _context.Rooms.Remove(room);
        _context.SaveChanges();
    }

    public async Task<bool> HasActiveFutureReservationsAsync(int roomId)
    {
        return await _context.Reservations.AnyAsync(r =>
            r.RoomId == roomId &&
            r.Status != "Cancelled" &&
            r.StartTime > DateTime.UtcNow);
    }
}

Paso 9.2: Crear ReservationRepository.cs (Infrastructure/Repositories/)

using Microsoft.EntityFrameworkCore;
using RoomReservationApi.Domain.Entities;
using RoomReservationApi.Domain.Interfaces.Repositories;
using RoomReservationApi.Infrastructure.Data;

namespace RoomReservationApi.Infrastructure.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly ApplicationDbContext _context;

    public ReservationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Reservation?> GetByIdAsync(int id)
    {
        return await _context.Reservations
            .Include(r => r.Room)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<Reservation>> GetAllAsync()
    {
        return await _context.Reservations
            .Include(r => r.Room)
            .ToListAsync();
    }

    public async Task<IEnumerable<Reservation>> GetAllFilteredAsync(
        int? roomId = null, string? status = null, string? reservedBy = null,
        DateTime? startDate = null, DateTime? endDate = null,
        string? sortBy = "startTime", string? sortOrder = "asc")
    {
        var query = _context.Reservations
            .Include(r => r.Room)
            .AsQueryable();

        if (roomId.HasValue) query = query.Where(r => r.RoomId == roomId.Value);
        if (!string.IsNullOrEmpty(status)) query = query.Where(r => r.Status == status);
        if (!string.IsNullOrEmpty(reservedBy)) query = query.Where(r => r.ReservedBy.Contains(reservedBy));

        if (startDate.HasValue) query = query.Where(r => r.StartTime >= startDate.Value);
        if (endDate.HasValue) query = query.Where(r => r.EndTime <= endDate.Value);

        query = sortBy?.ToLower() switch
        {
            "endtime" => sortOrder?.ToLower() == "desc" 
                ? query.OrderByDescending(r => r.EndTime) 
                : query.OrderBy(r => r.EndTime),
            "createdat" => sortOrder?.ToLower() == "desc" 
                ? query.OrderByDescending(r => r.CreatedAt) 
                : query.OrderBy(r => r.CreatedAt),
            _ => sortOrder?.ToLower() == "desc" 
                ? query.OrderByDescending(r => r.StartTime) 
                : query.OrderBy(r => r.StartTime)
        };

        return await query.ToListAsync();
    }

    public async Task AddAsync(Reservation reservation)
    {
        await _context.Reservations.AddAsync(reservation);
        await _context.SaveChangesAsync();
    }

    public void Update(Reservation reservation)
    {
        _context.Reservations.Update(reservation);
        _context.SaveChanges();
    }

    public void Delete(Reservation reservation)
    {
        _context.Reservations.Remove(reservation);
        _context.SaveChanges();
    }

    public async Task<bool> HasOverlapAsync(int roomId, DateTime startTime, DateTime endTime, int? excludeReservationId = null)
    {
        return await _context.Reservations.AnyAsync(r =>
            r.RoomId == roomId &&
            r.Status != "Cancelled" &&
            r.Id != excludeReservationId &&
            r.StartTime < endTime &&
            r.EndTime > startTime);
    }

    public async Task<bool> HasActiveFutureReservationsAsync(int roomId)
    {
        return await _context.Reservations.AnyAsync(r =>
            r.RoomId == roomId &&
            r.Status != "Cancelled" &&
            r.StartTime > DateTime.UtcNow);
    }
}

Crear las clases de configuración
En el proyecto RoomReservationApi.Infrastructure, crea la siguiente carpeta:
Data/Configurations
1. Crear RoomConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomReservationApi.Domain.Entities;

namespace RoomReservationApi.Infrastructure.Data.Configurations;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.ToTable("Rooms");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(r => r.Capacity)
               .IsRequired();

        builder.Property(r => r.IsActive)
               .IsRequired()
               .HasDefaultValue(true);

        builder.Property(r => r.CreatedAt)
               .IsRequired()
               .HasDefaultValueSql("GETUTCDATE()");
    }
}

Crear ReservationConfiguration.cs

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomReservationApi.Domain.Entities;

namespace RoomReservationApi.Infrastructure.Data.Configurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.ToTable("Reservations");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.RoomId).IsRequired();
        builder.Property(r => r.ReservedBy).IsRequired().HasMaxLength(200);
        builder.Property(r => r.Purpose).IsRequired().HasMaxLength(500);
        builder.Property(r => r.StartTime).IsRequired();
        builder.Property(r => r.EndTime).IsRequired();
        builder.Property(r => r.Status).IsRequired().HasMaxLength(20);
        builder.Property(r => r.CreatedAt).IsRequired().HasDefaultValueSql("GETUTCDATE()");

        // Relación
        builder.HasOne(r => r.Room)
               .WithMany(r => r.Reservations)
               .HasForeignKey(r => r.RoomId)
               .OnDelete(DeleteBehavior.Restrict);

        // ==================== ÍNDICES COMPUESTOS ====================

        // Índice crítico para validación de traslapes
        builder.HasIndex(r => new { r.RoomId, r.StartTime, r.EndTime })
               .HasDatabaseName("IX_Reservations_RoomId_Start_End");

        // Índice para listados y filtros
        builder.HasIndex(r => new { r.RoomId, r.Status, r.StartTime })
               .HasDatabaseName("IX_Reservations_RoomId_Status_Start");

        // Índice para búsquedas por rango de fechas
        builder.HasIndex(r => new { r.StartTime, r.EndTime })
               .HasDatabaseName("IX_Reservations_Start_End");

        // Índice para filtro por usuario
        builder.HasIndex(r => r.ReservedBy)
               .HasDatabaseName("IX_Reservations_ReservedBy");
    }
}


PARTE 10: DbContext y Extensiones DI
Paso 10.1: ApplicationDbContext.cs (Infrastructure/Data/)
using Microsoft.EntityFrameworkCore;
using RoomReservationApi.Domain.Entities;
using RoomReservationApi.Infrastructure.Data.Configurations;

namespace RoomReservationApi.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Room> Rooms { get; set; } = null!;
    public DbSet<Reservation> Reservations { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Aplicar todas las configuraciones
        modelBuilder.ApplyConfiguration(new RoomConfiguration());
        modelBuilder.ApplyConfiguration(new ReservationConfiguration());
    }
}


Paso 10.2: Infrastructure/Extensions/ServiceExtensions.cs

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RoomReservationApi.Application.Interfaces.Services;
using RoomReservationApi.Application.Mappings;
using RoomReservationApi.Application.Services;
using RoomReservationApi.Application.Validators;
using RoomReservationApi.Domain.Interfaces.Repositories;
using RoomReservationApi.Infrastructure.Data;
using RoomReservationApi.Infrastructure.Repositories;

namespace RoomReservationApi.Infrastructure.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Repositories
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();

        // Services
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<IReservationService, ReservationService>();

        // Validators
        services.AddScoped<IValidator<CreateRoomDto>, CreateRoomValidator>();
        services.AddScoped<IValidator<UpdateRoomDto>, UpdateRoomValidator>();
        services.AddScoped<IValidator<CreateReservationDto>, CreateReservationValidator>();
        services.AddScoped<IValidator<UpdateReservationDto>, UpdateReservationValidator>();

        // AutoMapper
        services.AddAutoMapper(typeof(MappingProfile));

        return services;
    }
}

aso 11.1: Configurar appsettings.json (en RoomReservationApi.Api)
Abre el archivo appsettings.json y reemplaza todo su contenido con:

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=RoomReservationDb;Trusted_Connection=True;MultipleActiveResultSets=true;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}


Paso 11.2: Configurar Program.cs (RoomReservationApi.Api/Program.cs)
Reemplaza todo el contenido del archivo Program.cs con lo siguiente:

using Microsoft.EntityFrameworkCore;
using RoomReservationApi.Infrastructure.Extensions;
using RoomReservationApi.Application.Mappings;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;   // Desactiva validación automática (importante)
    })
    .AddJsonOptions(options =>
    {
        // Configuración moderna con System.Text.Json (recomendado en .NET 8)
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo 
    { 
        Title = "Room Reservation API", 
        Version = "v1",
        Description = "API para gestión de salas y reservaciones"
    });
});

// Registrar todos los servicios de Infrastructure
builder.Services.AddInfrastructure(builder.Configuration);

// AutoMapper ya está registrado en ServiceExtensions

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();



Paso 11.3: Crear RoomsController.cs (Api/Controllers/)

Crea la carpeta Controllers en el proyecto Api si no existe.
Click derecho → Add → Controller → API Controller - Empty
Nombre: RoomsController.cs

using Microsoft.AspNetCore.Mvc;
using RoomReservationApi.Application.DTOs;
using RoomReservationApi.Application.Interfaces.Services;
using RoomReservationApi.Shared.Entities;

namespace RoomReservationApi.Api.Controllers;

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

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
Paso 11.4: Crear ReservationsController.cs
Nombre: ReservationsController.cs
C#

using Microsoft.AspNetCore.Mvc;
using RoomReservationApi.Application.DTOs;
using RoomReservationApi.Application.Interfaces.Services;
using RoomReservationApi.Shared.Entities;

namespace RoomReservationApi.Api.Controllers;

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

/////////////////////////////////////////////////////////////////////////////

RoomReservationApi.Api/Properties/launchSettings.json
Reemplaza TODO su contenido con esta versión limpia y profesional:
JSON
{
  "$schema": "http://json.schemastore.org/launchsettings.json",
  "iisSettings": {
    "windowsAuthentication": false,
    "anonymousAuthentication": true,
    "iisExpress": {
      "applicationUrl": "http://localhost:62357",
      "sslPort": 44383
    }
  },
  "profiles": {
    "RoomReservationApi": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "launchUrl": "swagger",
      "applicationUrl": "https://localhost:7257;http://localhost:5089",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "IIS Express": {
      "commandName": "IISExpress",
      "launchBrowser": true,
      "launchUrl": "swagger",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}

/////////////////////////////////////////////////////////////////////////////
CREAR EXCEPCIONES GLOBALES
crear carpeta Middleware y clase GlobalExceptionMiddleware.cs
RoomReservationApi.Api.Middleware.GlobalExceptionMiddleware.cs
using RoomReservationApi.Domain.Exceptions;
using RoomReservationApi.Shared.Entities;
using System.Net;
using System.Text.Json;

namespace RoomReservationApi.Api.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = new ApiResponse<object>();

            switch (exception)
            {
                case FluentValidation.ValidationException validationEx:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = ApiResponse<object>.ErrorResponse(
                        "Error de validación",
                        validationEx.Errors.Select(e => e.ErrorMessage).ToList());
                    break;

                case BusinessExceptions businessEx:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = ApiResponse<object>.ErrorResponse(businessEx.Message);
                    break;

                case NotFoundException notFoundEx:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    response = ApiResponse<object>.ErrorResponse(notFoundEx.Message);
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response = ApiResponse<object>.ErrorResponse(
                        "Ocurrió un error interno en el servidor. Por favor intente más tarde.");
#if DEBUG
                    response.Errors = new List<string> { exception.Message };
#endif
                    break;
            }

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            await context.Response.WriteAsJsonAsync(response, options);
        }
    }
}

agregar en Program.cs

app.UseMiddleware<GlobalExceptionMiddleware>(); //esta linea

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();


En la capa de Dominio crear carpeta Exceptions y clase NotFoundException.cs
RoomReservationApi.Domain.Exceptions.NotFoundException.cs
namespace RoomReservationApi.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
        { }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}


En la capa de Dominio crear carpeta Exceptions y clase BusinessExceptions.cs
RoomReservationApi.Domain.Exceptions.BusinessExceptions.cs
namespace RoomReservationApi.Domain.Exceptions
{
    public class BusinessExceptions : Exception
    {
        public BusinessExceptions()
        { }

        public BusinessExceptions(string message) : base(message)
        {
        }

        public BusinessExceptions(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
/////////////////////////////////////////////////////////////////////////////

Nueva funcionalidad:
GET /api/rooms/with-reservations
Este endpoint devolverá todas las salas, y dentro de cada una una lista de reservaciones que puedas filtrar por status.

PASO 1: Agregar método en IRoomService.cs
Abre RoomReservationApi.Application/Interfaces/Services/IRoomService.cs y agrega este método
Task<ApiResponse<PagedResult<RoomWithReservationsDto>>> GetAllWithReservationsAsync(
    int page = 1, 
    int pageSize = 10,
    string? status = null);   // null = todas, o "Pending", "Confirmed", "Cancelled"
	
PASO 2: Crear DTO para respuesta (Application/DTOs)
Crea un nuevo archivo RoomWithReservationsDto.cs:
namespace RoomReservationApi.Application.DTOs;

public class RoomWithReservationsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }

    public List<ReservationDto> Reservations { get; set; } = new();
}

PASO 3: Actualizar RoomService.cs
Agrega este método al final de la clase RoomService:

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
PASO 4: Agregar endpoint en RoomsController.cs
Agrega este método en RoomsController.cs:

[HttpGet("with-reservations")]
public async Task<IActionResult> GetAllWithReservations(
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10,
    [FromQuery] string? status = null)   // null = todas, o Pending/Confirmed/Cancelled
{
    var response = await _roomService.GetAllWithReservationsAsync(page, pageSize, status);
    return Ok(response);
}

/////////////////////////////////////////////////////////////////////////////////////////////////

Crear y Aplicar Migraciones (Aquí se crea la BD)

Abre Package Manager Console.
En la parte superior, cambia el Default project a RoomReservationApi.Infrastructure.
Ejecuta los siguientes comandos en orden:

PM>Add-Migration InitialCreate -Project RoomReservationApi.Infrastructure -StartupProject RoomReservationApi.Api

Este comando crea la carpeta Migrations dentro del proyecto Infrastructure y genera los archivos de migración.

PM>Update-Database -Project RoomReservationApi.Infrastructure -StartupProject RoomReservationApi.Api

Este es el momento exacto donde se crea la base de datos.

Si todo va bien, verás un mensaje similar a:
Applying migration 'InitialCreate'.
Done.


Eliminar y Recrear todo con EF Core (La mejor para este proyecto)
Esta es la forma profesional cuando estás desarrollando:

En Package Manager Console ejecuta:

PM>Drop-Database -Project RoomReservationApi.Infrastructure -StartupProject RoomReservationApi.Api 

Elimina la carpeta Migrations que está dentro del proyecto RoomReservationApi.Infrastructure.
Vuelve a crear la migración:

/////////////////////////////////////////////////////////////////////////////////////////////////

AGREGAR NUEVAS TABLAS
Orden	Paso										Qué haces
1		✅Crear entity User.cs						Domain.Entities/User.cs
Crear Domain.Entities/User.cs:
namespace RoomReservationApi.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }        
        public string Email { get; set; } = string.Empty;        
        public string PasswordHash { get; set; } = string.Empty;        
        public string Name { get; set; } = string.Empty;        
        public string? PhoneNumber { get; set; }        
        public bool IsActive { get; set; } = true;        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;        
        public DateTime? UpdatedAt { get; set; }        
        // Relación
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
2		✅Crear DTO UserDto.cs						Application.DTOs/UserDto.cs
Creo una carperta DTOs.User y ahi creo los Dto's que voy a uasar
namespace RoomReservationApi.Application.DTOs;
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }

namespace RoomReservationApi.Application.DTOs;    
    public class CreateUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string? PhoneNumber { get; set; }
    }

namespace RoomReservationApi.Application.DTOs;    
    public class UpdateUserDto
    {
        public string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }
3		✅Crear configuración UserConfiguration.cs	Infrastructure.Data.Configurations/UserConfiguration.cs
Crear Infrastructure.Data.Configurations/UserConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomReservationApi.Domain.Entities;

namespace RoomReservationApi.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            // ←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←
            // Esquema: Security, Tabla: User
            // ←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←
            entity.ToTable("User", "Security");
            
            entity.HasKey(u => u.Id);
            
            // ←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←
            // Campos de User (sin relación con Reservation)
            // ←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←
            entity.Property(u => u.Email)
                  .IsRequired()
                  .HasMaxLength(256);
            
            // ←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←
            // Índice único para Email (obligatorio para login)
            // ←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←
            entity.HasIndex(u => u.Email)
                  .IsUnique();
            
            entity.Property(u => u.PasswordHash)
                  .IsRequired()
                  .HasMaxLength(256);
            
            entity.Property(u => u.Name)
                  .IsRequired()
                  .HasMaxLength(100);
            
            entity.Property(u => u.PhoneNumber)
                  .HasMaxLength(20);
            
            entity.Property(u => u.IsActive)
                  .IsRequired()
                  .HasDefaultValue(true);
            
            entity.Property(u => u.CreatedAt)
                  .IsRequired()
                  .HasDefaultValueSql("GETUTCDATE()");
            
            entity.Property(u => u.UpdatedAt)
                  .IsRequired(false);
            
            // ←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←
            // NO hay relación con Reservation (tu tabla no tiene UserId)
            // ←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←←
        }
    }
}
4		✅REGISTRAR configuración en DbContext		ApplicationDbContext.cs
Infrastructure.Data/ApplicationDbContext.cs

        public DbSet<User> Users { get; set; } = null!; 
		-----
		-----
		
            modelBuilder.ApplyConfiguration(new UserConfiguration());
			
5       ✅ REGISTRAR servicio en Infrastructure.Extensions.ServiceExtensions.cs (¡SI CREAS UserService!):
            // Services
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IReservationService, ReservationService>();
			
			Services.AddScoped<IUserService, UserService>();
			
6		✅ Ejecutar migraciones	Add-Migration + Update-Database
1. Crea migración con NOMBRE NUEVO
PM>Add-Migration AddUserEntity -Project RoomReservationApi.Infrastructure -StartupProject RoomReservationApi.Api
 

Este comando crea la carpeta Migrations dentro del proyecto Infrastructure y genera los archivos de migración.

PM>Update-Database -Project RoomReservationApi.Infrastructure -StartupProject RoomReservationApi.Api


```