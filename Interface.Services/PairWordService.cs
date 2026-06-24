using Application.Repositories;
using Application.Services;
using Models;
using Repositories;

namespace Interface.Services
{
    public class PairWordService : IPairWordService
    {
        private readonly IPairWordRepository _pairWordRepository;
        private readonly IDictionaryRepository _dictionaryRepository;
        public PairWordService(
            IPairWordRepository pairWordRepository,
            IDictionaryRepository dictionaryRepository)
        {
            _pairWordRepository = pairWordRepository;
            _dictionaryRepository = dictionaryRepository;
        }
        public async Task<PairWord> AddAsync(long dictionaryId, string word, string translate)
        {
            var pairWord = new PairWord() { Word=word, Translate=translate, DictionaryId= dictionaryId };

            return await _pairWordRepository.AddAsync(pairWord);
        }

        public async Task<IEnumerable<PairWord>> GetAllWordsAsync(long dictionaryId)
        {
            return await _pairWordRepository.GetAllAsync(dictionaryId);
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
