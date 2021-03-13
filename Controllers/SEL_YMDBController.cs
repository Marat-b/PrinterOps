using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrOps.Models;

namespace PrOps.Controllers
{
    public class SEL_YMDBController : Controller
    {
        //
        // GET: /SEL_YMDB/

        public ActionResult Index()
        {
            string strBranches;
            if (String.IsNullOrEmpty(Request.RequestContext.HttpContext.Request["Branches"]))
            {
                strBranches = "";
            }
            else
            {
                strBranches = Request.RequestContext.HttpContext.Request["Branches"].ToString();
            }
            SEL_YMDB sel_YMDB = new SEL_YMDB(Convert.ToInt32(Request.RequestContext.HttpContext.Request["Year"]), Convert.ToInt32(Request.RequestContext.HttpContext.Request["Month"]),Convert.ToInt32(Request.RequestContext.HttpContext.Request["Day"]), strBranches);
            return View(sel_YMDB);
        }

    }
}
