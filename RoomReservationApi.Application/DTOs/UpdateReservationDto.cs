namespace RoomReservationApi.Application.DTOs
{
    public class UpdateReservationDto
    {
        public string ReservedBy { get; set; } = string.Empty;
        public string Purpose { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}