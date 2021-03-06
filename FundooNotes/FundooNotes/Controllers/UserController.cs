using BussinessLayer.Interfaces;
using DataBaseLayer.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Services;
using System;
using System.Linq;

namespace FundooNotes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        FundooContext fundooContext;
        IUserBL userBL;
        public UserController(FundooContext fundooContext, IUserBL userBL)
        {
            this.fundooContext = fundooContext;
            this.userBL = userBL;
        }
        [HttpPost("Register")]
        public IActionResult AddUser(UserPostModel user)
        {
            try
            {
                this.userBL.AddUser(user);
                return this.Ok(new { success = true, message = "Registration Successful" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost("Login/{Email}/{Password}")]
        public ActionResult LoginUser(string Email, string Password)
        {
            try
            {
                var user = fundooContext.User.FirstOrDefault(u=>u.Email == Email);
                if (user == null)
                {
                    return this.BadRequest(new { success = false, message = "Email does not Exist" });
                }
                string token = this.userBL.LoginUser(Email, Password);
                return this.Ok(new { success = true, message = "Login Successful",token = token});
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost("ForgetPassword/{Email}")]
        public ActionResult ForgetPassword(string Email)
        {
            try
            {
                var user = fundooContext.User.FirstOrDefault(u => u.Email == Email);
                if (user == null)
                {
                    return this.BadRequest(new { success = false, message = "Email does not Exist" });
                }
                bool result =  this.userBL.ForgetPassword(Email);
                return this.Ok(new { success = true, message = "Email has send" });
            }
            catch(Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPost("ResetPassword")]
        public ActionResult ResetPassword(PasswordModel passwordModel)
        {
            try
            {
                var currentUser = HttpContext.User;
                int userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
                var Email = (currentUser.Claims.FirstOrDefault(c => c.Type == "Email").Value);
                if(passwordModel.NewPassword!= passwordModel.ConfirmPassword)
                {
                    return this.BadRequest(new { success = false, message = "New Password and Confirm Password must be same" });
                }
                bool result = this.userBL.ResetPassword(Email, passwordModel);
                return this.Ok(new { success = true, message = "Password change Successfully" });
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
