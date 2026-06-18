using Microsoft.EntityFrameworkCore;
using RoomReservationApi.Domain.Entities;
using RoomReservationApi.Domain.Interfaces.Repositories;
using RoomReservationApi.Infrastructure.Data;

namespace RoomReservationApi.Infrastructure.Repositories
{
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
}