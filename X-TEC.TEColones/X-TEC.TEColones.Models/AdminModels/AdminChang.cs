using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using X_TEC.TEColones.Models.AdminModels;

namespace X_TEC.TEColones.Models.AdminModels
{
    public class AdminChang : User
    {

        public string Identification { get; set; }

        public string Department { get; set; }

        public DashboardModel Dashboard { get; set; }

    }
}
