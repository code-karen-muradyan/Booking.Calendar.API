using Booking.Calendar.API.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Calendar.API.Infrastructure.Repositories
{
    public interface ICalendatRepository
    {
        Task<bool> CreateAppointment(Apponintment apponintment);
    }
}
