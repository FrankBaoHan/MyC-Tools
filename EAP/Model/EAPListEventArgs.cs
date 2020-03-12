using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAP
{
    public delegate void ListUpdateEvent(Object sender, EAPListEventArgs args);

    public class EAPListEventArgs : EventArgs
    {
        public EAPListPojo[] pojos { get; set; }

        public EAPListEventArgs(EAPListPojo[] pojos)
        {
            this.pojos = pojos;
        }
    }
}
