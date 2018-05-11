using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using nPrimeApi.Models;
using System.Threading;
using System.Threading.Tasks;

namespace nPrimeApi.Data
{
    public class RoleStore : IRoleStore<ApplicationRole>
    {
        private readonly DbContext _context = null;

        public RoleStore(IOptions<Settings> settings)
        {
            _context = new DbContext(settings);
        }


        public async Task<IdentityResult> CreateAsync(ApplicationRole role, CancellationToken cancellationToken)
        {

            cancellationToken.ThrowIfCancellationRequested();

            await _context.Role.InsertOneAsync(role, null, cancellationToken);

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationRole role, CancellationToken cancellationToken)

        {
            cancellationToken.ThrowIfCancellationRequested();

            ReplaceOneResult actionResult
                = await _context.Role
                    .ReplaceOneAsync(n => n.Id.Equals(role.Id)
                        , role
                        , new UpdateOptions { IsUpsert = true }
                        , cancellationToken);

            if (actionResult.IsAcknowledged
                && actionResult.ModifiedCount > 0)
                return IdentityResult.Success;

            return IdentityResult.Failed();
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            DeleteResult actionResult
                = await _context.Role.DeleteOneAsync(
                    Builders<ApplicationRole>.Filter.Eq(nameof(ApplicationRole.Id), role.Id), cancellationToken);

            if (actionResult.IsAcknowledged
                && actionResult.DeletedCount > 0)
                return IdentityResult.Success;

            return IdentityResult.Failed();
        }

        public Task<string> GetRoleIdAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Id.ToString());
        }

        public Task<string> GetRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Name);
        }

        public Task SetRoleNameAsync(ApplicationRole role, string roleName, CancellationToken cancellationToken)
        {
            role.Name = roleName;

            return Task.FromResult(0);
        }

        public Task<string> GetNormalizedRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.NormalizedName);
        }

        public Task SetNormalizedRoleNameAsync(ApplicationRole role, string normalizedName, CancellationToken cancellationToken)
        {
            role.NormalizedName = normalizedName;

            return Task.FromResult(0);
        }

        public async Task<ApplicationRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var filter = Builders<ApplicationRole>.Filter.Eq(nameof(ApplicationRole.Id), roleId);

            return await _context.Role
                .Find(filter)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<ApplicationRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var filter = Builders<ApplicationRole>.Filter.Eq(nameof(ApplicationRole.NormalizedName), normalizedRoleName);

            return await _context.Role
                .Find(filter)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public void Dispose()
        {

        }
    }
}
