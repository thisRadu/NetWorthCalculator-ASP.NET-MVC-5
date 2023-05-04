using System;
using System.Collections.Generic;

#nullable disable

namespace NetWorthCalculator.Models
{
    public partial class NetWorthItemResult
    {
        public int Id { get; set; }
        public int NetWorthId { get; set; }
        public int Item { get; set; }
        public decimal Value { get; set; }

        public virtual NetWorthItem ItemNavigation { get; set; }
        public virtual NetWorth NetWorth { get; set; }
    }
}
