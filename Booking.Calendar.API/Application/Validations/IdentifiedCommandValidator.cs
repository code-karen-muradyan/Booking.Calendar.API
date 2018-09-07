using Booking.Calendar.API.Application.Commands;
using Booking.Calendar.API.Models.Write;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Calendar.API.Application.Validations
{
    public class IdentifiedCommandValidator : AbstractValidator<IdentifiedCommand<CreateAppointmentCommand, bool>>
    {
        public IdentifiedCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
        }
    }

    public class IdentifiedUpdateCommandValidator : AbstractValidator<IdentifiedCommand<UpdateAppointmentCommand, bool>>
    {
        public IdentifiedUpdateCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
        }
    }

    public class IdentifiedRemoveCommandValidator : AbstractValidator<IdentifiedCommand<RemoveAppointmentCommand, bool>>
    {
        public IdentifiedRemoveCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
        }
    }
}
