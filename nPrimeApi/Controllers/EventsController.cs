using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nPrimeApi.Services;
using nPrimeApi.Models;

namespace nPrimeApi.Controllers
{
    [Produces("application/json")]
    [Route("api/events")]
    public class EventsController : Controller
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Event>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ReadAllAsync()
        {
            var events = await _eventService.ReadAllAsync();
            return Ok(events);
        }

        [HttpGet("{eventId}")]
        public async Task<IActionResult> ReadSingleEvent(string eventId)
        {
            var eventObj = await _eventService.ReadSingleAsync(eventId);

            if (eventObj != null)
                return Ok(eventObj);
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult CreateAsync([FromBody] Event eventObj)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _eventService.CreateAsync(eventObj).Wait();
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEvent([FromBody] Event eventObj)
        {
            var result = await _eventService.UpdateAsync(eventObj);

            if (result)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpDelete("{eventId}")]
        public async Task<IActionResult> DeleteEvent(string eventId)
        {
            var result = await _eventService.DeleteAsync(eventId);

            if (result)
                return Ok(result);
            else
                return NotFound();
        }

    }
}