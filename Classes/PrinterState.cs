using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrOps.Classes
{
    public class PrinterState
    {
        private DataClassesPODataContext _db;
        public PrinterState(DataClassesPODataContext db)
        {
            _db = db;
        }

        public IEnumerable<sp_SEL_PrinterStateResult> State()
        {
            return _db.sp_SEL_PrinterState();
        }
    }
}