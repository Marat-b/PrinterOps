using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrOps.Models;

namespace PrOps.Controllers
{
    public class SEL_YMDController : Controller
    {
        //
        // GET: /SEL_YMD/

        public ActionResult Index()
        {
            SEL_YMD sel_YMD=new SEL_YMD(Convert.ToInt32(Request.RequestContext.HttpContext.Request["Year"]), Convert.ToInt32(Request.RequestContext.HttpContext.Request["Month"]), Convert.ToInt32(Request.RequestContext.HttpContext.Request["Day"]));
            return View(sel_YMD);
        }

    }
}
