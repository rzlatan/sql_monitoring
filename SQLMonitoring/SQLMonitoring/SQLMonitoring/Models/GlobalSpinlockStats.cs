using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMonitoring.Model
{
    public class GlobalSpinlockStats
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        
        public string Server { get; set; }
    
        public string Spinlock { get; set; }

        public long Collisions { get; set; }

        public long Backoffs { get; set; }
    }
}
