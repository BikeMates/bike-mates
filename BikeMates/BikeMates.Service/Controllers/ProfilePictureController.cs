using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http.Headers;
using System.Security.Claims;
//project reffrences
using BikeMates.Application.Services;
using BikeMates.Domain.Entities;
using BikeMates.Contracts.Services;
using BikeMates.DataAccess.Repository;
using BikeMates.DataAccess;
using BikeMates.Service.Models;

namespace BikeMates.Service.Controllers
{
    [RoutePrefix("api/profilepicture")]
    public class ProfilePictureController : ApiController
    {
        private UserService userService;

        public ProfilePictureController()
        {
            userService = new UserService(new UserRepository(new BikeMatesDbContext()));
        }

        //POST api/profilepicture
        [HttpPost]
        public async Task<HttpResponseMessage> AddImage()
        {
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var id = principal.Claims.Where(c => c.Type == "id").Single().Value;

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

            // This illustrates how to get the file names.
            foreach (MultipartFileData file in provider.FileData)
            {
                FileInfo currentFile = new FileInfo(file.LocalFileName);
                path = file.Headers.ContentDisposition.FileName;
                oldfilePath = file.LocalFileName;
                newfilePath = String.Format("{0}\\{1}", currentFile.Directory.FullName, id);
            }
            File.Delete(newfilePath);
            File.Move(oldfilePath, newfilePath);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            return response;

        }

        [HttpGet]
        public HttpResponseMessage GetImage(string id)
        {
            string fileName = id;
            string rootPath = HttpContext.Current.Server.MapPath("~/Resources");

            var filePath = Path.Combine(rootPath, fileName);
            if (!File.Exists(filePath)) //If image not found - then default image
            { filePath = Path.Combine(rootPath, "icon-user-default.jpg"); }

            //Read File as Byte Array
            byte[] fileData = File.ReadAllBytes(filePath);

            if (fileData == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            HttpResponseMessage Response = new HttpResponseMessage(HttpStatusCode.OK);
            Response.Content = new ByteArrayContent(fileData);
            Response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/*");
            return Response;
        }
    }
}
