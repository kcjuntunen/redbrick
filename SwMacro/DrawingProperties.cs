using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using SolidWorks.Interop.swconst;
using SolidWorks.Interop.sldworks;

namespace redbrick.csproj
{
    public class DrawingProperties : ICollection<SwProperty>
    {
        protected ArrayList _innerArray;
        protected SldWorks swApp;

        public DrawingProperties(SldWorks sw)
        {
            this.swApp = sw;
            this._innerArray = new ArrayList();
        }

        public void CreateDefaultSet()
        {
            this._innerArray.Add(new SwProperty("PartNo", swCustomInfoType_e.swCustomInfoText, "$PRP:\"SW-File Name\"", true));
            this._innerArray.Add(new SwProperty("CUSTOMER", swCustomInfoType_e.swCustomInfoText, string.Empty, true));
            this._innerArray.Add(new SwProperty("REVISION LEVEL", swCustomInfoType_e.swCustomInfoText, "100", true));
            this._innerArray.Add(new SwProperty("DrawnBy", swCustomInfoType_e.swCustomInfoText, string.Empty, true));
            this._innerArray.Add(new SwProperty("DATE", swCustomInfoType_e.swCustomInfoText, DateTime.Now.ToShortDateString(), true));
            this._innerArray.Add(new SwProperty("M1", swCustomInfoType_e.swCustomInfoText, string.Empty, true));
            this._innerArray.Add(new SwProperty("FINISH 1", swCustomInfoType_e.swCustomInfoText, string.Empty, true));
            this._innerArray.Add(new SwProperty("M2", swCustomInfoType_e.swCustomInfoText, string.Empty, true));
            this._innerArray.Add(new SwProperty("FINISH 2", swCustomInfoType_e.swCustomInfoText, string.Empty, true));
            this._innerArray.Add(new SwProperty("M3", swCustomInfoType_e.swCustomInfoText, string.Empty, true));
            this._innerArray.Add(new SwProperty("FINISH 3", swCustomInfoType_e.swCustomInfoText, string.Empty, true));
            this._innerArray.Add(new SwProperty("M4", swCustomInfoType_e.swCustomInfoText, string.Empty, true));
            this._innerArray.Add(new SwProperty("FINISH 4", swCustomInfoType_e.swCustomInfoText, string.Empty, true));
            this._innerArray.Add(new SwProperty("M5", swCustomInfoType_e.swCustomInfoText, string.Empty, true));
            this._innerArray.Add(new SwProperty("FINISH 5", swCustomInfoType_e.swCustomInfoText, string.Empty, true));
        }

        public void UpdateProperty(SwProperty property)
        {
            foreach (SwProperty p in this._innerArray)
            {
                if (property.Name == p.Name)
                {
                    p.Ctl = property.Ctl;
                    p.Field = property.Field;
                    p.Global = property.Global;
                    p.ResValue = property.ResValue;
                    p.Table = property.Table;
                    p.Type = property.Type;
                    p.Value = property.Value;
                }
            }
        }

        public SwProperty GetProperty(string name)
        {
            foreach (SwProperty p in this._innerArray)
            {
                if (name == p.Name)
                {
                    return p;
                }
            }
            return null;
        }

