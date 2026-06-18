namespace RoomReservationApi.Application.DTOs
{
    public class CreateReservationDto
    {
        public int RoomId { get; set; }
        public string ReservedBy { get; set; } = string.Empty;
        public string Purpose { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}