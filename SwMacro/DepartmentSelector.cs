using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace redbrick.csproj
{
    public partial class DepartmentSelector : UserControl
    {
        public DepartmentSelector()
        {
            InitializeComponent();
        }

        private void rbDeptWood_CheckedChanged(object sender, EventArgs e)
        {
            OnCheckedChanged(new EventArgs());
            if (!rbDeptWood.Checked)
                this.OpType = "WOOD";
            else
                this.OpType = "METAL";
        }

        protected virtual void OnCheckedChanged(EventArgs e)
        {
            EventHandler eh = CheckedChanged;
            if (eh != null)
                eh(this, e);
        }

        public event EventHandler CheckedChanged;

        private string _opType;

        public string OpType
        {
            get { return _opType; }
            set { _opType = value; }
        }
	
    }
}
