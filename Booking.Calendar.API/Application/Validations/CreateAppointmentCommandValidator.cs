using Booking.Calendar.API.Models.Write;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Calendar.API.Application.Validations
{
    public class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
    {
        public CreateAppointmentCommandValidator()
        {
            RuleFor(command => command.Categoria).NotEmpty();
            RuleFor(command => command.ClassEvent).NotEmpty();
            RuleFor(command => command.From).NotEmpty();
            RuleFor(command => command.To).NotEmpty();
            RuleFor(command => command.Title).NotEmpty();
            RuleFor(command => command.Date).NotEmpty().Must(BeValidStartDate).WithMessage("Please specify a valid book start date"); 
        }

        private bool BeValidStartDate(string dateTime)
        {
            return DateTime.Parse(dateTime) >= DateTime.UtcNow;
        }

      
    }
}
