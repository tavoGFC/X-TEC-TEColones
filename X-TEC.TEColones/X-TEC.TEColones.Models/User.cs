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


        public void RenderImage()
        {
            //Convert byte arry to base64string   
            string imreBase64Data = Convert.ToBase64String(PhotoBytes);
            string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);

            Photo = imgDataURL;
        }

        public string DefaultPhoto()
        {
            return "http://cdn.onlinewebfonts.com/svg/img_569204.png";
        }


    }
}
