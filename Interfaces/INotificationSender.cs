namespace Notifications.Interfaces
{
    public interface INotificationSender
    {
        string ChannelName { get; }
        void Send(string message);
    }
}