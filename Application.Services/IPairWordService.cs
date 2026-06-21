using Models;

namespace Application.Services
{
    public interface IPairWordService
    {
        Task<IEnumerable<PairWord>> GetAllWordsAsync();

        Task<PairWord> AddAsync(string word, string translate);
        Task<PairWord> UpdateAsync(PairWord word);
        Task<bool> DeleteAsync(long Id);
    }
}
