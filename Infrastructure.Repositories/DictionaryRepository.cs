using AutoMapper;
using AutoMapper.QueryableExtensions;
using Application.Dto;
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
        private readonly IMapper _mapper;

        public DictionaryRepository(
            AppDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<DictionaryDto>> GetAllAsync()
        {
            return await _dbContext
                .Dictionaries
                .ProjectTo<DictionaryDto>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync();
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
