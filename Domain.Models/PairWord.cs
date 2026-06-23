using Domain.Models;

namespace Models
{
    public class PairWord
    {
        public long Id { get; set; }
        public string Word { get; set; }
        public string Translate { get; set; }
        public long DictionaryId { get; set; }
        public Dictionary Dictionary { get; set; }
    }
}
