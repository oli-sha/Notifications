using Notifications.Interfaces;

namespace Notifications.Helpers
{
    public class NotificationHelper
    {
        private readonly INotificationSender _sender;
        private readonly ILogger _logger;

        public NotificationHelper(INotificationSender sender, ILogger logger)
        {
            _sender = sender;
            _logger = logger;
        }

        public void Send(string message)
        {
            _logger.LogInfo($"Helper: отправка через {_sender.ChannelName}");
            _sender.Send(message);
        }
    }
}