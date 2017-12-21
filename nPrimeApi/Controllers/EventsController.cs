using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nPrimeApi.Interfaces;
using nPrimeApi.Models;

namespace nPrimeApi.Controllers
{
    [Produces("application/json")]
    [Route("api/events")]
    public class EventsController : Controller
    {
        private readonly IEventRepository _EventRepository;

        public EventsController(IEventRepository EventRepository)
        {
            _EventRepository = EventRepository;
        }

        [HttpGet]
        public Task<IEnumerable<Event>> Get()
        {
            return GetEventInternal();
        }

        private async Task<IEnumerable<Event>> GetEventInternal()
        {
            return await _EventRepository.GetAllEvents();
        }
    }
}