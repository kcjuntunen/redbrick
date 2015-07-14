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
        }

        public override string ToString()
        {
            string ret = string.Empty;
            foreach (SwProperty p in this._innerArray)
            {
                ret += string.Format("{0}: {1}\n", p.Name, p.Value);
            }
            return ret;
        }

        public void Read()
        {
            ModelDoc2 md = (ModelDoc2)SwApp.ActiveDoc;
            CustomPropertyManager pm = md.Extension.get_CustomPropertyManager(string.Empty);

            int res;
            bool useCached = false;
            string valOut = string.Empty;
            string resValOut = string.Empty;
            bool wasResolved;

            int revLetterOffset = 64;
            int revLimit = 0;

            if (!int.TryParse(Properties.Settings.Default.RevLimit, out revLimit))
            {
                revLimit = 15;
            }

            for (int i = 1; i <= revLimit; i++)
            {
                string r = "REVISION A" + (char)(i + revLetterOffset);
                string e = "ECO " + i.ToString();
                string de = "DESCRIPTION " + i.ToString();
                string l = "LIST " + i.ToString();
                string da = "DATE " + i.ToString();
                System.Diagnostics.Debug.WriteLine(string.Format("{0}: {1}: {2}: {3}", r, e, de, l));
                if (pm.Get5(r, useCached, out valOut, out resValOut, out wasResolved) ==
                    (int)swCustomInfoGetResult_e.swCustomInfoGetResult_ResolvedValue)
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("Found {0}.", r));
                    SwProperty rp = new SwProperty(r, swCustomInfoType_e.swCustomInfoText, valOut, true);
                    System.Windows.Forms.MessageBox.Show(rp.ToString());
                    rp.ResValue = resValOut;

                    res = pm.Get5(e, useCached, out valOut, out resValOut, out wasResolved);
                    SwProperty ep = new SwProperty(e, swCustomInfoType_e.swCustomInfoText, valOut, true);
                    ep.ResValue = resValOut;

                    res = pm.Get5(de, useCached, out valOut, out resValOut, out wasResolved);
                    SwProperty dep = new SwProperty(de, swCustomInfoType_e.swCustomInfoText, valOut, true);
                    dep.ResValue = resValOut;

                    res = pm.Get5(l, useCached, out valOut, out resValOut, out wasResolved);
                    SwProperty lp = new SwProperty(l, swCustomInfoType_e.swCustomInfoText, valOut, true);
                    lp.ResValue = resValOut;

                    res = pm.Get5(da, useCached, out valOut, out resValOut, out wasResolved);
                    SwProperty dap = new SwProperty(da, swCustomInfoType_e.swCustomInfoDate, valOut, true);
                    dap.ResValue = resValOut;

                    DrawingRev dr = new DrawingRev(rp, ep, dep, lp, dap);
                    this._innerArray.Add(dr);
                }
                else
                {
                    break;
                }
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

        private SldWorks _swApp;

        public SldWorks SwApp
        {
            get { return _swApp; }
            set { _swApp = value; }
        }

    }
}
