using Models;

namespace Application.Services
{
    public interface IPairWordService
    {
        Task<IEnumerable<PairWord>> GetAllWordsAsync();

        Task<PairWord> AddAsync(string word, string translate);
    }
}
