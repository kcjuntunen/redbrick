using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using SolidWorks.Interop.swconst;
using SolidWorks.Interop.sldworks;

namespace redbrick.csproj
{
    public class SwProperties : ICollection<SwProperty>
    {
        protected ArrayList _innerArray;
        protected SldWorks swApp;

        public SwProperties(SldWorks sw)
        {
            this.swApp = sw;
            this._innerArray = new ArrayList();

        }

        private void CreateDefaultSet()
        {
            this._innerArray.Add(new SwProperty("CUTLIST MATERIAL", swCustomInfoType_e.swCustomInfoNumber, "TBD MATERIAL", false));
            this._innerArray.Add(new SwProperty("EDGE FRONT (L)", swCustomInfoType_e.swCustomInfoNumber, string.Empty, false));
            this._innerArray.Add(new SwProperty("EDGE BACK (L)", swCustomInfoType_e.swCustomInfoNumber, string.Empty, false));
            this._innerArray.Add(new SwProperty("EDGE LEFT (W)", swCustomInfoType_e.swCustomInfoNumber, string.Empty, false));
            this._innerArray.Add(new SwProperty("EDGE RIGHT (W)", swCustomInfoType_e.swCustomInfoNumber, string.Empty, false));

            this._innerArray.Add(new SwProperty("Description", swCustomInfoType_e.swCustomInfoText, string.Empty, true));
            this._innerArray.Add(new SwProperty("LENGTH", swCustomInfoType_e.swCustomInfoText, "\"D1@Sketch1\"", true));
            this._innerArray.Add(new SwProperty("WIDTH", swCustomInfoType_e.swCustomInfoText, "\"D2@Sketch1\"", true));
            this._innerArray.Add(new SwProperty("THICKNESS", swCustomInfoType_e.swCustomInfoText, "\"D1@Boss-Extrude1\"", true));
            this._innerArray.Add(new SwProperty("WALL THICKNESS", swCustomInfoType_e.swCustomInfoText, "\"Thickness@Sheet-Metal1\"", true));
            this._innerArray.Add(new SwProperty("COMMENT", swCustomInfoType_e.swCustomInfoText, string.Empty, true));
            this._innerArray.Add(new SwProperty("BLANK QTY", swCustomInfoType_e.swCustomInfoDouble, "1", true));
            this._innerArray.Add(new SwProperty("CNC1", swCustomInfoType_e.swCustomInfoText, "NA", true));
            this._innerArray.Add(new SwProperty("CNC2", swCustomInfoType_e.swCustomInfoText, "NA", true));
            this._innerArray.Add(new SwProperty("OVERL", swCustomInfoType_e.swCustomInfoDouble, "0.0", true));
            this._innerArray.Add(new SwProperty("OVERW", swCustomInfoType_e.swCustomInfoDouble, "0.0", true));
            this._innerArray.Add(new SwProperty("OP1", swCustomInfoType_e.swCustomInfoText, string.Empty, true));
            this._innerArray.Add(new SwProperty("OP2", swCustomInfoType_e.swCustomInfoText, string.Empty, true));
            this._innerArray.Add(new SwProperty("OP3", swCustomInfoType_e.swCustomInfoText, string.Empty, true));
            this._innerArray.Add(new SwProperty("OP4", swCustomInfoType_e.swCustomInfoText, string.Empty, true));
            this._innerArray.Add(new SwProperty("OP5", swCustomInfoType_e.swCustomInfoText, string.Empty, true));
            this._innerArray.Add(new SwProperty("DEPARTMENT", swCustomInfoType_e.swCustomInfoText, "WOOD", true));
            this._innerArray.Add(new SwProperty("UPDATE CNC", swCustomInfoType_e.swCustomInfoYesOrNo, "No", true));
            this._innerArray.Add(new SwProperty("INCLUDE IN CUTLIST", swCustomInfoType_e.swCustomInfoYesOrNo, "Yes", true));

            this._innerArray.Add(new SwProperty("PartNo", swCustomInfoType_e.swCustomInfoText, "$PRP:\"SW-File Name\"", true));
            this._innerArray.Add(new SwProperty("MATERIAL", swCustomInfoType_e.swCustomInfoText, "\"SW-Material@{0}\"", true));
            this._innerArray.Add(new SwProperty("WEIGHT", swCustomInfoType_e.swCustomInfoText, "\"SW-Mass@{0}\"", true));
            this._innerArray.Add(new SwProperty("VOLUME", swCustomInfoType_e.swCustomInfoText, "\"SW-Volume@{0}\"", true));
            this._innerArray.Add(new SwProperty("COST-TOTALCOST", swCustomInfoType_e.swCustomInfoText, "\"SW-Cost-TotalCost@{0}\"", true));
        }

