using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using nPrimeApi.Models;
using System.Threading;
using System.Threading.Tasks;

namespace nPrimeApi.Data
{
    public class UserStore : IUserEmailStore<ApplicationUser>, IUserPasswordStore<ApplicationUser>
    {
        private readonly DbContext _context = null;

        public UserStore(IOptions<Settings> settings)
        {
            _context = new DbContext(settings);
        }

        #region Application User
        public async Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _context.Users.InsertOneAsync(user, null, cancellationToken);

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            DeleteResult actionResult
                = await _context.Users.DeleteOneAsync(
                    Builders<ApplicationUser>.Filter.Eq(nameof(ApplicationUser.Id), user.Id), cancellationToken);

            if (actionResult.IsAcknowledged
                && actionResult.DeletedCount > 0)
                return IdentityResult.Success;
            
            return IdentityResult.Failed();
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ReplaceOneResult actionResult
                = await _context.Users
                    .ReplaceOneAsync(n => n.Id.Equals(user.Id)
                        , user
                        , new UpdateOptions { IsUpsert = true }
                        , cancellationToken);

            if(actionResult.IsAcknowledged
                   && actionResult.ModifiedCount > 0)
                return IdentityResult.Success;

            return IdentityResult.Failed();
        }

        public async Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var filter = Builders<ApplicationUser>.Filter.Eq(nameof(ApplicationUser.Id), userId);

            return await _context.Users
                .Find(filter)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var filter = Builders<ApplicationUser>.Filter.Eq(nameof(ApplicationUser.NormalizedUserName), normalizedUserName);

            return await _context.Users
                .Find(filter)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.FromResult(0);
        }

        public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.FromResult(0);
        }

        #endregion

        #region email

        public Task SetEmailAsync(ApplicationUser user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.FromResult(0);
        }

        public Task<string> GetEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public async Task<ApplicationUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var filter = Builders<ApplicationUser>.Filter.Eq(nameof(ApplicationUser.NormalizedEmail), normalizedEmail);

            return await _context.Users
                .Find(filter)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public Task<string> GetNormalizedEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedEmail);
        }

        public Task SetNormalizedEmailAsync(ApplicationUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            return Task.FromResult(0);
        }
        #endregion

        #region password
        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }
        #endregion

        public void Dispose()
        {
            
        }
    }
}
