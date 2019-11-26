using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ThingMagic;

namespace Demo
{
    public partial class mainForm : Form
    {
        private string readerURI;
        public Reader r;
        private int[] antennaList = { 0, 0, 0, 0 };
        private int readPower = 2000;
        private int writePower = 3000;
        string warningText = "";
        ConcurrentDictionary<String, TagReadData> infoDict = new ConcurrentDictionary<String, TagReadData>();
        Thread infoThread = null;

        public mainForm()
        {
            InitializeComponent();
            this.Text = "简易配置工具";
            connectTypeCombo.SelectedIndex = 0;
            getCOM();
            startReadingBtn.Enabled = false;
            buzzerBtn.Enabled = false;
            //infoDGV.RowHeadersVisible = false; //隐藏首列
            this.infoDGV.RowHeadersWidth = 80;
            infoDGV.RowsDefaultCellStyle.BackColor = Color.FromArgb(237, 243, 254);
            infoDGV.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(199, 237, 204);
            
        }

        private int getCOM()
        {
            getcomCombo.Items.Clear();
            string[] coms = SerialPort.GetPortNames();
            if(coms.Length > 0)
            {
                foreach (string vPortName in coms)
                {
                    getcomCombo.Items.Add(vPortName);
                }
                //Console.WriteLine(getcomCombo.SelectedIndex);
                getcomCombo.SelectedIndex = 0;
                readerURI = "tmr:///" + getcomCombo.SelectedItem;
                //Console.WriteLine("getCom " + readerURI);
                return coms.Length;
            }
            getcomCombo.Items.Add("无串口被检测到");
            getcomCombo.SelectedIndex = getcomCombo.Items.IndexOf("无串口被检测到");
            return -1;
        }

        private void connectTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (connectTypeCombo.Text.Equals("COM"))
            {
                ipaddressTb.Visible = false;
                getcomCombo.Visible = true;
                getCOM();
            }
            else if (connectTypeCombo.Text.Equals("TCP"))
            {
                getcomCombo.Visible = false;
                ipaddressTb.Visible = true;
                readerURI = "tcp://" + ipaddressTb.Text;
                //Console.WriteLine("connectType select " + readerURI);
            }
        }

        private void ipaddressTb_TextChanged(object sender, EventArgs e)
        {
            readerURI = "tcp://" + ipaddressTb.Text;
            //Console.WriteLine("tcp change " + readerURI);
        }

        private void connectBtn_Click(object sender, EventArgs e)
        {
            if (connectBtn.Text.Equals("连接"))
            {
                if (Connect())
                {
                    connectBtn.Text = "点击断开";
                    connectStateLbl.Text = "已连接";
                    connectStateLbl.ForeColor = Color.SpringGreen;
                    startReadingBtn.Enabled = true;
                    antCbl.Enabled = false;
                    ipsetBtn.Enabled = false;
                    buzzerBtn.Enabled = true;
                }
                else
                    Disconnect();
            }
            else // disconnect
            {
                r.StopReading();
                Disconnect();
                connectBtn.Text = "连接";
                connectStateLbl.Text = "已断开";
                connectStateLbl.ForeColor = Color.Red;
                startReadingBtn.Enabled = false;
                //防止其他指令操作
                setWritepowerBtn.Enabled = true;
                getWritepowerBtn.Enabled = true;
                setReadpowerBtn.Enabled = true;
                getReadpowerBtn.Enabled = true;
                genSettingsBtn.Enabled = true;
                ipsetBtn.Enabled = true;
                antCbl.Enabled = true;
                ipsetBtn.Enabled = true;
                buzzerBtn.Enabled = false;
                startReadingBtn.Text = "开始读卡";
                antLbl.Text = "Ant ";
                RF_timeLbl.Text = "RF ";
                infoLb.Items.Clear();
            }
        }

