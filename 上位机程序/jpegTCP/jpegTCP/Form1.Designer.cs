namespace jpegTCP
{
    partial class MainWindow
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.picBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cobProtocol = new System.Windows.Forms.ComboBox();
            this.labIP = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRec = new System.Windows.Forms.TextBox();
            this.MainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.infoLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.labPort = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.btnHead = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.MainStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // picBox
            // 
            this.picBox.Location = new System.Drawing.Point(12, 12);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(563, 434);
            this.picBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox.TabIndex = 0;
            this.picBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(608, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "协议类型";
            // 
            // cobProtocol
            // 
            this.cobProtocol.FormattingEnabled = true;
            this.cobProtocol.Items.AddRange(new object[] {
            "TCP Server",
            "TCP Client"});
            this.cobProtocol.Location = new System.Drawing.Point(712, 35);
            this.cobProtocol.Name = "cobProtocol";
            this.cobProtocol.Size = new System.Drawing.Size(121, 23);
            this.cobProtocol.TabIndex = 2;
            this.cobProtocol.Text = "TCP Server";
            this.cobProtocol.SelectedIndexChanged += new System.EventHandler(this.cobProtocol_SelectedIndexChanged);
            // 
            // labIP
            // 
            this.labIP.AutoSize = true;
            this.labIP.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labIP.Location = new System.Drawing.Point(608, 84);
            this.labIP.Name = "labIP";
            this.labIP.Size = new System.Drawing.Size(68, 15);
            this.labIP.TabIndex = 3;
            this.labIP.Text = "本地IP：";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(712, 74);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(121, 25);
            this.txtIP.TabIndex = 6;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(741, 160);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(109, 34);
            this.btnStart.TabIndex = 7;
            this.btnStart.Text = "连接";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(601, 160);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(109, 34);
            this.btnDisconnect.TabIndex = 8;
            this.btnDisconnect.Text = "断开连接";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(598, 208);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "网络数据接收：";
            // 
            // txtRec
            // 
            this.txtRec.Location = new System.Drawing.Point(601, 236);
            this.txtRec.Multiline = true;
            this.txtRec.Name = "txtRec";
            this.txtRec.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRec.Size = new System.Drawing.Size(249, 82);
            this.txtRec.TabIndex = 11;
            // 
            // MainStatusStrip
            // 
            this.MainStatusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infoLabel});
            this.MainStatusStrip.Location = new System.Drawing.Point(0, 462);
            this.MainStatusStrip.Name = "MainStatusStrip";
            this.MainStatusStrip.Size = new System.Drawing.Size(872, 25);
            this.MainStatusStrip.TabIndex = 12;
            this.MainStatusStrip.Text = "statusStrip1";
            // 
            // infoLabel
            // 
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(39, 20);
            this.infoLabel.Text = "就绪";
            // 
            // labPort
            // 
            this.labPort.AutoSize = true;
            this.labPort.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labPort.Location = new System.Drawing.Point(608, 127);
            this.labPort.Name = "labPort";
            this.labPort.Size = new System.Drawing.Size(82, 15);
            this.labPort.TabIndex = 13;
            this.labPort.Text = "本地端口：";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(712, 117);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(121, 25);
            this.txtPort.TabIndex = 14;
            this.txtPort.Text = "8081";
            // 
            // btnHead
            // 
            this.btnHead.Location = new System.Drawing.Point(601, 339);
            this.btnHead.Name = "btnHead";
            this.btnHead.Size = new System.Drawing.Size(109, 34);
            this.btnHead.TabIndex = 15;
            this.btnHead.Text = "前进";
            this.btnHead.UseVisualStyleBackColor = true;
            this.btnHead.Click += new System.EventHandler(this.btnHead_Click);
            // 
            // btnRight
            // 
            this.btnRight.Location = new System.Drawing.Point(741, 399);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(109, 34);
            this.btnRight.TabIndex = 16;
            this.btnRight.Text = "右转";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(741, 339);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(109, 34);
            this.btnStop.TabIndex = 17;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnLeft
            // 
            this.btnLeft.Location = new System.Drawing.Point(601, 399);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(109, 34);
            this.btnLeft.TabIndex = 18;
            this.btnLeft.Text = "左转";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 487);
            this.Controls.Add(this.btnLeft);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnRight);
            this.Controls.Add(this.btnHead);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.labPort);
            this.Controls.Add(this.MainStatusStrip);
            this.Controls.Add(this.txtRec);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.labIP);
            this.Controls.Add(this.cobProtocol);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picBox);
            this.Name = "MainWindow";
            this.Text = "jpegTCP";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.Resize += new System.EventHandler(this.MainWindow_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.MainStatusStrip.ResumeLayout(false);
            this.MainStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cobProtocol;
        private System.Windows.Forms.Label labIP;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRec;
        private System.Windows.Forms.StatusStrip MainStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel infoLabel;
        private System.Windows.Forms.Label labPort;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button btnHead;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnLeft;
    }
}

