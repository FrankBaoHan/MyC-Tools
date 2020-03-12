using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAP
{
    public interface IListViewer
    {
        void updateEvent(Object sender, EAPListEventArgs args);
    }
}
