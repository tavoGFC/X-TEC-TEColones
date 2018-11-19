using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X_TEC.TEColones.Models.AdminModels
{
    public class ConfigurationModel
    {

        public float PlasticValue { get; set; }

        //public int GlassValue = 12;
        public float GlassValue { get; set; }

        public float PaperValue { get; set; }

        public float AluminumValue { get; set; }

        public float DinningExchange { get; set; }

        public float StudyExchange { get; set; }

        public string CONSUMER_KEY { get; set; }

        public string CONSUMER_SECRET { get; set; }

        public string ACCESS_TOKEN { get; set; }

        public string ACCESS_TOKEN_SECRET { get; set; }

    }
}
