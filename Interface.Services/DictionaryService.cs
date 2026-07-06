using AutoMapper;
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
        private readonly IMapper _mapper;

        public DictionaryService(
            IDictionaryRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
            
            return _mapper.Map<DictionaryDto>(res);
        }

        public async Task<bool> DeleteDictionaryAsync(long id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<bool> UpdateDictionaryAsync(long Id, string Name)
        {
            var res = await _repository.UpdateNameAsync(Id, Name);
            
            return res.Name == Name;
        }
    }
}
