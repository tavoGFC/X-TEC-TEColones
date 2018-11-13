using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace X_TEC.TEColones.Models
{
    public class SCMModel : User
    {

        public int EmployeeNumber { get; set; }
        public string Department { get; set;}



        public void wawa()
        {
            System.Diagnostics.Debug.WriteLine(" WAWAWWAWAA ");
        }

    }
}