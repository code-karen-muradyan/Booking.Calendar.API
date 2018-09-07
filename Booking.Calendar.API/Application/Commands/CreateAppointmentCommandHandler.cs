using Booking.Calendar.API.Infrastructure.Idempotency;
using Booking.Calendar.API.Infrastructure.Repositories;
using Booking.Calendar.API.Models.Write;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Booking.Calendar.API.Application.Commands
{
    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, bool>
    {
        private readonly ICalendatRepository _calendatRepository;
        public CreateAppointmentCommandHandler(ICalendatRepository calendatRepository)
        {
            _calendatRepository = calendatRepository;
        }

        public async Task<bool> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            
            var startDate = new DateTime(DateTime.Parse(request.Date).Year, DateTime.Parse(request.Date).Month, DateTime.Parse(request.Date).Day, int.Parse(request.StartTime.Split(':')[0]), int.Parse(request.StartTime.Split(':')[1]),0);
            var endDate = new DateTime(DateTime.Parse(request.Date).Year, DateTime.Parse(request.Date).Month, DateTime.Parse(request.Date).Day, int.Parse(request.EndTime.Split(':')[0]), int.Parse(request.EndTime.Split(':')[1]), 0);
            return await _calendatRepository.CreateAppointment(new Models.Dto.Apponintment
            {
                Categoria = request.Categoria,
                ClassEvent = request.ClassEvent,
                Description = request.Description,
                Title = request.Title,
                From = request.From,
                To = request.To,               
                StartDate = startDate,
                EndDate = endDate
            });
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
