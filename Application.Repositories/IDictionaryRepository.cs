using Application.Dto;
using Domain.Models;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Repositories
{
    public interface IDictionaryRepository
    {
        Task<IEnumerable<DictionaryDto>> GetAllAsync();

        Task<Dictionary?> GetByIdAsync(long id);

        Task<Dictionary> AddAsync(Dictionary dictionary);

        Task<Dictionary> UpdateNameAsync(long Id, string Name);

        Task<bool> DeleteAsync(long id);
    }
}
