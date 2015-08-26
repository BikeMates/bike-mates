using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


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
using System.IO;
using System.Diagnostics;




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
        public async Task<HttpResponseMessage> PostFormData()
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

                // This illustrates how to get the file names.
                foreach (MultipartFileData file in provider.FileData)
                {

                     //Trace.WriteLine(file.Headers.ContentDisposition.FileName);
                 //  if ( System.IO.File.Exists(); )
                  //  { System.IO.File.Delete(); }
                   
                    file.Headers.ContentDisposition.FileName = "749eae97-ff20-4d8c-8bd0-7e7fc27a9ed2";
                    FileInfo currentFile = new FileInfo(file.LocalFileName);

                    currentFile.MoveTo(currentFile.Directory.FullName + "\\" + "749eae97-ff20-4d8c-8bd0-7e7fc27a9ed2" + ".jpeg");
                    path = currentFile.FullName;
                    
                }

                string fileName = "749eae97-ff20-4d8c-8bd0-7e7fc27a9ed2" + ".jpeg";
                string pathr = HttpContext.Current.Server.MapPath(String.Format("~/Resources/{0}", fileName));
             

                //path.Replace("\\" , "//");
                User user = userService.GetUser(id);
                user.Picture = "~/Resources/"+fileName;
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

    }
}
