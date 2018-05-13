using nPrimeApi.Data;
using nPrimeApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace nPrimeApi.Services
{
    public class EventAttendanceService : IEventAttendanceService
    {
        private readonly IEventAttendanceRepository _repository;

        public EventAttendanceService(IEventAttendanceRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<EventAttendance>> ReadAllAsync()
        {
            return await _repository.ReadAllAsync();
        }

        public async Task<EventAttendance> ReadSingleAsync(string objectId)
        {
            return await _repository.ReadSingleAsync(objectId);
        }

        public async Task CreateAsync(EventAttendance obj)
        {
            await _repository.CreateAsync(obj);
        }

        public async Task<bool> UpdateAsync(EventAttendance obj)
        {
            return await _repository.UpdateAsync(obj);
        }

        public async Task<bool> DeleteAsync(string objectId)
        {
            return await _repository.DeleteAsync(objectId);
        }
    }

}
