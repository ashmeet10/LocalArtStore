﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Services
{
    public class NullMailService : IMailService
    {
        private readonly ILogger<NullMailService> _logger;
        public NullMailService(ILogger<NullMailService> logger)
        {
            _logger = logger;
        }
        public void SendMessage(string to, string subject, string body)
        {
            _logger.LogInformation($"To: {to} Subject:{subject} Body:{body}");
        }
    }
}
