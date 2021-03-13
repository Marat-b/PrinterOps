using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrOps.Classes;

namespace PrOps.Controllers
{
    public class PrinterStateController : Controller
    {
        //
        // GET: /PrinterSate/
        private DataClassesPODataContext _db;
        public PrinterStateController()
        {
            _db = new DataClassesPODataContext();
        }

        public ActionResult State()
        {
            PrinterState printerState = new PrinterState(_db);
            return View(printerState.State());
        }

    }
}
