using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;
using nPrimeApi.Models;

namespace nPrimeApi.Repositories
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> ReadAllAsync();
        Task<Event> ReadSingleAsync(string eventId);
        Task CreateAsync(Event eventObj);
        Task<bool> UpdateAsync(Event eventObj);
        Task<bool> DeleteAsync(string eventId);
    }
}
    