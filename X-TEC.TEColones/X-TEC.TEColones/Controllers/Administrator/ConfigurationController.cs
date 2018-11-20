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

        /// <summary>
        /// Get page of configuration values of the materials.
        /// </summary>
        /// <returns></returns>
        public ActionResult MaterialValueConfiguration()
        {
            ConfigurationModel Config = new ConfigurationModel();
            //DBConnection.GetMaterialTCSValue(Config);
            return View("~/Views/Administrator/Configuration/MaterialConfig.cshtml", Config);
        }

        /// <summary>
        /// /// Get page of configuration values of the TCS
        /// </summary>
        /// <returns></returns>
        public ActionResult TCSValueConfiguration()
        {
            ConfigurationModel Config = new ConfigurationModel();
            //DBConnection.GetBenefitsValue(Config);
            return View("~/Views/Administrator/Configuration/TEColonesConfig.cshtml", Config);
        }

        /// <summary>
        /// Get page of configuration of the credentials of the twitter account.
        /// /// </summary>
        /// <returns></returns>
        public ActionResult TwitterConfiguration()
        {
            ConfigurationModel Config = new ConfigurationModel();
            //DBConnection.GetTwitterData(Config);
            return View("~/Views/Administrator/Configuration/TwitterConfig.cshtml", Config);
        }

        
        /// <summary>
        /// Sends the new TCS values of the material from the input table and send them to the database.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetNewMaterialTCSValues()
        {
            //float PlasticNewValue = float.Parse(Request["inputPlastic"].ToString());
            //float PaperNewValue = float.Parse(Request["inputPlastic"].ToString());
            //float GlassNewValue = float.Parse(Request["inputPlastic"].ToString());
            //float AluminumNewValue = float.Parse(Request["inputPlastic"].ToString());

            //DBConnection.InsertMaterialTCSValue(PlasticNewValue, PaperNewValue, GlassNewValue, AluminumNewValue);

            return MaterialValueConfiguration();
        }

        
        [HttpPost]
        public ActionResult GetNewExchangeTCSValues()
        {
            return TCSValueConfiguration();
        }
        
    }
}