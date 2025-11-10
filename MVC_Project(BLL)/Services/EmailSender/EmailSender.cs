using MVC_Project_DAL_.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_BLL_.Services.EmailSender
{
    public class EmailSender : IEmailSender
    {
        public void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com",587);
            //Allow SSL Protocol
            client.EnableSsl = true;
            //Sender
            client.Credentials = new NetworkCredential("elpopkaras12@gmail.com", "hvifameksoxyzwkf");
            //Send To
            client.Send("elpopkaras12@gmail.com",email.To,email.Subject,email.Body);
        }
    }
}
