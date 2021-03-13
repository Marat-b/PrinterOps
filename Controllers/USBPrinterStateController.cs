using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrOps.Classes;

namespace PrOps.Controllers
{
    public class USBPrinterStateController : Controller
    {
        //
        // GET: /USBPrinterState/
        private DataClassesPODataContext _db;
        public USBPrinterStateController()
        {
            _db = new DataClassesPODataContext();
        }


        public ActionResult USBState()
        {
            USBPrinterState printerState = new USBPrinterState(_db);
            return View(printerState.USBState());
        }

    }
}
