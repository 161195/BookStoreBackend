using BuisnessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Services
{
    public class UserBL : IUserBL
    {
        IUserRL UserRL;
        public UserBL(IUserRL userRL)
        {
            this.UserRL = userRL;
        }
        public UserResponse Registration(UserModel user)
        {
            try
            {
                return this.UserRL.Registration(user);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
