using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace ScanDemo
{
    public partial class Form1 : Form
    {
        public delegate void SearchDeletegate(object sender, SearchEventArgs e);
        public event SearchDeletegate SearcheEvent;

        #region 声明变量
        //已扫描端口数目
        double scannedCount = 0;
        //正在运行的线程数目
        int runningThreadCount = 0;
      
       
        //最大工作线程数
        static int maxThread = 100;

        //IP地址
        string host = null;
        //端口
        int port = 1234;


        int startIP=1;
        int endIP=255;
        string addresIP = "192.168.1.";
        #endregion

        #region 窗体方法
         public Form1()
        {
            InitializeComponent();
        }       

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = string.Format("chris 扫描IP段指定端口 {0}-{1}:{2}",
                addresIP+startIP,endIP,port);
            SearcheEvent += new SearchDeletegate(frm_SearcheEvent);

            ipsetBtn.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
        }

#endregion

        #region 扫描方法

        public void Scan(string m_host, int m_port)
        {
            //我们直接使用比较高级的TcpClient类
            TcpClient tc = new TcpClient();
            //设置超时时间
            tc.SendTimeout = tc.ReceiveTimeout = 2000;

            try
            {
                //同步方法

                //IPAddress ip = IPAddress.Parse(host);
                //IPEndPoint IPendp = new IPEndPoint(ip, port);
                //tc.Connect(IPendp);

                //异步方法
                IAsyncResult oAsyncResult = tc.BeginConnect(m_host, m_port, null, null);
                oAsyncResult.AsyncWaitHandle.WaitOne(1000, true);//1000为超时时间 

                if (tc.Connected)
                {
                    //如果连接上，证明此端口为开放状态

                    UpdateListBox(listBox1, m_host + ":" + m_port.ToString() + " -> " );// + "--> " + Dns.GetHostByAddress(m_host).HostName.ToString());
                }

                //if(tc.Connected)
                //    Console.WriteLine("--> " + Dns.GetHostByAddress(m_host).HostName.ToString());
            }
            catch (System.Net.Sockets.SocketException e)
            {
                //容错处理
                //MessageBox.Show("Port {0} is closed", host.ToString());
                //Console.WriteLine(e.Message);
            }
            finally
            {              
                tc.Close();
                tc = null;                
                scannedCount++;
                runningThreadCount--;
            }
        }
        #endregion

        #region 按钮
        //掃描按鈕
        private void button1_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                listBox1.Items.Clear();
                scannedCount = 0;               
                runningThreadCount = 0;
                backgroundWorker1.RunWorkerAsync();
            }
        }

        //取消按鈕
        private void button2_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
            ipsetBtn.Enabled = true;
            button1.Enabled = true; // 扫描
            button2.Enabled = true; // 取消
            button5.Enabled = true; // 指定网段
        }

        void frm_SearcheEvent(object sender, SearchEventArgs e)
        {
            startIP = Convert.ToInt32(e.SartIP.Split('.')[3]);
            endIP = Convert.ToInt32(e.EndIP.Split('.')[3]);

            addresIP = e.SartIP.Substring(0, e.SartIP.LastIndexOf('.') + 1);
            port = e.Port;

            this.Text = string.Format("Chris 扫描IP段指定端口 {0}-{1}:{2}",
                addresIP + startIP, endIP, port);
        }

        #endregion

        #region 异步控件

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {          
            double total = Convert.ToDouble(endIP-startIP+1);
            for (int ip = startIP; ip <= endIP; ip++)
            {
                if (backgroundWorker1.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                //IP地址段，默认：192.168.1.
                host = addresIP + ip.ToString();

                //带参数的多线程执行
                Thread thread = new Thread(() => Scan(host, port));
                thread.IsBackground = true;
                thread.Start();
                

                UpdateLabText(labTip, string.Format("正在扫描第：{0}台，共{1}台，进度：{2}%",
                        scannedCount, total, Convert.ToInt32((scannedCount / total) * 100)));
                backgroundWorker1.ReportProgress(Convert.ToInt32((scannedCount / total) * 100));
                runningThreadCount++;

                Thread.Sleep(10);
                //循环，直到某个线程工作完毕才启动另一新线程，也可以叫做推拉窗技术
                while (runningThreadCount >= maxThread) ;     
                 
            }

            //空循环，直到所有端口扫描完毕
            do
            {
                UpdateLabText(labTip, string.Format("正在扫描第：{0}台，共{1}台，进度：{2}%",
                        scannedCount, total, Convert.ToInt32((scannedCount / total) * 100)));
                backgroundWorker1.ReportProgress(Convert.ToInt32((scannedCount / total) * 100));
              
                Thread.Sleep(10);
                
            } while (runningThreadCount>0);

            
        }   

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            labTip.Text = "扫描完成！";
            ipsetBtn.Enabled = true;
            button1.Enabled = true; // 扫描
            button2.Enabled = true; // 取消
            button5.Enabled = true; // 指定网段
        }        

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }
        #endregion

        #region 异步控件显示
        //Label
        delegate void SetLabCallback(Label lb, string text);
        public void UpdateLabText(Label lb, string text)
        {
            try
            {
                if (lb.InvokeRequired)
                {
                    SetLabCallback d = new SetLabCallback(UpdateLabText);
                    this.Invoke(d, new object[] { lb, text });
                }
                else
                {
                    lb.Text = text.Trim();
                }
            }
            catch
            {
            }
        }

        //TextBox
        delegate void SetTextCallback(TextBox txtBox, string text);

        private void UpdateTextBox(TextBox txtBox, string text)
        {
            if (txtBox.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(UpdateTextBox);
                this.Invoke(d, new object[] { txtBox, text });
            }
            else
            {
                txtBox.Text = text.Trim();
            }
        }

        //ListBox
        delegate void SetListCallback(ListBox lstBox, string text);
        private void UpdateListBox(ListBox lstBox, string text)
        {
            if (lstBox.InvokeRequired)
            {
                SetListCallback d = new SetListCallback(UpdateListBox);
                this.Invoke(d, new object[] { lstBox, text });
            }
            else
            {
                lstBox.Items.Add(text.Trim());
            }
        }


        #endregion

        
        public bool ToSetIP(string connIp, string ip, string mask, string gateway)
        {
            bool ret = false;
            //create a new telnet connection to hostname "gobelijn" on port "23"
            TelnetConnection tc = new TelnetConnection(connIp, 24);

            //login with user "root",password "rootpassword", using a timeout of 100ms, 
            //and show server output
            string s = tc.Login("fuwit", "123456", 100);
            Console.WriteLine("4 :[" + s + "]");

            // server output should end with "{1}quot; or ">", otherwise the connection failed
            string prompt = s.TrimEnd();
            prompt = s.Substring(prompt.Length - 1, 1);
            if (prompt != ":" && prompt != ">")
                throw new Exception("Connection failed");

            prompt = "";
            do
            {
                // display server output
                Console.Write(tc.Read());

                // send client input to server
                //prompt = "ipset 192.168.8.166 255.255.255.0 192.168.8.1";//Console.ReadLine();
                prompt = "ipset "+ ip +" " + mask + " " + gateway;
                tc.WriteLine(prompt);

                // display server output
                s = tc.Read();
                Console.WriteLine(s);

                if (s.Contains("return value = 0"))
                {
                    Console.WriteLine("ipset success");
                    ret = true;
                }
                    

                prompt = "exit";
                tc.WriteLine(prompt);
            } while (tc.IsConnected && prompt.Trim() != "exit");

            Console.WriteLine("***DISCONNECTED");
            return ret;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ipTb.Text = "192.168.8.166";
            maskTb.Text = "255.255.255.0";
            gatewayTb.Text = "";
            string [] str = listBox1.SelectedItem.ToString().Split(":".ToCharArray());// ascii : 72
            foreach(string s in str)
            {
                Console.WriteLine(s);
            }
            ipTb.Text = str[0];
            string[] g = str[0].Split(".".ToArray());
            for(int i = 0; i<g.Length-1; i++)
            {
                gatewayTb.Text += g[i] + ".";
            }
            gatewayTb.Text += "1";
        }

        private void ipsetBtn_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedItem == null)
            {
                MessageBox.Show("请在左侧选择要修改IP的选项！");
                return;
            }
            ipsetBtn.Enabled = false;
            button1.Enabled = false; // 扫描
            button2.Enabled = false; // 取消
            button5.Enabled = false; // 指定网段


            string connIp = listBox1.SelectedItem.ToString().Split(":".ToCharArray())[0];
            string ip = ipTb.Text;
            string mask = maskTb.Text;
            string gateway = gatewayTb.Text;
            string command = "ifconfig eth0 " + ip + " " + mask + " " + gateway;

            Console.WriteLine(connIp);
            bool ret = ToSetIP(connIp, ip, mask, gateway);
            if (ret == true)
            {
                ipsetBtn.Enabled = true;
                button1.Enabled = true; // 扫描
                button2.Enabled = true; // 取消
                button5.Enabled = true; // 指定网段
                MessageBox.Show("IP设置成功，重启设备后生效。");
            }
                
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ipsetBtn.Enabled = false;
            button1.Enabled = true; // 扫描
            button2.Enabled = true; // 取消
            button5.Enabled = false; // 指定网段

            //验证IP
            string startIP = startIPTb.Text.Trim();
            string endIP = endIPTb.Text.Trim();

            if (string.IsNullOrEmpty(startIP) || string.IsNullOrEmpty(endIP))
            {
                MessageBox.Show("IP地址不能为空！");
                return;
            }

            if (!IPCheck(startIP))
            {
                MessageBox.Show("开始地址错误！");
                startIPTb.Focus();
                return;
            }

            if (!IPCheck(endIP))
            {
                MessageBox.Show("结束地址错误");
                endIPTb.Focus();
                return;
            }

            if (!startIP.Substring(0, startIP.LastIndexOf('.') + 1).Equals(
                endIP.Substring(0, endIP.LastIndexOf('.') + 1)))
            {
                MessageBox.Show("IP地址前3位必须相同！");
                return;
            }


            //验证端口
            int port = 8086;

            if (!string.IsNullOrEmpty(portTb.Text.Trim()))
            {
                port = Convert.ToInt32(portTb.Text.Trim());
            }
            else
            {
                MessageBox.Show("端口错误！");
                portTb.Focus();
                return;
            }

            SendValue(startIP, endIP, port);
            Console.WriteLine("success");
        }

        /// <summary>
        /// 正规则试验IP地址
        /// </summary>
        /// <param name="IP"></param>
        /// <returns></returns>
        public bool IPCheck(string IP)
        {
            string num = "(25[0-5]|2[0-4]\\d|[0-1]\\d{2}|[1-9]?\\d)";

            return Regex.IsMatch(IP, ("^" + num + "\\." + num + "\\." + num + "\\." + num + "$"));
        }

        /// <summary>
        /// 事件传值，将值传回
        /// </summary>
        /// <param name="StartIP"></param>
        /// <param name="EndIP"></param>
        /// <param name="Port"></param>
        private void SendValue(string StartIP, string EndIP, int Port)
        {
            Console.WriteLine("SendValue " + startIP + " - " + endIP + " port: " + port);
            SearcheEvent?.Invoke(this, new SearchEventArgs(StartIP, EndIP, Port));
        }

    }

    enum Verbs
    {
        WILL = 251,
        WONT = 252,
        DO = 253,
        DONT = 254,
        IAC = 255
    }

    enum Options
    {
        SGA = 3
    }

    class TelnetConnection
    {
        TcpClient tcpSocket;

        int TimeOutMs = 1 * 1000;

        public TelnetConnection(String Hostname, int Port)
        {
            tcpSocket = new TcpClient(Hostname, Port);
        }

        public string Login(string Username, string Password, int LoginTimeOutMs)
        {
            int oldTimeOutMs = TimeOutMs;
            TimeOutMs = LoginTimeOutMs;
            string s = Read();
            Console.WriteLine("1 :[" + s + "]");
            if (!s.TrimEnd().EndsWith(":"))
                throw new Exception("Failed to connect : no login prompt");
            WriteLine(Username);


            s += Read();
            Console.WriteLine("2 :" + s + "]");
            if (!s.TrimEnd().EndsWith(":"))
                throw new Exception("Failed to connect : no password prompt");
            WriteLine(Password);

            s += Read();
            Console.WriteLine("3 :[" + s + "]");
            TimeOutMs = oldTimeOutMs;

            return s;
        }

        public void DisConnect()
        {
            if (tcpSocket != null)
            {
                if (tcpSocket.Connected)
                {
                    tcpSocket.Client.Disconnect(true);
                }
            }
        }

        public void WriteLine(string cmd)
        {
            Write(cmd + "\r\n");
        }

        public void Write(string cmd)
        {
            if (!tcpSocket.Connected) return;
            byte[] buf = System.Text.ASCIIEncoding.ASCII.GetBytes(cmd.Replace("\0xFF", "\0xFF\0xFF"));
            tcpSocket.GetStream().Write(buf, 0, buf.Length);
        }

        public string Read()
        {
            if (!tcpSocket.Connected) return null;
            StringBuilder sb = new StringBuilder();
            do
            {
                ParseTelnet(sb);
                System.Threading.Thread.Sleep(TimeOutMs);
            } while (tcpSocket.Available > 0);
            if (sb.ToString().EndsWith("\0"))
                return ConvertToGB2312(sb.ToString(0, sb.Length - 1));
            return ConvertToGB2312(sb.ToString(0, sb.Length));
        }

        public bool IsConnected
        {
            get { return tcpSocket.Connected; }
        }

        void ParseTelnet(StringBuilder sb)
        {
            while (tcpSocket.Available > 0)
            {
                int input = tcpSocket.GetStream().ReadByte();
                switch (input)
                {
                    case -1:
                        break;
                    case (int)Verbs.IAC:
                        // interpret as command
                        int inputverb = tcpSocket.GetStream().ReadByte();
                        if (inputverb == -1) break;
                        switch (inputverb)
                        {
                            case (int)Verbs.IAC:
                                //literal IAC = 255 escaped, so append char 255 to string
                                sb.Append(inputverb);
                                break;
                            case (int)Verbs.DO:
                            case (int)Verbs.DONT:
                            case (int)Verbs.WILL:
                            case (int)Verbs.WONT:
                                // reply to all commands with "WONT", unless it is SGA (suppres go ahead)
                                int inputoption = tcpSocket.GetStream().ReadByte();
                                if (inputoption == -1) break;
                                tcpSocket.GetStream().WriteByte((byte)Verbs.IAC);
                                if (inputoption == (int)Options.SGA)
                                    tcpSocket.GetStream().WriteByte(inputverb == (int)Verbs.DO ? (byte)Verbs.WILL : (byte)Verbs.DO);
                                else
                                    tcpSocket.GetStream().WriteByte(inputverb == (int)Verbs.DO ? (byte)Verbs.WONT : (byte)Verbs.DONT);
                                tcpSocket.GetStream().WriteByte((byte)inputoption);
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        sb.Append((char)input);
                        break;
                }
            }
        }

        private string ConvertToGB2312(string str_origin)
        {
            char[] chars = str_origin.ToCharArray();
            byte[] bytes = new byte[chars.Length];
            for (int i = 0; i < chars.Length; i++)
            {
                int c = (int)chars[i];
                bytes[i] = (byte)c;
            }
            Encoding Encoding_GB2312 = Encoding.GetEncoding("GB2312");
            string str_converted = Encoding_GB2312.GetString(bytes);
            return str_converted;
        }
    }
}
