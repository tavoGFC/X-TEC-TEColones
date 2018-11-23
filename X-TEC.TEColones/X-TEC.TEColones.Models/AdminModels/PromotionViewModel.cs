using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace X_TEC.TEColones.Models.AdminModels
{
    public class PromotionViewModel
    {
        public string Materials { get; set; }

        public List<ListItem> ListCountries { get; set; }

        public List<string> ComboMaterials { get; set; }

        public bool Condition { get; set; }

        public float TCSValue { get; set; }

        public string InitialDate { get; set; }

        public string FinalDate { get; set; }

    }
}
