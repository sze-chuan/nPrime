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
    public class MemberRepository : IMemberRepository
    {
        private readonly DbContext _context = null;

        public MemberRepository(IOptions<Settings> settings)
        {
            _context = new DbContext(settings);
        }

        public async Task<IEnumerable<Member>> ReadAllAsync()
        {
            try
            {
                return await _context.Members
                        .Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<Member> ReadSingleAsync(string memberId)
        {
            if (!ObjectId.TryParse(memberId, out var memberObjectId))
                return null;
                
            var filter = Builders<Member>.Filter.Eq("ObjectId", memberObjectId);

            try
            {
                return await _context.Members
                                .Find(filter)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task CreateAsync(Member item)
        {
            try
            {
                await _context.Members.InsertOneAsync(item);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> DeleteAsync(string memberId)
        {
            try
            {
                DeleteResult actionResult
                    = await _context.Members.DeleteOneAsync(
                        Builders<Member>.Filter.Eq("ObjectId", memberId));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> UpdateAsync(Member memberObj)
        {
            try
            {

                ReplaceOneResult actionResult
                    = await _context.Members
                                    .ReplaceOneAsync(n => n.ObjectId.Equals(memberObj.ObjectId)
                                            , memberObj
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
