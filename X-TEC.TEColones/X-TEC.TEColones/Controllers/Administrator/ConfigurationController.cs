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
            ConfigurationModel Config = new ConfigurationModel();
            DBConnection.GetMaterialTCSValue(Config);
            return View("~/Views/Administrator/Configuration/MaterialConfig.cshtml", Config);
        }

        /// <summary>
        /// Get page of configuration values of the TCS (second tab).
        /// </summary>
        /// <returns></returns>
        public ActionResult TCSValueConfiguration()
        {
            ConfigurationModel Config = new ConfigurationModel();
            DBConnection.GetBenefitsValue(Config);
            return View("~/Views/Administrator/Configuration/TEColonesConfig.cshtml", Config);
        }

        /// <summary>
        /// Get page of configuration of the credentials of the twitter account.
        /// /// </summary>
        /// <returns></returns>
        public ActionResult TwitterConfiguration()
        {
            ConfigurationModel Config = new ConfigurationModel();            
            return View("~/Views/Administrator/Configuration/TwitterConfig.cshtml", Config);
        }
        #endregion

        #region UpdatedMainViewTabs
        public ActionResult UpdatedMaterialValueConfiguration(ConfigurationModel Config)
        {
            return View("~/Views/Administrator/Configuration/MaterialConfig.cshtml", Config);

        }

        public ActionResult UpdatedTCSValueConfiguration(ConfigurationModel Config)
        {
            return View("~/Views/Administrator/Configuration/TEColonesConfig.cshtml", Config);
        }

        public ActionResult UptatedTwitterConfiguration(ConfigurationModel Config)
        {
            return View("~/Views/Administrator/Configuration/TwitterConfig.cshtml", Config);

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
            float PlasticNewValue = float.Parse(Request["inputPlastic"].ToString());
            float PaperNewValue = float.Parse(Request["inputPaper"].ToString());
            float GlassNewValue = float.Parse(Request["inputGlass"].ToString());
            float AluminumNewValue = float.Parse(Request["inputAluminum"].ToString());

            DBConnection.InsertNewMaterialTCSValue(PlasticNewValue, PaperNewValue, GlassNewValue, AluminumNewValue);

            ConfigurationModel Config = new ConfigurationModel
            {
                PlasticValue = PlasticNewValue,
                PaperValue = PaperNewValue,
                GlassValue = GlassNewValue,
                AluminumValue = AluminumNewValue
            };
            return UpdatedMaterialValueConfiguration(Config);
        }

        [HttpPost]
        public ActionResult GetNewBenefitsValues()
        {
            float NewStudyExchange = float.Parse(Request["inputTCSStudy"].ToString());
            float NewDinningExchange = float.Parse(Request["inputTCSDinning"].ToString());

            DBConnection.InsertNewBenefitsValue(NewDinningExchange, NewStudyExchange);

            ConfigurationModel Config = new ConfigurationModel
            {
                StudyExchange = NewStudyExchange,
                DinningExchange = NewDinningExchange
            };
            return UpdatedTCSValueConfiguration(Config);
        }

        [HttpPost]
        public ActionResult GetNewTwitterCredentials()
        {
            string NewCONSUMER_KEY = Request["inputConsumerKey"].ToString();
            string NewCONSUMER_SECRET = Request["inputConsumerSecret"].ToString();
            string NewACCESS_TOKEN = Request["inputAccessToken"].ToString();
            string NewACCESS_TOKEN_SECRET = Request["inputAccessTokenSecret"].ToString();

            DBConnection.InsertNewTwitterData(NewCONSUMER_KEY, NewCONSUMER_SECRET, NewACCESS_TOKEN, NewACCESS_TOKEN_SECRET);

            ConfigurationModel Config = new ConfigurationModel()
            {
                CONSUMER_KEY = NewCONSUMER_KEY,
                CONSUMER_SECRET = NewCONSUMER_SECRET,
                ACCESS_TOKEN = NewACCESS_TOKEN,
                ACCESS_TOKEN_SECRET = NewACCESS_TOKEN_SECRET
            };
            return UptatedTwitterConfiguration(Config);
        }
        #endregion

    }
}