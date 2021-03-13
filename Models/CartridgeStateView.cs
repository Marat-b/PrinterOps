using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace PrOps.Models
{
    public class CartridgeStateView:RemainDaysView
    {
        private DateTime _inputDate;
        private DateTime currentDate;
        public CartridgeStateView(string inputDate):base()
        {
            //InputDate = inputDate;
            
            try
            {
                _inputDate = Convert.ToDateTime(inputDate);
            }
            catch 
            {
                _inputDate = DateTime.Now;
            }
            currentDate = DateTime.Now;
        }

        public int CountCartridge
        {
            get { return fCountCartridge(); }
        }

        //public string InputDate { set {_inputDate=value; } }

        private int fCountCartridge()
        {
            int countCartridge;
            Debug.WriteLine(string.Format("_inputDate={0}", _inputDate));
            if (this.NumberDays > 0)
            {
                countCartridge = Convert.ToInt32(Math.Ceiling(((_inputDate - (currentDate.AddDays(this.NumberDays))).TotalDays) / (currentDate.AddDays(this.NumberDays) - this.ChargeDate).TotalDays));
            }
            else
            {
                countCartridge = Convert.ToInt32(Math.Ceiling((_inputDate - currentDate).TotalDays / (currentDate - this.ChargeDate).TotalDays)); // +1;
                if (countCartridge <= 0)
                    countCartridge = 1;
            }
            return countCartridge;
        }
    }
}