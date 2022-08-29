using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Hiring.Service.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendAsync(string to, string subject, string body)
        {
            // Create Messsage
            var message = new MailMessage();
            message.From = new MailAddress("myaspnet.trainer@gmail.com", "AspNet Trainer");
            message.To.Add(new MailAddress(to));
            //message.To.Add(to);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            // Send Messsage
            var emailClient = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 465,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("myaspnet.trainer@gmail.com", "Aa@0Aa@0")
            };

            await emailClient.SendMailAsync(message);
        }
    }

    public interface IEmailService
    {
        public Task SendAsync(string to, string subject, string body);
    }
}