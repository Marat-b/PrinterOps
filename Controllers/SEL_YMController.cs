using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Linq;
using PrOps.Models;

namespace PrOps.Controllers
{
    public class SEL_YMController : Controller
    {
        //
        // GET: /SEL_YM/
        private DataClassesPODataContext _POContext = new DataClassesPODataContext();
        //private static SEL_YM_Input _sel_YM_Input = new SEL_YM_Input();
        //private SEL_YM_View _sel_YM_View=new SEL_YM_View();
        


        public ActionResult Index() //      int? iYear,int? iMonth
        {
            /*_sel_YM_Input.iMonth = iMonth;
            _sel_YM_Input.iYear = iYear;*/
            //SEL_YM_Input sel_YM_Input = new SEL_YM_Input();
            //return View(sel_YM_Input); // RedirectToAction("Index_View");
            /*if (ViewBag.Year==null)
            {
                ViewBag.Year = 2014;
            }
            if (ViewBag.Month==null)
            {
                ViewBag.Month = 1;
            }
            */
            /*ViewBag.Year=Request.RequestContext.HttpContext.Request["Year"];
            Request.RequestContext.HttpContext.Session["Year"] = Request.RequestContext.HttpContext.Request["Year"];
            _sel_YM_Input.iYear = Convert.ToInt32( Request.RequestContext.HttpContext.Request["Year"]);
            _sel_YM_Input.iMonth = Convert.ToInt32(Request.RequestContext.HttpContext.Request["Month"]);
            return View(_POContext.sp_SEL_YM(_sel_YM_Input.iYear, _sel_YM_Input.iMonth));*/
            //_sel_YM_View._POContext = new DataClassesPODataContext();
            //_sel_YM_View.SEL_YMResult = _POContext.sp_SEL_YM(sel_YM_View.iYear, sel_YM_View.iMonth);
            //_sel_YM_View._POContext.sp_SEL_YM(sel_YM_View.iYear, sel_YM_View.iMonth);
            SEL_YM ym = new SEL_YM(Convert.ToInt32(Request.RequestContext.HttpContext.Request["Year"]), Convert.ToInt32(Request.RequestContext.HttpContext.Request["Month"]));
            return View(ym);
        }

        /*.
        public ActionResult Index(SEL_YM_Input sel_YM_Input)
        {
            //SEL_YM _sel_YM = new SEL_YM();
            _sel_YM_Input.iYear = sel_YM_Input.iYear;
            _sel_YM_Input.iMonth = sel_YM_Input.iMonth;
            //ISingleResult < sp_SEL_YMResult > sel_YMRes = _POContext.sp_SEL_YM(_sel_YM_Input.iYear,_sel_YM_Input.iMonth );
            
            return this.RedirectToAction("Index_View");
            //return View(sel_YMRes.GetEnumerator());
        }
        */
        /*public ActionResult Index_View()
        {
            //ISingleResult<sp_SEL_YMResult> sel_YMRes = _POContext.sp_SEL_YM(2013, 10);
            return View(_POContext.sp_SEL_YM(_sel_YM_Input.iYear, _sel_YM_Input.iMonth));
        }*/

    }
}
