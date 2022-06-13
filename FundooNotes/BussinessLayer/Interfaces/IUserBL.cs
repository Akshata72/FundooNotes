using DataBaseLayer.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interfaces
{
    public interface IUserBL
    {
        public void AddUser(UserPostModel userPostModel);
        public string LoginUser(string Email, string Password);
        public bool ForgetPassword(string Email);
    }
}
