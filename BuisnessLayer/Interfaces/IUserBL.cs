using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Interfaces
{
    public interface IUserBL
    {
        public UserResponse Registration(UserModel user);   //to post new registration data   
        public string GetLogin(UserLogin User1);
        public string ForgetPassword(ForgetPasswordModel model);
    }
}
