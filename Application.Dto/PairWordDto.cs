using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dto
{
    public class PairWordDto
    {
        public long Id { get; set; }
        public string Word { get; set; }
        public string Translate { get; set; }
        public long DictionaryId { get; set; }
    }
}
