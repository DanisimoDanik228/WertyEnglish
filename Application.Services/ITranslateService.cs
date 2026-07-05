using Application.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public interface ITranslateService
    {
        Task<string> TranslateWord(string word);
        Task<TranslationResultDto> GetTranslationAlternativesAsync(string word);
    }
}
