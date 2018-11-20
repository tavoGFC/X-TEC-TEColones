using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace X_TEC.TEColones.Models.AdminModels
{
    public class AdminModel : User
    {
        public AdminModel()
        {
            ConfigurationModel = new ConfigurationModel();
        }
        public ConfigurationModel ConfigurationModel { get; set; }

        public string Identification { get; set; }

        public string Department { get; set; }



    }
}
