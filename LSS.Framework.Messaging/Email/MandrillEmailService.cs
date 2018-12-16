using Mandrill;
using Mandrill.Models;
using Mandrill.Requests.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.Framework.Messaging.Email
{
    public class MandrillEmailService : IEmailService
    {
        private readonly MandrillApi _api;

        public MandrillEmailService(string key)
        {
            _api = new MandrillApi(key, true);
        }

        public void Send()
        {
            throw new NotImplementedException();
        }
    }
}
