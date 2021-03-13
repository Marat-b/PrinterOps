using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrOps.Classes;

namespace PrOps.Controllers
{
    public class PrinterLoadYMController : Controller
    {

        private DataClassesPODataContext _db;
        private static int _iYear=0;
        private static int _iMonth = 0;

        public PrinterLoadYMController()
        {
            _db = new DataClassesPODataContext();
        }
        //
        // GET: /PrinterLoadYM/

        public ActionResult Index()
        {
            if (_iYear == 0)
            {
                _iYear = DateTime.Today.Year;
            }
            if (_iMonth == 0)
            {
                _iMonth = DateTime.Today.Month;
            }
            ViewBag.GetYear = _iYear;
            ViewBag.GetMonth = _iMonth;
            PrinterLoad printerLoad = new PrinterLoad(_db);
            IEnumerable<sp_SEL_LOAD_YMResult> m_load_View = printerLoad.PrinterState(_iYear,_iMonth);
            return View(m_load_View);
            
        }

        [HttpPost]
        public ActionResult Index(int Year,int Month)
        {
            try
            {
                // TODO: Add update logic here
                _iYear = Year;
                _iMonth = Month;

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
