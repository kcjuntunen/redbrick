using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace redbrick.csproj
{
    public partial class Ops : UserControl
    {
        private CutlistData cd = new CutlistData();
        private SldWorks swApp;
        private ModelDoc2 md;
        private CustomPropertyManager spP;
        private CustomPropertyManager glP;

        private List<SwProperty> propertySet;

        //public Ops(string OpType)
        //{
        //    InitializeComponent();
        //    this.OpType = OpType;
        //    this.RefreshOps(this.OpType);
        //}

        public Ops(SldWorks sw, List<SwProperty> prop)
        {
            InitializeComponent();
            this.swApp = sw;
            this.md = (ModelDoc2)this.swApp.ActiveDoc;
            ConfigurationManager cm = this.md.ConfigurationManager;
            Configuration conf = cm.ActiveConfiguration;

            this.spP = this.md.Extension.get_CustomPropertyManager(conf.Name);
            this.glP = this.md.Extension.get_CustomPropertyManager(string.Empty);

            this.propertySet = prop;
            this.OpType = "WOOD";
            this.RefreshOps(this.OpType);
            this.GetProperties();
        }

        private void fillBox(object occ)
        {
            ComboBox c = (ComboBox)occ;
            c.DataSource = cd.GetOps(this.OpType).Tables[0];
            c.DisplayMember = "OPDESCR";
            c.ValueMember = "OPNAME";
        }

        private void GetProperties()
        {
            int res;
            bool useCached = false;
            string valOut;
            string resValOut;
            bool wasResolved;
            SwProperty prp;
            for (int i = 1; i < 6; i++)
            {
                string op = string.Format("OP{0}", i.ToString());
                res = glP.Get5(op, useCached, out valOut, out resValOut, out wasResolved);
                prp = new SwProperty(op, swCustomInfoType_e.swCustomInfoText, valOut, true);

                foreach (Control c in this.tableLayoutPanel1.Controls)
                {
                    if (c.Name.ToUpper() == ("CB" + op).ToUpper())
                    {
                        (c as ComboBox).DisplayMember = "OPNAME";
                        System.Windows.Forms.MessageBox.Show((c as ComboBox).FindString(valOut).ToString() + ": " + valOut);
                        (c as ComboBox).Items[(c as ComboBox).FindString(valOut)].Selected = true;
                        (c as ComboBox).DisplayMember = "OPDESCR";
                        if (valOut == string.Empty)
                            (c as ComboBox).SelectedIndex = -1;

                        prp.Ctl = (c as ComboBox);
                    }
                }

                propertySet.Add(prp);
            }
        }

        public void RefreshOps(string opType)
        {
            if (opType == null)
                this.OpType = "WOOD";
            else
                this.OpType = opType;

            ComboBox[] cc = { this.cbOp1, this.cbOp2, this.cbOp3, this.cbOp4, this.cbOp5 };
            foreach (ComboBox c in cc)
            {
                this.fillBox((object)c);
            }
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
