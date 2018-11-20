using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X_TEC.TEColones.Models.AdminModels;

namespace X_TEC.TEColones.Controllers.Administrator
{
    public class PromotionController : Controller
    {
        // GET: Promotion
        public ActionResult ViewPromotion()
        {
            AdminModel AdminModel = (AdminModel)TempData["admin"];
            return View("~/Views/Administrator/Promotion/ViewPromotion.cshtml", AdminModel);
        }

        public ActionResult CreatePromotion()
        {
            AdminModel AdminModel = (AdminModel)TempData["admin"];
            return View("~/Views/Administrator/Promotion/CreatePromotion.cshtml", AdminModel);
        }
    }
}