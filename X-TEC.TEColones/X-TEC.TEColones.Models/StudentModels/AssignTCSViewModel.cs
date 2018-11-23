using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X_TEC.TEColones.Models.StudentModels
{
   public class AssignTCSViewModel
    {


        public AssignTCSViewModel()
        {
            TCS = 0.0f;
            Benefits = new List<Benefit>();
        }

        public float TCS { get; set; }
              
        public List<Benefit> Benefits { get; set; }

        public float ExRDinningRoom { get; set; }

        public float ExREnrollment { get; set; }
        
        
        public void SetExchanRate()
        {
            ExRDinningRoom = Benefits.Find(x => x.Type.Contains("Comedor") || x.Type.Contains("Soda")).ExchangeRate;
            ExREnrollment = Benefits.Find(x => x.Type.Contains("Derecho") || x.Type.Contains("Matricula")).ExchangeRate;
        }
    }

    
       
    public class Benefit
    {

        public string Type { get; set; }

        public float ExchangeRate { get; set; }


    }


}
