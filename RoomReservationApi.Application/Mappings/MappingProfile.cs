using AutoMapper;
using RoomReservationApi.Application.DTOs;
using RoomReservationApi.Domain.Entities;

namespace RoomReservationApi.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateRoomDto, Room>();
            CreateMap<Room, RoomDto>();
            CreateMap<UpdateRoomDto, Room>().ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<CreateReservationDto, Reservation>();
            CreateMap<Reservation, ReservationDto>();
            CreateMap<UpdateReservationDto, Reservation>().ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}