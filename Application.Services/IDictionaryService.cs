using Application.Dto;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public interface IDictionaryService
    {
        Task<IEnumerable<DictionaryDto>> GetAllDictionariesAsync();
        Task<Dictionary?> GetDictionaryByIdAsync(long id);
        Task<DictionaryDto> CreateDictionaryAsync(string name);
        Task<bool> DeleteDictionaryAsync(long id);
    }
}
