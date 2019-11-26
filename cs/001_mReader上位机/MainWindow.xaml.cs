using System;
using System.Collections.Generic;
using System.Windows;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using ThingMagic;


namespace CLS
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        ReceiveTagData rt = new ReceiveTagData();
        static int n = 0;
        ObservableCollection<Tag> tagdata = new ObservableCollection<Tag>();
        Dictionary<string, TagReadData> epcIndex = new Dictionary<string, TagReadData>();
        public MainWindow()
        {
            InitializeComponent();
            //rt.TagRead += delegate(Object sender, TagReadDataEventArgs e)
            //{
            //    Console.WriteLine("Background read: " + e.TagReadData);
            //    if (0 < e.TagReadData.Data.Length)
            //    {
            //        Console.WriteLine("  Data:" + ByteFormat.ToHex(e.TagReadData.Data, "", " "));
            //    }
            //};
            //rt.StatsListener += delegate(object sender, StatsReportEventArgs e)
            //{
            //    Console.WriteLine(e.StatsReport.ToString());
            //};
            this.DataResult.ItemsSource = tagdata;
        }


        private void Btconnect_Click_1(object sender, RoutedEventArgs e)
        {
            if (Convert.Equals(connectButton.Content, "Connect"))
            {
                try
                {
                    tagdata.Clear();
                    epcIndex.Clear();
                    count.Text = "0";
                    string readerUri = cmbReaderAddr.Text;
                    MatchCollection mc = Regex.Matches(readerUri, @"(?<=\().+?(?=\))");
                    foreach (Match m in mc)
                    {
                        readerUri = m.ToString();
                    }

                    rt.Connect();
                    //clientSocket.Connect(readerUri, port);
                    //serverStream = clientSocket.GetStream();
                    //byte[] response = new byte[256];
                    //response = receiveMessage(0x22, 1000);
                    rt.TagRead += r_TagRead;
                    //cmbReaderAddr.Visibility = Visibility.Collapsed;
                    cmbReaderAddr.IsEnabled = false;
                    connectButton.Content = "Disconnect";
                    btnread.IsEnabled = true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }

            }
            else if (Convert.Equals(connectButton.Content, "Disconnect"))
            {
                //r.Destroy();
                //clientSocket.Close();
                rt.close();
                rt.TagRead -= r_TagRead;
                connectButton.Content = "Connect";
                btnread.Content = "Read";
                //cmbReaderAddr.Visibility = Visibility.Visible;
                cmbReaderAddr.IsEnabled = true;
                btnread.IsEnabled = false;
                //tagdata.Clear();
                //btrefresh.IsEnabled = true;
            }
        }

        void r_TagRead(object sender, TagReadDataEventArgs e)
        {
            string Tid = e.TagReadData.EpcString;
            if (Tid == "0x")
                return;

            Dispatcher.BeginInvoke(new ThreadStart(delegate()
            {
                if (!epcIndex.ContainsKey(Tid))
                {
                    tagdata.Add(new Tag() { SerialNumber = epcIndex.Count + 1, Epc = e.TagReadData.EpcString, Count = e.TagReadData.ReadCount, Rssi = e.TagReadData.Rssi });
                    epcIndex.Add(Tid, e.TagReadData);
                    //total++;
                    //tbcount.Text = total.ToString();
                    //readcount.Content = total.ToString();
                    count.Text = epcIndex.Count.ToString();
                }
                else
                {
                    for (int i = 0; i < tagdata.Count; i++)
                    {
                        if (Convert.Equals(tagdata[i].Epc, Tid))
                        {
                            //tagdata[i].Tid = ByteFormat.ToHex(e.TagReadData.Data);
                            tagdata[i].Epc = e.TagReadData.EpcString;
                            tagdata[i].Count++;
                            break;
                        }
                    }
                }

            }));
        }

        private void btnread_Click_1(object sender, RoutedEventArgs e)
        {
            if (btnread.Content.ToString() == "Read")
            {
                rt.ReceiveData();
                btnread.Content = "Stop";
                connectButton.IsEnabled = false;
                btnsetreadpower.IsEnabled = false;
                btngetreadpower.IsEnabled = false;
                btnreboot.IsEnabled = false;
                btnChooseFirmware.IsEnabled = false;
                btnUpdate.IsEnabled = false;
            }
            else if (btnread.Content.ToString() == "Stop")
            {
                rt.stop();
                btnread.Content = "Read";
                connectButton.IsEnabled = true;
                btnsetreadpower.IsEnabled = true;
                btngetreadpower.IsEnabled = true;
                btnreboot.IsEnabled = true;
                btnChooseFirmware.IsEnabled = true;
                btnUpdate.IsEnabled = true;
            }
        }

        private void btnsetreadpower_Click_1(object sender, RoutedEventArgs e)
        {
            int readpower1 = (int)(float.Parse(readpower.Text) * 100);
            rt.CmdSetReadTxPower(readpower1);
        }

        private void btngetreadpower_Click_1(object sender, RoutedEventArgs e)
        {
            float readpower1;
            readpower1 = rt.CmdGetReadTxPower();
            readpower.Text = (readpower1 / 100).ToString();
        }

        OpenFileDialog openFile = new OpenFileDialog();
        private void btnChooseFirmware_Click(object sender, RoutedEventArgs e)
        {
            openFile.ShowDialog();
            txtFirmwarePath.Text = openFile.FileName.ToString();
        }

        //progress bar start flag
        bool prgStart = false;
        //progress bar stop flag
        bool prgStop = false;
        // Cache exception if any while upgrading the firmware for future use
        Exception exceptionCrc = null;
        private Thread progressStatus = null;
        bool start;
        /// <summary>
        /// Start the progress bar
        /// </summary>
        private void startProgress()
        {
            start = true;
            if (progressStatus == null)
            {
                this.progressStatus = new Thread(ProgressBarWork);
                this.progressStatus.IsBackground = true;
                this.progressStatus.Start();
            }
        }

        /// <summary>
        /// Progress bar implementation
        /// </summary>
        private void ProgressBarWork()
        {
            Thread.Sleep(1000);
            int i = 0;
            while (i < 101)
            {
                progressBar.Dispatcher.Invoke(new del1(updateProgressBar), new object[] { i });
                //start = true, when firmware Update started.
                if (start)
                {
                    //If no exception caught, prgStop = false
                    if (!prgStop)
                    {
                        i++;
                    }
                }
                else
                {
                    if (prgStop)
                    {
                        //If exception caught, prgStop = true, come out of the while loop,
                        //firmware Update failed
                        break;
                    }
                    else
                    {
                        //start = false and prgStop = false, when firmware Update done
                        //successfully.
                        i = 102;
                        double maxvalue = 0;
                        progressBar.Dispatcher.Invoke(new ThreadStart(delegate()
                        {
                            maxvalue = progressBar.Maximum;
                        }));
                        progressBar.Dispatcher.Invoke(new del1(updateProgressBar),
                            new object[] { Convert.ToInt32(maxvalue) });
                    }
                }
                Thread.Sleep(350);
            }
        }

        /// <summary>
        /// Stop the progress bar
        /// </summary>
        private void stopProgress()
        {
            start = false;
            if (progressStatus != null)
            {
                progressStatus.Join(100);
                progressStatus = null;
            }
        }

        /// <summary>
        /// Delegate with parameter
        /// </summary>
        /// <param name="x"></param>
        delegate void del1(int x);

        /// <summary>
        /// Delegate without parameter
        /// </summary>
        delegate void del2();

        /// <summary>
        /// Update progress bar
        /// </summary>
        /// <param name="y">value</param>
        void updateProgressBar(int y)
        {
            progressBar.Value = y;
        }

        /// <summary>
        /// Update the firmware Update status if successful
        /// </summary>
        void updateStatus()
        {
            //Update the status to Update successful
            // isFirmwareUpdateFailed = false;
            Dispatcher.BeginInvoke(new ThreadStart(delegate()
            {
                stackPanelFrmwrUpdatePrgss.Visibility = System.Windows.Visibility.Collapsed;
                //enable all the expanders when firmware Update is completed
                //gridConnect.IsEnabled = true;
                //gridReadOptions.IsEnabled = true;
                //gridPerformanceMetrics.IsEnabled = true;
                //gridPerformanceTuning.IsEnabled = true;
                //gridDisplayOptions.IsEnabled = true;
                //gridRdrDiagnostics.IsEnabled = true;
                //EnableDisableExpanderControl(true);
                //btnRead.IsEnabled = true;
            }));
            progressBar.Dispatcher.Invoke(new del1(updateProgressBar), new object[] { 0 });
            MessageBox.Show("Firmware Update Successful:" + "Please restart Universal Reader Assistant",
               "Universal Reader Assistant Message", MessageBoxButton.OK, MessageBoxImage.Information);
            txtFirmwarePath.Text = "";
            btnUpdate.IsEnabled = true;
            btnChooseFirmware.IsEnabled = true;
            //tcTagResults.IsEnabled = true;
            //Firmware Update failed, disconnect the reader.
            this.Dispatcher.BeginInvoke(new ThreadStart(delegate()
            {
                //btnConnect.Content = "Disconnect";
                //btnConnect_Click(this, new RoutedEventArgs());
                //objReader = null;
            }));
        }


        /// <summary>
        /// Update the firmware on the connected reader
        /// </summary>
        /// <param name="sender"></param>\
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                try
                {
                    if (cmbReaderAddr.Text == "")
                    {
                        throw new IOException();
                    }

                }
                catch (System.IO.IOException)
                {
                    if (!cmbReaderAddr.Text.Contains("COM"))
                    {
                        MessageBox.Show("Application needs a valid Reader Address of type COMx",
                            "Universal Reader Assistant Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show("Reader not connected on " + cmbReaderAddr.Text,
                            "Universal Reader Assistant Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    return;
                }
                try
                {
                    // connect to the reader

                }
                catch (Exception ex)
                {
                    //Onlog(ex.Message);
                    MessageBox.Show(ex.Message, "Universal Reader Assistant Message",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (txtFirmwarePath.Text == "")
                {
                    MessageBox.Show("Please select a firmware to load",
                        "Universal Reader Assistant Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (null != rt)
                {
                    // Disable the UI controls when ura is loading the firmware
                    btnUpdate.IsEnabled = false;
                    btnChooseFirmware.IsEnabled = false;
                    //tcTagResults.IsEnabled = false;
                    //prgStart = false;
                    //prgStop = false;
                    // Cache the exception when thrown for future use
                    //exceptionCrc = null;
                    // Set progress bar to initial stage
                    progressBar.Dispatcher.Invoke(new del1(updateProgressBar), new object[] { 0 });
                    System.IO.FileStream firmware = new System.IO.FileStream(txtFirmwarePath.Text,
                        System.IO.FileMode.Open, System.IO.FileAccess.Read);
                    string uri = txtFirmwarePath.Text;
                    Thread updateProgress = new Thread(delegate()
                    {
                        //Wait till updateTrd thread starts
                        while (!prgStart)
                        {
                            Thread.Sleep(10);
                        }
                        //After updateTrd thread starts, prgStart will be true
                        while (prgStart)
                        {
                            startProgress();
                        }
                        //If no exception received call stopProgress method
                        if (null == exceptionCrc)
                        {
                            stopProgress();
                        }
                    });

                    Thread updateTrd = new Thread(delegate()
                    {
                        try
                        {
                            prgStart = true;
                            Dispatcher.BeginInvoke(new ThreadStart(delegate()
                            {
                                stackPanelFrmwrUpdatePrgss.Visibility = System.Windows.Visibility.Visible;
                                //disable all the expanders when firmware Update is in progress
                                //gridConnect.IsEnabled = false;
                                //gridReadOptions.IsEnabled = false;
                                //gridPerformanceMetrics.IsEnabled = false;
                                //gridPerformanceTuning.IsEnabled = false;
                                //gridDisplayOptions.IsEnabled = false;
                                //gridRdrDiagnostics.IsEnabled = false;
                                //EnableDisableExpanderControl(false);
                                //btnRead.IsEnabled = false;
                            }));
                            // Load the firmware on to the connected reader
                            //objReader.FirmwareLoad(firmware);
                            rt.FirmwareLoad(uri);
                            prgStart = false;
                            // Update the firmware Update status
                            Dispatcher.Invoke(new del2(updateStatus));
                        }
                        catch (Exception ex)
                        {
                            //Freeze the progress-bar status when an exception is caught 
                            prgStart = false;
                            // Cache the exception thrown
                            exceptionCrc = ex;
                            prgStop = true;
                            start = false;
                            progressStatus = null;

                            this.Dispatcher.BeginInvoke(new ThreadStart(delegate()
                            {
                                stackPanelFrmwrUpdatePrgss.Visibility = System.Windows.Visibility.Collapsed;
                            }));
                            if (!exceptionCrc.Message.Contains("Autonomous mode is already enabled on reader"))
                            {
                                MessageBox.Show(exceptionCrc.Message.ToString(), "Universal Reader Assistant Message",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else
                            {
                                MessageBox.Show(exceptionCrc.Message.ToString(), "Universal Reader Assistant Message",
                                    MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                            stopProgress();

                            //updateFailedStatus();
                        }
                        finally
                        {
                            //if (isFirmwareUpdateFailed && !exceptionCrc.Message.Contains("Autonomous mode is already enabled on reader"))
                            //{
                            //    MessageBox.Show("Firmware Update failed : " + exceptionCrc.Message.ToString(),
                            //        "Universal Reader Assistant Message", MessageBoxButton.OK, MessageBoxImage.Error);
                            //}
                            //isFirmwareUpdateFailed = false;
                        }
                    });
                    updateProgress.Start();
                    updateTrd.Start();
                }
            }
            catch (System.IO.IOException ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Universal Reader Assistant Message",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                btnUpdate.IsEnabled = true;
                btnChooseFirmware.IsEnabled = true;
                //tcTagResults.IsEnabled = true;
            }
            catch (System.UnauthorizedAccessException)
            {
                MessageBox.Show("Access to " + " denied. Please check if another program is "
                    + " accessing this port", "Universal Reader Assistant Message", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                btnUpdate.IsEnabled = true;
                btnChooseFirmware.IsEnabled = true;
                //tcTagResults.IsEnabled = true;
            }
        }



        public class Tag : INotifyPropertyChanged
        {
            private string epc;
            private int rssi;
            private int count;
            private string tid;
            private int serialNo;
            public event PropertyChangedEventHandler PropertyChanged;
            public string Epc
            {
                get { return epc; }
                set
                {
                    epc = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Epc"));
                    }
                }
            }

            public int Rssi
            {
                get { return rssi; }
                set
                {
                    rssi = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Rssi"));
                    }
                }
            }

            public int Count
            {
                get { return count; }
                set
                {
                    count = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Count"));
                    }
                }

            }

            public int SerialNumber
            {
                get { return serialNo; }
                set
                {
                    serialNo = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SerialNumber"));
                    }
                }
            }
            //public string Tid
            //{
            //    get { return tid; }
            //    set
            //    {
            //        tid = value;
            //        if (PropertyChanged != null)
            //        {
            //            this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Tid"));
            //        }
            //    }
            //}
        }

        private void btnclear_Click_1(object sender, RoutedEventArgs e)
        {
            tagdata.Clear();
            epcIndex.Clear();
            count.Text = "0";
        }

        private void btnreboot_Click_1(object sender, RoutedEventArgs e)
        {
            rt.reboot();
        }
    }
}

