using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Ecommerce_App.Models.Services
{
    public class EmailSenderService : IEmailSender
    {
        private IConfiguration _configuration;

        public EmailSenderService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Uses SendGrid to send out emails to customers,
        /// from "The Outdoor Store" at admin@theoutdoorstore.com.
        /// </summary>
        /// <param name="email">The customer's email address that the email will be sent to</param>
        /// <param name="subject">The subject of the email</param>
        /// <param name="htmlMessage">The specific contents of the email</param>
        /// <returns>A complete task</returns>
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            SendGridClient client = new SendGridClient(_configuration["SENDGRID_API_KEY"]);

            SendGridMessage message = new SendGridMessage();

            message.SetFrom("admin@theoutdoorstore.com", "The Outdoor Store");
            message.AddTo(email);
            message.SetSubject(subject);
            message.AddContent(MimeType.Html, htmlMessage);
            await client.SendEmailAsync(message);

        }
    }
}
