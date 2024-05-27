
using System.Net;
using System.Net.Mail;

namespace BigonWebShoppingApp.Helpers.Services
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendAsync(string to, string subject, string body, bool isHtml = true)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = isHtml;
                message.To.Add(to);

                message.From = new MailAddress(_configuration["Mail:Username"], "Bigon", System.Text.Encoding.UTF8);
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
                smtpClient.EnableSsl = true;
                smtpClient.Port = 587;
                smtpClient.Host = _configuration["Mail:Host"];
                await smtpClient.SendMailAsync(message);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
