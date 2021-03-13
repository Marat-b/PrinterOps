using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrinterOpsService.Model
{
    public class SNMPPrinterColorStateModel
    {
        public string ColorNameBlack { get; set; }
        public int TonerRemainingBlack { get; set; }
        public int MaxLevelBlack { get; set; }

        public string ColorNameCyan { get; set; }
        public int TonerRemainingCyan { get; set; }
        public int MaxLevelCyan { get; set; }

        public string ColorNameMagenta { get; set; }
        public int TonerRemainingMagenta { get; set; }
        public int MaxLevelMagenta { get; set; }

        public string ColorNameYellow { get; set; }
        public int TonerRemainingYellow { get; set; }
        public int MaxLevelYellow { get; set; }

        public string SerialNumber { get; set; }
    }
}