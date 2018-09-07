using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Calendar.API.Models.Dto
{
    public class GoogleEventModel
    {
        public string    EventId     { get; set; }
        public string    Summary     { get; set; } 
        public string    Location    { get; set; }  
        public string    Description { get; set; }
        public DateTime? Start       { get; set; }
        public DateTime? End         { get; set; }
        public string    TimeZone    { get; set; } 
    } 
}
