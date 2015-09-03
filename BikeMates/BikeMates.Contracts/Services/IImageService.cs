using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web; 
using System.Web.Http;

namespace BikeMates.Contracts.Services
{
    public interface IImageService
    {
        void SaveImage(string userId, MultipartFormDataStreamProvider provider);
        string GetPath(string userId);
        byte[] GetImage(string userId);

    }
}
