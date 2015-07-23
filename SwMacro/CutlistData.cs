using System;
using System.Data;
using System.Data.Odbc;
using System.Collections.Generic;
using System.Text;

namespace redbrick.csproj
{	
    public class CutlistData : IDisposable
    {
        private object threadLock = new object();

        private OdbcConnection conn;

        public OdbcConnection Connection
        {
            get { return conn; }
            private set { conn = value; }
        }
	
        public CutlistData()
        {
            conn = new OdbcConnection(Properties.Settings.Default.ConnectionString);
            conn.Open();
        }

        public void Dispose()
        {
            conn.Close();
        }

        private DataSet GetMaterials()
        {
            lock (threadLock)
            {
                string SQL = "SELECT MATID,DESCR,COLOR FROM CUT_MATERIALS ORDER BY DESCR;";
                //conn.Open();
                OdbcCommand comm = new OdbcCommand(SQL, conn);
                OdbcDataAdapter da = new OdbcDataAdapter(comm);
                DataSet ds = new DataSet();
                da.Fill(ds);

                //conn.Close();
                return ds;
            }
        }

        private DataSet GetEdges()
        {
            lock (threadLock)
            {
                string SQL = "SELECT EDGEID,DESCR,COLOR,THICKNESS FROM CUT_EDGES ORDER BY DESCR;";
                //conn.Open();
                OdbcCommand comm = new OdbcCommand(SQL, conn);
                OdbcDataAdapter da = new OdbcDataAdapter(comm);
                DataSet ds = new DataSet();
                da.Fill(ds);
                //conn.Close();
                DataRow dar = ds.Tables[0].NewRow();
                dar[0] = 0;
                dar[1] = string.Empty;
                dar[2] = string.Empty;
                ds.Tables[0].Rows.Add(dar);
                return ds;
            }
        }

        public DataSet GetOps(string opType)
        {
            lock (threadLock)
            {
                if (opType != this.OpType)
                {
                    this.OpType = opType;
                    string SQL = string.Format("SELECT OPID, OPNAME, OPDESCR FROM CUT_PART_TYPES "
                        + "INNER JOIN CUT_OPS ON CUT_PART_TYPES.TYPEID = CUT_OPS.OPTYPE WHERE CUT_PART_TYPES.TYPEDESC = '{0}' ORDER BY OPDESCR", opType);
                    //string SQL = "SELECT OPID, OPNAME, OPDESCR, OPTYPE FROM CUT_OPS ORDER BY OPDESCR";
                    //conn.Open();
                    OdbcCommand comm = new OdbcCommand(SQL, conn);
                    OdbcDataAdapter da = new OdbcDataAdapter(comm);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    //conn.Close();
                    DataRow dar = ds.Tables[0].NewRow();
                    dar[0] = 0;
                    dar[1] = string.Empty;
                    dar[2] = string.Empty;
                    ds.Tables[0].Rows.Add(dar);
                    this.Ops = ds;
                    return this.Ops;
                }
                else
                {
                    return this.Ops;
                }
            }
        }

        public double GetEdgeThickness(int ID)
        {
            string SQL = string.Format("SELECT THICKNESS FROM CUT_EDGES WHERE EDGEID = {0}", ID.ToString());
            OdbcCommand comm = new OdbcCommand(SQL, conn);
            OdbcDataReader dr = comm.ExecuteReader();
            if (dr.HasRows)
                return dr.GetDouble(0);
            else
                return 0.0;
        }

        private DataSet _materials;

        public DataSet Materials
        {
            get 
            {
                if (this._materials != null)
                {
                    return this._materials.Copy();
                }
                else
                {
                    this._materials = this.GetMaterials();
                    return this._materials;
                }
            }
            private set { _materials = value; }
        }

        private DataSet _edges;

        public DataSet Edges
        {
            get
            {
                if (this._edges != null)
                {
                    return this._edges.Copy();
                }
                else
                {
                    this._edges = this.GetEdges();
                    return this._edges;
                }
            }
            private set { _edges = value; }
        }

        private string _opType;

        public string OpType
        {
            get { return _opType; }
            set { _opType = value; }
        }


        private DataSet _Ops;

        public DataSet Ops
        {
            get
            {
                if (this._Ops != null)
                {
                    return this._Ops.Copy();
                }
                else
                {
                    if ((this.OpType != "WOOD") || (this.OpType != "METAL"))
                        this._Ops = this.GetOps("WOOD");
                    else
                        this._Ops = this.GetOps(this.OpType);

                    return this._Ops;
                }
            }
            private set { _Ops = value; }
        }
	
    }
}
