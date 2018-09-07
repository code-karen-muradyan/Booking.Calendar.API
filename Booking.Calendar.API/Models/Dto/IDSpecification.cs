using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Calendar.API.Models.Dto
{
    public class IDSpecification 
    {
        public int Id { get; set; }
        public string GoogleId { get; set; }

        public int AppointmentId { get; set; }
        public Apponintment Apponintment { get; set; }
    }
}
