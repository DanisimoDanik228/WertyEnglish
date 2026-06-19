using Application.Options;
using Application.Services;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace Infrastructure.Services
{
    public class TranslateService : ITranslateService
    {
        private readonly TranslateSetting _translateSetting;
        private readonly HttpClient _httpClient;

        public TranslateService(
            IOptions<TranslateSetting> options,
            HttpClient httpClient)
        {
            _translateSetting = options.Value;
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri(_translateSetting.AdressLibreTranslate);
        }

        public async Task<string> TranslateWord(string word)
        {
            var request = new TranslationRequest()
            { 
                q = word,
                source = "en",
                target = "ru"
            };

            var response = await _httpClient.PostAsJsonAsync("translate", request);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<TranslationResponse>();
            return result?.TranslatedText ?? "Error Translation";
        }

        public class TranslationResponse
        {
            public string TranslatedText { get; set; }
            public List<string> Alternatives { get; set; }
        }

        public class TranslationRequest
        {
            public string q { get; set; }
            public string source { get; set; }
            public string target { get; set; }
            public string format { get; set; } = "text";
            public int alternatives { get; set; } = 3;
            public string api_key { get; set; } = "";
        }
    }
}
