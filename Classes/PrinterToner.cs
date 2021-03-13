using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrOps.Classes
{
    public class PrinterToner
    {
        private DataClassesPODataContext _db;
        public PrinterToner(DataClassesPODataContext db)
        {
            _db = db;
        }

        public IEnumerable<sp_SEL_TONERResult> StateToner()
        {
            return _db.sp_SEL_TONER();
        }
        /*
        public IEnumerable<sp_SEL_TONER_CriticalResult> StateTonerCritical()
        {
            return _db.sp_SEL_TONER_Critical();
        }*/
    }
}