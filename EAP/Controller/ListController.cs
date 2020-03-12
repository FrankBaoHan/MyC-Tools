using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAP
{
    class ListController : IListController
    {
        private IListViewer viewer;
        private IListModel model;

        public ListController(IListViewer viewer, IListModel model)
        {
            this.viewer = viewer;
            this.model = model;

            model.bind(viewer.updateEvent);
        }

        public void update(string mdbPath)
        {
            model.update(mdbPath);
        }
    }
}
