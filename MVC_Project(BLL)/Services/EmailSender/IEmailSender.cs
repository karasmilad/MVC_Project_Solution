using MVC_Project_DAL_.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_BLL_.Services.EmailSender
{
    public interface IEmailSender
    {
        void SendEmail(Email email);
    }
}
