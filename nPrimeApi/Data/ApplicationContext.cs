using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using nPrimeApi.Models;

namespace nPrimeApi.Data
{
    public class ApplicationContext
    {
        private readonly IMongoDatabase _database = null;

        public ApplicationContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Event> Events
        {
            get { return _database.GetCollection<Event>("Event"); }
        }
    }
}
