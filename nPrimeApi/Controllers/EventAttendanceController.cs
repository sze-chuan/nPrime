using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nPrimeApi.Models;
using nPrimeApi.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace nPrimeApi.Controllers
{
    [Produces("application/json")]
    [Route("api/attendance")]
    public class EventAttendanceController : Controller
    {
        private readonly IEventAttendanceService _eventAttendanceService;

        public EventAttendanceController(IEventAttendanceService eventAttendanceService)
        {
            _eventAttendanceService = eventAttendanceService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EventAttendance>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ReadAllAsync()
        {
            var events = await _eventAttendanceService.ReadAllAsync();
            return Ok(events);
        }

        [HttpGet("{objectId}")]
        public async Task<IActionResult> Read(string objectId)
        {
            var eventObj = await _eventAttendanceService.ReadSingleAsync(objectId);

            if (eventObj != null)
                return Ok(eventObj);
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult CreateAsync([FromBody] EventAttendance obj)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _eventAttendanceService.CreateAsync(obj).Wait();
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] EventAttendance obj)
        {
            var result = await _eventAttendanceService.UpdateAsync(obj);

            if (result)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpDelete("{objectId}")]
        public async Task<IActionResult> Delete(string objectId)
        {
            var result = await _eventAttendanceService.DeleteAsync(objectId);

            if (result)
                return Ok(result);
            else
                return NotFound();
        }
    }
}