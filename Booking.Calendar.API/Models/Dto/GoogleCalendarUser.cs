using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Calendar.API.Models.Dto
{
    public class GoogleCalendarUser
    { 
        public string Email { get; set; }
        public string CredentialJSON { get; set; }
    }
}
