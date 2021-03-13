using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrOps.Models
{
    public class SEL_YMB : SEL_YM
    {
        private DataClassesPODataContext _dataContext;
        private int _iYear;
        private int _iMonth;
        private string _strBranch="";
        //private YM _YM;
        public SEL_YMB(int iYear,int iMonth, string strBranch):base(iYear, iMonth)
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

        public string GetBranch()
        {
            return _strBranch;
        }

        public IEnumerable<sp_SEL_YMBResult> GetYMB()
        {
            return _dataContext.sp_SEL_YMB(_iYear, _iMonth, _strBranch);
        }

        public IEnumerable<sp_SEL_BRANCHResult> GetBranches()
        {
            return _dataContext.sp_SEL_BRANCH();
        }

        public IEnumerable<sp_SEL_YMB_TOTALResult> GetYMBTotal()
        {
            return _dataContext.sp_SEL_YMB_TOTAL(_iYear, _iMonth, _strBranch);
        }
    }
}