using System;
namespace BikeMates.Contracts.MailSender
{
    public interface IMailService
    {
        void Send(string userEmail, string message);
    }
}
