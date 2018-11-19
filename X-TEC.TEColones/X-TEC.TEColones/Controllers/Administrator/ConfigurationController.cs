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
            DBConnection.GetMaterialTCSValue(Config);
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine
                (
                Config.PlasticValue.ToString(),
                Config.PaperValue.ToString(), 
                Config.GlassValue.ToString(), 
                Config.AluminumValue.ToString()
                );
            return View("~/Views/Administrator/Configuration/MaterialConfig.cshtml", Config);
        }

        /// <summary>
        /// /// Get page of configuration values of the TCS
        /// </summary>
        /// <returns></returns>
        public ActionResult TCSValueConfiguration()
        {
            ConfigurationModel Config = new ConfigurationModel();
            return View("~/Views/Administrator/Configuration/TEColonesConfig.cshtml");
        }

        /// <summary>
        /// Get page of configuration of the credentials of the twitter account.
        /// /// </summary>
        /// <returns></returns>
        public ActionResult TwitterConfiguration()
        {
            ConfigurationModel Config = new ConfigurationModel();
            DBConnection.GetTwitterData(Config);
            return View("~/Views/Administrator/Configuration/TwitterConfig.cshtml", Config);
        }

    }
}