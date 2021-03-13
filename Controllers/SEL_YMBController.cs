using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrOps.Models;

namespace PrOps.Controllers
{
    public class SEL_YMBController : Controller
    {
        //
        // GET: /SEL_YMB/

        public ActionResult Index()
        {
            string strBranches;
            if (String.IsNullOrEmpty(Request.RequestContext.HttpContext.Request["Branches"]))
            { 
                strBranches="";
            }
            else
            {
                strBranches= Request.RequestContext.HttpContext.Request["Branches"].ToString();
            }
            SEL_YMB sel_YMB = new SEL_YMB(Convert.ToInt32(Request.RequestContext.HttpContext.Request["Year"]), Convert.ToInt32(Request.RequestContext.HttpContext.Request["Month"]), strBranches);
            return View(sel_YMB);
        }

    }
}
