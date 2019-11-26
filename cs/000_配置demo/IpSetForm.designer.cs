namespace ScanDemo
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.button2 = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.labTip = new System.Windows.Forms.Label();
            this.ipTb = new System.Windows.Forms.TextBox();
            this.maskTb = new System.Windows.Forms.TextBox();
            this.gatewayTb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ipsetBtn = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.portTb = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.endIPTb = new System.Windows.Forms.TextBox();
            this.startIPTb = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(436, 159);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "扫描";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(12, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(418, 292);
            this.listBox1.TabIndex = 1;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(492, 159);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(48, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(12, 320);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(607, 22);
            this.progressBar1.TabIndex = 5;
            // 
            // labTip
            // 
            this.labTip.AutoSize = true;
            this.labTip.Location = new System.Drawing.Point(262, 345);
            this.labTip.Name = "labTip";
            this.labTip.Size = new System.Drawing.Size(23, 12);
            this.labTip.TabIndex = 6;
            this.labTip.Text = "Tip";
            // 
            // ipTb
            // 
            this.ipTb.Location = new System.Drawing.Point(495, 229);
            this.ipTb.Name = "ipTb";
            this.ipTb.Size = new System.Drawing.Size(124, 21);
            this.ipTb.TabIndex = 8;
            // 
            // maskTb
            // 
            this.maskTb.Location = new System.Drawing.Point(495, 256);
            this.maskTb.Name = "maskTb";
            this.maskTb.Size = new System.Drawing.Size(124, 21);
            this.maskTb.TabIndex = 9;
            // 
            // gatewayTb
            // 
            this.gatewayTb.Location = new System.Drawing.Point(495, 283);
            this.gatewayTb.Name = "gatewayTb";
            this.gatewayTb.Size = new System.Drawing.Size(124, 21);
            this.gatewayTb.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(439, 238);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "IP地址";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(439, 265);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "子网掩码";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(439, 292);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "网关";
            // 
            // ipsetBtn
            // 
            this.ipsetBtn.Location = new System.Drawing.Point(436, 200);
            this.ipsetBtn.Name = "ipsetBtn";
            this.ipsetBtn.Size = new System.Drawing.Size(104, 23);
            this.ipsetBtn.TabIndex = 14;
            this.ipsetBtn.Text = "网络设置";
            this.ipsetBtn.UseVisualStyleBackColor = true;
            this.ipsetBtn.Click += new System.EventHandler(this.ipsetBtn_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(436, 118);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(104, 23);
            this.button5.TabIndex = 23;
            this.button5.Text = "指定网段";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // portTb
            // 
            this.portTb.Location = new System.Drawing.Point(483, 91);
            this.portTb.Name = "portTb";
            this.portTb.Size = new System.Drawing.Size(57, 21);
            this.portTb.TabIndex = 22;
            this.portTb.Text = "8086";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(445, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "端口：";
            // 
            // endIPTb
            // 
            this.endIPTb.Location = new System.Drawing.Point(441, 64);
            this.endIPTb.Name = "endIPTb";
            this.endIPTb.Size = new System.Drawing.Size(99, 21);
            this.endIPTb.TabIndex = 19;
            this.endIPTb.Text = "192.168.8.255";
            // 
            // startIPTb
            // 
            this.startIPTb.Location = new System.Drawing.Point(441, 34);
            this.startIPTb.Name = "startIPTb";
            this.startIPTb.Size = new System.Drawing.Size(99, 21);
            this.startIPTb.TabIndex = 18;
            this.startIPTb.Text = "192.168.8.1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(439, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 15;
            this.label8.Text = "IP地址：";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 370);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.portTb);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.endIPTb);
            this.Controls.Add(this.startIPTb);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.ipsetBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gatewayTb);
            this.Controls.Add(this.maskTb);
            this.Controls.Add(this.ipTb);
            this.Controls.Add(this.labTip);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "扫描IP段指定端口";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label labTip;
        private System.Windows.Forms.TextBox ipTb;
        private System.Windows.Forms.TextBox maskTb;
        private System.Windows.Forms.TextBox gatewayTb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button ipsetBtn;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox portTb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox endIPTb;
        private System.Windows.Forms.TextBox startIPTb;
        private System.Windows.Forms.Label label8;
    }
}

