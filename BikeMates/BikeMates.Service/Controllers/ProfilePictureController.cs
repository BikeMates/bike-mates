using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;

//wsmu -why so many using
using BikeMates.Application.Services;
using BikeMates.Domain.Entities;
using BikeMates.Contracts.Services;
using BikeMates.DataAccess.Repository;
using BikeMates.DataAccess;
using BikeMates.Service.Models;
using System.Threading.Tasks;
using System.Web;

using BikeMates.Service.Helpers;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Security.Claims;




namespace BikeMates.Service.Controllers
{
    public class ProfilePictureController : ApiController
    {
        private UserService userService;

        public ProfilePictureController()
        {
            userService = new UserService(new UserRepository(new BikeMatesDbContext()));
        }



        //POST api/profilepicture
        [HttpPost]
        public  async Task<HttpResponseMessage> PostFormData()
        {

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
                            
                string id = "749eae97-ff20-4d8c-8bd0-7e7fc27a9ed2";
                string path = "";
                string newfilePath = "";
                string oldfilePath = "";

                // This illustrates how to get the file names.
                foreach (MultipartFileData file in provider.FileData)
                {
                     //Trace.WriteLine(file.Headers.ContentDisposition.FileName);
                  
                   
                    //file.Headers.ContentDisposition.FileName = "749eae97-ff20-4d8c-8bd0-7e7fc27a9ed2";
                    FileInfo currentFile = new FileInfo(file.LocalFileName);

                //     if ( System.IO.File.Exists(currentFile.Directory.FullName) ) 
                  //       { System.IO.File.Delete(currentFile.Directory.FullName); }

                    //currentFile.MoveTo(currentFile.Directory.FullName + "\\" + "749eae97-ff20-4d8c-8bd0-7e7fc27a9ed2" + ".jpeg");
                    path = file.Headers.ContentDisposition.FileName;
                    oldfilePath = file.LocalFileName;
                    newfilePath = currentFile.Directory.FullName + "\\" + "749eae97-ff20-4d8c-8bd0-7e7fc27a9ed2" + ".jpeg";
                    
                }



                File.Delete(newfilePath); // Delete the existing file if exists
                File.Move(oldfilePath, newfilePath); // Rename the oldFileName into newFileName

               // string root = HttpContext.Current.Server.MapPath("~/Resources");
                string fileName = "749eae97-ff20-4d8c-8bd0-7e7fc27a9ed2" + ".jpeg";
                string pathr = "OLOLOLTROLOLOLTROLOLOL";// HttpContext.Current.Server.MapPath(String.Format("~/Resources/{0}", fileName));
                //pathr = "http://localhost:51952/Resources/" + fileName;
                
               // path.Replace("\\" , "//");
                User user = userService.GetUser(id);
                user.Picture = pathr;
                userService.Update(user);
               

                //Bad way to get to the page - but still works 
                var response = Request.CreateResponse(HttpStatusCode.Moved);
                response.Headers.Location = new Uri("http://localhost:51949/Account/Profile");
                return response;







            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }



        }


        [HttpGet]
        public HttpResponseMessage Get(string id)
        {
            //ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            //var userId = principal.Claims.Where(c => c.Type == "id").Single().Value;

            string fileName = id;
            string ext = "jpeg";
            string rootPath = HttpContext.Current.Server.MapPath("~/Resources");

            var filePath = Path.Combine(rootPath, fileName + "." + ext);
            if (!File.Exists(filePath)) //Not found then throw Exception
                throw new HttpResponseException(HttpStatusCode.NotFound);

            HttpResponseMessage Response = new HttpResponseMessage(HttpStatusCode.OK);

            //Read File as Byte Array
            byte[] fileData = File.ReadAllBytes(filePath);

            if (fileData == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            //Set Response contents and MediaTypeHeaderValue
            Response.Content = new ByteArrayContent(fileData);
            Response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
           

            return Response;


            //string fileName = string.Format("{0}.jpg", Id);
            //if (!FileProvider.Exists(fileName))
            //    throw new HttpResponseException(HttpStatusCode.NotFound);

            //FileStream fileStream = FileProvider.Open(fileName);
            //HttpResponseMessage response = new HttpResponseMessage { Content = new StreamContent(fileStream) };
            //response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
            //response.Content.Headers.ContentLength = FileProvider.GetLength(fileName);
            //return response;


        }


    }
}
