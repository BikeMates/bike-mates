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

            try
            {
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
                    newfilePath = currentFile.Directory.FullName + "\\" + "749eae97-ff20-4d8c-8bd0-7e7fc27a9ed2";// +".jpeg";
                }

                File.Delete(newfilePath); // Delete the existing file if exists
                File.Move(oldfilePath, newfilePath); // Rename the oldFileName into newFileName
                
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                return response;
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
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

            //Set Response contents and MediaTypeHeaderValue
            HttpResponseMessage Response = new HttpResponseMessage(HttpStatusCode.OK);
            Response.Content = new ByteArrayContent(fileData);
            Response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/*");
            return Response;

            //FileStream fileStream = FileProvider.Open(fileName);
            //HttpResponseMessage response = new HttpResponseMessage { Content = new StreamContent(fileStream) };
            //response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
            //response.Content.Headers.ContentLength = FileProvider.GetLength(fileName);
            //return response;

        }
    }
}
