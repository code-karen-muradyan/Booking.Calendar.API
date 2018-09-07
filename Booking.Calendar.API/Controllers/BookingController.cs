using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Booking.Calendar.API.Application.Commands;
using Booking.Calendar.API.Application.Queries;
using Booking.Calendar.API.Infrastructure.Repositories;
using Booking.Calendar.API.Models.Dto;
using Booking.Calendar.API.Models.Read;
using Booking.Calendar.API.Models.Write;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Calendar.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICalendarQueries  _calendarQueries;

        public BookingController(IMediator mediator, ICalendarQueries calendarQueries)
        {

            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _calendarQueries = calendarQueries ?? throw new ArgumentNullException(nameof(calendarQueries));
        }


        /// <summary>
        /// This method returned all appointments for login user
        /// </summary>
        /// <returns></returns>
        [Route("items/owner/{email}/categoria/{categoria}")]
        [ProducesResponseType(typeof(IEnumerable<Apponintment>), (int)HttpStatusCode.OK)]
        [HttpGet]
        public async Task<IActionResult> Get(string email , string categoria, [FromQuery]DateTime date)
        {
            var result= await _calendarQueries.GetAppointmentsFromUserAsync(email,categoria,date);
            return Ok(result);
        }

        /// <summary>
        /// This method return appointment for id
        /// </summary>
        /// <param name="value"></param>
        [Route("items/{id}")]
        [ProducesResponseType(typeof(Apponintment),(int)HttpStatusCode.OK)]
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _calendarQueries.GetAppointmentAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// This method update appointment data, local and for google calendar 
        /// </summary>
        /// <param name="value"></param>
        [Route("items")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post([FromBody] CreateAppointmentCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            bool commandResult = false;
            if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
            {
                var requestCancelOrder = new IdentifiedCommand<CreateAppointmentCommand, bool>(command, guid);
                commandResult = await _mediator.Send(requestCancelOrder);
            }
            return Ok(commandResult);
        }

        /// <summary>
        /// This method update data new appointment, local and for google calendar
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [Route("items")]
        [HttpPut]
        [ProducesResponseType(typeof(Apponintment), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Put(int id, [FromBody] CreateAppointmentCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            bool commandResult = false;
            if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
            {
                var requestCancelOrder = new IdentifiedCommand<CreateAppointmentCommand, bool>(command, guid);
                commandResult = await _mediator.Send(requestCancelOrder);
            }
            return Ok(commandResult);
        }


        /// <summary>
        /// This method update appointment data, local and for google calendar 
        /// </summary>
        /// <param name="value"></param>
        [Route("rooms")]
        [HttpGet]
        [ProducesResponseType(typeof(Room),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetRooms()
        {
            var rooms = new List<Room> {
                new Room{ Name = "Room1" },
                new Room { Name = "Room2" },
                new Room { Name = "Room3" },
                new Room { Name = "Room4" }
            };
            return Ok(rooms);
        }


    }
}
