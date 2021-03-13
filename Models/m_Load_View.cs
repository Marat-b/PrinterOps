using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrOps.Models
{
    public class m_Load_View
    {
        public string SerialNumber { get; set; }
        public string Model { get; set; }
        public string printer_name { get; set; }
        public string IPAddress { get; set; }
        public int PagePrinted { get; set; }
    }
}