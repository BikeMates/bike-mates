using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace BikeMates.Application.Services
{
    public static class MailSender
    {
        public static void Send(string userEmail, string message)
        {
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(ConfigurationManager.AppSettings["MailAccount"]);
            msg.To.Add(new MailAddress(userEmail));
            msg.Subject = "Reset Password";
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(message, null, MediaTypeNames.Text.Html));
            msg.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587));
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(
                 ConfigurationManager.AppSettings["MailAccount"],
                 ConfigurationManager.AppSettings["MailPassword"]);
            smtpClient.Credentials = credentials;
            smtpClient.EnableSsl = true;
            smtpClient.Send(msg);
        }
    }
}
