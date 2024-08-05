using Entities.Models;
using Microsoft.Extensions.Options;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Services
{
    public class SMSManager : ISMSService
    {
        private readonly TwilioSettings _twilioSettings;

        public SMSManager(IOptions<TwilioSettings> twilioSettings)
        {
            _twilioSettings = twilioSettings.Value;
            TwilioClient.Init(_twilioSettings.AccountSid, _twilioSettings.AuthToken);
        }
        public void SendSms(string toPhoneNumber, string message)
        {
            var messageOptions = new CreateMessageOptions(new PhoneNumber(toPhoneNumber))
            {
                From = new PhoneNumber(_twilioSettings.FromPhoneNumber),
                Body = message
            };

            MessageResource.Create(messageOptions);
        }
    }
}
