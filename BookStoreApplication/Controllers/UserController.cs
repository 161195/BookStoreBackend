using BuisnessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserBL BL;
        public UserController(IUserBL BL)
        {
            this.BL = BL;
        }
        [HttpPost]                                      //to add new registration
        public IActionResult UserRegistration(UserModel user)
        {
            try
            {
                UserResponse userDetails = this.BL.Registration(user);
                if (userDetails !=null)
                {
                    return this.Ok(new { Success = true, message = "Registration Successful" ,userDetails });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Registration Unsuccessful" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.InnerException ,msg = ex.Message });
            }
        }
        [HttpPost("Login")]
        public IActionResult GetLogin(UserLogin user1)
        {
            try
            {
                string result = this.BL.GetLogin(user1);
                if (result == null)
                {
                    return BadRequest(new { Success = false, message = "Email or Password Not Found" });
                }
                return Ok(new { Success = true, message = "Login Successful", UserLoginInfo = result });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Success = false, message = e.Message });
            }
        }
        [HttpPost]
        [Route("forgetPassword")]
        public IActionResult ForgetPassword(ForgetPasswordModel model)
        {
            try
            { 
                string result = this.BL.ForgetPassword(model);
                if (result !=null)
                {
                    return Ok(new { Success = true, message = "Password Reset Mail Sent" });
                }
                return BadRequest(new { Success = false, message = "Invalid Credentials for reset password" });
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
