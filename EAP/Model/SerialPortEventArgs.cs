using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAP
{
    public delegate void ComEvents(Object sender, SerialPortEventArgs args);

    public class SerialPortEventArgs : EventArgs
    {
        public const int DOWNLOAD_EXCEPT_RECEIVE_LENGTH = 5;

        public bool isOpen { get; set; }

        public byte[] data { get; set; }
    }
}
