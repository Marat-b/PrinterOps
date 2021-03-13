using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrOps.Models
{
    public class SEL_YMDB : SEL_YMB 
    {
        private DataClassesPODataContext _dataContext;
        private int _iYear;
        private int _iMonth;
        private string _strBranch="";
        private int _iDay;

        public SEL_YMDB(int iYear,int iMonth, int iDay,string strBranch):base(iYear, iMonth,strBranch)
        {
            if (iYear > 0)
            {
                _iYear = iYear;
            }
            else
            {
                _iYear = DateTime.Today.Year;
            }
            if (iMonth > 0)
            {
                _iMonth = iMonth;
            }
            else
            {
                _iMonth = DateTime.Today.Month;
            }
            if (iDay > 0)
            {
                _iDay = iDay;
            }
            else
            {
                _iDay = DateTime.Today.Day;
            }



            if (string.IsNullOrWhiteSpace(strBranch) || string.IsNullOrEmpty(strBranch))
            {
                _strBranch = "";
            }
            else
            {
                _strBranch = strBranch;
            }

            _dataContext = new DataClassesPODataContext();

        }


        public int GetDay()
        {

            return _iDay;
        }

        public int DaysInMonth()
        {
            return DateTime.DaysInMonth(_iYear, _iMonth);
        }

        public IEnumerable<sp_SEL_YMDBResult> GetYMDB()
        {
            return _dataContext.sp_SEL_YMDB(_iYear, _iMonth,_iDay, _strBranch);
        }
                

        public IEnumerable<sp_SEL_YMDB_TOTALResult> GetYMDBTotal()
        {
            return _dataContext.sp_SEL_YMDB_TOTAL(_iYear, _iMonth,_iDay, _strBranch);
        }
    }
}