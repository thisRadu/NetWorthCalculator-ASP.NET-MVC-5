using System;
using System.Collections.Generic;

#nullable disable

namespace NetWorthCalculator.Models
{
    public partial class NetWorth
    {
        public NetWorth()
        {
            NetWorthItemResults = new HashSet<NetWorthItemResult>();
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Advice { get; set; }

        public virtual ICollection<NetWorthItemResult> NetWorthItemResults { get; set; }
    }
}
