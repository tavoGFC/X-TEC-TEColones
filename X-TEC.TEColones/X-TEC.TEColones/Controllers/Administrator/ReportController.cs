using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace X_TEC.TEColones.Controllers.Administrator
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            return View("~/Views/Administrator/Report/Report.cshtml");
        }
    }
}