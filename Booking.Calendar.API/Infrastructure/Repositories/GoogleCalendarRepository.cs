using Booking.Calendar.API.Application.Extentions;
using Booking.Calendar.API.Models.Dto;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TimeZoneConverter;

namespace Booking.Calendar.API.Infrastructure.Repositories
{
    public class GoogleCalendarRepository : IGoogleCalendarRepository
    {

        private readonly GoogleCalendarUser _user;
        private readonly IConfiguration _configuration;

        public GoogleCalendarRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            var conf = configuration.GetSection("GoogleCalendarUser");
            _user = new GoogleCalendarUser()
            {
                CredentialJSON = conf.GetValue<string>("CredentialJSON"),
                Email = conf.GetValue<string>("Email")
            };
        }

        CalendarListEntry GetCurrentCalendar(CalendarService service, string CalendarID = "primary")
        {
            if (service == null)
                return null;
            return service.CalendarList.Get(CalendarID).Execute();
        }

        private CalendarService GetCalendarService()
        {
            var _fileName = _user.CredentialJSON?.Split('.');
            if (_user?.Email == null || _fileName.Count() == 2 && _fileName.LastOrDefault().ToUpper() != "JSON")
                return null;

            UserCredential credential = null;
            using (var stream = new FileStream(_user.CredentialJSON, FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                new string[] { CalendarService.Scope.Calendar },
                _user.Email,
                CancellationToken.None,
                new FileDataStore(credPath, true)).Result;
            }
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Booking Calendar",
            });
            return service;
        }
        public async Task<GoogleEventModel> InsertEvent(GoogleEventModel eventModel, string calendarID)
        {
            if (!eventModel.Start.HasValue)
                return null;
            var start = eventModel.Start.Value;
            var service = GetCalendarService();
            var simpleStartDate = new DateTime(start.Year, start.Month, start.Day, start.Hour, start.Minute, start.Second);
            var calendartimeZone = GetCurrentCalendar(service).TimeZone;
            Event newEvent = new Event()
            {
                Id = eventModel.EventId,
                Summary = eventModel.Summary,
                Location = eventModel.Location,
                Description = eventModel.Description,
                Start = new EventDateTime()
                {
                    DateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(simpleStartDate, TZConvert.IanaToWindows(calendartimeZone)),
                    TimeZone = calendartimeZone,
                },
                End = new EventDateTime()
                {
                    DateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(simpleStartDate.AddMinutes(15), TZConvert.IanaToWindows(calendartimeZone)),
                    TimeZone = calendartimeZone,
                },
                Attendees = new EventAttendee[] { new EventAttendee() { Email = _user.Email } },
            };
            //newEvent.Start.DateTimeRaw = newEvent.Start.DateTime.Value.ToLongDateString();
            //newEvent.End.DateTimeRaw = newEvent.End.DateTime.Value.ToLongDateString();
            EventsResource.InsertRequest request = service.Events.Insert(newEvent, calendarID);
            Event createdEvent = await request.ExecuteAsync();
            return createdEvent.MapToGoogleEventModel();

        }

        public async Task<bool> RemoveEvent(string eventId, string calendarID)
        {
            var service = GetCalendarService(); 
            EventsResource.DeleteRequest request = service.Events.Delete(calendarID, eventId);
            var res = await request.ExecuteAsync();
            return true;
        }

        public async Task<GoogleEventModel> UpdateEvent(GoogleEventModel eventModel, string calendarID)
        {
            if (!eventModel.Start.HasValue)
                return null;
            var start = eventModel.Start.Value;
            var service = GetCalendarService();
            var calendartimeZone = GetCurrentCalendar(service).TimeZone;
            var simpleStartDate = new DateTime(start.Year, start.Month, start.Day, start.Hour, start.Minute, start.Second);
            Event gEvent = new Event
            {
                Id = eventModel.EventId,
                Summary = eventModel.Summary,
                Location = eventModel.Location,
                Description = eventModel.Description,
                Start = new EventDateTime()
                {
                    DateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(simpleStartDate, TZConvert.IanaToWindows(calendartimeZone)),
                },
                End = new EventDateTime()
                {
                    DateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(simpleStartDate.AddMinutes(15), TZConvert.IanaToWindows(calendartimeZone)),
                }
            };
            //gEvent.Start.DateTimeRaw = gEvent.Start.DateTime.Value.ToLongDateString();
            //gEvent.End.DateTimeRaw = gEvent.End.DateTime.Value.ToLongDateString();
            EventsResource.UpdateRequest request = service.Events.Update(gEvent, calendarID, gEvent.Id);
            Event UpdatedEvent = await request.ExecuteAsync();
            return UpdatedEvent.MapToGoogleEventModel();
        } 
    }
}
