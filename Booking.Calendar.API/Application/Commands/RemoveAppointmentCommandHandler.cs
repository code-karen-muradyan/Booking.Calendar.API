using Booking.Calendar.API.Infrastructure.Idempotency;
using Booking.Calendar.API.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Booking.Calendar.API.Application.Commands
{
    public class RemoveAppointmentCommandHandler : IRequestHandler<RemoveAppointmentCommand, bool>
    {
        private readonly ICalendatRepository _calendatRepository;
        private readonly IGoogleCalendarRepository _googleCalendarRepository;
        public RemoveAppointmentCommandHandler(ICalendatRepository calendatRepository, IGoogleCalendarRepository googleCalendarRepository)
        {
            _calendatRepository = calendatRepository;
            _googleCalendarRepository = googleCalendarRepository;
        }
        public async Task<bool> Handle(RemoveAppointmentCommand request, CancellationToken cancellationToken)
        {
            var spec = _calendatRepository.GetAppointmentSpecificationByID(request.Id);
            var res = await _calendatRepository.RemoveAppointment(request.Id);
            if (res)
            {
              res =  await _googleCalendarRepository.RemoveEvent(spec.GoogleId, "primary");
            } 
            return res;
        }
    }

    public class RemoveAppointmentIdentifiedCommandHandler : IdentifiedCommandHandler<RemoveAppointmentCommand, bool>
    {
        public RemoveAppointmentIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return true;                // Ignore duplicate requests for creating order.
        }
    }
}
