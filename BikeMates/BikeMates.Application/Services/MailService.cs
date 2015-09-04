using BikeMates.Contracts.Services;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace BikeMates.Application.Services
{
    public class MailService : IMailService
    {
        private readonly string MailAccount;
        private readonly string MailPassword;

        public MailService()
        {
            MailAccount = ConfigurationManager.AppSettings["MailAccount"]; //TODO: Inject this in constructor. Move read from config to Ninject module
            MailPassword = ConfigurationManager.AppSettings["MailPassword"];
        }

        public void Send(string userEmail, string message)
        {
            MailMessage msg = CreateMessage(userEmail, message);

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            NetworkCredential credentials = new NetworkCredential(MailAccount, MailPassword);
            smtpClient.Credentials = credentials;
            smtpClient.EnableSsl = true;
            smtpClient.Send(msg);
        }

        private MailMessage CreateMessage(string userEmail, string message)
        {
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(MailAccount);
            msg.To.Add(new MailAddress(userEmail));
            msg.Subject = "Reset Password";
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(message, null, MediaTypeNames.Text.Html));
            msg.IsBodyHtml = true;
            return msg;
        }
    }
}
