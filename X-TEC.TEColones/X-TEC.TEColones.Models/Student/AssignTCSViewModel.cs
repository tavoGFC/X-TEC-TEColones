using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X_TEC.TEColones.Models
{
   public class AssignTCSViewModel
    {


        public AssignTCSViewModel()
        {
            TCS = 0.0f;
            ExrDinningRoom = 0.0f;
            ExrEnrollment = 0.0f;
        }

        public float TCS { get; set; }

        //ExchangeRate:Exr
        public float ExrDinningRoom { get; set; }

        //ExchangeRate:Exr
        public float ExrEnrollment { get; set; }


    }
}
