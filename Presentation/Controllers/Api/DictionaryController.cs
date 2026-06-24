using Application.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Presentation.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DictionaryController : ControllerBase
    {
        private readonly IDictionaryService _dictionaryService;
        public DictionaryController(
            IDictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;

        }

        [HttpGet]
        public async Task<IActionResult> GetAllDictionaryies()
        {
            var allWords = await _dictionaryService.GetAllDictionariesAsync();
            return Ok(allWords);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDictionary(string name)
        {
            var word = await _dictionaryService.CreateDictionaryAsync(name);
            return Ok(word);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDictionary(long Id)
        {
            var res = await _dictionaryService.DeleteDictionaryAsync(Id);
            return Ok(res);
        }
    }
}
