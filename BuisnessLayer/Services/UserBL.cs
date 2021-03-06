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
        public string GetLogin(UserLogin User1)
        {
            try
            {
                return this.UserRL.GetLogin(User1);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public string ForgetPassword(ForgetPasswordModel model)
        {
            try
            {
                return this.UserRL.ForgetPassword(model);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool ResetPassword(ResetPasswordModel model, string email)
        {
            try
            {
                return this.UserRL.ResetPassword(model, email);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
