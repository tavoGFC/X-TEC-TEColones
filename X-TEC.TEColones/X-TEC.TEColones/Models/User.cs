using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace X_TEC.TEColones.Models
{
    public class User
    {

        public string Name { get; set; }
        public string College { get; set; }
        public string Photo { get; set; }
        public List<string> Emails = new List<string>();
        public string Sede { get; set; }

    }
}