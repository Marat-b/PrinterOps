using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrOps.Models
{
    public class CartridgeStateView2 : CartridgeStateView
    {
        private DateTime _inputDate;
        public string BranchNumber { get; set; }

        public CartridgeStateView2(string inputDate):base( inputDate)
        {
            try
            {
                _inputDate = Convert.ToDateTime(inputDate);
            }
            catch
            {
                _inputDate = DateTime.Now;
            }
            
        }
    }
}