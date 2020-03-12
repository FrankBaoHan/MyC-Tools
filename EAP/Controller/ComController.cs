using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAP
{
    public class ComController : IComController
    {
        private IComViewer viewer;
        private IComModel model;

        public ComController(IComViewer viewer, IComModel model)
        {
            this.viewer = viewer;
            this.model = model;

            model.bind(viewer.comOpenEvent, viewer.comCloseEvent, viewer.comReceiveEvent);
        }

        public void open(string portName, string baudRate, string dataBits, string stopBits, string parity)
        {
            model.open(portName, baudRate, dataBits, stopBits, parity);
        }

        public void close()
        {
            model.close();
        }

        public void send(byte[] data)
        {
            model.send(data);
        }

        public void send(string data)
        {
            byte[] bData = TypeHelper.string2Bytes(data);

            model.send(bData);
        }

        public void send2EAP(byte address, bool isEN, int reg, byte[] data)
        {
            byte[] eapData = ModbusHelper.getEapWriteCmd(address, isEN, reg, data);

            model.send(eapData);
        }

        public void send2EAP(byte address, bool isEN, int reg, string data)
        {
            byte[] bData = TypeHelper.string2Bytes(data);

            send2EAP(address, isEN, reg, bData);
        }
    }
}
