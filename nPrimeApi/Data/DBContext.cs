using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using nPrimeApi.Models;

namespace nPrimeApi.Data
{
    public class DbContext
    {
        private readonly IMongoDatabase _database = null;

        public DbContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Event> Events
        {
            get => _database.GetCollection<Event>("Event");
        }

        public IMongoCollection<ApplicationUser> Users
        {
            get => _database.GetCollection<ApplicationUser>("User");
        }

        public IMongoCollection<ApplicationRole> Roles
        {
            get => _database.GetCollection<ApplicationRole>("Role");
        }
    }
}
