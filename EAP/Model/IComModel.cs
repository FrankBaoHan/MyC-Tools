using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAP
{
    public interface IComModel
    {
        void bind(ComEvents openEvent, ComEvents closeEvent, ComEvents receiveEvent);

        void open(string portName, string baudRate, string dataBits, string stopBits, string parity);

        void close();

        void send(byte[] data);
    }
}
