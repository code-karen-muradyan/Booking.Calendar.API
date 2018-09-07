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
        public async Task<Apponintment> CreateAppointment(Apponintment apponintment)
        {
            var _eventID = Guid.NewGuid().ToString().Replace("-", string.Empty);
            var Idspec = new IDSpecification
            {
                AppointmentId = apponintment.Id,
                GoogleId = _eventID
            };
            _calendarContext.IDSpecifications.Add(Idspec);
            apponintment.SpecifiedID = Idspec;
            _calendarContext.Apponintments.Add(apponintment);
            var result = await _calendarContext.SaveEntitiesAsync();
            if (result)
                return apponintment;
            else
                return null;
        }

        public IDSpecification GetAppointmentSpecificationByID(int id)
        {
            return _calendarContext.IDSpecifications.FirstOrDefault(x => x.AppointmentId == id);
        }
 

        public Task<bool> RemoveAppointment(int id)
        {
             var appointmentToDelate = _calendarContext.Apponintments.FirstOrDefault(x => x.Id == id);
            _calendarContext.Apponintments.Remove(appointmentToDelate);
            return _calendarContext.SaveEntitiesAsync();
        }

        public async Task<Apponintment> UpdateAppointment(Apponintment apponintment)
        {
            var app = _calendarContext.Apponintments.FirstOrDefault(x => x.Id == apponintment.Id);
            app.From =  apponintment.From; 
            app.Categoria =  apponintment.Categoria; 
            app.Title =  apponintment.Title;
            app.Description = apponintment.Description;
            app.StartDate =  apponintment.StartDate; 
            app.ClassEvent =  apponintment.ClassEvent; 
            app.SpecifiedID = _calendarContext.IDSpecifications.FirstOrDefault(x => x.AppointmentId == apponintment.Id);
            if (app?.SpecifiedID == null)
                return null;
            _calendarContext.Apponintments.Update(app);
            var res = await _calendarContext.SaveEntitiesAsync();
            if (res)
                return app;
            else return null;
        }
    }
}
