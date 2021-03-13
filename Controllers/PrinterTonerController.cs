using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrOps.Classes;
using PrOps.Models;

namespace PrOps.Controllers
{
    public class PrinterTonerController : Controller
    {
        //
        // GET: /PrinterToner/
        private DataClassesPODataContext _db;

        public PrinterTonerController()
        {
            _db = new DataClassesPODataContext();
            _db.CommandTimeout = 500;
        }

        public ActionResult Index()
        {
            PrinterToner printerToner = new PrinterToner(_db);
            return View(printerToner.StateToner());
        }

        public ActionResult ChargeCartridge()
        {
            PrinterToner printerToner = new PrinterToner(_db);
            string strSerialNumber = Request.QueryString["SerialNumber"].ToString();
            IEnumerable<ChargeCartridge> chargeCartridge = (from c in _db.t_ChargeCartridges
                                                            where c.SerialNumber == strSerialNumber
                                                            orderby c.ChargeDate
                                                            select new ChargeCartridge
                                                            {
                                                                OldRemainToner = c.OldRemainToner,
                                                                NewRemainToner = c.NewRemainToner,
                                                                PageCount = c.PageCount,
                                                                ColorToner = c.ColorToner,
                                                                ChargeDate = c.ChargeDate
                                                            }).ToList();
            return PartialView(chargeCartridge);


        }

    }
}
