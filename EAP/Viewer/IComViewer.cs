using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAP
{
    public interface IComViewer
    {
        void comOpenEvent(Object sender, EventArgs args);

        void comCloseEvent(Object sender, EventArgs args);

        void comReceiveEvent(Object sender, EventArgs args);
    }
}
