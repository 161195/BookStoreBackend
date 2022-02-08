using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Model
{
    public class UserResponse
    {
        public long UserId
        {
            get; set;
        }
        public string FullName { get; set; }
        public string EmailId { get; set; }       
        public string MobileNumber { get; set; }
    }
}
