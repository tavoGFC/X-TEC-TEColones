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
            PromotionModel = new PromotionModel();
        }

        public ConfigurationModel ConfigurationModel { get; set; }

        public PromotionModel PromotionModel  { get; set; }

        public string Department { get; set; }

        public string PhoneNumber { get; set; }



    }
}
