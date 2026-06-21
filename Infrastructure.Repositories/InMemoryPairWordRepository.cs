using Models;
using Repositories;

namespace Infrastructure.Repositories
{
    public class InMemoryPairWordRepository : IPairWordRepository
    {
        private static readonly List<PairWord> _storage = new List<PairWord>();
        private static int _nextId = 1;

        public async Task<IEnumerable<PairWord>> GetAllAsync()
        {
            return _storage.AsEnumerable();
        }

        public async Task<PairWord?> GetByIdAsync(long id)
        {
            return _storage.FirstOrDefault(p => p.Id == id);
        }

        public async Task<PairWord> AddAsync(PairWord pairWord)
        {
            pairWord.Id = _nextId++;
            _storage.Add(pairWord);

            return pairWord;
        }

        public async Task<PairWord> UpdateAsync(PairWord pairWord)
        {
            var index = _storage.FindIndex(p => p.Id == pairWord.Id);
            if (index != -1)
            {
                _storage[index] = pairWord;
                return pairWord;
            }

            return null;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var item = _storage.FirstOrDefault(p => p.Id == id);
            if (item != null)
            {
                _storage.Remove(item);
                return true;
            }

            return false;
        }

        public async Task<bool> ExistsAsync(long id)
        {
            return _storage.Any(p => p.Id == id);
        }
    }
}
