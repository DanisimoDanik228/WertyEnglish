using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Diagnostics;

namespace WertyEnglish.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WordController : ControllerBase
    {
        private readonly IPairWordService _pairWordService;
        public WordController(IPairWordService pairWordService)
        {
            _pairWordService = pairWordService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWords()
        {
            var allWords = await _pairWordService.GetAllWordsAsync();
            return Ok(allWords);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePairWord(string Word, string Translate)
        {
            var word = await _pairWordService.AddAsync(Word, Translate);
            return Ok(word);
        }
    }
}
