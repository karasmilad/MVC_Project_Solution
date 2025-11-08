using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_BLL_.Services.Attachment_Service
{
    public class AttachmentService : IAttachmentService
    {
        public string? Upload(IFormFile fileName, string folderName)
        {
            //1- Check Extension
            var AllowedExtension = new List<string>() { ".jpg", ".png", ".jpeg", ".gif", ".pdf", ".docx" };
            var fileExtension = Path.GetExtension(fileName.FileName);
            if (!AllowedExtension.Contains(fileExtension))
            {
                return null;
            }
            //2- Check Size
            const int maxSize = 2 * 1024 * 1024; //2 MB
            if (fileName.Length > maxSize || fileName.Length == 0)
            {
                return null;
            }
            //3- Get Located Folder Path
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","Files",folderName);
            //4- Make Attachment Unique Name
            var uniqueFileName = $"{Guid.NewGuid()}_{fileName.FileName}";
            //5- Get File Path
            var filePath = Path.Combine(FolderPath,uniqueFileName);
            //6- Create File Stream 
            using FileStream fileStream = new FileStream(filePath, FileMode.Create);
            //7- Copy File to File Stream
            fileName.CopyTo(fileStream);
            return uniqueFileName;
        }
        public bool Delete(string filePath)
        {
            if(!File.Exists(filePath))return false;
            File.Delete(filePath);
            return true;
        }
    }
}