        public virtual IEnumerator<SwProperty> GetEnumerator()
        {
            return (new List<SwProperty>(this).GetEnumerator());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (new List<SwProperty>(this).GetEnumerator());
        }

        public void UpdateProperty(SwProperty property)
        {
            foreach (SwProperty p in this)
            {
                if (property.Name ==  p.Name)
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

        //System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        //{
        //    return this.GetEnumerator();
        //}

        public void Write()
        {
            ModelDoc2 md = (ModelDoc2)swApp.ActiveDoc;
            CustomPropertyManager glP = md.Extension.get_CustomPropertyManager(string.Empty);
            Configuration conf = md.ConfigurationManager.ActiveConfiguration;
            CustomPropertyManager spP = md.Extension.get_CustomPropertyManager(conf.Name);
            swCustomPropertyAddOption_e opt = swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd;
            


            foreach (SwProperty p in this._innerArray)
            {
                if (p.Global)
                {
                    glP.Add3(p.Name, (int)p.Type, p.Value, (int)opt);
                }
                else
                {
                    spP.Add3(p.Name, (int)p.Type, p.Value, (int)opt);
                }
            }
        }

        public override string ToString()
        {
            string ret = string.Empty;
            foreach (SwProperty p in this)
            {
                ret += string.Format("{0}: {1}", p.Name, p.Value);
            }
            return ret;
        }

        private int _clID;

        public int CutlistID
        {
            get { return _clID; }
            set { _clID = value; }
        }


        #region ICollection<SwProperty> Members

        public void Add(SwProperty item)
        {
            if (!this.Contains(item.Name))
            {
                this._innerArray.Add(item);   
            }
        }

        public void Clear()
        {
            this._innerArray.Clear();
        }

        public bool Contains(SwProperty item)
        {
            foreach (SwProperty p in this._innerArray)
            {
                if (p.Name == item.Name)
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
                if (p.Name == name)
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
            get { return this._isReadOnly; }
        }

        public bool Remove(SwProperty item)
        {
            bool res = false;

            for (int i = 0; i < this._innerArray.Count; i++)
            {
                SwProperty obj = (SwProperty)this._innerArray[i];
                if (obj.Name == item.Name)
                {
                    this._innerArray.RemoveAt(i);
                    res = true;
                    break;
                }
            }

            return res;
        }

        public bool Remove(string name)
        {
            bool res = false;

            for (int i = 0; i < this._innerArray.Count; i++)
            {
                SwProperty obj = (SwProperty)this._innerArray[i];
                if (obj.Name == name)
                {
                    this._innerArray.RemoveAt(i);
                    res = true;
                    break;
                }
            }

            return res;
        }

        public virtual SwProperty this[int index]
        {
            get
            {
                return (SwProperty)_innerArray[index];
            }
            set
            {
                _innerArray[index] = value;
            }
        }

        private bool _isReadOnly;

        protected bool MyProperty
        {
            get { return _isReadOnly; }
            set { _isReadOnly = value; }
        }
	
        #endregion
    }
}
