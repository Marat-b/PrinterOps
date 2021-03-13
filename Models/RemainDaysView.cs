using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace PrOps.Models
{
    public class RemainDaysView
    {
        // Remain days before charging
        private int maxLevelBlack;
        private int newRemainToner;
        private int remainBlack;
        private System.DateTime chargeDate;

        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public string IPAddress { get; set; }
        public int MaxLevelBlack
        {
            get { return maxLevelBlack; }
            set
            {
                 maxLevelBlack = value; 
            }
        }
        public int NewRemainToner { get { return newRemainToner; }
            set {
             newRemainToner = value; 
            } }
        public int RemainBlack
        {
            get { return remainBlack; }
            set {
             remainBlack = value; 
            }
        }
        public System.DateTime ChargeDate
        {
           get { return chargeDate; }
            set
            {
                if (value!=null)
                {
                    chargeDate = value;
                }
                /*else
                {
                    chargeDate = DateTime.Now;
                }*/
            }
            

        }

        public int NumberDays
        {
            get
            {
                return fNumberDays(newRemainToner, remainBlack, chargeDate);
            }
        }


        public string IssueDate
        {
            get { return (DateTime.Now.AddDays(  fNumberDays(newRemainToner, remainBlack, chargeDate)).ToShortDateString()); }
        }

        private int DateDiff(System.DateTime dtInit, System.DateTime dtEnd)
        {
            if (dtEnd < dtInit)
                throw new ArithmeticException("Init date should be previous to End date.");
            Debug.WriteLine(String.Format("dtInit={1}, Days={0}", Convert.ToInt32((dtEnd - dtInit).TotalDays).ToString(),dtInit.ToString()));

            
                    return Convert.ToInt32( (dtEnd - dtInit).TotalDays);
            
        }

        private int fNumberDays( int newRemainToner, int remainBlack,DateTime chargeDate)
        {
            int numberDays;
            
            if ((chargeDate.Year!=1) || (remainBlack>0))
            {
                int Raznost=newRemainToner - remainBlack;
                if (Raznost>0) 
                {
                    numberDays = (DateDiff(chargeDate, DateTime.Now) * remainBlack) / (Raznost);
                }
                else
                {
                    numberDays=365;
                }
            }
            else
            {
                numberDays = 0;
            }
            return numberDays;
            
        }
    }
}