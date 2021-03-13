using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using PrOps.Models;


namespace PrOps.Controllers
{
    public class BranchSerialController : Controller
    {
        private DataClassesPODataContext _db;
        // GET: BranchSerial
        public BranchSerialController()
        {
            _db = new DataClassesPODataContext();
        }

        public ActionResult BranchSerialIndex()
        {
            IEnumerable<BranchSerialInputModel> model = from p in _db.t_PrinterStates
                                                        join b in _db.t_BranchSerials on p.SerialNumber equals b.SerialNumber into pb
                                                        from pb2 in pb.DefaultIfEmpty() orderby p.IPAddress 
                                                        select new BranchSerialInputModel()
                                                        {
                                                            SerialNumber = p.SerialNumber,
                                                            Model = p.Model,
                                                            IPAddress = p.IPAddress,
                                                            BranchNumber = pb2.BranchNumber
                                                        };
            return View(model);
        }

        // GET: BranchSerial/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BranchSerial/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BranchSerial/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BranchSerial/Edit/5
        public ActionResult BranchSerialEdit(string SerialNumber)
        {
            BranchSerialInputModel model = (from p in _db.t_PrinterStates
                                           join b in _db.t_BranchSerials on p.SerialNumber equals b.SerialNumber into pb
                                           from pb2 in pb.DefaultIfEmpty() 
                                           where p.SerialNumber==SerialNumber 
                                           select new BranchSerialInputModel()
                                           {
                                               SerialNumber = p.SerialNumber,
                                               Model = p.Model,
                                               IPAddress = p.IPAddress,
                                               BranchNumber = pb2.BranchNumber
                                           }).FirstOrDefault();
            return View(model);
        }

        // POST: BranchSerial/Edit/5
        [HttpPost]
        public ActionResult BranchSerialEdit(string SerialNumber,FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                //Debug.WriteLine("BranchName="+ collection.GetValue("BranchName"));
                Debug.WriteLine(SerialNumber);
                Debug.WriteLine(collection["BranchNumber"]);
                /*if (!(string.IsNullOrEmpty(SerialNumber)))
                {*/
                    _db.sp_MERGE_BranchSerial(collection["BranchNumber"].ToString(), SerialNumber);
                //}
                return RedirectToAction("BranchSerialIndex");
            }
            catch
            {
                return View();
            }
        }

        // GET: BranchSerial/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BranchSerial/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
