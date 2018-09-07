using Booking.Calendar.API.Application.Commands;
using Booking.Calendar.API.Models.Write;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Calendar.API.Application.Validations
{
    public class RemoveAppointmentCommandValidator : AbstractValidator<RemoveAppointmentCommand>
    {
        public RemoveAppointmentCommandValidator()
        { 
            RuleFor(command => command.Id).NotEmpty().Must(BeValidID).WithMessage("Please specify a valid ID"); 
        }

        private bool BeValidID(int id)
        {
            return id > 0;
        }

      
    }
}
