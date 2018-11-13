using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace X_TEC.TEColones.Models.AdminModels
{
    public class Admin : User
    {

        public string Identification { get; set; }

        public string Department { get; set; }



    }
}
