using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrOps.Classes
{
    public class USBPrinterState
    {
        private DataClassesPODataContext _db;
        public USBPrinterState(DataClassesPODataContext db)
        {
            _db = db;
        }

        public IEnumerable<sp_SEL_USBPrinterStateResult> USBState()
        {
            return _db.sp_SEL_USBPrinterState();
        }
    }
}