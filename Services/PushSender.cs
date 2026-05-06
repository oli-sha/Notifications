using System;
using Notifications.Interfaces;

namespace Notifications.Services
{
    public class PushSender : INotificationSender
    {
        private readonly ILogger _logger;
        private static readonly Random _rnd = new Random();

        public string ChannelName => "Push";

        public PushSender(ILogger logger)
        {
            _logger = logger;
        }

        public void Send(string message)
        {
            _logger.LogInfo($"Push: отправка '{message}'");
            Thread.Sleep(400);

            if (_rnd.Next(100) < 10)
                throw new Exception("Push-сервер не отвечает");

            _logger.LogInfo("Push: успешно отправлено");
        }
    }
}