using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrOps.Models
{
    public class SEL_YMD : SEL_YM
    {
        private DataClassesPODataContext _dataContext;
        private int _iYear;
        private int _iMonth;
        private int _iDay;

        public SEL_YMD(int iYear,int iMonth,int iDay) : base(iYear,iMonth)
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
            _dataContext = new DataClassesPODataContext();

        }
        /*
        public int GetYear()
        {

            return _iYear;

        }

        public int GetMonth()
        {

            return _iMonth;
        }*/

        public int GetDay()
        {
            
            return _iDay;
        }

        public IEnumerable<sp_SEL_YMDResult> GetYMD()
        {
            return _dataContext.sp_SEL_YMD(_iYear, _iMonth,_iDay);
        }

        public int DaysInMonth()
        {
            return DateTime.DaysInMonth(_iYear, _iMonth);
        }

        public IEnumerable<sp_SEL_YMD_TOTALResult> GetYMDTotal()
        {
            return _dataContext.sp_SEL_YMD_TOTAL(_iYear, _iMonth,_iDay);
        }
    }
}