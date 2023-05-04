using System;
using System.Collections.Generic;

#nullable disable

namespace NetWorthCalculator.Models
{
    public partial class NetWorthItem
    {
        public NetWorthItem()
        {
            NetWorthItemResults = new HashSet<NetWorthItemResult>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public virtual ICollection<NetWorthItemResult> NetWorthItemResults { get; set; }
    }
}
