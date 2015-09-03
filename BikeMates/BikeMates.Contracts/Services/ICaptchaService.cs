using System;

namespace BikeMates.Contracts.Services
{
    public interface ICaptchaService
    {
        bool checkCaptcha(string response);
    }
}
