using RoomReservationApi.Domain.Entities;

namespace RoomReservationApi.Domain.Interfaces.Repositories
{
    public interface IRoomRepository
    {
        Task<Room?> GetByIdAsync(int id);

        Task<IEnumerable<Room>> GetAllAsync();

        Task AddAsync(Room room);

        void Update(Room room);

        void Delete(Room room);

        Task<bool> HasActiveFutureReservationsAsync(int roomId);
    }
}