using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using nPrimeApi.Repositories;
using nPrimeApi.Models;

namespace nPrimeApi.Data
{
    public class EventRepository : IEventRepository
    {
        private readonly DbContext _context = null;

        public EventRepository(IOptions<Settings> settings)
        {
            _context = new DbContext(settings);
        }

        public async Task<IEnumerable<Event>> ReadAllAsync()
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

        public async Task<Event> ReadSingleAsync(string eventId)
        {
            if (!ObjectId.TryParse(eventId, out var eventObjectId))
                return null;
                
            var filter = Builders<Event>.Filter.Eq("ObjectId", eventObjectId);

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

        public async Task CreateAsync(Event item)
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

        public async Task<bool> DeleteAsync(string eventId)
        {
            if (!ObjectId.TryParse(eventId, out var eventObjectId))
                return false;

            try
            {
                DeleteResult actionResult
                    = await _context.Events.DeleteOneAsync(
                        Builders<Event>.Filter.Eq("ObjectId", eventObjectId));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> UpdateAsync(Event eventObj)
        {
            try
            {

                ReplaceOneResult actionResult
                    = await _context.Events
                                    .ReplaceOneAsync(n => n.ObjectId.Equals(eventObj.ObjectId)
                                            , eventObj
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
    }

}
