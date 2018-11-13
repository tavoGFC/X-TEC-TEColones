using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X_TEC.TEColones.Models
{
    public class Student : User
    {
        public Student()
        {
            AssignTCS = new AssignTCSViewModel();
            ShareTCS = new ShareTCSViewModel();
        }

        public string Identification { get; set; }

        public string Skills { get; set; }

        public string Description { get; set; }

        public List<string> Emails { get; set; }

        public List<string> PhoneNumbers { get; set; }

        public AssignTCSViewModel AssignTCS { get; set; }

        public ShareTCSViewModel ShareTCS { get; set; }

    }
}
