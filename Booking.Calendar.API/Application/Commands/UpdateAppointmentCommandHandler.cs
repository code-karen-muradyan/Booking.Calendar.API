using Booking.Calendar.API.Infrastructure.Idempotency;
using Booking.Calendar.API.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Booking.Calendar.API.Models.Dto;
using System.Threading.Tasks;

namespace Booking.Calendar.API.Application.Commands
{
    public class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand, bool>
    {
        private readonly IGoogleCalendarRepository _googleCalendarRepository;
        private readonly ICalendatRepository _calendarRepository;
        public UpdateAppointmentCommandHandler(IGoogleCalendarRepository googleCalendarRepository, ICalendatRepository calendatRepository)
        {
            _googleCalendarRepository = googleCalendarRepository;
            _calendarRepository = calendatRepository;
        }
        public async Task<bool> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var result = false;
            var appointmentToUpdate = new Apponintment()
            {
                Categoria = request.Categoria,
                ClassEvent = request.ClassEvent,
                Description = request.Description,
                From = request.From,
                StartDate = request.StartDate,
                Id = request.Id,
                Title = request.Title,
                To = request.To
            };
            var res  = await  _calendarRepository.UpdateAppointment(appointmentToUpdate);
            if (res != null)
            {
                var GoogleEventModel = new GoogleEventModel()
                {
                    EventId = res.SpecifiedID.GoogleId,
                    Description = res.Description,
                    Start = res.StartDate,
                    End = res.StartDate.AddMinutes(15),
                    Summary = res.Title, 
                };
                result = await _googleCalendarRepository.UpdateEvent(GoogleEventModel, "primary") != null;
            }  
            return result;
        }

        public class UpdateAppointmentIdentifiedCommandHandler : IdentifiedCommandHandler<UpdateAppointmentCommand, bool>
        {
            public UpdateAppointmentIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
            {
            }

            protected override bool CreateResultForDuplicateRequest()
            {
                return true;                // Ignore duplicate requests for creating order.
            }
        }
    }
}
