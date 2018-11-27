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
        //"Sede:CA Tonelada:120/Sede:CA Tonelada:150/Sede:CA Tonelada:180/Sede:CA Tonelada:190"
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
            return View("~/Views/Administrator/Configuration/TwitterConfig.cshtml", AdminModel);
        }
        #endregion

        #region Get&SendMaterialsTCSValuesToDatabase
        /// <summary>
        /// Sends the new TCS values of the material from the input table and send them to the database.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetNewMaterialTCSValues()
        {
            AdminModel AdminModel = (AdminModel)TempData["admin"];
            
            TwitterConnection.SetCredentials();
            foreach (KeyValuePair<string, float> item in AdminModel.ConfigurationModel.Materials)
            {
                string newValue = Request[item.Key];
                if (!string.IsNullOrWhiteSpace(newValue))
                {                   
                    string name = item.Key;
                    DBConnection.InsertNewMaterialTCSValue(name, float.Parse(newValue));
                    string message = String.Format("La tasa de cambio del material {0} por kg es: {1} TEColones", name, newValue);
                    TwitterConnection.Publish(message);
                }
            }
            DBConnection.GetMaterialTCSValue(AdminModel.ConfigurationModel);
            return View("~/Views/Administrator/Configuration/MaterialConfig.cshtml", AdminModel);           
        }
        #endregion

        #region Get&SendBenefitsTCSValuesToDatabase

        /// <summary>
        /// Sends the new benefits values of the TCS from the input table and send them to the database.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetNewBenefitsValues()
        {
            AdminModel AdminModel = (AdminModel)TempData["admin"];
            string NewStudyExchange = Request["inputTCSStudy"];
            string NewDinningExchange = Request["inputTCSDinning"];

            TwitterConnection.SetCredentials();
            if (!string.IsNullOrWhiteSpace(NewStudyExchange))
            {
                DBConnection.InsertNewBenefitsValue(float.Parse(NewStudyExchange), "Matricula");
                TwitterConnection.Publish("La tasa de cambio de TEColones en los Derechos de Estudio (Matricula) es: " + NewStudyExchange + " colones");
            }
            if (!string.IsNullOrWhiteSpace(NewDinningExchange))
            {
                DBConnection.InsertNewBenefitsValue(float.Parse(NewDinningExchange), "Comedor");
                TwitterConnection.Publish("La tasa de cambio de TEColones en el Comedor Intitucional es: " + NewDinningExchange + " colones");
            }
            
            DBConnection.GetBenefitsValue(AdminModel.ConfigurationModel);
            return View("~/Views/Administrator/Configuration/TEColonesConfig.cshtml", AdminModel);
        }
        #endregion

        #region Get&SendTwitterCredentialsToDatabase

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

            TwitterConnection.SetCredentials(NewCONSUMER_KEY, NewCONSUMER_SECRET, NewACCESS_TOKEN, NewACCESS_TOKEN_SECRET);

            return View("~/Views/Administrator/Configuration/TwitterConfig.cshtml", AdminModel);
        }
        #endregion

    }
}