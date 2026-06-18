using Microsoft.EntityFrameworkCore;
using RoomReservationApi.Domain.Entities;
using RoomReservationApi.Domain.Interfaces.Repositories;
using RoomReservationApi.Infrastructure.Data;

namespace RoomReservationApi.Infrastructure.Repositories
{
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
}