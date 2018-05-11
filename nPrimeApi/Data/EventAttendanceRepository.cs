using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using nPrimeApi.Models;
using nPrimeApi.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace nPrimeApi.Data
{
    public class EventAttendanceRepository : Repository<EventAttendance>, IEventAttendanceRepository
    {
        public EventAttendanceRepository(IOptions<Settings> settings): base(settings)
        {
        }
    }
}
