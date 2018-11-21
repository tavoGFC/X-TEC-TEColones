using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X_TEC.TEColones.Models.AdminModels
{
    public class ConfigurationViewModel
    {

        public List<float> ValuesTCS { get; set; }

        public float PlasticValue { get; set; }

        public float GlassValue { get; set; }

        public float PaperValue { get; set; }

        public float AluminumValue { get; set; }

        public float StudyExchange { get; set; }

        public float DinningExchange { get; set; }

        public string CONSUMER_KEY { get; set; }

        public string CONSUMER_SECRET { get; set; }

        public string ACCESS_TOKEN { get; set; }

        public string ACCESS_TOKEN_SECRET { get; set; }

        

        public void SetValues()
        {
            PlasticValue = ValuesTCS[0];
            PaperValue = ValuesTCS[1];
            GlassValue = ValuesTCS[2];
            AluminumValue = ValuesTCS[3];
        }
    }
}
