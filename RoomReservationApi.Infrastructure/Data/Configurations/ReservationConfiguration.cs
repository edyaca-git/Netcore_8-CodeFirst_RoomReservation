using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomReservationApi.Domain.Entities;

namespace RoomReservationApi.Infrastructure.Data.Configurations
{
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
}