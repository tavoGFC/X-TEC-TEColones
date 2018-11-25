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

        /// <summary>
        /// Gets the page of creating new single promotions. 
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateSinglePromotion()
        {
            AdminModel AdminModel = (AdminModel)TempData["admin"];
            AdminModel.PromotionModel = new PromotionViewModel();
            DBConnection.GetMaterialType(AdminModel.PromotionModel);
            return View("~/Views/Administrator/Promotion/CreateSinglePromotion.cshtml", AdminModel);
        }

        /// <summary>
        /// Gets the page of creating new combo promotions. 
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateComboPromotion()
        {
            AdminModel AdminModel = (AdminModel)TempData["admin"];
            AdminModel.PromotionModel = new PromotionViewModel();
            DBConnection.GetMaterialType(AdminModel.PromotionModel);
            return View("~/Views/Administrator/Promotion/CreateComboPromotion.cshtml", AdminModel);
        }

        /// <summary>
        /// Gets the page of viewing all the single promotions created, retrieved from the database. 
        /// </summary>
        /// <returns></returns>
        public ActionResult ViewSinglePromotion()
        {
            AdminModel AdminModel = (AdminModel)TempData["admin"];
            AdminModel.PromotionModel = new PromotionViewModel();
            DBConnection.GetPromotion(AdminModel.PromotionModel, "single");
            return View("~/Views/Administrator/Promotion/ViewSinglePromotion.cshtml", AdminModel);
        }

        /// <summary>
        /// Gets the page of viewing all the combo promotions created, retrieved from the database. 
        /// </summary>
        /// <returns></returns>
        public ActionResult ViewComboPromotion()
        {
            AdminModel AdminModel = (AdminModel)TempData["admin"];
            AdminModel.PromotionModel = new PromotionViewModel();
            DBConnection.GetPromotion(AdminModel.PromotionModel, "combo");
            return View("~/Views/Administrator/Promotion/ViewComboPromotion.cshtml", AdminModel);
        }
        #endregion

        #region NewPromotionMethods

        /// <summary>
        /// Gets the information of a new single promotion and send it to the database. 
        /// </summary>
        /// <returns></returns>
        public ActionResult NewSinglePromotion()
        {
            AdminModel AdminModel = (AdminModel)TempData["admin"];

            // get the type of material
            string materialType = Request["materialType"].ToString();

            // get the amount of kg of the material
            int amountKg = int.Parse(Request["inputAmountKg"].ToString());

            // get the value of tcs of the promotion
            float valueTCS = float.Parse(Request["inputValueTCS"].ToString());

            // get the datetime of the promotion
            string finishDate = Request["inputFinishDate"].ToString();

            // value of the active state of the promotion: 1=active, 0=notactive; by default is 0
            int activeValue = 0;

            // if the user wants the promotion to be active and publish now, else not
            if (Request["publish"] != null && Request["save"] == null)
            {
                activeValue = 1;
            }

            // if the user let one option blank is shown a message
            if (Request["publish"] == null && Request["save"] == null)
            {
                ViewBag.Msj = "Se debe seleccionar si desea Activar o Almacenar.";
            }

            // if the user wants the promotion to be save and ketp for later
            if (Request["publicar"] == null && Request["almacenar"] != null)
            {
                activeValue = 0;
            }

            // send the information to the database
            DBConnection.InsertNewPromotion(AdminModel.Id, valueTCS, finishDate, activeValue, 1);
            DBConnection.GetNewestIdPromotion(AdminModel.PromotionModel);
            int IdPromotion = AdminModel.PromotionModel.LatestIdPromotion;
            DBConnection.InsertPromosMaterial(IdPromotion, materialType, amountKg);

            // if the promotion is active, make it a tweet
            if (activeValue == 1)
            {
                TwitterConnection.SetCredentials();
                TwitterConnection.Publish(
                    "Hay una nueva promocion de: " + valueTCS + " TEColones. Consiste en entregar: " +
                    amountKg + " kg de " + materialType +". La promocion es valida hasta: " + finishDate
                );
            }
            return View("~/Views/Administrator/Promotion/CreateSinglePromotion.cshtml", AdminModel);
        }

        /// <summary>
        /// Gets the information of a new combo promotion and send it to the database. 
        /// </summary>
        /// <returns></returns>
        public ActionResult NewComboPromotion()
        {
            AdminModel AdminModel = (AdminModel)TempData["admin"];

            Dictionary<string, int> dict = new Dictionary<string, int>();

            // checks the value of the inputs: name, amount of kg, the checkbox
            foreach (var item in AdminModel.PromotionModel.ListMaterials)
            {
                string materialType = item; // which type of material is, the name of the material
                string amountKg = Request[item]; // the amount, float, of kg of the material
                string checkBox = Request["checkbox " + item]; // the value of the checkbox

                if (amountKg != " " && checkBox != null)
                {
                    int amountKgInt = int.Parse(amountKg); // converts the string type of amount to float for the sending it to the database
                    dict.Add(materialType, amountKgInt);
                }
                else {
                    continue;
                }
            }

            // to be a combo at least must be two materials, else proceed to choose again. 
            if (dict.Count < 2)
            {
                ViewBag.Msj = "Para crear una PromocionCombo minimo son dos materiales. Si desea solo un material, proceda a la seccion de Promo Individual.";
            }

            // checks and gets the value of the tcs and the datetime of it
            float valueTCS = float.Parse(Request["inputValueTCS"]);
            string finishDate = Request["inputFinishDate"].ToString();

            // value of the active state of the promotion: 1=active, 0=notactive; by default is 0
            int activeValue = 0;

            // if the user wants the promotion to be active and publish now, else not
            if(Request["publish"] != null && Request["save"] == null)
            {
                activeValue = 1;
            }

            // if the user let one option blank is shown a message
            if (Request["publish"] == null && Request["save"] == null)
            {
                ViewBag.Msj = "Se debe seleccionar si desea Activar o Almacenar.";
            }
            
            // if the user wants the promotion to be save and ketp for later
            if (Request["publicar"] == null && Request["almacenar"] != null)
            {
                activeValue = 0;
            }

            // send the information to the database
            DBConnection.InsertNewPromotion(AdminModel.Id, valueTCS, finishDate, activeValue, 0);
            DBConnection.GetNewestIdPromotion(AdminModel.PromotionModel);
            int IdPromotion = AdminModel.PromotionModel.LatestIdPromotion;

            foreach (var item in dict)
            {
                DBConnection.InsertPromosMaterial(IdPromotion, item.Key, item.Value);
            }

            // if the promotion is active, make it a tweet
            if (activeValue == 1)
            {
                TwitterConnection.SetCredentials();
                TwitterConnection.Publish("Hay una nueva Promocion en Combo de: " + valueTCS + " TEColones. Consiste en entregar: ");
                foreach (var item in dict)
                {
                    TwitterConnection.Publish("Entregar " + item.Value + " kg " + " de " + item.Key);
                }
                TwitterConnection.Publish("La promocion es valida hasta: " + finishDate);
            }
            
            return View("~/Views/Administrator/Promotion/CreateComboPromotion.cshtml", AdminModel);
        }
        #endregion

        #region EditMethods

        public ActionResult EditSinglePromotion()
        {
            AdminModel AdminModel = (AdminModel)TempData["admin"];
            return View("~/Views/Administrator/Promotion/ViewSinglePromotion.cshtml", AdminModel);
        }

        public ActionResult EditComboPromotion()
        {
            AdminModel AdminModel = (AdminModel)TempData["admin"];
            return View("~/Views/Administrator/Promotion/ViewComboPromotion.cshtml", AdminModel);
        }
        #endregion
    }
}