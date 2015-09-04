using System;
namespace BikeMates.Contracts.Services
{
    public interface IMailService
    {
        void Send(string userEmail, string message);
    }
}
