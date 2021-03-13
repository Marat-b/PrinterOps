using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrOps.Models;
using AutoMapper;

namespace PrOps.Controllers
{
    public class NeededCartrigeController : Controller
    {
        // GET: NeededCartrige
        private DataClassesPODataContext _db;

        public  NeededCartrigeController()
        {
            _db = new DataClassesPODataContext();
        }
        /*
        public ActionResult Index()
        {
            IEnumerable<sp_SEL_RemainDaysResult> remainDays=_db.sp_SEL_RemainDays();
            IEnumerable<RemainDaysView> remainDaysView = new List<RemainDaysView>();
            Mapper.CreateMap<sp_SEL_RemainDaysResult, RemainDaysView>();
            Mapper.Map(remainDays, remainDaysView);
            return View(remainDaysView.OrderBy(x=>x.NumberDays).ToList());
        }*/

        public ActionResult Index()
        {
            IEnumerable<sp_SEL_RemainDays2Result> remainDays = _db.sp_SEL_RemainDays2();
            IEnumerable<RemainDaysView2> remainDaysView = new List<RemainDaysView2>();
            Mapper.CreateMap<sp_SEL_RemainDays2Result, RemainDaysView2>();
            Mapper.Map(remainDays, remainDaysView);
            return View(remainDaysView.OrderBy(x => x.NumberDays).ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}