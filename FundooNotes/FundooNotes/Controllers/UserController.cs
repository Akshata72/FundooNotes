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
    }
}
