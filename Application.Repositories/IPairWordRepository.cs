using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IPairWordRepository
    {
        Task<IEnumerable<PairWord>> GetAllAsync();

        Task<PairWord?> GetByIdAsync(int id);

        Task<PairWord> AddAsync(PairWord pairWord);

        Task UpdateAsync(PairWord pairWord);

        Task DeleteAsync(int id);

        Task<bool> ExistsAsync(int id);
    }
}
