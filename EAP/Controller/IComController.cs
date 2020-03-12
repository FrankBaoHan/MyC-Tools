using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAP
{
    public interface IComController
    {
        void open(
            string portName,
            string baudRate,
            string dataBits,
            string stopBits,
            string parity);

        void close();

        void send(byte[] data);

        void send(string data);

        void send2EAP(byte address, bool isEN, int reg, byte[] data);

        void send2EAP(byte address, bool isEN, int reg, string data);
    }
}
