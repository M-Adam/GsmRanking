using System;
using System.Collections.Generic;

namespace GsmRanking.Models
{
    public partial class Producers
    {
        public Producers()
        {
            Phones = new HashSet<Phones>();
        }

        public int Producerid { get; set; }
        public string ProducerName { get; set; }

        public virtual ICollection<Phones> Phones { get; set; }
    }
}
