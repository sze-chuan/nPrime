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
    public class Repository<T1>
    {
        private readonly string ObjectIdName = "ObjectId";
        private readonly DbContext _context;
        private readonly IMongoCollection<T1> _mongoCollection;

        public Repository(IOptions<Settings> settings)
        {
            _context = new DbContext(settings);
            _mongoCollection = GetMongoCollection();
        }

        public async Task<IEnumerable<T1>> ReadAllAsync()
        {
            try
            {
                return await _mongoCollection.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<T1> ReadSingleAsync(string objectId)
        {
            if (!ObjectId.TryParse(objectId, out var eventObjectId))
                return default(T1);

            var filter = Builders<T1>.Filter.Eq("ObjectId", eventObjectId);

            try
            {
                return await _mongoCollection
                                .Find(filter)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task CreateAsync(T1 item)
        {
            try
            {
                await _mongoCollection.InsertOneAsync(item);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> DeleteAsync(string objectId)
        {
            try
            {
                DeleteResult actionResult
                    = await _mongoCollection.DeleteOneAsync(
                        Builders<T1>.Filter.Eq("ObjectId", objectId));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> UpdateAsync(T1 obj)
        {
            try
            {

                ReplaceOneResult actionResult
                    = await _mongoCollection
                                    .ReplaceOneAsync(n => GetObjectId(n).Equals(GetObjectId(obj))
                                            , obj
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

        private IMongoCollection<T1> GetMongoCollection()
        {
            return (IMongoCollection<T1>)_context.GetType().GetProperty(typeof(T1).Name).GetValue(_context);
        }

        private string GetObjectId(T1 obj)
        {
            return (string) obj.GetType().GetProperty(ObjectIdName).GetValue(obj);
        }
    }

}
