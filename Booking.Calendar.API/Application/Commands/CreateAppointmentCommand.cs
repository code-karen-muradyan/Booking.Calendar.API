using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Calendar.API.Models.Write
{
    public class CreateAppointmentCommand: IRequest<bool>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Categoria { get; set; }
        public string ClassEvent { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}
