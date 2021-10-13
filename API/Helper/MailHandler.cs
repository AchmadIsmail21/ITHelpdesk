using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace API.Helper
{
    public class MailHandler
    {
        /*public string stringHtmlMessage;
        public string destinationEmail;
        public MailHandler(string stringHtmlMessage, string destinationEmail) {
            this.stringHtmlMessage = stringHtmlMessage;
            this.destinationEmail = destinationEmail;
        }
*/
        public void SendEmail(string htmlString, string toMailAddress)
        {
           
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("stevanloaned12@gmail.com");
                message.To.Add(new MailAddress(toMailAddress));
                message.Subject = "IT Support Helpdesk" + DateTime.Now.ToString();
                message.IsBodyHtml = true;
                message.Body = htmlString;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("stevanloaned12@gmail.com", "makannasi12");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);

        }
    }
}
