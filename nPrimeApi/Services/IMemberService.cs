using nPrimeApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace nPrimeApi.Services
{
    public interface IMemberService
    {
        Task<IEnumerable<Member>> ReadAllAsync();
        Task<Member> ReadSingleAsync(string memberId);
        Task CreateAsync(Member memberObj);
        Task<bool> DeleteAsync(string memberId);
        Task<bool> UpdateAsync(Member memberObj);
    }

}
