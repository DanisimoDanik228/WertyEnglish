using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Domain.Models
{
    public class Dictionary
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<PairWord> PairWords { get; set; } = new List<PairWord>();
    }
}
