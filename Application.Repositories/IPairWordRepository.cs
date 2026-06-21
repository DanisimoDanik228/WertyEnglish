using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IPairWordRepository
    {
        Task<IEnumerable<PairWord>> GetAllAsync();

        Task<PairWord?> GetByIdAsync(long id);

        Task<PairWord> AddAsync(PairWord pairWord);

        Task<PairWord> UpdateAsync(PairWord pairWord);

        Task<bool> DeleteAsync(long id);

        Task<bool> ExistsAsync(long id);
    }
}
