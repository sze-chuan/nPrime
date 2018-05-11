using System.Collections.Generic;
using System.Threading.Tasks;

namespace nPrimeApi.Repositories
{
    public interface IRepository<T1>
    {
        Task<IEnumerable<T1>> ReadAllAsync();
        Task<T1> ReadSingleAsync(string objectId);
        Task CreateAsync(T1 obj);
        Task<bool> UpdateAsync(T1 obj);
        Task<bool> DeleteAsync(string objectId);
    }
}
    