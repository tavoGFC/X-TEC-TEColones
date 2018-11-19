using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X_TEC.TEColones.Models.AdminModels
{
    public class ConfigurationModel
    {

        public int PlasticValue { get; set; }

        public int GlassValue = 12;
        //public int GlassValue { get; set; }

        public int PaperValue { get; set; }

        public int AluminumValue { get; set; }

        public int DinningExchange { get; set; }

        public int StudyExchange { get; set; }

        public string CONSUMER_KEY { get; set; }

        public string CONSUMER_SECRET { get; set; }

        public string ACCESS_TOKEN { get; set; }

        public string ACCESS_TOKEN_SECRET { get; set; }

    }
}
