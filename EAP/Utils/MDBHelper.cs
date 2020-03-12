using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAP
{
    public class MDBHelper
    {
        private const string connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="; 

        //有空补个连接池
        public static OleDbConnection getConnection(string mdbPathName)
        {
            OleDbConnection conn = null;

            try 
            {
                conn = new OleDbConnection(connStr + mdbPathName);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return conn;
        }

        public static void close(OleDbConnection conn)
        {
            if (conn != null)
            {
                try
                {
                    conn.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public static void close(OleDbConnection conn, OleDbDataReader reader)
        {
            if (conn != null)
            {
                try
                {
                    conn.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            
            if (reader != null)
            {
                try
                {
                    reader.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public static DataSet getDataset(OleDbConnection conn, string sql)
        {
            DataSet ds = new DataSet();

            try 
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter(sql, conn);
                adapter.Fill(ds); adapter.Fill(ds);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return ds;
        }

        public static DataTable getDataTable(OleDbConnection conn, string sql)
        {
            DataTable dt = new DataTable();

            try
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter(sql, conn);
                adapter.Fill(dt);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return dt;
        }

        public static EAPListPojo[] getEAPListPojosFromMdb(OleDbConnection conn, string sql)
        {
            EAPListPojo[] pojos = null;

            try
            {
                conn.Open();

                DataTable dt = getDataTable(conn, sql);

                DataRowCollection drs = dt.Rows;
                pojos = new EAPListPojo[drs.Count];

                int i = 0;

                foreach (DataRow dr in drs)
                {
                    pojos[i++] = new EAPListPojo((int)dr["ID"], (string)dr["EN"]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                try
                {
                    conn.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return pojos;
        }
    }
}
