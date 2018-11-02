using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X_TEC.TEColones.Persistence.Entity
{
    public class Student
    {
        public Student()
        {
            User = new User();
        }

        public User User { get; set; }

        public int NumberId { get; set; }

        public string Password { get; set; }


    }
}
