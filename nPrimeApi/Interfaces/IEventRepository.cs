using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;
using nPrimeApi.Models;

namespace nPrimeApi.Interfaces
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAllEvents();
        Task<Event> GetEvent(string id);

        // add new Event document
        Task AddEvent(Event item);

        // remove a single document / Event
        Task<bool> RemoveEvent(string id);

        // update just a single document / Event
        Task<bool> UpdateEvent(string id, Event name);

        // should be used with high cautious, only in relation with demo setup
        Task<bool> RemoveAllEvents();
    }
}
