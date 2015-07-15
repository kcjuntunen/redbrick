using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using SolidWorks.Interop.swconst;
using SolidWorks.Interop.sldworks;

namespace redbrick.csproj
{
    public class DrawingRevs : ICollection<SwProperty>
    {
        protected ArrayList _innerArray;

        public DrawingRevs(SldWorks sw)
        {
            this.SwApp = sw;
            this._innerArray = new ArrayList(15);
        }

        public override string ToString()
        {
            string ret = string.Empty;
            if (this._innerArray != null)
            {
                foreach (SwProperty p in this._innerArray)
                {
                    if (p.Name != null & p.Value != null)
                    {
                        ret += string.Format("{0}: {1}\n", p.Name, p.Value);   
                    }
                }
            }
            return ret;   
        }

        public void Read()
        {
            ModelDoc2 md = (ModelDoc2)SwApp.ActiveDoc;
            CustomPropertyManager pm = md.Extension.get_CustomPropertyManager(string.Empty);

            int revLetterOffset = 64;
            int revLimit = Properties.Settings.Default.RevLimit;

            for (int i = 1; i <= revLimit; i++)
            {
                string r = "REVISION " + (char)(i + revLetterOffset);
                string e = "ECO " + i.ToString();
                string de = "DESCRIPTION " + i.ToString();
                string l = "LIST " + i.ToString();
                string da = "DATE " + i.ToString();
                System.Diagnostics.Debug.Print(string.Format("{0}: {1}: {2}: {3}", r, e, de, l));

                SwProperty rp = this.AssignProperty(pm, r);
                if (rp.Value == "NULL")
                    break;

                SwProperty ep = this.AssignProperty(pm, e);
                SwProperty dep = this.AssignProperty(pm, de);
                SwProperty lp = this.AssignProperty(pm, l);
                SwProperty dap = this.AssignProperty(pm, da);

                if ((rp.Value != "NULL"))
                {
                    DrawingRev dr = new DrawingRev(rp, ep, dep, lp, dap);
                    if (dr != null)
                    {
                        //this._innerArray.Insert(i, dr);
                        this._innerArray.Add(dr);   
                    }
                }
                else
                    break;
            }
        }

        public void UpdateListBox()
        {
            foreach (DrawingRev r in this._innerArray)
            {
                object[] x = { r.Revision.Value, r.Eco.Value, r.Description.Value, r.List.Value, r.Date.Value };
                this.listBox.DataSource = r;
                
                
            }
        }

        public void Write()
        {
            ModelDoc2 md = (ModelDoc2)SwApp.ActiveDoc;
            CustomPropertyManager glP = md.Extension.get_CustomPropertyManager(string.Empty);
            swCustomPropertyAddOption_e opt = swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd;

            foreach (SwProperty p in this._innerArray)
            {
                glP.Add3(p.Name, (int)p.Type, p.Value, (int)opt);
            }
        }

        private SwProperty AssignProperty(CustomPropertyManager pm, string name)
        {
            int res;
            int success = (int)swCustomInfoGetResult_e.swCustomInfoGetResult_ResolvedValue;
            bool useCached = false;
            string valOut = string.Empty;
            string resValOut = string.Empty;
            bool wasResolved;

            SwProperty rp = new SwProperty();

            if (!InThere(pm, name))
            {
                return rp;
            }
            else
            {
            }

            res = pm.Get5(name, useCached, out valOut, out resValOut, out wasResolved);

            if (res == success)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Found {0}.", name));
                rp.Name = name;
                rp.Value = valOut;
                rp.ResValue = resValOut;
                rp.Type = swCustomInfoType_e.swCustomInfoText;
                rp.Global = true;
            }
            return rp;
        }

        private bool InThere(CustomPropertyManager c, string name)
        {
            foreach (string p in (string[])c.GetNames())
            {
                if (p == name)
                {
                    return true;
                }      
            }
            return false;
        }

        #region ICollection<SwProperty> Members

        public void Add(SwProperty item)
        {
            this._innerArray.Add(item);
        }

        public void Clear()
        {
            this._innerArray.Clear();
        }

        public bool Contains(SwProperty item)
        {
            foreach (SwProperty p in this._innerArray)
            {
                if (item.Name == p.Name)
                {
                    return true;
                }
            }
            return false;
        }

        public bool Contains(string name)
        {
            foreach (SwProperty p in this._innerArray)
            {
                if (name == p.Name)
                {
                    return true;
                }
            }
            return false;
        }

        public void CopyTo(SwProperty[] array, int arrayIndex)
        {
            this._innerArray.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return this._innerArray.Count; }
        }

        public bool IsReadOnly
        {
            get { return this._innerArray.IsReadOnly; }
        }

        public bool Remove(SwProperty item)
        {
            int count = 0;
            foreach (SwProperty p in this._innerArray)
            {
                if (item.Name == p.Name)
                {
                    this._innerArray.RemoveAt(count);
                    return true;
                }
                count++;
            }
            return false;
        }

        public bool Remove(string name)
        {
            int count = 0;
            foreach (SwProperty p in this._innerArray)
            {
                if (name == p.Name)
                {
                    this._innerArray.RemoveAt(count);
                    return true;
                }
                count++;
            }
            return false;
        }

        #endregion

        #region IEnumerable<SwProperty> Members

        public IEnumerator<SwProperty> GetEnumerator()
        {
            return (new List<SwProperty>(this).GetEnumerator());
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this._innerArray.GetEnumerator();
        }

        #endregion

        

        private System.Windows.Forms.DataGridView _lb;

        public System.Windows.Forms.DataGridView listBox
        {
            get { return _lb; }
            set { _lb = value; }
        }
	

        private SldWorks _swApp;

        public SldWorks SwApp
        {
            get { return _swApp; }
            set { _swApp = value; }
        }

    }
}
