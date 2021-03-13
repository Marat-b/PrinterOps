using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrinterOpsService.Model
{
    public class USBPrinterStateModel
    {
        

            public string SerialNumber { get; set; }

            public string Model { get; set; }

            public int PageCount { get; set; }

            public string DisplayMessage { get; set; }

            public string AlertMessage { get; set; }

            public string ComputerName { get; set; }
        
    }
}