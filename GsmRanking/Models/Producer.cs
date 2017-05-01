using System;
using System.Collections.Generic;

namespace GsmRanking.Models
{
    public partial class Producer
    {
        public Producer()
        {
            Phones = new HashSet<Phone>();
        }

        public int IdProducer { get; set; }
        public string ProducerName { get; set; }

        public virtual ICollection<Phone> Phones { get; set; }
    }
}
