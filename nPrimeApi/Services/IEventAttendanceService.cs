using nPrimeApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace nPrimeApi.Services
{
    public interface IEventAttendanceService
    {
        Task<IEnumerable<EventAttendance>> ReadAllAsync();
        Task<EventAttendance> ReadSingleAsync(string objectId);
        Task CreateAsync(EventAttendance obj);
        Task<bool> DeleteAsync(string objectId);
        Task<bool> UpdateAsync(EventAttendance obj);
    }
}
