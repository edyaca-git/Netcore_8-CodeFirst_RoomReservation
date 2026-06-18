namespace RoomReservationApi.Application.DTOs.User
{
    public class UpdateUserDto
    {
        public string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }
}