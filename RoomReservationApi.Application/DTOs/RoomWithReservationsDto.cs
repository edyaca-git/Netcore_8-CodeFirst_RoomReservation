namespace RoomReservationApi.Application.DTOs
{
    public class RoomWithReservationsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<ReservationDto> Reservations { get; set; } = new();
    }
}