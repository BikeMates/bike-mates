using BikeMates.Application.Services;
using BikeMates.Contracts.Services;
using BikeMates.DataAccess;
using BikeMates.DataAccess.Repository;
using BikeMates.Domain.Entities;
using BikeMates.Service.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace BikeMates.Service.Controllers
{
    [RoutePrefix("api/profilepicture")]
    public class ProfilePictureController : BaseController
    {
        private readonly IImageService imageService;
        
        public ProfilePictureController(IImageService imageService)
        {
            this.imageService = imageService;        
        }

        //POST api/profilepicture
        [HttpPost]
        public async Task<HttpResponseMessage> AddImage()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            string root = HttpContext.Current.Server.MapPath("~/Resources");
            var provider = new MultipartFormDataStreamProvider(root);
            // Read the form data.
            await Request.Content.ReadAsMultipartAsync(provider);
            imageService.SaveImage(this.UserId, provider);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage GetImage(string id)
        {
            byte[] fileData = imageService.GetImage(id);
            if (fileData == null)
            {
                return new  HttpResponseMessage(HttpStatusCode.NotFound);
            }

            HttpResponseMessage Response = new HttpResponseMessage(HttpStatusCode.OK);
            Response.Content = new ByteArrayContent(fileData);
            Response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/*");
            return Response;
        }
    }
}
