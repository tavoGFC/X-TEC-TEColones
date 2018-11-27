using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using X_TEC.TEColones.Models.StudentModels;

namespace X_TEC.TEColones.Models.AdminModels
{
    public class DashboardModel
    {
        public float ToneladasAnuales { get; set; }

        public List<float> ToneladasXmes { get; set; }
       
        public string TxS { get; set; }

        public string TxS { get; set; }

        public List<float> DineroXbeneficio { get; set; }

        public List<int> UsuariosXmes { get; set; }

        public List<StudentModel> Top10 { get; set; }

    }
    
}