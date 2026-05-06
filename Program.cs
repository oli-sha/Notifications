using Microsoft.Extensions.DependencyInjection;
using Notifications.Interfaces;
using Notifications.Logger;
using Notifications.Services;
using Notifications.Helpers;

namespace Notifications
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var services = new ServiceCollection();

            services.AddSingleton<ILogger, DualLogger>();

            services.AddTransient<INotificationSender, EmailSender>();
            services.AddTransient<INotificationSender, SmsSender>();
            services.AddTransient<INotificationSender, PushSender>();

            services.AddTransient<NotificationHelper>();
            services.AddTransient<Func<INotificationSender, NotificationHelper>>(provider =>
            {
                return sender => ActivatorUtilities.CreateInstance<NotificationHelper>(provider, sender);
            });

            services.AddTransient<MainForm>();

            using var provider = services.BuildServiceProvider();
            var form = provider.GetRequiredService<MainForm>();
            Application.Run(form);
        }
    }
}