using FluentValidation;
using RoomReservationApi.Application.DTOs;

namespace RoomReservationApi.Application.Validators
{
    public class CreateRoomValidator : AbstractValidator<CreateRoomDto>
    {
        public CreateRoomValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre de la sala es obligatorio")
                .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres");

            RuleFor(x => x.Capacity)
                .GreaterThan(0).WithMessage("La capacidad debe ser mayor a 0");
        }
    }
}