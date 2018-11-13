using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X_TEC.TEColones.Models
{
    public class ShareTCSViewModel
    {

        public ShareTCSViewModel()
        {
            UserToShereName = " ";
            UserToShareId = " ";
            TCSToShare = " ";
        }

        public string UserToShareId { get; set; }

        public string UserToShereName { get; set; }

        public string TCSToShare { get; set; }



    }
}
