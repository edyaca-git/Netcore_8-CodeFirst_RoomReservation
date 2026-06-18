using FluentValidation;
using RoomReservationApi.Application.DTOs;

namespace RoomReservationApi.Application.Validators
{
    public class UpdateReservationValidator : AbstractValidator<UpdateReservationDto>
    {
        public UpdateReservationValidator()
        {
            RuleFor(x => x.ReservedBy)
                .NotEmpty().WithMessage("El responsable (ReservedBy) es obligatorio");

            RuleFor(x => x.Purpose)
                .NotEmpty().WithMessage("El propósito es obligatorio");

            RuleFor(x => x.StartTime)
                .GreaterThan(DateTime.UtcNow)
                .WithMessage("La fecha y hora de inicio debe ser mayor a la fecha actual");

            RuleFor(x => x.EndTime)
                .GreaterThan(x => x.StartTime)
                .WithMessage("La fecha y hora de fin debe ser posterior a la de inicio");
        }
    }
}