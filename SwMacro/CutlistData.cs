using System;
using System.Data;
using System.Data.Odbc;
using System.Collections.Generic;
using System.Text;

namespace redbrick.csproj
{	
    class CutlistData
    {	
        private OdbcConnection conn;
        public CutlistData()
        {
            conn = new OdbcConnection(Properties.Settings.Default.ConnectionString);
        }

        public DataSet GetMaterials()
        {
            string SQL = "SELECT MATID,DESCR,COLOR FROM CUT_MATERIALS ORDER BY DESCR;";
            conn.Open();
            OdbcCommand comm = new OdbcCommand(SQL, conn);
            OdbcDataAdapter da = new OdbcDataAdapter(comm);
            DataSet ds = new DataSet();
            da.Fill(ds);

            conn.Close();
            return ds;
        }

        public DataSet GetEdges()
        {
            string SQL = "SELECT EDGEID,DESCR,COLOR,THICKNESS FROM CUT_EDGES ORDER BY DESCR;";
            conn.Open();
            OdbcCommand comm = new OdbcCommand(SQL, conn);
            OdbcDataAdapter da = new OdbcDataAdapter(comm);
            DataSet ds = new DataSet();
            da.Fill(ds);

            conn.Close();
            return ds;
        }

        public string GetMaterialColor(string descr)
        {
            string SQL = "SELECT COLOR FROM CUT_MATERIALS WHERE DESCR LIKE '%" + descr + "%';";
            string result = string.Empty;
            conn.Open();
            OdbcCommand comm = new OdbcCommand(SQL, conn);
            OdbcDataReader dr = comm.ExecuteReader();

            try
            {
                dr.Read();
                result = dr.GetValue(0).ToString();
            }
            catch (Exception x)
            {
                System.Windows.Forms.MessageBox.Show(x.Message);
            }
            conn.Close();
            return result;
        }

        public string GetEdgeColor(string descr)
        {
            string SQL = "SELECT COLOR FROM CUT_EDGES WHERE DESCR LIKE '%" + descr + "%';";
            string result = string.Empty;
            conn.Open();
            OdbcCommand comm = new OdbcCommand(SQL, conn);
            OdbcDataReader dr = comm.ExecuteReader();

            try
            {
                dr.Read();
                result = dr.GetValue(0).ToString();
            }
            catch (Exception x)
            {
                System.Windows.Forms.MessageBox.Show(x.Message);
            }
            conn.Close();
            return result;
        }
    }
}
