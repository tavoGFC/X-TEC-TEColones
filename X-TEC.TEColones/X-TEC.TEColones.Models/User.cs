using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X_TEC.TEColones.Models
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string University { get; set; }

        public string Headquarter { get; set; }

        public string Email { get; set; }

        public string Photo { get; set; }

        public byte[] PhotoBytes { get; set; }



    }
}
