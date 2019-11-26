namespace Demo
{
    partial class mainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.connectBtn = new System.Windows.Forms.Button();
            this.connectTypeCombo = new System.Windows.Forms.ComboBox();
            this.getcomCombo = new System.Windows.Forms.ComboBox();
            this.ipaddressTb = new System.Windows.Forms.TextBox();
            this.antCbl = new System.Windows.Forms.CheckedListBox();
            this.readpowerTb = new System.Windows.Forms.TextBox();
            this.writepowerTb = new System.Windows.Forms.TextBox();
            this.readpowerLbl = new System.Windows.Forms.Label();
            this.writepowerLbl = new System.Windows.Forms.Label();
            this.setReadpowerBtn = new System.Windows.Forms.Button();
            this.setWritepowerBtn = new System.Windows.Forms.Button();
            this.getReadpowerBtn = new System.Windows.Forms.Button();
            this.getWritepowerBtn = new System.Windows.Forms.Button();
            this.connectStateLbl = new System.Windows.Forms.Label();
            this.genSettingsBtn = new System.Windows.Forms.Button();
            this.gen2Settings1 = new System.Windows.Forms.RadioButton();
            this.gen2Settings2 = new System.Windows.Forms.RadioButton();
            this.gen2Settings3 = new System.Windows.Forms.RadioButton();
            this.infoDGV = new System.Windows.Forms.DataGridView();
            this.startReadingBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.countLbl = new System.Windows.Forms.Label();
            this.clearBtn = new System.Windows.Forms.Button();
            this.ipsetBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.antLbl = new System.Windows.Forms.Label();
            this.RF_timeLbl = new System.Windows.Forms.Label();
            this.infoLb = new System.Windows.Forms.ListBox();
            this.buzzerBtn = new System.Windows.Forms.Button();
            this.buzzerTimeTb = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.gpioTestBtn = new System.Windows.Forms.Button();
            this.testGpioTimeTb = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.infoDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // connectBtn
            // 
            this.connectBtn.Location = new System.Drawing.Point(13, 13);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(75, 23);
            this.connectBtn.TabIndex = 0;
            this.connectBtn.Text = "连接";
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.connectBtn_Click);
            // 
            // connectTypeCombo
            // 
            this.connectTypeCombo.FormattingEnabled = true;
            this.connectTypeCombo.Items.AddRange(new object[] {
            "COM",
            "TCP"});
            this.connectTypeCombo.Location = new System.Drawing.Point(101, 24);
            this.connectTypeCombo.Name = "connectTypeCombo";
            this.connectTypeCombo.Size = new System.Drawing.Size(47, 20);
            this.connectTypeCombo.TabIndex = 1;
            this.connectTypeCombo.SelectedIndexChanged += new System.EventHandler(this.connectTypeCombo_SelectedIndexChanged);
            // 
            // getcomCombo
            // 
            this.getcomCombo.FormattingEnabled = true;
            this.getcomCombo.Location = new System.Drawing.Point(158, 23);
            this.getcomCombo.Name = "getcomCombo";
            this.getcomCombo.Size = new System.Drawing.Size(135, 20);
            this.getcomCombo.TabIndex = 2;
            this.getcomCombo.Visible = false;
            this.getcomCombo.SelectedIndexChanged += new System.EventHandler(this.getcomCombo_SelectedIndexChanged);
            // 
            // ipaddressTb
            // 
            this.ipaddressTb.Location = new System.Drawing.Point(159, 22);
            this.ipaddressTb.Name = "ipaddressTb";
            this.ipaddressTb.Size = new System.Drawing.Size(135, 21);
            this.ipaddressTb.TabIndex = 3;
            this.ipaddressTb.Text = "192.168.8.166:8086";
            this.ipaddressTb.Visible = false;
            this.ipaddressTb.TextChanged += new System.EventHandler(this.ipaddressTb_TextChanged);
            // 
            // antCbl
            // 
            this.antCbl.FormattingEnabled = true;
            this.antCbl.Items.AddRange(new object[] {
            "天线 1",
            "天线 2",
            "天线 3",
            "天线 4"});
            this.antCbl.Location = new System.Drawing.Point(12, 73);
            this.antCbl.Name = "antCbl";
            this.antCbl.Size = new System.Drawing.Size(76, 68);
            this.antCbl.TabIndex = 4;
            this.antCbl.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.antCbl_ItemCheck);
            // 
            // readpowerTb
            // 
            this.readpowerTb.Location = new System.Drawing.Point(12, 329);
            this.readpowerTb.Name = "readpowerTb";
            this.readpowerTb.Size = new System.Drawing.Size(76, 21);
            this.readpowerTb.TabIndex = 5;
            this.readpowerTb.Text = "0";
            // 
            // writepowerTb
            // 
            this.writepowerTb.Location = new System.Drawing.Point(12, 399);
            this.writepowerTb.Name = "writepowerTb";
            this.writepowerTb.Size = new System.Drawing.Size(76, 21);
            this.writepowerTb.TabIndex = 6;
            this.writepowerTb.Text = "0";
            // 
            // readpowerLbl
            // 
            this.readpowerLbl.AutoSize = true;
            this.readpowerLbl.Location = new System.Drawing.Point(12, 314);
            this.readpowerLbl.Name = "readpowerLbl";
            this.readpowerLbl.Size = new System.Drawing.Size(41, 12);
            this.readpowerLbl.TabIndex = 7;
            this.readpowerLbl.Text = "读功率";
            // 
            // writepowerLbl
            // 
            this.writepowerLbl.AutoSize = true;
            this.writepowerLbl.Location = new System.Drawing.Point(11, 384);
            this.writepowerLbl.Name = "writepowerLbl";
            this.writepowerLbl.Size = new System.Drawing.Size(41, 12);
            this.writepowerLbl.TabIndex = 8;
            this.writepowerLbl.Text = "写功率";
            // 
            // setReadpowerBtn
            // 
            this.setReadpowerBtn.Location = new System.Drawing.Point(94, 356);
            this.setReadpowerBtn.Name = "setReadpowerBtn";
            this.setReadpowerBtn.Size = new System.Drawing.Size(58, 23);
            this.setReadpowerBtn.TabIndex = 9;
            this.setReadpowerBtn.Text = "设置";
            this.setReadpowerBtn.UseVisualStyleBackColor = true;
            this.setReadpowerBtn.Click += new System.EventHandler(this.setReadpowerBtn_Click);
            // 
            // setWritepowerBtn
            // 
            this.setWritepowerBtn.Location = new System.Drawing.Point(94, 428);
            this.setWritepowerBtn.Name = "setWritepowerBtn";
            this.setWritepowerBtn.Size = new System.Drawing.Size(58, 23);
            this.setWritepowerBtn.TabIndex = 10;
            this.setWritepowerBtn.Text = "设置";
            this.setWritepowerBtn.UseVisualStyleBackColor = true;
            this.setWritepowerBtn.Click += new System.EventHandler(this.setWritepowerBtn_Click);
            // 
            // getReadpowerBtn
            // 
            this.getReadpowerBtn.Location = new System.Drawing.Point(94, 327);
            this.getReadpowerBtn.Name = "getReadpowerBtn";
            this.getReadpowerBtn.Size = new System.Drawing.Size(58, 23);
            this.getReadpowerBtn.TabIndex = 11;
            this.getReadpowerBtn.Text = "获取";
            this.getReadpowerBtn.UseVisualStyleBackColor = true;
            this.getReadpowerBtn.Click += new System.EventHandler(this.getReadpowerBtn_Click);
            // 
            // getWritepowerBtn
            // 
            this.getWritepowerBtn.Location = new System.Drawing.Point(94, 399);
            this.getWritepowerBtn.Name = "getWritepowerBtn";
            this.getWritepowerBtn.Size = new System.Drawing.Size(58, 23);
            this.getWritepowerBtn.TabIndex = 12;
            this.getWritepowerBtn.Text = "获取";
            this.getWritepowerBtn.UseVisualStyleBackColor = true;
            this.getWritepowerBtn.Click += new System.EventHandler(this.getWritepowerBtn_Click);
            // 
            // connectStateLbl
            // 
            this.connectStateLbl.AutoSize = true;
            this.connectStateLbl.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.connectStateLbl.ForeColor = System.Drawing.Color.DodgerBlue;
            this.connectStateLbl.Location = new System.Drawing.Point(605, 16);
            this.connectStateLbl.Name = "connectStateLbl";
            this.connectStateLbl.Size = new System.Drawing.Size(59, 16);
            this.connectStateLbl.TabIndex = 13;
            this.connectStateLbl.Text = "未连接";
            // 
            // genSettingsBtn
            // 
            this.genSettingsBtn.Location = new System.Drawing.Point(12, 505);
            this.genSettingsBtn.Name = "genSettingsBtn";
            this.genSettingsBtn.Size = new System.Drawing.Size(112, 23);
            this.genSettingsBtn.TabIndex = 15;
            this.genSettingsBtn.Text = "Gen2设置";
            this.genSettingsBtn.UseVisualStyleBackColor = true;
            this.genSettingsBtn.Click += new System.EventHandler(this.genSettingsBtn_Click);
            // 
            // gen2Settings1
            // 
            this.gen2Settings1.AutoSize = true;
            this.gen2Settings1.Location = new System.Drawing.Point(12, 461);
            this.gen2Settings1.Name = "gen2Settings1";
            this.gen2Settings1.Size = new System.Drawing.Size(113, 16);
            this.gen2Settings1.TabIndex = 16;
            this.gen2Settings1.TabStop = true;
            this.gen2Settings1.Text = "最佳标签数 1~50";
            this.gen2Settings1.UseVisualStyleBackColor = true;
            // 
            // gen2Settings2
            // 
            this.gen2Settings2.AutoSize = true;
            this.gen2Settings2.Location = new System.Drawing.Point(12, 483);
            this.gen2Settings2.Name = "gen2Settings2";
            this.gen2Settings2.Size = new System.Drawing.Size(125, 16);
            this.gen2Settings2.TabIndex = 17;
            this.gen2Settings2.TabStop = true;
            this.gen2Settings2.Text = "最佳标签数 50~100";
            this.gen2Settings2.UseVisualStyleBackColor = true;
            // 
            // gen2Settings3
            // 
            this.gen2Settings3.AutoSize = true;
            this.gen2Settings3.Location = new System.Drawing.Point(13, 439);
            this.gen2Settings3.Name = "gen2Settings3";
            this.gen2Settings3.Size = new System.Drawing.Size(53, 16);
            this.gen2Settings3.TabIndex = 18;
            this.gen2Settings3.TabStop = true;
            this.gen2Settings3.Text = "配置3";
            this.gen2Settings3.UseVisualStyleBackColor = true;
            this.gen2Settings3.Visible = false;
            // 
            // infoDGV
            // 
            this.infoDGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.infoDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.infoDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.infoDGV.Location = new System.Drawing.Point(181, 51);
            this.infoDGV.Name = "infoDGV";
            this.infoDGV.RowTemplate.Height = 23;
            this.infoDGV.Size = new System.Drawing.Size(696, 484);
            this.infoDGV.TabIndex = 19;
            this.infoDGV.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.infoDGV_RowStateChanged);
            // 
            // startReadingBtn
            // 
            this.startReadingBtn.Location = new System.Drawing.Point(14, 44);
            this.startReadingBtn.Name = "startReadingBtn";
            this.startReadingBtn.Size = new System.Drawing.Size(75, 23);
            this.startReadingBtn.TabIndex = 20;
            this.startReadingBtn.Text = "开始读卡";
            this.startReadingBtn.UseVisualStyleBackColor = true;
            this.startReadingBtn.Click += new System.EventHandler(this.startReadingBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(99, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 21;
            this.label1.Text = "连接类型";
            // 
            // countLbl
            // 
            this.countLbl.AutoSize = true;
            this.countLbl.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.countLbl.ForeColor = System.Drawing.Color.DodgerBlue;
            this.countLbl.Location = new System.Drawing.Point(473, 13);
            this.countLbl.Name = "countLbl";
            this.countLbl.Size = new System.Drawing.Size(24, 27);
            this.countLbl.TabIndex = 22;
            this.countLbl.Text = "0";
            // 
            // clearBtn
            // 
            this.clearBtn.Location = new System.Drawing.Point(302, 22);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(75, 23);
            this.clearBtn.TabIndex = 23;
            this.clearBtn.Text = "清空";
            this.clearBtn.UseVisualStyleBackColor = true;
            this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
            // 
            // ipsetBtn
            // 
            this.ipsetBtn.Location = new System.Drawing.Point(703, 1);
            this.ipsetBtn.Name = "ipsetBtn";
            this.ipsetBtn.Size = new System.Drawing.Size(92, 43);
            this.ipsetBtn.TabIndex = 24;
            this.ipsetBtn.Text = "IP检索和设置";
            this.ipsetBtn.UseVisualStyleBackColor = true;
            this.ipsetBtn.Click += new System.EventHandler(this.ipsetBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label2.Location = new System.Drawing.Point(393, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 22);
            this.label2.TabIndex = 25;
            this.label2.Text = "标签数：";
            // 
            // antLbl
            // 
            this.antLbl.AutoSize = true;
            this.antLbl.ForeColor = System.Drawing.Color.DodgerBlue;
            this.antLbl.Location = new System.Drawing.Point(158, 7);
            this.antLbl.Name = "antLbl";
            this.antLbl.Size = new System.Drawing.Size(29, 12);
            this.antLbl.TabIndex = 26;
            this.antLbl.Text = "Ant ";
            // 
            // RF_timeLbl
            // 
            this.RF_timeLbl.AutoSize = true;
            this.RF_timeLbl.Location = new System.Drawing.Point(302, 6);
            this.RF_timeLbl.Name = "RF_timeLbl";
            this.RF_timeLbl.Size = new System.Drawing.Size(23, 12);
            this.RF_timeLbl.TabIndex = 27;
            this.RF_timeLbl.Text = "RF ";
            // 
            // infoLb
            // 
            this.infoLb.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.infoLb.Cursor = System.Windows.Forms.Cursors.Default;
            this.infoLb.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.infoLb.ForeColor = System.Drawing.Color.Black;
            this.infoLb.FormattingEnabled = true;
            this.infoLb.HorizontalScrollbar = true;
            this.infoLb.ItemHeight = 17;
            this.infoLb.Location = new System.Drawing.Point(14, 148);
            this.infoLb.Name = "infoLb";
            this.infoLb.ScrollAlwaysVisible = true;
            this.infoLb.Size = new System.Drawing.Size(161, 157);
            this.infoLb.TabIndex = 28;
            // 
            // buzzerBtn
            // 
            this.buzzerBtn.Location = new System.Drawing.Point(801, 2);
            this.buzzerBtn.Name = "buzzerBtn";
            this.buzzerBtn.Size = new System.Drawing.Size(76, 21);
            this.buzzerBtn.TabIndex = 29;
            this.buzzerBtn.Text = "报警测试";
            this.buzzerBtn.UseVisualStyleBackColor = true;
            this.buzzerBtn.Click += new System.EventHandler(this.buzzerBtn_Click);
            // 
            // buzzerTimeTb
            // 
            this.buzzerTimeTb.Location = new System.Drawing.Point(801, 27);
            this.buzzerTimeTb.Name = "buzzerTimeTb";
            this.buzzerTimeTb.Size = new System.Drawing.Size(53, 21);
            this.buzzerTimeTb.TabIndex = 30;
            this.buzzerTimeTb.Text = "5";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(860, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 31;
            this.label3.Text = "秒";
            // 
            // gpioTestBtn
            // 
            this.gpioTestBtn.Location = new System.Drawing.Point(101, 51);
            this.gpioTestBtn.Name = "gpioTestBtn";
            this.gpioTestBtn.Size = new System.Drawing.Size(51, 23);
            this.gpioTestBtn.TabIndex = 32;
            this.gpioTestBtn.Text = "GPIO";
            this.gpioTestBtn.UseVisualStyleBackColor = true;
            this.gpioTestBtn.Click += new System.EventHandler(this.gpioTestBtn_Click);
            // 
            // testGpioTimeTb
            // 
            this.testGpioTimeTb.Location = new System.Drawing.Point(101, 81);
            this.testGpioTimeTb.Name = "testGpioTimeTb";
            this.testGpioTimeTb.Size = new System.Drawing.Size(51, 21);
            this.testGpioTimeTb.TabIndex = 33;
            this.testGpioTimeTb.Text = "20";
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 546);
            this.Controls.Add(this.testGpioTimeTb);
            this.Controls.Add(this.gpioTestBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buzzerTimeTb);
            this.Controls.Add(this.buzzerBtn);
            this.Controls.Add(this.infoLb);
            this.Controls.Add(this.RF_timeLbl);
            this.Controls.Add(this.antLbl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ipsetBtn);
            this.Controls.Add(this.clearBtn);
            this.Controls.Add(this.countLbl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.startReadingBtn);
            this.Controls.Add(this.infoDGV);
            this.Controls.Add(this.gen2Settings3);
            this.Controls.Add(this.gen2Settings2);
            this.Controls.Add(this.gen2Settings1);
            this.Controls.Add(this.genSettingsBtn);
            this.Controls.Add(this.connectStateLbl);
            this.Controls.Add(this.getWritepowerBtn);
            this.Controls.Add(this.getReadpowerBtn);
            this.Controls.Add(this.setWritepowerBtn);
            this.Controls.Add(this.setReadpowerBtn);
            this.Controls.Add(this.writepowerLbl);
            this.Controls.Add(this.readpowerLbl);
            this.Controls.Add(this.writepowerTb);
            this.Controls.Add(this.readpowerTb);
            this.Controls.Add(this.antCbl);
            this.Controls.Add(this.ipaddressTb);
            this.Controls.Add(this.getcomCombo);
            this.Controls.Add(this.connectTypeCombo);
            this.Controls.Add(this.connectBtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "mainForm";
            ((System.ComponentModel.ISupportInitialize)(this.infoDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button connectBtn;
        private System.Windows.Forms.ComboBox connectTypeCombo;
        private System.Windows.Forms.ComboBox getcomCombo;
        private System.Windows.Forms.TextBox ipaddressTb;
        private System.Windows.Forms.CheckedListBox antCbl;
        private System.Windows.Forms.TextBox readpowerTb;
        private System.Windows.Forms.TextBox writepowerTb;
        private System.Windows.Forms.Label readpowerLbl;
        private System.Windows.Forms.Label writepowerLbl;
        private System.Windows.Forms.Button setReadpowerBtn;
        private System.Windows.Forms.Button setWritepowerBtn;
        private System.Windows.Forms.Button getReadpowerBtn;
        private System.Windows.Forms.Button getWritepowerBtn;
        private System.Windows.Forms.Label connectStateLbl;
        private System.Windows.Forms.Button genSettingsBtn;
        private System.Windows.Forms.RadioButton gen2Settings1;
        private System.Windows.Forms.RadioButton gen2Settings2;
        private System.Windows.Forms.RadioButton gen2Settings3;
        private System.Windows.Forms.DataGridView infoDGV;
        private System.Windows.Forms.Button startReadingBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label countLbl;
        private System.Windows.Forms.Button clearBtn;
        private System.Windows.Forms.Button ipsetBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label antLbl;
        private System.Windows.Forms.Label RF_timeLbl;
        private System.Windows.Forms.ListBox infoLb;
        private System.Windows.Forms.Button buzzerBtn;
        private System.Windows.Forms.TextBox buzzerTimeTb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button gpioTestBtn;
        private System.Windows.Forms.TextBox testGpioTimeTb;
    }
}

