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

        private bool notMachine = false;

        //public Ops(string OpType)
        //{
        //    InitializeComponent();
        //    this.OpType = OpType;
        //    this.RefreshOps(this.OpType);
        //}

        public Ops(SldWorks sw, List<SwProperty> prop)
        {
            this.notMachine = false;

            InitializeComponent();
            this.swApp = sw;
            this.md = (ModelDoc2)this.swApp.ActiveDoc;
            ConfigurationManager cm = this.md.ConfigurationManager;
            Configuration conf = cm.ActiveConfiguration;

            this.spP = this.md.Extension.get_CustomPropertyManager(conf.Name);
            this.glP = this.md.Extension.get_CustomPropertyManager(string.Empty);

            this.propertySet = prop;
            this.OpType = "WOOD";
            //this.RefreshOps(this.OpType);
            //this.GetProperties();
        }

        private void fillBox(object occ)
        {
            this.notMachine = false;
            ComboBox c = (ComboBox)occ;
            c.DataSource = cd.GetOps(this.OpType).Tables[0];
            c.DisplayMember = "OPDESCR";
            c.ValueMember = "OPNAME";
            //this.notMachine = true;
        }

        public void GetProperties()
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
                prp.ResValue = resValOut;
                prp.Table = "CUT_PARTS";
                prp.Field = op + "ID";

                foreach (Control c in this.tableLayoutPanel1.Controls)
                {
                    if (c.Name.ToUpper().Contains(op))
                    {
                        prp.Ctl = (c as ComboBox);
                        //System.Windows.Forms.MessageBox.Show((c as ComboBox).FindString(valOut).ToString() + ": " + valOut);

                        if (valOut != string.Empty)
                        {
                            //DataTable dt = (DataTable)(c as ComboBox).DataSource;
                            (c as ComboBox).DisplayMember = "OPNAME";
                            int idx = this.GetIndex(((c as ComboBox).DataSource as DataTable), valOut);
                            (c as ComboBox).SelectedIndex = idx;
                            (c as ComboBox).DisplayMember = "OPDESCR";
                            //DataSet ds = dt.DataSet;
                            //ds.Tables[0].Select("OPNAME Like '" + valOut + "'");
                        }
                        else
                        {
                            //(c as ComboBox).SelectedIndex = -1;
                        }
                    }
                }
                propertySet.Add(prp);
            }
            this.notMachine = true;
        }

        public void RefreshOps(string opType)
        {
            this.notMachine = false;
            if (opType == null)
                this.OpType = "WOOD";
            else
                this.OpType = opType;

            ComboBox[] cc = { this.cbOp1, this.cbOp2, this.cbOp3, this.cbOp4, this.cbOp5 };
            foreach (ComboBox c in cc)
            {
                this.fillBox((object)c);
            }
            //this.notMachine = true;
        }

        public EventArgs RefreshOpBoxes(string opType)
        {
            EventArgs e = new EventArgs();
            this.OpType = opType;
            this.RefreshOps(this.OpType);
            return e;
        }

        private int GetIndex(DataTable dt, string val)
        {
            int count = 0;
            foreach (DataRow dr in dt.Rows)
            {
                count++;

                if (dr.ItemArray[1].ToString() == val)
                    return count;
            }
            return -1;
        }

        private void AddProp(ComboBox cb, string prp_Name)
        {
            if (notMachine)
            {
                if (cb.Text != string.Empty)
                {
                    //System.Windows.Forms.MessageBox.Show(string.Format("notMachine = {0}", this.notMachine.ToString()));
                    swCustomPropertyAddOption_e opt = swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd;
                    swCustomInfoAddResult_e res = swCustomInfoAddResult_e.swCustomInfoAddResult_GenericFail;
                    string cheese = string.Empty;
                    foreach (SwProperty prp in this.propertySet)
                    {
                        //cheese += prp.Name + ", " + prp.Value + ", " + prp.Type.ToString() + ", " + opt.ToString() + "\n";
                        if ((prp.Name == prp_Name) && ((cb.SelectedItem as DataRowView) != null))
                        {
                            prp.Value = (cb.SelectedItem as DataRowView).Row.ItemArray[1].ToString();
                            res = (swCustomInfoAddResult_e)glP.Add3(prp.Name, (int)prp.Type, prp.Value, (int)opt);

                            System.Windows.Forms.MessageBox.Show("glP.Add3(" + prp.Name + ", " + prp.Value + ", " + prp.Type.ToString() + ", " + opt.ToString() + ")");
                        }
                    }
                    //System.Windows.Forms.MessageBox.Show(cheese);
                }
                this.notMachine = false;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("this.DelProp(prp_Name);");
                this.DelProp(prp_Name);
            }
        }

        private void DelProp(string prp_Name)
        {
            if (notMachine)
            {
                swCustomPropertyAddOption_e opt = swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd;
                glP.Add3(prp_Name, (int)swCustomInfoType_e.swCustomInfoText, string.Empty, (int)opt);
            }
        }

        private string _opType;

        public string OpType
        {
            get { return _opType; }
            set { _opType = value; }
        }

        private void cbOp2_TextUpdate(object sender, EventArgs e)
        {
        }

        private void cbOp1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.AddProp(this.cbOp1, "OP1");
        }

        private void cbOp2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.AddProp(this.cbOp2, "OP2");
        }

        private void cbOp3_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.AddProp(this.cbOp3, "OP3");
        }

        private void cbOp4_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.AddProp(this.cbOp4, "OP4");
        }

        private void cbOp5_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.AddProp(this.cbOp5, "OP5");
        }
    }
}
