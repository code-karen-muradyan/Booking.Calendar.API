using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Calendar.API.Infrastructure.Exceptions
{   
    /// <summary>
    /// Exception type for app exceptions
    /// </summary>
    public class CalendarDomainException : Exception
    {
        public CalendarDomainException()
        { }

        public CalendarDomainException(string message)
            : base(message)
        { }

        public CalendarDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}