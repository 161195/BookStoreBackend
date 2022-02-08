using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Model
{
     public class LoginResponse
     {
        public long UserId
        {
            get; set;
        }
        public string EmailId { get; set; }
        public string token { get; set; }

     }
}
