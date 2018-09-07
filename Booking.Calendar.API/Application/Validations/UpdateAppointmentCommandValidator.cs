using Booking.Calendar.API.Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Calendar.API.Application.Validations
{
    public class UpdateAppointmentCommandValidator : AbstractValidator<UpdateAppointmentCommand>
    {
        public UpdateAppointmentCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
            RuleFor(command => command.Categoria).NotEmpty();
            RuleFor(command => command.ClassEvent).NotEmpty();
            RuleFor(command => command.From).NotEmpty();
            RuleFor(command => command.To).NotEmpty();
            RuleFor(command => command.Title).NotEmpty();
            RuleFor(command => command.StartDate).NotEmpty().Must(BeValidStartDate).WithMessage("Please specify a valid book start date");
        }

        private bool BeValidStartDate(DateTime dateTime)
        {
            return dateTime >= DateTime.UtcNow;
        }
          
    }
}
