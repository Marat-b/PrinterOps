using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrOps.Models;
using PrOps.Classes;

namespace PrOps.Controllers
{
    
    public class PrinterLoadYController : Controller
    {
        //
        // GET: /PrinterLoad/
        private DataClassesPODataContext _db;
        private static int _iYear=0;

        public PrinterLoadYController()
        {
            _db = new DataClassesPODataContext();
        }



        public ActionResult Index()
        {
            if (_iYear == 0 )
            {
                _iYear = DateTime.Today.Year;
            }
            ViewBag.GetYear = _iYear;
            PrinterLoad printerLoad = new PrinterLoad(_db);
            IEnumerable<sp_SEL_LOAD_YResult> m_load_View =  printerLoad.PrinterState(_iYear);
            return View(m_load_View);
        }


        [HttpPost]
        public ActionResult Index(int Year)
        {
            try
            {
                // TODO: Add update logic here
                _iYear = Year;

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        //
        // GET: /PrinterLoad/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        
        
        //
        // GET: /PrinterLoad/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /PrinterLoad/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        
 
    }
}
