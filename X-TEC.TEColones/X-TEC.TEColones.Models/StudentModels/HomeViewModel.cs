using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X_TEC.TEColones.Models.StudentModels
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            Materials = new List<Material>();
        }

        public List<Material> Materials { get; set; }

    }


    public class Material
    {

        public string Type { get; set; }

        public float  ValueTCS { get; set; }

    }
}
