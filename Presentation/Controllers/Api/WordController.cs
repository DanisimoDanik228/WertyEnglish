using Microsoft.AspNetCore.Mvc;
using Models;
using System.Diagnostics;

namespace WertyEnglish.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WordController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllWords()
        {
            var t1 = new PairWord();
            return Ok(new PairWord[] { t1, t1 });
        }

        [HttpPost]
        public async Task<IActionResult> CreatePairWord(string Word, string Translate)
        {
            var word = new PairWord() { Id=12,Word = Word,Translate=Translate};
            return Ok(word);
        }
    }
}
