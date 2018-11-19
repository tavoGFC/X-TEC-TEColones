using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X_TEC.TEColones.Models.StudentModels
{
    public class StudentModel : User
    {
        public StudentModel()
        {
            AssignTCS = new AssignTCSViewModel();
            ShareTCS = new ShareTCSViewModel();
            HomeVM = new HomeViewModel();
        }

        public string Skills { get; set; }

        public string Description { get; set; }        
       
        public string PhoneNumber { get; set; }

        public int TCS { get; set; }
        
        public List<string> PhoneNumbers { get; set; }

        public List<string> Emails { get; set; }
        
        public AssignTCSViewModel AssignTCS { get; set; }

        public ShareTCSViewModel ShareTCS { get; set; }

        public HomeViewModel HomeVM { get; set; }

        
    }
}
