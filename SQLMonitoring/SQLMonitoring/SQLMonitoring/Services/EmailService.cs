using Microsoft.Extensions.Configuration;
using SQLMonitoring.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SQLMonitoring.Utilities
{
    public class EmailService
    {
        private IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool SendEmail(string email)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("sql.monitoring.service@gmail.com");
            mailMessage.To.Add(new MailAddress(email));

            var confirmationLink = _configuration.GetValue<string>("Hosting:Url") + "Home/Confirmation?email=" +CryptographyService.EncryptString(email);
            mailMessage.Subject = "Welcome to SQL Monitoring";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body =
                @$"Welcome to SQL Monitoring. We are happy to see you on the platform ! <br/>
                   Please verify your account with click on the following link: {confirmationLink} <br/>
                   <br/>
                   Regards,<br/>
                   SQL Monitoring team";

            var usernameCred = _configuration.GetValue<string>("EmailService:Username");
            var passwordCred = _configuration.GetValue<string>("EmailService:Password");

            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(usernameCred, passwordCred);

            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                // log exception
            }
            return false;
        }
    }
}
