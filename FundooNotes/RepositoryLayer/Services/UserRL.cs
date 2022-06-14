using DataBaseLayer.Users;
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;


namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        FundooContext fundooContext;
        IConfiguration configuration;
        public UserRL(FundooContext fundooContext,IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }
        public void AddUser(UserPostModel userPostModel)
        {
            try
            {
                User user = new User();
                user.FirstName = userPostModel.FirstName;
                user.LastName = userPostModel.LastName;
                user.Email = userPostModel.Email;
                user.Password = Encryption.EncodePasswordToBase64(userPostModel.Password);
                user.Address = userPostModel.Address;
                user.CreatedDate = DateTime.Now;
                user.ModifiedDate = DateTime.Now;
                fundooContext.Add(user);
                fundooContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ForgetPassword(string Email)
        {
            try
            {
                var user = fundooContext.User.FirstOrDefault(u => u.Email == Email);
                if (user == null)
                {
                    return false;
                }
                MessageQueue messageQueue = new MessageQueue();
                messageQueue.Path = @".\Private$\FundooQueue";
                //ADD MESSAGE TO QUEUE
                if (MessageQueue.Exists(messageQueue.Path))
                {
                    messageQueue = new MessageQueue(@".\Private$\FundooQueue");
                }
                else
                {
                    messageQueue = MessageQueue.Create(messageQueue.Path);
                }
                Message Mymessage = new Message();
                Mymessage.Formatter = new BinaryMessageFormatter();
                Mymessage.Body = GenerateJWToken(Email, user.UserId);
                Mymessage.Label = "Forget Password Label";
                messageQueue.Send(Mymessage);
                Message msg = messageQueue.Receive();
                msg.Formatter = new BinaryMessageFormatter();
                EmailService.SendEmail(Email, msg.Body.ToString());
                messageQueue.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReceiveCompleted);

                messageQueue.BeginReceive();
                messageQueue.Close();

                return true;
            } 
            catch (Exception)
            {
                throw;
            }
        }

        public string LoginUser(string Email, string Password)
        {
            try
            {
                var user = fundooContext.User.FirstOrDefault(u => u.Email == Email);
                Password = Encryption.DecodeFrom64(user.Password);
                if (user != null)
                {
                   return GenerateJWToken(Email, user.UserId);
                }
                return null;
            }
            catch(Exception)
            {
                throw;
            }
        }
        private void msmqQueue_ReceiveCompleted(object sender,ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue queue = (MessageQueue)sender;
                Message msg = queue.EndReceive(e.AsyncResult);
                EmailService.SendEmail(e.Message.ToString(), GenrateToken(e.Message.ToString()));
                queue.BeginReceive();
            }
            catch(MessageQueueException ex)
            {
               
                if (ex.MessageQueueErrorCode ==
                   MessageQueueErrorCode.AccessDenied)
                {
                    Console.WriteLine("Access is denied. " +
                        "Queue might be a system queue.");
                }
            }
        }

        private string GenrateToken(string Email)
        {
            try
            {
                var user = fundooContext.User.FirstOrDefault(u => u.Email == Email);
                if (user == null)
                {
                    return null;
                }
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("Email", Email)
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials =
                    new SigningCredentials(
                        new SymmetricSecurityKey(tokenKey),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch(Exception)
            {
                throw;
            }
        }

        private string GenerateJWToken(string Email,int userId)
        {
            var user = fundooContext.User.FirstOrDefault(u => u.Email == Email);
            if(user == null)
            {
                return null;
            }
            //generate token

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Email", Email),
                    new Claim("UserId",userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool ResetPassword(string Email,PasswordModel passwordModel)
        {
            try
            {
                var user = fundooContext.User.FirstOrDefault(u => u.Email == Email);
                if (user == null)
                {
                    return false;
                }
                if(passwordModel.NewPassword!=passwordModel.ConfirmPassword)
                {
                    return false;
                }
                user.Password = Encryption.EncodePasswordToBase64(passwordModel.NewPassword);
                fundooContext.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
