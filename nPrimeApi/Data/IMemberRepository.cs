using nPrimeApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace nPrimeApi.Repositories
{
    public interface IMemberRepository
    {
        Task<IEnumerable<Member>> ReadAllAsync();
        Task<Member> ReadSingleAsync(string memberId);
        Task CreateAsync(Member memberObj);
        Task<bool> UpdateAsync(Member memberObj);
        Task<bool> DeleteAsync(string memberId);
    }
}
    