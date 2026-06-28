using Application.Dto;
using Application.Repositories;
using Application.Services;
using Domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services
{
    public class DictionaryService : IDictionaryService
    {
        private readonly IDictionaryRepository _repository;

        public DictionaryService(IDictionaryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<DictionaryDto>> GetAllDictionariesAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Dictionary?> GetDictionaryByIdAsync(long id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<DictionaryDto> CreateDictionaryAsync(string name)
        {
            var res = await _repository.AddAsync(new Dictionary()
            {
                Name = name
            });
            
            var resDictionaryDto = new DictionaryDto
            {
                Id = res.Id,
                Name = res.Name,
                CountWord = 0
            };

            return resDictionaryDto;
        }

        public async Task<bool> DeleteDictionaryAsync(long id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
