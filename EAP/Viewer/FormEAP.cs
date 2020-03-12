using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EAP
{
    public partial class FormEAP : Form, IComViewer
    {
        private IComController controller;

        private const int DOWNLOAD_MODE_LENGTH = 5;
        private const long DOWNLOAD_TIME_OUT = 10L;

        public FormEAP()
        {
            InitializeComponent();

            init();
        }

        private void init()
        {
            btn_openClose.Text = "Close";
        }

        public void setController(IComController controller)
        {
            this.controller = controller;
        }

        public void comCloseEvent(object sender, EventArgs args)
        {
            btn_openClose.Text = ((SerialPortEventArgs)args).isOpen ? "Open" : "Close";
        }

        public void comOpenEvent(object sender, EventArgs args)
        {
            btn_openClose.Text = ((SerialPortEventArgs)args).isOpen ? "Open" : "Close";
        }

        //receive
        #region
        /*
         0x00:无状态
         0x01:上载列表，回复固定长度 DOWNLOAD_MODE_LENGTH
         0x02:询问列表，回复不固定长度
        */
        private int mode = 0x00;

        private byte[] dataDownload = new byte[DOWNLOAD_MODE_LENGTH];
        private int offsetDownload = 0;
        private long timeDownload = 0L;

        public void comReceiveEvent(object sender, EventArgs args)
        {
            //非创建线程调用UI,使用委托操作;
            if (this.InvokeRequired)
            {
                try
                {
                    Invoke(new Action<Object, SerialPortEventArgs>(comReceiveEvent), sender, args);
                }
                catch (System.Exception)
                {
                    //disable form destroy exception
                }
                return;
            }

            SerialPortEventArgs spArgs = (SerialPortEventArgs)args;

            switch (mode)
            {
                case 0x01:
                    Stopwatch sw = new Stopwatch();
                    sw.Start();

                    if (offsetDownload < DOWNLOAD_MODE_LENGTH)
                    {
                        if (timeDownload >= DOWNLOAD_TIME_OUT)
                        {
                            //接收超时
                            Console.WriteLine(">>>>>>>>接收超时");

                            downloadStateReset();

                            break;
                        }

                        int dataLen = spArgs.data.Length;
                        int leftLen = DOWNLOAD_MODE_LENGTH - offsetDownload;

                        int readLen = dataLen < leftLen ? dataLen : leftLen;

                        try
                        {
                            Array.Copy(spArgs.data, 0, dataDownload, offsetDownload, readLen);
                        }
                        catch (Exception e)
                        {
                            //异常描述
                            Console.WriteLine(">>>>>>>>" + e.Message.ToString());

                            downloadStateReset();

                            break;
                        }

                        offsetDownload += readLen;
                        sw.Stop();
                        timeDownload += sw.ElapsedMilliseconds;

                        if (offsetDownload >= DOWNLOAD_MODE_LENGTH)
                        {
                            //接收完成
                            textBox1.AppendText(TypeHelper.bytes2Hex(dataDownload) + "\r\n");

                            downloadStateReset();

                            suc++;
                            label1.Text = suc.ToString();
                        }
                    }

                    break;

                case 0x02:
                    break;

                default:
                    break;
            }
        }
        private int suc = 0;
        private void downloadStateReset()
        {
            timeDownload = 0L;
            offsetDownload = 0;
            Array.Clear(dataDownload, 0, dataDownload.Length);
        }
        #endregion

        private void btn_openClose_Click(object sender, EventArgs e)
        {
            if (btn_openClose.Text.Equals("Close"))
            {
                controller.open("COM4", "9600", "8", "1", System.IO.Ports.Parity.None.ToString());
            }
            else if (btn_openClose.Text.Equals("Open"))
            {
                controller.close();
            }
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            suc = 0;
            controller.open("COM6", "9600", "8", "1", System.IO.Ports.Parity.None.ToString());

            mode = 0x01;

            for (int i = 0; i < 100; i++)
            {
                controller.send2EAP(1, true, 1, "(P)ME LO PRESS.LOWER TRIP!");
                CommonHelper.wait(60);
            }

            mode = 0x00;
            controller.close();
        }
    }
}
