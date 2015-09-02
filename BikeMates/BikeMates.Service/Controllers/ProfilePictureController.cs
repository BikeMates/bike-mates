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
        //POST api/profilepicture
        [HttpPost]
        public async Task<HttpResponseMessage> AddImage()
        {
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var id = principal.Claims.Single(c => c.Type == "id").Value;

            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            string root = HttpContext.Current.Server.MapPath("~/Resources");
            var provider = new MultipartFormDataStreamProvider(root);

            // Read the form data.
            await Request.Content.ReadAsMultipartAsync(provider);

            string path = "";
            string newfilePath = "";
            string oldfilePath = "";

            foreach (MultipartFileData file in provider.FileData)
            {
                FileInfo currentFile = new FileInfo(file.LocalFileName);
                path = file.Headers.ContentDisposition.FileName;
                oldfilePath = file.LocalFileName;
                newfilePath = String.Format("{0}\\{1}", currentFile.Directory.FullName, id);
            }

            if (File.Exists(newfilePath))
            {
                File.Delete(newfilePath);
            }
            File.Move(oldfilePath, newfilePath);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage GetImage(string id)
        {
            string fileName = id;
            string rootPath = HttpContext.Current.Server.MapPath("~/Resources");

            var filePath = Path.Combine(rootPath, fileName);
            if (!File.Exists(filePath)) //If image not found - then default image
            {
                filePath = Path.Combine(rootPath, "icon-user-default.jpg");
            }

            byte[] fileData = File.ReadAllBytes(filePath);
            if (fileData == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            HttpResponseMessage Response = new HttpResponseMessage(HttpStatusCode.OK);
            Response.Content = new ByteArrayContent(fileData);
            Response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/*");
            return Response;
        }
    }
}
