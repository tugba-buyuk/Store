using Entities.Models;
using FluentEmail.Core;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly IFluentEmailFactory _fluentEmailFactory;

        public EmailService(ILogger<EmailService> logger, IFluentEmailFactory fluentEmailFactory)
        {
            _logger = logger;
            _fluentEmailFactory = fluentEmailFactory;
        }

        public async Task Send(EmailMessageModel emailMessage)
        {
            _logger.LogInformation("Sending email");
            await _fluentEmailFactory.Create().To(emailMessage.ToAddress)
                .Subject(emailMessage.Subject)
                .Body(emailMessage.Body, true)
                .SendAsync();
        }

       
    }
}
