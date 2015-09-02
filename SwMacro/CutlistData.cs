using System;
using System.Data;
using System.Data.Odbc;
using System.IO;
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
            if (this._materials == null)
            {
                lock (threadLock)
                {
#if DEBUG
                    DateTime start;
                    DateTime end;
                    start = DateTime.Now;
#endif
                    string SQL = "SELECT MATID,DESCR,COLOR FROM CUT_MATERIALS ORDER BY DESCR;";
                    //conn.Open();
                    OdbcCommand comm = new OdbcCommand(SQL, conn);
                    OdbcDataAdapter da = new OdbcDataAdapter(comm);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
#if DEBUG
                    end = DateTime.Now;
                    System.Diagnostics.Debug.Print("*** MAT ***<<< " + (end - start).ToString() + " >>>");
#endif
                    return ds;
                }
            }
            else
	        {
		         return this._materials;
	        }
        }

        private DataSet GetMaterials2()
        {
            string cacheFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string cacheFileName = Properties.Settings.Default.MatCacheFileName;
            string cacheFile = cacheFilePath + @"\" + cacheFileName;
            int cacheExpireTime = Properties.Settings.Default.CacheExpireTime;

            FileInfo fi = new FileInfo(cacheFile);

            if (((!fi.Exists) || (DateTime.Now - fi.LastWriteTime) > new TimeSpan(0, cacheExpireTime, 0)))
            {
                FileStream fs = new FileStream(fi.FullName, System.IO.FileMode.OpenOrCreate);
                lock (threadLock)
                {
                    string SQL = "SELECT MATID,DESCR,COLOR FROM CUT_MATERIALS ORDER BY DESCR;";
                    //conn.Open();
                    OdbcCommand comm = new OdbcCommand(SQL, conn);
                    OdbcDataAdapter da = new OdbcDataAdapter(comm);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    //conn.Close();
                    using (TextWriter sw = new StreamWriter(fs))
                    {
#if DEBUG
                        System.Diagnostics.Debug.Print("Writing " + cacheFile);
#endif
                        System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(DataSet));
                        xs.Serialize(sw, ds);
                    }
                    return ds;
                }
            }
            else if (this._materials == null)
            {
#if DEBUG
                DateTime start;
                DateTime end;
                start = DateTime.Now;
                System.Diagnostics.Debug.Print("Reading from " + cacheFile);
#endif
                DataSet ds = new DataSet();
                ds.ReadXml(cacheFile);
#if DEBUG
                end = DateTime.Now;
                System.Diagnostics.Debug.Print("<<< " + (end - start).ToString() + " >>>");
#endif
                return ds;
            }
            else
            {
                return this._materials;
            }
        }

        private DataSet GetEdges()
        {
            lock (threadLock)
            {
#if DEBUG
                DateTime start;
                DateTime end;
                start = DateTime.Now;
#endif
                string SQL = "SELECT EDGEID,DESCR,COLOR,THICKNESS FROM CUT_EDGES ORDER BY DESCR;";
                //conn.Open();
                OdbcCommand comm = new OdbcCommand(SQL, conn);
                OdbcDataAdapter da = new OdbcDataAdapter(comm);
                DataSet ds = new DataSet();
                //conn.Close();
                da.Fill(ds);
                
                DataRow dar = ds.Tables[0].NewRow();
                dar[0] = 0;
                dar[1] = string.Empty;
                dar[2] = "None";
                dar[3] = 0.0;

                ds.Tables[0].Rows.Add(dar); ;
#if DEBUG
                end = DateTime.Now;
                System.Diagnostics.Debug.Print("*** EDG ***<<< " + (end - start).ToString() + " >>>");
#endif
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

        public int GetMaterialID(string description)
        {
            if (description == null)
                return 0;

            string SQL = string.Format("SELECT MATID FROM CUT_MATERIALS WHERE DESCR = '{0}'", description);
            OdbcCommand comm = new OdbcCommand(SQL, conn);
            OdbcDataReader dr = comm.ExecuteReader();
            if (dr.HasRows)
                return dr.GetInt32(0);
            else
                return 0;
        }

        public eco GetECOData(string ecoNumber)
        {
            eco e = new eco();
            string SQL = string.Format("SELECT GEN_USERS.FIRST, GEN_USERS.LAST, ECR_MAIN.CHANGES, " +
                "ECR_STATUS.STATUS, ECR_MAIN.ERR_DESC, ECR_MAIN.REVISION FROM " +
                "(ECR_MAIN LEFT JOIN GEN_USERS ON ECR_MAIN.REQ_BY = GEN_USERS.UID) " + 
                "LEFT JOIN ECR_STATUS ON ECR_MAIN.STATUS = ECR_STATUS.STAT_ID WHERE " +
                "(((ECR_MAIN.[ECR_NUM])={0}));", ecoNumber);
            //conn.Open(); 
            OdbcCommand comm = new OdbcCommand(SQL, conn);
            OdbcDataReader dr = comm.ExecuteReader();
            e.EcrNumber = int.Parse(ecoNumber);
            if (dr.HasRows)
            {
                e.Changes = ReturnString(dr, 2);
                e.ErrDescription = ReturnString(dr, 4);
                e.Revision = ReturnString(dr, 5);
                e.Status = ReturnString(dr, 3);
                e.RequestedBy = ReturnString(dr, 0) + " " + ReturnString(dr, 1);
            }
            return e;
        }

        public DataSet GetAuthors()
        {
#if DEBUG
                DateTime start;
                DateTime end;
                start = DateTime.Now;
#endif
                string SQL = "SELECT GEN_USERS.* FROM GEN_USERS WHERE (((GEN_USERS.DEPT)=6));";
                //conn.Open();
                OdbcCommand comm = new OdbcCommand(SQL, conn);
                OdbcDataAdapter da = new OdbcDataAdapter(comm);
                DataSet ds = new DataSet();
                //conn.Close();
                da.Fill(ds);
                
                //DataRow dar = ds.Tables[0].NewRow();
                //dar[0] = 0;
                //dar[1] = string.Empty;
                //dar[2] = "None";
                //dar[3] = 0.0;

                //ds.Tables[0].Rows.Add(dar); ;
#if DEBUG
                end = DateTime.Now;
                System.Diagnostics.Debug.Print("*** AUTH ***<<< " + (end - start).ToString() + " >>>");
#endif
                return ds;
        }

        private string ReturnString(OdbcDataReader dr, int i)
        {
            if (dr.HasRows)
            {
                if (dr.IsDBNull(i))
                {
                    return string.Empty;
                }
                else
                {
                    return dr.GetValue(i).ToString();
                }
            }
            return string.Empty;
        }

        private DataSet _materials;

        public DataSet Materials
        {
            get { return this.GetMaterials(); }
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
