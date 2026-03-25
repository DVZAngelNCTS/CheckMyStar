using Microsoft.Extensions.Configuration;

using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

using CheckMyStar.Bll.Abstractions;
using CheckMyStar.Bll.Responses;
using CheckMyStar.Bll.Templates;

namespace CheckMyStar.Bll.Services
{
    public partial class SendMailService(IConfiguration configuration) : ISendMailService
    {
        public async Task<BaseResponse> Send(string to, string resetLink, CancellationToken ct)
        {
            BaseResponse response = new BaseResponse();

            var email = new MimeMessage();
            var from = configuration.GetSection("Email")["From"];
            var port = configuration.GetSection("Email")["Port"];
            var smtpServer = configuration.GetSection("Email")["SmtpServer"];
            var userName = configuration.GetSection("Email")["Username"];
            var password = configuration.GetSection("Email")["Password"];

            if (!string.IsNullOrEmpty(from) && !string.IsNullOrEmpty(port) && !string.IsNullOrEmpty(smtpServer) && !string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                email.From.Add(new MailboxAddress("Support", from));
                email.To.Add(new MailboxAddress("", to));
                email.Subject = "CheckMyStar : Réinitialisation de votre mot de passe";

                var builder = new BodyBuilder
                {
                    HtmlBody = EmailTemplates.ResetPassword(resetLink)
                };

                email.Body = builder.ToMessageBody();

                using var smtp = new SmtpClient();

                await smtp.ConnectAsync(smtpServer, int.Parse(port), SecureSocketOptions.StartTls);

                await smtp.AuthenticateAsync(userName, password);

                response.IsSuccess = true;
                response.Message = await smtp.SendAsync(email);

                await smtp.DisconnectAsync(true);
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Impossible d'envoyer le mail";
            }

            return response;
        }
    }
}
