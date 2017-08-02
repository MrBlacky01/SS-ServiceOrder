using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace ServiceOrder.Common.Services.EmailService
{
    public class EmailService: IIdentityMessageService
    {
        private ServiceMailData ServiceMailData { get; set; }

        public EmailService(ServiceMailData serviceMailData)
        {
            ServiceMailData = serviceMailData;
        }
        public Task SendAsync(IdentityMessage message)
        {
            var sendMessage = GenerateMessage(message);
            return SendMail(sendMessage);
        }

        private MailMessage GenerateMessage(IdentityMessage messageData)
        {
            var message = new MailMessage();
            message.To.Add(messageData.Destination);
            message.From = new MailAddress(ServiceMailData.UserName); 
            message.Subject = messageData.Subject;
            message.Body = messageData.Body;
            message.IsBodyHtml = true;
            return message;
        }

        private async Task SendMail(MailMessage message)
        {
            using (var smtp = new SmtpClient())
            {
                InitializeSmtp(smtp);
                await smtp.SendMailAsync(message);
        
            }           
        }

        private void InitializeSmtp(SmtpClient smtpClient)
        {
            var credential = new NetworkCredential
            {
                UserName = ServiceMailData.UserName,
                Password = ServiceMailData.Password
            };
            smtpClient.Credentials = credential;
            smtpClient.Host = ServiceMailData.Host;
            smtpClient.EnableSsl = ServiceMailData.EnableSsl;
        }
    }
}
