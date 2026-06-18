using FluentValidation;
using RoomReservationApi.Application.DTOs;

namespace RoomReservationApi.Application.Validators
{
    public class UpdateRoomValidator : AbstractValidator<UpdateRoomDto>
    {
        public UpdateRoomValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres");

            RuleFor(x => x.Capacity)
                .GreaterThan(0).WithMessage("La capacidad debe ser mayor a 0");
        }
    }
}