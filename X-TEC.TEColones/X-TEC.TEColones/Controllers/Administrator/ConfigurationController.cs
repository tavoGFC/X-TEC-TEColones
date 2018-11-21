using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X_TEC.TEColones.Models.AdminModels;
using X_TEC.TEColones.Persistence;

namespace X_TEC.TEColones.Controllers.Administrator
{
    public class ConfigurationController : Controller
    {
        #region MainViewTabsMethods
        /// <summary>
        /// Get page of configuration values of the materials.
        /// </summary>
        /// <returns></returns>
        public ActionResult MaterialValueConfiguration()
        {
            AdminModel AdminModel = (AdminModel)TempData["admin"];
            AdminModel.ConfigurationModel = new ConfigurationViewModel();
            DBConnection.GetMaterialTCSValue(AdminModel.ConfigurationModel);
            return View("~/Views/Administrator/Configuration/MaterialConfig.cshtml", AdminModel);
        }

        /// <summary>
        /// Get page of configuration values of the TCS (second tab).
        /// </summary>
        /// <returns></returns>
        public ActionResult TCSValueConfiguration()
        {
            AdminModel AdminModel = (AdminModel)TempData["admin"];
            AdminModel.ConfigurationModel = new ConfigurationViewModel();
            DBConnection.GetBenefitsValue(AdminModel.ConfigurationModel);
            return View("~/Views/Administrator/Configuration/TEColonesConfig.cshtml", AdminModel);
        }

        /// <summary>
        /// Get page of configuration of the credentials of the twitter account.
        /// /// </summary>
        /// <returns></returns>
        public ActionResult TwitterConfiguration()
        {
            AdminModel AdminModel = (AdminModel)TempData["admin"];
            //ConfigurationViewModel Config = new ConfigurationViewModel();            
            return View("~/Views/Administrator/Configuration/TwitterConfig.cshtml", AdminModel);
        }
        #endregion

        #region Get&SendInformationDatabase
        /// <summary>
        /// Sends the new TCS values of the material from the input table and send them to the database.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetNewMaterialTCSValues()
        {
            AdminModel AdminModel = (AdminModel)TempData["admin"];
            float PlasticNewValue = float.Parse(Request["inputPlastic"].ToString());
            float PaperNewValue = float.Parse(Request["inputPaper"].ToString());
            float GlassNewValue = float.Parse(Request["inputGlass"].ToString());
            float AluminumNewValue = float.Parse(Request["inputAluminum"].ToString());

            DBConnection.InsertNewMaterialTCSValue(PlasticNewValue, PaperNewValue, GlassNewValue, AluminumNewValue);

            AdminModel.ConfigurationModel = new ConfigurationViewModel
            {
                PlasticValue = PlasticNewValue,
                PaperValue = PaperNewValue,
                GlassValue = GlassNewValue,
                AluminumValue = AluminumNewValue
            };
            return View("~/Views/Administrator/Configuration/MaterialConfig.cshtml", AdminModel);
        }

        /// <summary>
        /// Sends the new benefits values of the TCS from the input table and send them to the database.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetNewBenefitsValues()
        {
            AdminModel AdminModel = (AdminModel)TempData["admin"];
            float NewStudyExchange = float.Parse(Request["inputTCSStudy"].ToString());
            float NewDinningExchange = float.Parse(Request["inputTCSDinning"].ToString());

            DBConnection.InsertNewBenefitsValue(NewDinningExchange, NewStudyExchange);

            AdminModel.ConfigurationModel = new ConfigurationViewModel
            {
                StudyExchange = NewStudyExchange,
                DinningExchange = NewDinningExchange
            };
            return View("~/Views/Administrator/Configuration/TEColonesConfig.cshtml", AdminModel);
        }

        /// <summary>
        /// Sends the new Twitter data credentials of the input spaces and send them to the database.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetNewTwitterCredentials()
        {
            AdminModel AdminModel = (AdminModel)TempData["admin"];
            string NewCONSUMER_KEY = Request["inputConsumerKey"].ToString();
            string NewCONSUMER_SECRET = Request["inputConsumerSecret"].ToString();
            string NewACCESS_TOKEN = Request["inputAccessToken"].ToString();
            string NewACCESS_TOKEN_SECRET = Request["inputAccessTokenSecret"].ToString();

            DBConnection.InsertNewTwitterData(NewCONSUMER_KEY, NewCONSUMER_SECRET, NewACCESS_TOKEN, NewACCESS_TOKEN_SECRET);

            AdminModel.ConfigurationModel = new ConfigurationViewModel()
            {
                CONSUMER_KEY = NewCONSUMER_KEY,
                CONSUMER_SECRET = NewCONSUMER_SECRET,
                ACCESS_TOKEN = NewACCESS_TOKEN,
                ACCESS_TOKEN_SECRET = NewACCESS_TOKEN_SECRET
            };

            return View("~/Views/Administrator/Configuration/TwitterConfig.cshtml", AdminModel);
        }
        #endregion

    }
}