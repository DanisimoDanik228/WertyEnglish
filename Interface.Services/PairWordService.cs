using Application.Dto;
using Application.Repositories;
using Application.Services;
using Models;
using Repositories;

namespace Interface.Services
{
    public class PairWordService : IPairWordService
    {
        private readonly IPairWordRepository _pairWordRepository;
        public PairWordService(
            IPairWordRepository pairWordRepository)
        {
            _pairWordRepository = pairWordRepository;
        }
        public async Task<PairWord> AddAsync(long dictionaryId, string word, string translate)
        {
            var pairWord = new PairWord() { Word=word, Translate=translate, DictionaryId= dictionaryId };

            return await _pairWordRepository.AddAsync(pairWord);
        }

        public async Task<IEnumerable<PairWordDto>> GetAllWordsAsync(long dictionaryId)
        {
            var t = await _pairWordRepository.GetAllAsync(dictionaryId);
            return t.Select(t => new PairWordDto() {
                Id=t.Id,
                DictionaryId=t.DictionaryId,
                Word=t.Word,
                Translate=t.Translate});
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
