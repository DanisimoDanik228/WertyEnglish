using Application.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace Infrastructure.Repositories
{
    public class DictionaryRepository : IDictionaryRepository
    {
        private readonly AppDbContext _dbContext;

        public DictionaryRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Dictionary>> GetAllAsync()
        {
            return await _dbContext.Dictionaries.AsNoTracking().ToListAsync();
        }

        public async Task<Dictionary?> GetByIdAsync(long id)
        {
            return await _dbContext.Dictionaries.FindAsync(id);
        }

        public async Task<Dictionary> AddAsync(Dictionary dictionary)
        {
            var res = await _dbContext.Dictionaries.AddAsync(dictionary);
            await _dbContext.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await _dbContext.Dictionaries.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            _dbContext.Dictionaries.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
