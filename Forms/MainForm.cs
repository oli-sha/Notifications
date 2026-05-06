using Notifications.Helpers;
using Notifications.Interfaces;

namespace Notifications
{
    public partial class MainForm : Form
    {
        private readonly Dictionary<string, INotificationSender> _channels;
        private readonly ILogger _logger;
        private readonly Func<INotificationSender, NotificationHelper> _helperFactory;
        private int _successCount = 0;
        private int _smsCount = 0;
        private int _emptyAttempts = 0;

        public MainForm(
    IEnumerable<INotificationSender> channels,
    ILogger logger,
    Func<INotificationSender, NotificationHelper> helperFactory)
        {
            _channels = channels.ToDictionary(c => c.ChannelName);
            _logger = logger;
            _helperFactory = helperFactory;

            InitializeComponent();

            txtMessage.PlaceholderText = "Введите текст";

            cmbServiceType.DataSource = _channels.Keys.ToList();
            _logger.OnNewLog += AppendLog;

            Shown += MainForm_Shown;
        }

        private void MainForm_Shown(object? sender, EventArgs e)
        {
            cmbServiceType.Focus();
        }

        private void TxtMessage_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                BtnSend_Click(sender, EventArgs.Empty);
            }
        }

        private void CmbServiceType_SelectedIndexChanged(object? sender, EventArgs e)
        {
            string selected = cmbServiceType.SelectedItem?.ToString() ?? "";
            _logger.LogInfo($"Пользователь выбрал сервис: {selected}");
        }



        private async void BtnSend_Click(object? sender, EventArgs e)
        {
            string message = txtMessage.Text.Trim();

            if (string.IsNullOrEmpty(message))
            {
                _emptyAttempts++;
                _logger.LogWarning($"Пустое сообщение #{_emptyAttempts}");

                if (_emptyAttempts >= 3)
                {
                    ShowEasterEgg("Ты любишь отправлять пустоту! 3 раза подряд!");
                    _emptyAttempts = 0;
                }
                else
                {
                    MessageBox.Show("Сообщение не может быть пустым", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }

            string selected = cmbServiceType.SelectedItem?.ToString() ?? "";
            if (!_channels.TryGetValue(selected, out var channel))
            {
                _logger.LogError($"Канал {selected} не найден");
                return;
            }

            btnSend.Enabled = false;
            try
            {
                await Task.Run(() =>
                {
                    var helper = _helperFactory(channel);
                    helper.Send(message);
                });

                _successCount++;
                if (selected == "SMS") _smsCount++;

                await CheckEasterEggs(selected, message);


                txtMessage.Clear();
                txtMessage.Focus();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка: {ex.Message}");
                MessageBox.Show($"Не удалось отправить: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSend.Enabled = true;
            }
        }

        private void AppendLog(string log)
        {
            if (rtbLog.InvokeRequired)
            {
                rtbLog.Invoke(() => AppendLog(log));
                return;
            }
            rtbLog.AppendText(log + Environment.NewLine);
            rtbLog.ScrollToCaret();
        }

        private async Task CheckEasterEggs(string channel, string message)
        {
            if (_successCount == 3)
                ShowEasterEgg("Кого вы выберите? - девочек");

            if (message.Contains("дарина", StringComparison.OrdinalIgnoreCase))
                ShowEasterEgg("дарина легенда");

            if (message.Contains("Дина Сергеевна"))
                ShowEasterEgg("дс+аа = <3");

            if (message.Contains("чебоксары", StringComparison.OrdinalIgnoreCase))
                ShowEasterEgg("Чебоксары - жемчужина России");

            if (_successCount % 5 == 0)
                ShowEasterEgg("Этим вы прострелили себе вторую ногу...");

            if (message.Contains("new", StringComparison.OrdinalIgnoreCase))
                ShowEasterEgg("проблема в new");

            if (message.Contains("инвестиция", StringComparison.OrdinalIgnoreCase))
                ShowEasterEgg("лего дорожает быстрее золота");

            if (message.Contains("совет", StringComparison.OrdinalIgnoreCase))
                ShowEasterEgg("коробки можно складывать друг в дружку");

            if (message.Split(' ').Length > 10)
                ShowEasterEgg("Отстооой");

            if (message.Contains("удив", StringComparison.OrdinalIgnoreCase))
                ShowEasterEgg("мак оказывается можно и нужно заряжать...");

            if (message.Contains("согласуйте", StringComparison.OrdinalIgnoreCase))
                ShowEasterEgg("согласовано");

            if (_successCount == 1)
                ShowEasterEgg("зря вы конечно первыми вышли");

            await Task.CompletedTask;
        }

        private void ShowEasterEgg(string text)
        {
            _logger.LogInfo($"[ПАСХАЛКА] {text}");
            MessageBox.Show(text, "секретная ачивка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void panelSecret_Click(object sender, EventArgs e)
        {

        }
    }
}