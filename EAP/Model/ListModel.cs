using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAP
{
    public class ListModel : IListModel
    {
        public ListUpdateEvent updateEvent;

        public void bind(ListUpdateEvent listUpdateEvent)
        {
            updateEvent += listUpdateEvent;
        }

        public void update(string mdbPath)
        {
            OleDbConnection conn = MDBHelper.getConnection(mdbPath);
            string sql = "SELECT ID,EN FROM Title WHERE ID>0 ORDER BY ID ASC";

            EAPListPojo[] pojos = MDBHelper.getEAPListPojosFromMdb(conn, sql);
            PojoListHelper.merge(pojos);

            if (updateEvent != null)
            {
                updateEvent.Invoke(this, new EAPListEventArgs(pojos));
            }
        }
    }
}
