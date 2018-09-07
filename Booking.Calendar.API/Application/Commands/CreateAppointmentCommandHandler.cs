using Booking.Calendar.API.Infrastructure.Idempotency;
using Booking.Calendar.API.Infrastructure.Repositories;
using Booking.Calendar.API.Models.Write;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Booking.Calendar.API.Models.Dto;
using System.Threading.Tasks;

namespace Booking.Calendar.API.Application.Commands
{
    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, bool>
    {
        private readonly ICalendatRepository _calendatRepository;
        private readonly IGoogleCalendarRepository _googleCalendarRepository;
        public CreateAppointmentCommandHandler(ICalendatRepository calendatRepository, IGoogleCalendarRepository googleCalendarRepository)
        {
            _calendatRepository = calendatRepository;
            _googleCalendarRepository = googleCalendarRepository;
        }

        public async Task<bool> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var result = false;
            var apponintment = new Models.Dto.Apponintment
            {
                Categoria = request.Categoria,
                ClassEvent = request.ClassEvent,
                Description = request.Description,
                Title = request.Title,
                From = request.From,
                To = request.To,
                StartDate = request.StartDate
            };
            var res = await _calendatRepository.CreateAppointment(apponintment);
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

                result = await _googleCalendarRepository.InsertEvent(GoogleEventModel, "primary") != null;
            }
            return result;
        }
    }

    public class CreateAppointmentIdentifiedCommandHandler : IdentifiedCommandHandler<CreateAppointmentCommand, bool>
    {
        public CreateAppointmentIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return true;                // Ignore duplicate requests for creating order.
        }
    }
}
