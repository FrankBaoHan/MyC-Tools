using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAP
{
    public interface IListModel
    {
        void bind(ListUpdateEvent listUpdateEvent);

        void update(string mdbPath);
    }
}
