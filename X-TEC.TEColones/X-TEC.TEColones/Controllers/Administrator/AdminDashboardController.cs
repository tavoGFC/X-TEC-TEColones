using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X_TEC.TEColones.Models.StudentModels;
using X_TEC.TEColones.Models.AdminModels;
using X_TEC.TEColones.Persistence;
using Newtonsoft.Json;

namespace X_TEC.TEColones.Controllers.Administrator
{
    public class AdminDashboardController : Controller
    {
        

        // GET: Dashboard
        public ActionResult Dashboard()
        {
            AdminModel AdminModel = (AdminModel)TempData["admin"];
            
            AdminModel.DashboardModel = new DashboardModel();
            
            //TONELADAS POR MES
            DBConnection.GetTonsMonth(AdminModel.DashboardModel);

            //USUARIOS POR MES
            DBConnection.GetUsersMonth(AdminModel.DashboardModel);

            //DINERO BENEFICIO
            DBConnection.GetMoneyMonth(AdminModel.DashboardModel);

            //VELOCIMETRO
            DBConnection.GetTonsPeriod(AdminModel.DashboardModel);

            //TOP10
            DBConnection.GetTopStudents(AdminModel.DashboardModel);
             
            //SEDES
            DBConnection.GetTonsCampuses(AdminModel.DashboardModel);

            return View("~/Views/Administrator/AdminDashboard/Dashboard.cshtml", AdminModel);

        }     

        
    }
}