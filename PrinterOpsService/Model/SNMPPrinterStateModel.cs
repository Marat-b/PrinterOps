using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrinterOpsService.Model
{
    public class SNMPPrinterStateModel
    {
        public string SerialNumber { get; set; }
        public string DisplayMessage { get; set; }
        public string AlertMessage { get; set; }
        public string ContactInfo { get; set; }
        public string LocationInfo { get; set; }
        public string Model { get; set; }
        public string IPAddress { get; set; }
        public int PageCount { get; set; }
        public string Uptime { get; set; }
    }
}