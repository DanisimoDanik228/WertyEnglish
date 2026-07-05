using Application.Dto;
using Models;

namespace Application.Services
{
    public interface IPairWordService
    {
        Task<IEnumerable<PairWordDto>> GetAllWordsAsync(long ditionaryId);
        Task<PairWord> AddAsync(long dictionaryId, string word, string translate);
        Task<PairWord> UpdateAsync(PairWord word);
        Task<bool> DeleteAsync(long Id);
    }
}
