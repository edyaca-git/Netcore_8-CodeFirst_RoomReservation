using RoomReservationApi.Domain.Entities;

namespace RoomReservationApi.Domain.Interfaces.Repositories
{
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
}