using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using X_TEC.TEColones.Models.AdminModels;
using X_TEC.TEColones.Persistence;

namespace X_TEC.TEColones.Controllers.Administrator
{
    public class AdminHomeController : Controller
    {

        // GET: AdminHome
        [OutputCache(NoStore = true, Location = OutputCacheLocation.Client, Duration = 1)]
        public ActionResult Home()
        {
            AdminModel AdminModel = (AdminModel)TempData["admin"];
            AdminModel.DashboardModel = new DashboardModel();
            DBConnection.GetTonsPeriod(AdminModel.DashboardModel);
            return View("~/Views/Administrator/AdminHome/AdminHome.cshtml", AdminModel);
        }
    }
}