        private void Gen2Settings()
        {
            Console.WriteLine("Gen2Settings");

            Gen2.LinkFrequency blf = Gen2.LinkFrequency.LINK250KHZ;
            Gen2.Tari tari = Gen2.Tari.TARI_25US;
            Gen2.TagEncoding tagen = Gen2.TagEncoding.FM0;
            Gen2.Session session = Gen2.Session.S0;
            Gen2.Target target = Gen2.Target.AB;
            string choice = null;
            if (gen2Settings1.Checked)
            {
                //场景配置1 最佳标签数 1~50
                blf = Gen2.LinkFrequency.LINK640KHZ;
                tari = Gen2.Tari.TARI_6_25US;
                tagen = Gen2.TagEncoding.FM0;
                session = Gen2.Session.S1;
                target = Gen2.Target.A;
                choice = "配置1";
            }
            else if (gen2Settings2.Checked)
            {
                //场景配置3 最佳标签数 50~100
                blf = Gen2.LinkFrequency.LINK250KHZ;
                tari = Gen2.Tari.TARI_25US;
                tagen = Gen2.TagEncoding.M4;
                session = Gen2.Session.S0;
                target = Gen2.Target.A;
                choice = "配置 3";

            }
            else if (gen2Settings3.Checked)
            {
                //场景配置2  最佳标签数 100~200
                blf = Gen2.LinkFrequency.LINK250KHZ;
                tari = Gen2.Tari.TARI_25US;
                tagen = Gen2.TagEncoding.M4;
                session = Gen2.Session.S1;
                target = Gen2.Target.A;
                choice = "配置 2";
            }
            else
                choice = "默认配置";

            r.ParamSet("/reader/gen2/BLF", blf); // LINK640KHZ/LINK250KHZ/LINK320KHZ
            r.ParamSet("/reader/gen2/tagEncoding", tagen); // FM0/M2/M4/M8
            r.ParamSet("/reader/gen2/tari", tari); // TARI_25US/TARI_12_5US/TARI_6_25US
            r.ParamSet("/reader/gen2/session", session); // S0/S1/S2/S3
            r.ParamSet("/reader/gen2/target", target);  // A/AB/B/BA
            r.ParamSet("/reader/gen2/q", new Gen2.DynamicQ()); // StaticQ(qValue)/DynamicQ()
            if(choice.StartsWith("默认配置") == false)
                MessageBox.Show(choice + " 配置成功");
            //Console.WriteLine("Gen2 settings success");
        }

        private bool Connect()
        {
            
            if (connectTypeCombo.SelectedItem.Equals("TCP"))
            {
                //Reader.setSerialTransport("tcp", new SerialTransportTCP.Factory()); // java
                Console.WriteLine("SetSerialTransport");
                Reader.SetSerialTransport("tcp", SerialTransportTCP.CreateSerialReader);
            }

            if(readerURI.Contains("无串口被检测到"))
                return false;

            if (checkAntennaList() == 0)
                return false;
            
            Console.WriteLine("create " + readerURI);
            r = Reader.Create(readerURI);
            Console.WriteLine("created ...");

            try
            {
                r.Connect();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                if (null != r)
                {
                    r.Destroy();
                    r = null;
                }
            }

            Console.WriteLine("connected ...");

            // 设置天线
            if (antennaList != null)
            {
                foreach (int antenna in antennaList)
                {
                    if (antenna != 0)
                    {
                        //Console.WriteLine("paramSet antenna" + antenna);
                        antLbl.Text += "[" + antenna + "]";
                        r.ParamSet("/reader/tagop/antenna", antenna);
                    }
                }
            }

            // 设置Region
            Reader.Region[] supportedRegions = (Reader.Region[])r.ParamGet("/reader/region/supportedRegions");
            if (supportedRegions.Length < 1)
            {
                throw new Exception("Reader doesn't support any regions");
            }
            else
            {
                //foreach (Reader.Region region in supportedRegions)
                //    Console.WriteLine(region);
                r.ParamSet("/reader/region/id", supportedRegions[0]);
            }

            // 获取读写器信息
            String region = r.ParamGet("/reader/region/id").ToString();
            String model = r.ParamGet("/reader/version/model").ToString();
            String hw = r.ParamGet("/reader/version/hardware").ToString();
            String sw = r.ParamGet("/reader/version/software").ToString();
            ///String sn = r.ParamGet("/reader/version/serial").ToString();

            infoLb.Items.Add("Model    : " + model);
            infoLb.Items.Add("Region   : " + region);
            infoLb.Items.Add("Hardware : " + hw);
            infoLb.Items.Add("Software : " + sw);
            //infoLb.Items.Add("Serial   : " + sn);

            readPower = Convert.ToInt32(r.ParamGet("/reader/radio/readPower").ToString());
            writePower = Convert.ToInt32(r.ParamGet("/reader/radio/writePower").ToString());

            Console.WriteLine("paramSettings ...");
            

            readpowerTb.Text = readPower.ToString();
            writepowerTb.Text = writePower.ToString();

            r.ParamSet("/reader/read/asyncOffTime", 0);
            r.ParamSet("/reader/read/asyncOnTime", 1000);
            RF_timeLbl.Text += "On[" + r.ParamGet("/reader/read/asyncOffTime")
                + "] Off[" + r.ParamGet("/reader/read/asyncOnTime") + "]";

            Gen2Settings();

            int[] setAntennaList = null;
            setAntennaList = new int[antCbl.CheckedItems.Count];
            if (antCbl.CheckedItems.Count > 0)
            {
                for (int i = 0, j = 0; i < antCbl.Items.Count; i++)
                {
                    if (antCbl.GetItemChecked(i) && j < antCbl.CheckedItems.Count)
                    {
                        setAntennaList[j++] = i + 1;
                    }
                }
            }
            else
                MessageBox.Show("setAntennaList error");
            
            SimpleReadPlan plan = new SimpleReadPlan(setAntennaList, TagProtocol.GEN2, null, null, 1000);
            r.ParamSet("/reader/read/plan", plan);

            // 相当于连接后设置一个监听
            r.ReadException += ReadException;
            r.TagRead += PrintTagRead;
            
            return true;
        }

