using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X_TEC.TEColones.Models.AdminModels;

namespace X_TEC.TEColones.Controllers.Administrator
{
    public class AdminDashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            AdminModel AdminModel = (AdminModel)TempData["admin"];
            return View("~/Views/Administrator/AdminDashboard/Index.cshtml", AdminModel);
        }
    }
}