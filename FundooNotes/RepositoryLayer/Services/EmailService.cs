using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace RepositoryLayer.Services
{
    public class EmailService
    {
        public static void SendEmail(string Email,string token)
        {
            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = true;
                client.Credentials = new NetworkCredential("remapitesting963@gmail.com", "Remant@123");

                MailMessage mailMessage = new MailMessage();
                mailMessage.To.Add(Email);
                mailMessage.From = new MailAddress("remapitesting963@gmail.com");
                mailMessage.Subject = "Password Reset Link";
                mailMessage.Body = $"www.FundooNotes.com/reset-password/{token}";
                client.Send(mailMessage);
            }
        }
    }
}
