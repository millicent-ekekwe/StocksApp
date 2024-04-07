using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using StocksApp.Dtos.Email;
using StocksApp.Helpers;
using StocksApp.Interfaces;
using System.Net.Mail;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace StocksApp.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }
        public async Task SendEmailRegistration(EmailDto request)
        {
            string body = PopulateRegisterEmail(request.UserName, request.OTP);
            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(_config["EmailSettings:EmailUserName"]));  //the email will be sent from EmailUsername in appsettings
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
           /* email.Body = new TextPart(TextFormat.Html)
            {
                Text = request.Body
            };*/

           var builder = new BodyBuilder();
           builder.HtmlBody = body;

           email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();

            smtp.Connect(_config["EmailSettings:EmailHost"], int.Parse(_config["EmailSettings:EmailPort"]), SecureSocketOptions.StartTls);   //connects to the host
            smtp.Authenticate(_config["EmailSettings:EmailUserName"], _config["EmailSettings:EmailPassword"]);          //authenticates
            await smtp.SendAsync(email);    //sends the email

            smtp.Disconnect(true);

        }

        private string PopulateRegisterEmail(string UserName, string OTP)
        {
            string body = string.Empty;

            string filePath = Directory.GetCurrentDirectory() + @"\Template\RegistrationTemplate.html";

            using (StreamReader reader = new StreamReader(filePath))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{UserName}", UserName);
            body = body.Replace("{OTP}", OTP);

            return body;


        }
    }
}
