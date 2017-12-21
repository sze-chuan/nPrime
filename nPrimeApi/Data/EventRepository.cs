using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using nPrimeApi.Interfaces;
using nPrimeApi.Models;

namespace nPrimeApi.Data
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationContext _context = null;

        public EventRepository(IOptions<Settings> settings)
        {
            _context = new ApplicationContext(settings);
        }

        public async Task<IEnumerable<Event>> GetAllEvents()
        {
            try
            {
                return await _context.Events
                        .Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<Event> GetEvent(string id)
        {
            var filter = Builders<Event>.Filter.Eq("Id", id);

            try
            {
                return await _context.Events
                                .Find(filter)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task AddEvent(Event item)
        {
            try
            {
                await _context.Events.InsertOneAsync(item);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> RemoveEvent(string id)
        {
            try
            {
                DeleteResult actionResult
                    = await _context.Events.DeleteOneAsync(
                        Builders<Event>.Filter.Eq("Id", id));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> UpdateEvent(string id, Event item)
        {
            try
            {

                ReplaceOneResult actionResult
                    = await _context.Events
                                    .ReplaceOneAsync(n => n.Id.Equals(new ObjectId(id))
                                            , item
                                            , new UpdateOptions { IsUpsert = true });
                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> RemoveAllEvents()
        {
            try
            {
                DeleteResult actionResult
                    = await _context.Events.DeleteManyAsync(new BsonDocument());

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
    }

}
