using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using nPrimeApi.Repositories;
using nPrimeApi.Models;

namespace nPrimeApi.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<IEnumerable<Event>> ReadAllAsync()
        {
            return await _eventRepository.ReadAllAsync();
        }

        public async Task<Event> ReadSingleAsync(string eventId)
        {
            return await _eventRepository.ReadSingleAsync(eventId);
        }

        public async Task CreateAsync(Event eventObj)
        {
            await _eventRepository.CreateAsync(eventObj);
        }

        public async Task<bool> UpdateAsync(Event eventObj)
        {
            return await _eventRepository.UpdateAsync(eventObj);
        }

        public async Task<bool> DeleteAsync(string eventId)
        {
            return await _eventRepository.DeleteAsync(eventId);
        }
    }

}