        public void Read()
        {
            ModelDoc2 md = (ModelDoc2)swApp.ActiveDoc;
            CustomPropertyManager pm = md.Extension.get_CustomPropertyManager(string.Empty);
            int success = (int)swCustomInfoGetResult_e.swCustomInfoGetResult_ResolvedValue;
            int res;
            bool useCached = false;
            string valOut = string.Empty;
            string resValOut = string.Empty;
            bool wasResolved;
            

            res = pm.Get5("PartNo", useCached, out valOut, out resValOut, out wasResolved);
            if (res == success)
            {
                this._innerArray.Add(new SwProperty("PartNo", swCustomInfoType_e.swCustomInfoText, valOut, true));
                this.GetProperty("PartNo").ResValue = resValOut;
            }

            res = pm.Get5("CUSTOMER", useCached, out valOut, out resValOut, out wasResolved);
            if (res == success)
            {
                this._innerArray.Add(new SwProperty("CUSTOMER", swCustomInfoType_e.swCustomInfoText, valOut, true));   
            }

            res = pm.Get5("REVISION LEVEL", useCached, out valOut, out resValOut, out wasResolved);
            if (res == success)
            {
                this._innerArray.Add(new SwProperty("REVISION LEVEL", swCustomInfoType_e.swCustomInfoText, valOut, true));   
            }

            res = pm.Get5("DrawnBy", useCached, out valOut, out resValOut, out wasResolved);
            if (res == success)
            {
                this._innerArray.Add(new SwProperty("DrawnBy", swCustomInfoType_e.swCustomInfoText, valOut, true));   
            }

            res = pm.Get5("DATE", useCached, out valOut, out resValOut, out wasResolved);
            if (res == success)
            {
                this._innerArray.Add(new SwProperty("DATE", swCustomInfoType_e.swCustomInfoDate, valOut, true));   
            }

            res = pm.Get5("M1", useCached, out valOut, out resValOut, out wasResolved);
            if (res == success)
            {
                this._innerArray.Add(new SwProperty("M1", swCustomInfoType_e.swCustomInfoText, valOut, true));   
            }
            res = pm.Get5("FINISH 1", useCached, out valOut, out resValOut, out wasResolved);
            if (res == success)
            {
                this._innerArray.Add(new SwProperty("FINISH 1", swCustomInfoType_e.swCustomInfoText, valOut, true));   
            }

            res = pm.Get5("M2", useCached, out valOut, out resValOut, out wasResolved);
            if (res == success)
            {
                this._innerArray.Add(new SwProperty("M2", swCustomInfoType_e.swCustomInfoText, valOut, true));   
            }
            res = pm.Get5("FINISH 2", useCached, out valOut, out resValOut, out wasResolved);
            if (res == success)
            {
                this._innerArray.Add(new SwProperty("FINISH 2", swCustomInfoType_e.swCustomInfoText, valOut, true));   
            }

            res = pm.Get5("M3", useCached, out valOut, out resValOut, out wasResolved);
            if (res == success)
            {
                this._innerArray.Add(new SwProperty("M3", swCustomInfoType_e.swCustomInfoText, valOut, true));   
            }
            res = pm.Get5("FINISH 3", useCached, out valOut, out resValOut, out wasResolved);
            if (res == success)
            {
                this._innerArray.Add(new SwProperty("FINISH 3", swCustomInfoType_e.swCustomInfoText, valOut, true));   
            }

            res = pm.Get5("M4", useCached, out valOut, out resValOut, out wasResolved);
            if (res == success)
            {
                this._innerArray.Add(new SwProperty("M4", swCustomInfoType_e.swCustomInfoText, valOut, true));
            }
            res = pm.Get5("FINISH 4", useCached, out valOut, out resValOut, out wasResolved);
            if (res == success)
            {
                this._innerArray.Add(new SwProperty("FINISH 4", swCustomInfoType_e.swCustomInfoText, valOut, true));   
            }

            res = pm.Get5("M5", useCached, out valOut, out resValOut, out wasResolved);
            if (res == success)
            {
                this._innerArray.Add(new SwProperty("M5", swCustomInfoType_e.swCustomInfoText, valOut, true));   
            }
            res = pm.Get5("FINISH 5", useCached, out valOut, out resValOut, out wasResolved);
            if (res == success)
            {
                this._innerArray.Add(new SwProperty("FINISH 5", swCustomInfoType_e.swCustomInfoText, valOut, true));   
            }
        }

        public void Write()
        {
            ModelDoc2 md = (ModelDoc2)swApp.ActiveDoc;
            CustomPropertyManager glP = md.Extension.get_CustomPropertyManager(string.Empty);
            swCustomPropertyAddOption_e opt = swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd;

            foreach (SwProperty p in this._innerArray)
            {
                glP.Add3(p.Name, (int)p.Type, p.Value, (int)opt);
            }
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

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._innerArray.GetEnumerator();
        }

        #endregion
    }
}
