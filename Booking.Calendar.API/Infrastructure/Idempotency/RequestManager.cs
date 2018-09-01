using Booking.Calendar.API.Infrastructure.Exceptions;
using System;
using System.Threading.Tasks;

namespace Booking.Calendar.API.Infrastructure.Idempotency
{
    public class RequestManager : IRequestManager
    {
        private readonly CalendarContext _context;

        public RequestManager(CalendarContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task<bool> ExistAsync(Guid id)
        {
            var request = await _context.
                FindAsync<ClientRequest>(id);

            return request != null;
        }

        public async Task CreateRequestForCommandAsync<T>(Guid id)
        { 
            var exists = await ExistAsync(id);

            var request = exists ? 
                throw new CalendarDomainException($"Request with {id} already exists") : 
                new ClientRequest()
                {
                    Id = id,
                    Name = typeof(T).Name,
                    Time = DateTime.UtcNow
                };

            _context.Add(request);

            await _context.SaveChangesAsync();
        }
    }
}
