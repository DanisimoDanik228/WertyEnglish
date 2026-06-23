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
        private readonly ITranslateService _translateService;
        public WordController(
            IPairWordService pairWordService,
            ITranslateService translateService)
        {
            _pairWordService = pairWordService;
            _translateService = translateService;
            
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWords(long DitionaryId)
        {
            var allWords = await _pairWordService.GetAllWordsAsync(DitionaryId);
            return Ok(allWords);
        }

        [HttpGet]
        public async Task<IActionResult> TranslateWord(string Word)
        {
            var translate = await _translateService.TranslateWord(Word);
            return Ok(translate);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePairWord(long DitionaryId, string Word, string Translate)
        {
            var word = await _pairWordService.AddAsync(DitionaryId, Word, Translate);
            return Ok(word);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdatePairWord([FromBody] PairWord word)
        {
            var updateWord = await _pairWordService.UpdateAsync(word);
            return Ok(updateWord);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePairWord(long Id)
        {
            var res = await _pairWordService.DeleteAsync(Id);
            return Ok(res);
        }
    }
}
