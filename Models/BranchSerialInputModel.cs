using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PrOps.Models
{
    public class BranchSerialInputModel
    {
        [Display(Name="Серийный номер")]
        public string SerialNumber { get; set; }
        [Display(Name = "Модель принтера")]
        public string Model { get; set; }
        [Display(Name = "IP-адрес принтера")]
        public string IPAddress { get; set; }
        [Display(Name = "Номер филиала")]
        public string BranchNumber { get; set; }
    }
}