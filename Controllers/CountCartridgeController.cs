using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrOps.Models;
using AutoMapper;
using System.Diagnostics;

namespace PrOps.Controllers
{
    public class CountCartridgeController : Controller
    {
        private DataClassesPODataContext _db;

        public CountCartridgeController()
        {
            _db = new DataClassesPODataContext();
        }
        
        // GET: CountCartridge
        
        public ActionResult CountCartridge(string datetimepicker)
        {
            Debug.WriteLine(string.Format("datetimepicker={0}", datetimepicker));
            string dateTimePicker;
            if (datetimepicker!=null)
            {
                ViewBag.DTP = datetimepicker;
                dateTimePicker = datetimepicker;
            }
            else
            {
                ViewBag.DTP = DateTime.Today.ToShortDateString();
                dateTimePicker = DateTime.Today.ToShortDateString(); 
            }
            IEnumerable<sp_SEL_RemainDays2Result> remainDays = _db.sp_SEL_RemainDays2();
            //CartridgeStateView cartridgeStateView = new CartridgeStateView(dateTimePicker);
            //IEnumerable<CartridgeStateView> csv=cartridgeStateView.
            IEnumerable<CartridgeStateView2> cartridgeStateView = new List<CartridgeStateView2>() ;
           /* foreach(var item in cartridgeStateView)
            {
                item.InputDate = dateTimePicker;
            }*/
            Mapper.CreateMap<sp_SEL_RemainDays2Result, CartridgeStateView2>().ConstructProjectionUsing(x=>new CartridgeStateView2(dateTimePicker)); //.ConstructUsing(new CartridgeStateView(dateTimePicker));
            Mapper.Map(remainDays, cartridgeStateView);
            return View(cartridgeStateView.Where(x=>x.CountCartridge>0).OrderBy(x=>x.CountCartridge).ToList());
        }
       /*
        [HttpPost]
        public ActionResult CountCartridge(string datetimepicker)
        {
            string dateTimePicker = datetimepicker;
            return View(dateTimePicker);

        }*/

        public ActionResult ReportCountCartridge(string inputDate)
        {
            IEnumerable<sp_SEL_RemainDaysResult> remainDays = _db.sp_SEL_RemainDays();
            IEnumerable<CartridgeStateView> cartridgeStateView = new List<CartridgeStateView>();
            ViewBag.inputDate = inputDate;
            Mapper.CreateMap<sp_SEL_RemainDaysResult, CartridgeStateView>().ConstructProjectionUsing(x => new CartridgeStateView(inputDate)); 
            Mapper.Map(remainDays, cartridgeStateView);
            IEnumerable<ReportCartridgeStateView> reportCartridgeStateView = cartridgeStateView.Where(w=>w.CountCartridge>0).GroupBy(g => g.Model).Select(g => new ReportCartridgeStateView() { Model = g.First().Model, Sum = g.Sum(s => s.CountCartridge) });
            return View( reportCartridgeStateView.ToList());
        }

        
        public ActionResult ReportCountCartridge2(string inputDate)
        {
            //for sending to AHO
            ViewBag.inputDate = inputDate;
            IEnumerable<sp_SEL_RemainDays2Result> remainDays = _db.sp_SEL_RemainDays2();
            IEnumerable<CartridgeStateView2> cartridgeStateView = new List<CartridgeStateView2>();
            ViewBag.inputDate = inputDate;
            Mapper.CreateMap<sp_SEL_RemainDays2Result, CartridgeStateView2>().ConstructProjectionUsing(x => new CartridgeStateView2(inputDate));
            Mapper.Map(remainDays, cartridgeStateView);
            IEnumerable<ReportCartridgeStateView2> reportCartridgeStateView = cartridgeStateView.Where(w => w.CountCartridge > 0).GroupBy(gb=> new {gb.BranchNumber, gb.Model}).Select(g => new ReportCartridgeStateView2() {BranchNumber=g.First().BranchNumber, Model = g.First().Model, Sum = g.Sum(s => s.CountCartridge) }).OrderBy(o=>o.BranchNumber);
            /*IEnumerable<ReportCartridgeStateView2> reportCartridgeStateView = from c in cartridgeStateView
                                                                              where c.CountCartridge > 0
                                                                              group c by new { c.BranchNumber, c.Model } into g
                                                                              select new ReportCartridgeStateView2()
                                                                              {
                                                                                  BranchNumber = g.Key.BranchNumber,
                                                                                  Model = g.Key.Model,
                                                                                  Sum = g.Sum(s => s.CountCartridge)
                                                                              };*/
            return View(reportCartridgeStateView.ToList());
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