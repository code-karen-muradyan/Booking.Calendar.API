using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Booking.Calendar.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class InviteController : ControllerBase
    {
        /// <summary>
        /// This method update appointment data, local and for google calendar 
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post([FromBody] string value)
        {
            return Ok();
        }
    }
}
