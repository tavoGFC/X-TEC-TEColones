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
            AdminChang adminChang = new AdminChang(); //aqui iria el tempdata[admin]

            adminChang.Email = "holi@wawa.com";
            adminChang.Department = "Dep de pendejadas";
            adminChang.FirstName = "miNombre";
            adminChang.LastName = "miApellido";
            adminChang.Id = 12345;

            return View("~/Views/Administrator/AdminDashboard/Index.cshtml", adminChang);
        }
    }
}