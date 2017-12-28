using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using nPrimeApi.Models;

namespace nPrimeApi.Services
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> ReadAllAsync();
    }

}
