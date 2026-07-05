using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dto
{
    public class TranslationResultDto
    {
        public string TranslatedText { get; set; } = string.Empty;
        public List<string> Alternatives { get; set; } = new List<string>();
    }
}
