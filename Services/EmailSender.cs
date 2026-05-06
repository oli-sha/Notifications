using System;
using Notifications.Interfaces;

namespace Notifications.Services
{
    public class EmailSender : INotificationSender
    {
        private readonly ILogger _logger;
        private static readonly Random _rnd = new Random();

        public string ChannelName => "Email";

        public EmailSender(ILogger logger)
        {
            _logger = logger;
        }

        public void Send(string message)
        {
            _logger.LogInfo($"Email: отправка '{message}'");
            Thread.Sleep(500);

            if (_rnd.Next(100) < 10)
                throw new Exception("SMTP сервер недоступен");

            _logger.LogInfo("Email: успешно отправлено");
        }
    }
}