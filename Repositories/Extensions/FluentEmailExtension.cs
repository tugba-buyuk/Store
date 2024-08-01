using FluentEmail.Core.Interfaces;
using FluentEmail.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Extensions
{
    public static class FluentEmailExtension
    {
        public static void AddFluentEmail(this IServiceCollection services, ConfigurationManager configuration)
        {
            var emailSettings = configuration.GetSection("EmailSettings");
            var defaultFromEmail = emailSettings["DefaultFromEmail"];
            var host = emailSettings["Host"];
            var port = emailSettings.GetValue<int>("Port");
            var username = emailSettings["Username"];
            var password = emailSettings["Password"];

            services.AddFluentEmail(defaultFromEmail)
                .AddSmtpSender(new SmtpClient(host, port)
                {
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(username, password)
                });
        }
    }
}

