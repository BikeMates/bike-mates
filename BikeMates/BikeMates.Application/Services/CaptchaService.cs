using BikeMates.Contracts.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BikeMates.Application.Services
{
    public class CaptchaService: ICaptchaService
    {
        private readonly string googleRecaptchaApi;

        public CaptchaService()
        {
            googleRecaptchaApi = ConfigurationManager.AppSettings["GoogleRecaptchaApi"]; //TODO: Inject this in constructor. Move read from config to Ninject module
        }

         public bool checkCaptcha(string response) 
        {
             var captchaResponse = new { Success = false};
            const string secret = "6LdnvQsTAAAAAGM8ZQ8kr46eAalzSBzH_BpnYoN3";
            var webClient = new WebClient();
            var reply =
                webClient.DownloadString(
                    string.Format("{0}secret={1}&response={2}",googleRecaptchaApi, secret, response));
            var captchaModel = JsonConvert.DeserializeAnonymousType(reply, captchaResponse);
            return captchaModel.Success;
        }
        
    }
}
