using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PrOps.Models;

namespace PrOps.Classes
{
    public class PrinterLoad
    {
        private DataClassesPODataContext _db;
        //private int _Year;
        public PrinterLoad(DataClassesPODataContext db)
        {
            _db = db;
        }

        public IEnumerable<sp_SEL_LOAD_YResult> PrinterState(int iYear)
        {
            return _db.sp_SEL_LOAD_Y(iYear);
        }

        public IEnumerable<sp_SEL_LOAD_YMResult> PrinterState(int iYear,int iMonth)
        {
            return _db.sp_SEL_LOAD_YM(iYear,iMonth);
        }
    }
}