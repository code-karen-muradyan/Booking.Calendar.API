using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Calendar.API.Application.Queries
{
    public interface ICalendarQueries
    {
        Task<Models.Dto.Apponintment> GetAppointmentAsync(int id);

        Task<IEnumerable<Models.Dto.Apponintment>> GetAppointmentsFromUserAsync(string user, string categoria, DateTime date);
    }
}
