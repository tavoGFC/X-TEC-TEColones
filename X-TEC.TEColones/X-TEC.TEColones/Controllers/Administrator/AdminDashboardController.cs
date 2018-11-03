using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace X_TEC.TEColones.Controllers.Administrator
{
    public class AdminDashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            return View("~/Views/Administrator/AdminDashboard/Index.cshtml");
        }
    }
}