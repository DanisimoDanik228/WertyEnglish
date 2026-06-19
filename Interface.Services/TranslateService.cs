using Application.Options;
using Application.Services;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services
{
    public class TranslateService : ITranslateService
    {
        private readonly TranslateSetting _translateSetting;
        public TranslateService(IOptions<TranslateSetting> options)
        {
            _translateSetting = options.Value;
        }

        public async Task<string> TranslateWord(string word)
        {
            return word.ToUpper() + _translateSetting.AdressLibreTranslate;
        }
    }
}
