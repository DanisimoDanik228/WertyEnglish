using Application.Services;
using Models;
using Repositories;

namespace Interface.Services
{
    public class PairWordService : IPairWordService
    {
        private readonly IPairWordRepository _pairWordRepository;
        public PairWordService(IPairWordRepository pairWordRepository)
        {
            _pairWordRepository = pairWordRepository;
        }
        public async Task<PairWord> AddAsync(string word, string translate)
        {
            var pairWord = new PairWord() { Word=word, Translate=translate};

            return await _pairWordRepository.AddAsync(pairWord);
        }

        public async Task<IEnumerable<PairWord>> GetAllWordsAsync()
        {
            return await _pairWordRepository.GetAllAsync();
        }

        public async Task<bool> DeleteAsync(long Id)
        {
            return await _pairWordRepository.DeleteAsync(Id);
        }

        public async Task<PairWord> UpdateAsync(PairWord word)
        {
            return await _pairWordRepository.UpdateAsync(word);
        }
    }
}
