using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace SmartSchool.BL.Services
{
	public interface IMailService
	{
		Task SendEmailAsync(string toemail, string subject, string content);
	}
	public class MailService : IMailService
	{
		private readonly IConfiguration configuration;

		public MailService(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		public async Task SendEmailAsync(string toemail, string subject, string content)
		{
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(configuration.GetSection("Email").Value));
            email.To.Add(MailboxAddress.Parse(toemail));
            email.Subject = subject;




            email.Body = new TextPart(TextFormat.Html)
            {
                Text = content
            };
            using var smtp = new SmtpClient();
            smtp.Connect(configuration.GetSection("Host").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(configuration.GetSection("Email").Value, configuration.GetSection("Password").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
	}
}
