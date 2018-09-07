using Booking.Calendar.API.Models.Dto;
using Google.Apis.Calendar.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Calendar.API.Application.Extentions
{
    public static class MapperExtention
    {
        public static GoogleEventModel MapToGoogleEventModel(this Event gEvent)
        {
            return new GoogleEventModel()
            {
                EventId = gEvent.Id,
                Description = gEvent.Description,
                End = gEvent.End.DateTime,
                Location = gEvent.Location,
                Start = gEvent.Start.DateTime,
                Summary = gEvent.Summary,
                TimeZone = gEvent.Start.TimeZone
            };
        }

        public static Event MapToEventModel(this GoogleEventModel gEvent)
        {
            EventDateTime start = new EventDateTime()
            {
                DateTime = gEvent.Start,
                TimeZone = gEvent.TimeZone
            };
            EventDateTime end = new EventDateTime()
            {
                DateTime = gEvent.End,
                TimeZone = gEvent.TimeZone
            };
            return new Event()
            { 
                Description = gEvent.Description,
                End = end,
                Location = gEvent.Location,
                Start = start, 
                Summary = gEvent.Summary,
                Id = gEvent.EventId
            };
        }
    }
}
