using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X_TEC.TEColones.Models.AdminModels
{
    public class ConfigurationViewModel
    {

        public Dictionary<string, float> Materials { get; set; }

        public float PlasticValue { get; set; }

        public float GlassValue { get; set; }

        public float PaperValue { get; set; }

        public float AluminumValue { get; set; }

        public float StudyExchange { get; set; }

        public float DinningExchange { get; set; }


        /// <summary>
        /// Give the values of each material. 
        /// </summary>
        public void SetValues()
        {
            //PlasticValue = Materials[0];
            //PaperValue = Materials[1];
            //GlassValue = Materials[2];
            //AluminumValue = Materials[3];
        }
    }
}
