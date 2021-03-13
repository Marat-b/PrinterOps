using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrOps.Classes;
using PrOps.Models;

namespace PrOps.Controllers
{
    public class PrinterTonerCriticalController : Controller
    {
        //
        // GET: /PrinterTonerCritical/
        private DataClassesPODataContext _db;

        public PrinterTonerCriticalController()
        {
            _db = new DataClassesPODataContext();
        }

        public ActionResult Index()
        {
            PrinterToner printerToner = new PrinterToner(_db);
            return View(printerToner.StateTonerCritical());
        }

        public ActionResult PrinterTonerNeeded(int Months,int Percents)
        {
            //PrinterToner printerToner=new PrinterToner(_db);
            IEnumerable<PrinterTonerNeeded> printerTonerNeeded = (from p in _db.t_PrinterStates
                                                                  where _db.fn_PercentToner(p.MaxLevelBlack, p.RemainBlack) <= Percents && DateTime.Today.Month-Convert.ToDateTime(p.LastUpdate).Month<=Months 
                                                                  group p by p.Model into t
                                                                  select new PrinterTonerNeeded
                                                                  {
                                                                      Amount = t.Count(),
                                                                      Model = t.Key
                                                                  }).ToList();
            return View(printerTonerNeeded);
        }

    }
}
