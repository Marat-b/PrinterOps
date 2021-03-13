using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrOps.Models
{
    public class ChargeCartridge
    {
        public int OldRemainToner { get; set; }
        public int PageCount { get; set; }
        public int NewRemainToner { get; set; }
        public DateTime ChargeDate { get; set; }
        public string ColorToner { get; set; }
    }
}