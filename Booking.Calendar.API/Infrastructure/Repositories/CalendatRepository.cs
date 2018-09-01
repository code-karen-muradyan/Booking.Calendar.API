using Booking.Calendar.API.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Calendar.API.Infrastructure.Repositories
{
    public class CalendatRepository : ICalendatRepository
    {
        private readonly CalendarContext _calendarContext;

        public CalendatRepository(CalendarContext calendarContext)
        {
            _calendarContext = calendarContext;
        }
        public async Task<bool> CreateAppointment(Apponintment apponintment)
        {
            _calendarContext.Apponintments.Add(apponintment);
            var result = await _calendarContext.SaveEntitiesAsync();
            return true;
        }

      
    }
}
