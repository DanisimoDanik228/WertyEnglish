using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public interface IDictionaryService
    {
        Task<IEnumerable<Dictionary>> GetAllDictionariesAsync();
        Task<Dictionary?> GetDictionaryByIdAsync(long id);
        Task<Dictionary> CreateDictionaryAsync(string name);
        Task<bool> DeleteDictionaryAsync(long id);
    }
}
