using System;
using Notifications.Interfaces;

namespace Notifications.Services
{
    public class SmsSender : INotificationSender
    {
        private readonly ILogger _logger;
        private static readonly Random _rnd = new Random();

        public string ChannelName => "SMS";

        public SmsSender(ILogger logger)
        {
            _logger = logger;
        }

        public void Send(string message)
        {
            _logger.LogInfo($"SMS: отправка '{message}'");
            Thread.Sleep(300);

            if (_rnd.Next(100) < 15)
                throw new Exception("Ошибка SMS-шлюза");

            _logger.LogInfo("SMS: успешно отправлено");
        }
    }
}