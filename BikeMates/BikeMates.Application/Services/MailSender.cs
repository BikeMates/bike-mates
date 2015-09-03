using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace BikeMates.Application.Services
{
    public static class MailSender
    {
        public static void Send(string userEmail, string message)
        {
            //TODO: Create a method for Creating message
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(ConfigurationManager.AppSettings["MailAccount"]); //TODO: Do not get values from config here. Inject them in coonstructor
            msg.To.Add(new MailAddress(userEmail));
            msg.Subject = "Reset Password";
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(message, null, MediaTypeNames.Text.Html));
            msg.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587));// TODO: Why we need to convert to Int32? Can we just use 587?
            NetworkCredential credentials = new NetworkCredential(
                 ConfigurationManager.AppSettings["MailAccount"],
                 ConfigurationManager.AppSettings["MailPassword"]);
            smtpClient.Credentials = credentials;
            smtpClient.EnableSsl = true;
            smtpClient.Send(msg);
        }
    }
}
