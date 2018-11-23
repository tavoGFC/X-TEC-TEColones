using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X_TEC.TEColones.Models.AdminModels;
using X_TEC.TEColones.Persistence;

namespace X_TEC.TEColones.Controllers.Administrator
{
    public class PromotionController : Controller
    {
        #region MainViewTabsMethods
        public ActionResult CreateSinglePromotion()
        {
            AdminModel AdminModel = (AdminModel)TempData["admin"];
            AdminModel.PromotionModel = new PromotionViewModel();
            DBConnection.GetMaterialType(AdminModel.PromotionModel);
            return View("~/Views/Administrator/Promotion/CreateSinglePromotion.cshtml", AdminModel);
        }

        public ActionResult CreateComboPromotion()
        {
            AdminModel AdminModel = (AdminModel)TempData["admin"];
            AdminModel.PromotionModel = new PromotionViewModel();
            DBConnection.GetMaterialType(AdminModel.PromotionModel);
            return View("~/Views/Administrator/Promotion/CreateComboPromotion.cshtml", AdminModel);
        }

        public ActionResult ViewSinglePromotion()
        {
            AdminModel AdminModel = (AdminModel)TempData["admin"];
            return View("~/Views/Administrator/Promotion/ViewSinglePromotion.cshtml", AdminModel);
        }

        public ActionResult ViewComboPromotion()
        {
            AdminModel AdminModel = (AdminModel)TempData["admin"];
            return View("~/Views/Administrator/Promotion/ViewComboPromotion.cshtml", AdminModel);
        }
        #endregion

        #region NewSinglePromotionMethods

        public ActionResult NewSinglePromotion()
        {
            AdminModel AdminModel = (AdminModel)TempData["admin"];
            AdminModel.PromotionModel = new PromotionViewModel();

            //string TypeMaterial = Request[""]

            return View("~/Views/Administrator/Promotion/CreateSinglePromotion.cshtml",AdminModel); 
        }
        #endregion

        #region NewComboPromotionMethods
        public ActionResult NewComboPromotion()
        {
            return View("~/Views/Administrator/Promotion/CreateSinglePromotion.cshtml");
        }

        #endregion
    }
}