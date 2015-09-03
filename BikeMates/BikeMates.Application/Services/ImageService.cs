using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using BikeMates.Contracts.Services;
using System.IO;
using System.Web;

namespace BikeMates.Application.Services
{
    public class ImageService : IImageService
    {
        public void SaveImage(string userId,  MultipartFormDataStreamProvider provider)
        {

            string path = "";
            string newfilePath = "";
            string oldfilePath = "";

            foreach (MultipartFileData file in provider.FileData)
            {
                FileInfo currentFile = new FileInfo(file.LocalFileName);
                path = file.Headers.ContentDisposition.FileName;
                oldfilePath = file.LocalFileName;
                newfilePath = String.Format("{0}\\{1}", currentFile.Directory.FullName, userId);
            }

            if (File.Exists(newfilePath))
            {
                File.Delete(newfilePath);
            }
            File.Move(oldfilePath, newfilePath);
        }
        
        public string GetPath(string userId)
        {
            string fileName = userId;
            string rootPath = HttpContext.Current.Server.MapPath("~/Resources");

            string filePath = Path.Combine(rootPath, fileName);
            if (!File.Exists(filePath)) //If image not found - then default image
            {
                filePath = Path.Combine(rootPath, "icon-user-default.jpg");
            }

            return filePath;

        }


        public byte[] GetImage(string userId)
        {
            string filePath = this.GetPath(userId);
            byte[] fileData = File.ReadAllBytes(filePath);
            return fileData;
        }

    }
}
