namespace DotMetrics.TestChat
{
    partial class WindowsLiveMessenger
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WindowsLiveMessenger));
            this.receiveTxtbox = new System.Windows.Forms.TextBox();
            this.sendTxtbox = new System.Windows.Forms.TextBox();
            this.sendBtn = new System.Windows.Forms.Button();
            this.connectionBtn = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.disconnectBtn = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.IPtext = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Quit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // receiveTxtbox
            // 
            this.receiveTxtbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.receiveTxtbox.Location = new System.Drawing.Point(48, 74);
            this.receiveTxtbox.Multiline = true;
            this.receiveTxtbox.Name = "receiveTxtbox";
            this.receiveTxtbox.ReadOnly = true;
            this.receiveTxtbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.receiveTxtbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.receiveTxtbox.Size = new System.Drawing.Size(327, 295);
            this.receiveTxtbox.TabIndex = 0;
            // 
            // sendTxtbox
            // 
            this.sendTxtbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.sendTxtbox.Location = new System.Drawing.Point(48, 405);
            this.sendTxtbox.Name = "sendTxtbox";
            this.sendTxtbox.Size = new System.Drawing.Size(327, 20);
            this.sendTxtbox.TabIndex = 1;
            this.sendTxtbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.sendTxtbox_KeyDown);
            // 
            // sendBtn
            // 
            this.sendBtn.Location = new System.Drawing.Point(410, 405);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(96, 20);
            this.sendBtn.TabIndex = 3;
            this.sendBtn.Text = "Send";
            this.sendBtn.UseVisualStyleBackColor = true;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            // 
            // connectionBtn
            // 
            this.connectionBtn.Location = new System.Drawing.Point(410, 25);
            this.connectionBtn.Name = "connectionBtn";
            this.connectionBtn.Size = new System.Drawing.Size(117, 20);
            this.connectionBtn.TabIndex = 5;
            this.connectionBtn.Text = "Connect";
            this.connectionBtn.UseVisualStyleBackColor = true;
            this.connectionBtn.Click += new System.EventHandler(this.connectionBtn_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(555, 25);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(65, 17);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "IsServer";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // disconnectBtn
            // 
            this.disconnectBtn.Location = new System.Drawing.Point(410, 63);
            this.disconnectBtn.Name = "disconnectBtn";
            this.disconnectBtn.Size = new System.Drawing.Size(117, 23);
            this.disconnectBtn.TabIndex = 7;
            this.disconnectBtn.Text = "Disconnect";
            this.disconnectBtn.UseVisualStyleBackColor = true;
            this.disconnectBtn.Click += new System.EventHandler(this.disconnectBtn_Click);
            // 
            // timer
            // 
            this.timer.Interval = 10;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // IPtext
            // 
            this.IPtext.Location = new System.Drawing.Point(45, 25);
            this.IPtext.Name = "IPtext";
            this.IPtext.Size = new System.Drawing.Size(330, 20);
            this.IPtext.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(62, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Enter IP";
            // 
            // btn_Quit
            // 
            this.btn_Quit.Location = new System.Drawing.Point(543, 455);
            this.btn_Quit.Name = "btn_Quit";
            this.btn_Quit.Size = new System.Drawing.Size(77, 34);
            this.btn_Quit.TabIndex = 10;
            this.btn_Quit.Text = "Suicide";
            this.btn_Quit.UseVisualStyleBackColor = true;
            this.btn_Quit.Click += new System.EventHandler(this.btn_Quit_Click);
            // 
            // WindowsLiveMessenger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(632, 501);
            this.ControlBox = false;
            this.Controls.Add(this.btn_Quit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.IPtext);
            this.Controls.Add(this.disconnectBtn);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.connectionBtn);
            this.Controls.Add(this.sendBtn);
            this.Controls.Add(this.sendTxtbox);
            this.Controls.Add(this.receiveTxtbox);
            this.MaximumSize = new System.Drawing.Size(648, 540);
            this.MinimumSize = new System.Drawing.Size(648, 540);
            this.Name = "WindowsLiveMessenger";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "WindowsLiveMessenger";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox receiveTxtbox;
        private System.Windows.Forms.TextBox sendTxtbox;
        private System.Windows.Forms.Button sendBtn;
        private System.Windows.Forms.Button connectionBtn;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button disconnectBtn;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.TextBox IPtext;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Quit;
    }
}

