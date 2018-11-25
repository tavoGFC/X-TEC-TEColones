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
        // for single promos
        public List<List<String>> SinglePromoData { get; set; }

        // for combos promos
        public List<List<String>> ComboPromoData { get; set; }

        // for code utilities
        public int LatestIdPromotion { get; set; } // to get the newest id for the transactions

        public List<string> ListMaterials { get; set; } // to show in the dropdown list view
    }

    
}
