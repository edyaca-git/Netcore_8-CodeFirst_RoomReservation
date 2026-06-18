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