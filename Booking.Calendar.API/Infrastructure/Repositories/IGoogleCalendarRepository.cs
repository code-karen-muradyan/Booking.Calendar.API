using Booking.Calendar.API.Models.Dto;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Calendar.API.Infrastructure.Repositories
{
    public interface IGoogleCalendarRepository
    {
        Task<GoogleEventModel> InsertEvent(GoogleEventModel eventModel, string calendarID);
        Task<GoogleEventModel> UpdateEvent(GoogleEventModel eventModel, string calendarID);
        Task<bool> RemoveEvent(string eventId, string calendarID);
    }
}
