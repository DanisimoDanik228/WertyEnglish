using Microsoft.EntityFrameworkCore;
using Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class PairWordRepository : IPairWordRepository
    {
        private readonly AppDbContext _dbContext;

        public PairWordRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<PairWord>> GetAllAsync()
        {
            return await _dbContext.PairWords.AsNoTracking().ToListAsync();
        }

        public async Task<PairWord?> GetByIdAsync(long id)
        {
            return await _dbContext.PairWords.FindAsync(id);
        }

        public async Task<PairWord> AddAsync(PairWord pairWord)
        {
            await _dbContext.PairWords.AddAsync(pairWord);
            await _dbContext.SaveChangesAsync();
            return pairWord;
        }

        public async Task<PairWord> UpdateAsync(PairWord pairWord)
        {
            var existWord = await _dbContext.PairWords.FindAsync(pairWord.Id);
            existWord.Word = pairWord.Word;
            existWord.Translate = pairWord.Translate;
            await _dbContext.SaveChangesAsync();

            return pairWord;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await _dbContext.PairWords.FindAsync(id);
            if (entity == null)
            { 
                return false;
            }

            _dbContext.PairWords.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(long id)
        {
            return await _dbContext.PairWords.AnyAsync(p => p.Id == id);
        }
    }
}
