﻿using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using nPrimeApi.Models;
using nPrimeApi.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
                return await _context.Event
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
                return await _context.Event
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
                await _context.Event.InsertOneAsync(item);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> DeleteAsync(string eventId)
        {
            try
            {
                DeleteResult actionResult
                    = await _context.Event.DeleteOneAsync(
                        Builders<Event>.Filter.Eq("ObjectId", eventId));

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
                    = await _context.Event
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
