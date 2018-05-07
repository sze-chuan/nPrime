using nPrimeApi.Models;
using nPrimeApi.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace nPrimeApi.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<IEnumerable<Member>> ReadAllAsync()
        {
            return await _memberRepository.ReadAllAsync();
        }

        public async Task<Member> ReadSingleAsync(string memberId)
        {
            return await _memberRepository.ReadSingleAsync(memberId);
        }

        public async Task CreateAsync(Member memberObj)
        {
            await _memberRepository.CreateAsync(memberObj);
        }

        public async Task<bool> UpdateAsync(Member memberObj)
        {
            return await _memberRepository.UpdateAsync(memberObj);
        }

        public async Task<bool> DeleteAsync(string memberId)
        {
            return await _memberRepository.DeleteAsync(memberId);
        }
    }

}
