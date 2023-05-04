using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NetWorthCalculator.Models
{
    public class NetWorthViewModel
    {

        public ICollection<NetWorthItemResult> NetWorthItemResults { get; set; }
    }
}
