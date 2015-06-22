using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace redbrick.csproj
{
    public partial class Ops : UserControl
    {
        private CutlistData cd = new CutlistData();

        public Ops(string OpType)
        {
            InitializeComponent();
            this.OpType = OpType;
            this.RefreshOps(this.OpType);
        }

        private void fillBoxes(ComboBox[] cc)
        {
            foreach (ComboBox c in cc)
            {
                c.DataSource = cd.GetOps(this.OpType).Tables[0];
                c.DisplayMember = "OPDESCR";
            }
        }

        public void RefreshOps(string opType)
        {
            this.OpType = opType;
            ComboBox[] cc = { this.cbOp1, this.cbOp2, this.cbOp3, this.cbOp4, this.cbOp5 };
            fillBoxes(cc);
            return ;
        }

        public EventArgs RefreshOpBoxes(string opType)
        {
            EventArgs e = new EventArgs();
            this.OpType = opType;
            this.RefreshOps(this.OpType);
            return e;
        }

        private string _opType;

        public string OpType
        {
            get { return _opType; }
            set { _opType = value; }
        }

        private void cbOp2_TextUpdate(object sender, EventArgs e)
        {
            System.Windows.Forms.MessageBox.Show(cbOp2.Text);
        }
	
    }
}
