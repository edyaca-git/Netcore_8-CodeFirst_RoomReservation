using Microsoft.EntityFrameworkCore;
using RoomReservationApi.Domain.Entities;
using RoomReservationApi.Infrastructure.Data.Configurations;

namespace RoomReservationApi.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; } = null!;
        public DbSet<Reservation> Reservations { get; set; } = null!;

        public DbSet<User> Users { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aplicar todas las configuraciones
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
            modelBuilder.ApplyConfiguration(new ReservationConfiguration());

            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}