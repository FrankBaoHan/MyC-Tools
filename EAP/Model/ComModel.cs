using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAP
{
    public class ComModel : IComModel
    {
        private SerialPort sp = new SerialPort();
        private SerialPortEventArgs comArgs = new SerialPortEventArgs();

        ComEvents comOpenEvent, comCloseEvent, comReceiveEvent;

        //private const float EXPAND_COEFFICIENT = 0.7f;

        public void bind(
            ComEvents openEvent, 
            ComEvents closeEvent, 
            ComEvents receiveEvent)
        {
            this.comOpenEvent += openEvent;
            this.comCloseEvent += closeEvent;
            this.comReceiveEvent += receiveEvent;

            sp.DataReceived += this.received;
        }

        public void open(
            string portName, 
            string baudRate, 
            string dataBits, 
            string stopBits, 
            string parity)
        {
            if (sp.IsOpen)
            {
                sp.Close();
            }

            try 
            {
                sp.PortName = portName;
                sp.BaudRate = Convert.ToInt32(baudRate);
                sp.DataBits = Convert.ToInt16(dataBits);
                sp.StopBits = (StopBits)Enum.Parse(typeof(StopBits), stopBits);
                sp.Parity = (Parity)Enum.Parse(typeof(Parity), parity);

                sp.RtsEnable = true;
                sp.DtrEnable = true;

                sp.Open();

                comArgs.isOpen = true;

                //callback
                if (comOpenEvent != null)
                {
                    comOpenEvent.Invoke(this, comArgs);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void close()
        {
            if (sp.IsOpen)
            {
                try
                {
                    sp.Close();
                    comArgs.isOpen = false;

                    //callback
                    if (comCloseEvent != null)
                    {
                        comCloseEvent.Invoke(this, comArgs);
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public void send(byte[] data)
        {
            try 
            {
                sp.Write(data, 0, data.Length);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void received(object sender, EventArgs e)
        {
            if (comReceiveEvent != null)
            {
                lock (this)
                {
                    int len = sp.BytesToRead;
                    comArgs.data = new byte[len];

                    try
                    {
                        sp.Read(comArgs.data, 0, len);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    comReceiveEvent.Invoke(this, comArgs);
                }
            }
        }
    }
}
