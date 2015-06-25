using System;
using System.Collections.Generic;
using System.Text;

using SolidWorks.Interop.swconst;

namespace redbrick.csproj
{
    public class SwProperty
    {

        public SwProperty(string PropertyName, swCustomInfoType_e swType, string testValue, bool global)
        {
            this.Name = PropertyName;
            this.Type = swType;
            this.Value = testValue;
            this.Global = global;
        }

        private string _id;

        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }
	

        private string _propName;

        public string Name
        {
            get { return _propName; }
            set { _propName = value; }
        }

        private swCustomInfoType_e _propType;

        public swCustomInfoType_e Type
        {
            get { return _propType; }
            set { _propType = value; }
        }

        private string _value;

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        private string _resValue;

        public string ResValue
        {
            get { return _resValue; }
            set { _resValue = value; }
        }
	

        private string _field;

        public string Field
        {
            get { return _field; }
            set { _field = value; }
        }

        private string _table;

        public string Table
        {
            get { return _table; }
            set { _table = value; }
        }

        private System.Windows.Forms.Control _ctl;

        public System.Windows.Forms.Control Ctl
        {
            get { return _ctl; }
            set { _ctl = value; }
        }

        private bool _global;

        public bool Global
        {
            get { return _global; }
            set { _global = value; }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is SwProperty))
                return false;

            return (this.Name == (obj as SwProperty).Name) && (this.Value == (obj as SwProperty).Value);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode() ^ this.Value.GetHashCode();
        }
    }
}
