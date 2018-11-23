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
            ConfigurationModel = new ConfigurationViewModel();
            PromotionModel = new PromotionViewModel();
            DashboardModel = new DashboardModel();
        }

        public ConfigurationViewModel ConfigurationModel { get; set; }

        public PromotionViewModel PromotionModel  { get; set; }

        public DashboardModel DashboardModel { get; set; }

        public string Department { get; set; }

        public string PhoneNumber { get; set; }




    }
}
