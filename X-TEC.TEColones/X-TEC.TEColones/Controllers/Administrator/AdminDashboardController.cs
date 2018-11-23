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

            AdminModel adminChang = new AdminModel
            {

                //adminChang.Email = "holi@wawa.com";
                Department = "Dep de pendejadas",
                FirstName = "miNombre",
                LastName = "miApellido",
                Id = 12345
            }; //aqui iria el tempdata[admin]

            return View("~/Views/Administrator/AdminDashboard/Index.cshtml", adminChang);
            
        }
    }
}