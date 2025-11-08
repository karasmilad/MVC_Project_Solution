using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_BLL_.Services.Attachment_Service
{
    public interface IAttachmentService
    {
        public string? Upload(IFormFile fileName, string folderName);
        public bool Delete(string filePath);
    }
}
