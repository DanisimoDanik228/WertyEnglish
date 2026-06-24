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

        public async Task<IEnumerable<Dictionary>> GetAllDictionariesAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Dictionary?> GetDictionaryByIdAsync(long id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Dictionary> CreateDictionaryAsync(string name)
        {
            var newDictionary = new Dictionary
            {
                Name = name
            };

            return await _repository.AddAsync(newDictionary);
        }

        public async Task<bool> DeleteDictionaryAsync(long id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
