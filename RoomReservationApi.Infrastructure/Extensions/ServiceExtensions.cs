using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RoomReservationApi.Application.DTOs;
using RoomReservationApi.Application.Interfaces.Services;
using RoomReservationApi.Application.Mappings;
using RoomReservationApi.Application.Services;
using RoomReservationApi.Application.Validators;
using RoomReservationApi.Domain.Interfaces.Repositories;
using RoomReservationApi.Infrastructure.Data;
using RoomReservationApi.Infrastructure.Repositories;

namespace RoomReservationApi.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // DbContext
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
}