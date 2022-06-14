using DataBaseLayer.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        public void AddUser(UserPostModel userPostModel);
        public string LoginUser(string Email, string Password);
        public bool ForgetPassword(string Email);
        public bool ResetPassword(string Email,PasswordModel passwordModel);
    }
}
