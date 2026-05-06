namespace Notifications
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtMessage;
        private ComboBox cmbServiceType;
        private Button btnSend;
        private RichTextBox rtbLog;
        
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            txtMessage = new TextBox();
            cmbServiceType = new ComboBox();
            btnSend = new Button();
            rtbLog = new RichTextBox();
            SuspendLayout();
            // 
            // txtMessage
            // 
            txtMessage.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtMessage.BackColor = Color.FromArgb(192, 192, 255);
            txtMessage.Location = new Point(49, 48);
            txtMessage.Margin = new Padding(5);
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(911, 39);
            txtMessage.TabIndex = 1;
            txtMessage.KeyPress += TxtMessage_KeyPress;
            // 
            // cmbServiceType
            // 
            cmbServiceType.BackColor = Color.FromArgb(192, 192, 255);
            cmbServiceType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbServiceType.Location = new Point(49, 128);
            cmbServiceType.Margin = new Padding(5);
            cmbServiceType.Name = "cmbServiceType";
            cmbServiceType.Size = new Size(371, 40);
            cmbServiceType.TabIndex = 0;
            cmbServiceType.SelectedIndexChanged += CmbServiceType_SelectedIndexChanged;
            // 
            // btnSend
            // 
            btnSend.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSend.BackColor = Color.FromArgb(192, 192, 255);
            btnSend.Location = new Point(1007, 48);
            btnSend.Margin = new Padding(5);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(196, 122);
            btnSend.TabIndex = 2;
            btnSend.Text = "Отправить уведомление";
            btnSend.UseVisualStyleBackColor = false;
            btnSend.Click += BtnSend_Click;
            // 
            // rtbLog
            // 
            rtbLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtbLog.BackColor = Color.FromArgb(192, 192, 255);
            rtbLog.Location = new Point(49, 229);
            rtbLog.Margin = new Padding(5);
            rtbLog.Name = "rtbLog";
            rtbLog.ReadOnly = true;
            rtbLog.Size = new Size(1154, 692);
            rtbLog.TabIndex = 3;
            rtbLog.Text = "";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(128, 128, 255);
            ClientSize = new Size(1271, 987);
            Controls.Add(rtbLog);
            Controls.Add(btnSend);
            Controls.Add(cmbServiceType);
            Controls.Add(txtMessage);
            Margin = new Padding(5);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Уведомления";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}