        private void Disconnect()
        {
            if (r != null)
                r.Destroy();
        }

        private int checkAntennaList()
        {
            for (int i = 0; i < antennaList.Length; i++)
            {
                if (antennaList[i] != 0)
                {
                    antennaList[i] = 0;
                }
            }
            Console.WriteLine("天线数目: " + antCbl.CheckedItems.Count);
            if (antCbl.CheckedItems.Count > 0)
            {
                for (int i = 0; i < antCbl.Items.Count; i++)
                {
                    if (antCbl.GetItemChecked(i))
                    {
                        Console.WriteLine("ant[" + i + "] IsChecked " + antCbl.Items[i]);
                        antennaList[i] = i + 1;
                    }

                }
                return antCbl.Items.Count;
            }
            else
                MessageBox.Show("请选择天线!", "天线的提示");
            return 0;
        }

        private void getcomCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            readerURI = "tmr:///" + getcomCombo.SelectedItem;
            Console.WriteLine("com change " + readerURI);
        }

        private void antCbl_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            Console.WriteLine(e.Index + " item check");
        }

        private void getReadpowerBtn_Click(object sender, EventArgs e)
        {
            readpowerTb.Text = r.ParamGet("/reader/radio/readPower").ToString();
        }

        private void getWritepowerBtn_Click(object sender, EventArgs e)
        {
            writepowerTb.Text = r.ParamGet("/reader/radio/writePower").ToString();
        }

        private void setReadpowerBtn_Click(object sender, EventArgs e)
        {
            if (readpowerTb.Text == "0")
            {
                MessageBox.Show("请输入功率值");
                return;
            }
            readPower = Convert.ToInt32(readpowerTb.Text);
            try
            {
                r.ParamSet("/reader/radio/readPower", readPower);
                MessageBox.Show("读功率配置成功");
            }
            catch (Exception re)
            {
                MessageBox.Show(re.Message, re.Source);
            }

        }

        private void setWritepowerBtn_Click(object sender, EventArgs e)
        {
            if (writepowerTb.Text == "0")
            {
                MessageBox.Show("请输入功率值");
                return;
            }
                
            writePower = Convert.ToInt32(writepowerTb.Text);
            try
            {
                r.ParamSet("/reader/radio/writePower", writePower);
                MessageBox.Show("写功率配置成功");
            }
            catch (Exception re)
            {
                MessageBox.Show(re.Message, re.Source);
            }

        }

        private void startReadingBtn_Click(object sender, EventArgs e)
        {
            if(startReadingBtn.Text.Equals("开始读卡"))
            {
                Console.WriteLine("[" + antennaList[0] + antennaList[1] + antennaList[2] + antennaList[3] + "]");
                
                //防止其他指令操作
                setWritepowerBtn.Enabled = false;
                getWritepowerBtn.Enabled = false;
                setReadpowerBtn.Enabled = false;
                getReadpowerBtn.Enabled = false;
                genSettingsBtn.Enabled = false;
                buzzerBtn.Enabled = false;
                
                startReadingBtn.Text = "点击停止读卡";
                r.StartReading();
                Console.WriteLine("startReading ...");
            }
            else if(startReadingBtn.Text.Equals("点击停止读卡"))
            {
                //防止其他指令操作
                setWritepowerBtn.Enabled = true;
                getWritepowerBtn.Enabled = true;
                setReadpowerBtn.Enabled = true;
                getReadpowerBtn.Enabled = true;
                genSettingsBtn.Enabled = true;
                buzzerBtn.Enabled = true;

                startReadingBtn.Text = "开始读卡";
                r.StopReading();
                Console.WriteLine("stopReading ...");
            }
        }

        private void genSettingsBtn_Click(object sender, EventArgs e)
        {
            Gen2Settings();
        }

        /// <summary>
        /// Function that processes the Tag Data produced by StartReading();
        /// </summary>
        /// <param name="read"></param>
        void PrintTagRead(Object sender, TagReadDataEventArgs e)
        {
            ThingMagic.TagReadData trd = e.TagReadData;
            
            if(infoDict.ContainsKey(trd.EpcString) == false)
            {
                //Console.WriteLine("--------》 新增 EPC[" + trd.EpcString + "]");
                infoDict.TryAdd(trd.EpcString,trd);
                
                infoThread = new Thread(delegate () { showbinddata(infoDict); });
                infoThread.Start();
            }
            else
            {
                //Console.WriteLine("已包含 EPC["+trd.EpcString+"]");
                TagReadData oldtrd;
                infoDict.TryGetValue(trd.EpcString, out oldtrd);
                infoDict.TryUpdate(trd.EpcString,trd, oldtrd);
            }
        }

        private void showbinddata(ConcurrentDictionary<string, TagReadData> infoDict)
        {
            SetBindText(infoDict);
        }

        private void showInfo(string info)
        {
            SetText(info);
        }

        delegate void SetTextCallBack(string text);
        private void SetText(string text)
        {
            if (this.infoDGV.InvokeRequired)
            {
                SetTextCallBack stcb = new SetTextCallBack(SetText);
                this.Invoke(stcb, new object[] { text });
            }
            else
            {
                string[] info = text.Split(',');
                this.infoDGV.Rows.Add(info);
            }
        }

        delegate void SetBindTextCallBack(ConcurrentDictionary<string, TagReadData> infoDict);
        private void SetBindText(ConcurrentDictionary<string, TagReadData> infoDict)
        {
            if (this.infoDGV.InvokeRequired)
            {
                SetBindTextCallBack stcb = new SetBindTextCallBack(SetBindText);
                this.Invoke(stcb, new object[] { infoDict });
            }
            else
            {

                this.infoDGV.DataSource = (from v in infoDict
                                           select new
                                           {
                                               EPC = v.Value.EpcString,
                                               时间 = v.Value.Time,
                                               信号强度 = v.Value.Rssi,
                                               读取次数 = v.Value.ReadCount,
                                               天线 = v.Value.Antenna,
                                               协议 = v.Value.Tag.Protocol,
                                               频率 = v.Value.Frequency,
                                               相位 = v.Value.Phase
                                           }).ToArray();
                countLbl.Text = infoDict.Count.ToString();
            }
            if(infoThread!=null&&infoThread.ThreadState == ThreadState.Running)
                infoThread.Abort();
        }

        /// <summary>
        /// Synchronous Reads Exception
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ReadException(Object sender, ReaderExceptionEventArgs e)
        {
            if ((e.ReaderException is ReaderCodeException) && (((ReaderCodeException)e.ReaderException).Code == 0x504))
            {
                MessageBox.Show("Over Temperature");
            }
            else
            {
                // Clear previous shown error message on status bar
            }

            if (e.ReaderException.Message.Equals("No response from tag"))
            {
                // Display error message on the status bar
                MessageBox.Show("No response from tag");
                return;
            }

            // Log the exception
            Console.WriteLine(e.ReaderException);

            if (e.ReaderException.Message.Equals("Operation not supported. M5e does not support zero-length read."))
            {
                // Display error message on the status bar
                MessageBox.Show(e.ReaderException.Message);
                return;
            }

            if (e.ReaderException is FAULT_TAG_ID_BUFFER_FULL_Exception)
            {
                // Display error message on the status bar
                MessageBox.Show("Tag ID Buffer Full");
            }
            else if ((e.ReaderException.Message.Contains("The operation has timed out.")))
            {
                // Display error message on the status bar
                MessageBox.Show(e.ReaderException.Message);
            }
            else if (e.ReaderException.Message.Contains("The port '" + readerURI + "' does not exist."))
            {
                // Display error message on the status bar
                MessageBox.Show(e.ReaderException.Message);
            }
            else if ((e.ReaderException is ReaderCodeException)
                && ((((ReaderCodeException)e.ReaderException).Code == 0x504)
                || (((ReaderCodeException)e.ReaderException).Code == 0x505)))
            {
                switch (((ReaderCodeException)e.ReaderException).Code)
                {
                    case 0x504:
                        warningText = "Over Temperature";
                        break;
                    case 0x505:
                        warningText = "High Return Loss";
                        break;
                    default:
                        warningText = "warning";
                        break;
                }
                MessageBox.Show(warningText);
            }
            else if ((-1 != (e.ReaderException.Message.IndexOf("The I/O operation has been aborted"
                       + " because of either a thread exit or an application request."))))
            {
                MessageBox.Show(e.ReaderException.Message + "The port '" + readerURI + "' does not exist.");

                // Disconnect the reader when exception is received
                Disconnect();
            }
            else if ((-1 != e.ReaderException.Message.IndexOf("Specified port does not exist")))
            {
                MessageBox.Show("Connection to the reader is lost. Disconnecting the reader.", "Error : Universal Reader Assistant");

                // Disconnect the reader when exception is received
                Disconnect();
            }
            else if (-1 != e.ReaderException.Message.IndexOf("The port is closed."))
            {
                MessageBox.Show(e.ReaderException.Message, "Error");
            }
            else if ((e.ReaderException.Message.Contains("Connection Lost"))
                    || (e.ReaderException.Message.Contains("Request timed out")))
            {
                MessageBox.Show("Connection Lost" + "Or Request timed out");
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            infoDict.Clear();
            this.infoDGV.DataSource = (from v in infoDict
                                       select new
                                       {
                                           EPC = v.Value.EpcString,
                                           时间 = v.Value.Time,
                                           信号强度 = v.Value.Rssi,
                                           读取次数 = v.Value.ReadCount,
                                           天线 = v.Value.Antenna,
                                           协议 = v.Value.Tag.Protocol,
                                           频率 = v.Value.Frequency,
                                           相位 = v.Value.Phase
                                       }).ToArray();
            countLbl.Text = infoDict.Count.ToString();
        }

        private void infoDGV_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = string.Format("{0}", e.Row.Index + 1);
        }

        private void ipsetBtn_Click(object sender, EventArgs e)
        {
            ScanDemo.Form1 ipsetfrm = new ScanDemo.Form1();
            ipsetfrm.Show();
        }

        private void buzzerBtn_Click(object sender, EventArgs e)
        {
            int timeout = Convert.ToInt32(buzzerTimeTb.Text) * 1000;
            buzzerBtn.Enabled = false;
            r.ParamSet("/reader/gpio/buzzer", timeout);
            Thread.Sleep(timeout);
            Console.WriteLine("timeout= " + timeout);
            buzzerBtn.Enabled = true;
        }

        private void gpioTestBtn_Click(object sender, EventArgs e)
        {
            int time = Convert.ToInt32(testGpioTimeTb.Text);
            if (time < 0)
                return;

            while(time-- > 0)
            {
                GpioPin[] gpioPins = r.GpiGet();
                foreach (GpioPin gpio in gpioPins)
                {
                    Console.WriteLine("[" + gpio.Id + ", " + gpio.High + "]");
                    if (gpio.Id == 4 && gpio.High == false) // gpi4 低电平触发
                    {
                        Console.WriteLine("        gpi4 低电平");
                    }
                    else if (gpio.Id == 5 && gpio.High == true) // gpi30 高电平触发
                    {
                        Console.WriteLine("        gpi30 高电平");
                    }
                    else if (gpio.Id == 6 && gpio.High == true) // gpi31 高电平触发
                    {
                        Console.WriteLine("        gpi31 高电平");
                    }
                }
            }
        }
    }
}
