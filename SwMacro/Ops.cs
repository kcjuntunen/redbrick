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

        public SwProperties propertySet;

        private bool notMachine = false;

        public Ops(SldWorks sw, ref SwProperties prop)
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
        }

        private void fillBox(object occ)
        {
            this.notMachine = false;
            ComboBox c = (ComboBox)occ;
            c.DataSource = cd.GetOps(this.OpType).Tables[0];
            c.DisplayMember = "OPDESCR";
            c.ValueMember = "OPNAME";
        }

        public void GetProperties()
        {
            System.Threading.Thread.Sleep(1000);
            this.propertySet.GetProperty("OP1").Ctl = this.cbOp1;
            this.propertySet.GetProperty("OP2").Ctl = this.cbOp2;
            this.propertySet.GetProperty("OP3").Ctl = this.cbOp3;
            this.propertySet.GetProperty("OP4").Ctl = this.cbOp4;
            this.propertySet.GetProperty("OP5").Ctl = this.cbOp5;

            for (int i = 0; i < 6; i++)
            {
                string op = string.Format("OP{0}", i.ToString());

                foreach (Control c in this.tableLayoutPanel1.Controls)
                {
                    if ((c is ComboBox) && c.Name.ToUpper().Contains(op))
                    {
                        ComboBox cb = (c as ComboBox);
                        cb.DisplayMember = "OPNAME";
                        int idx = this.GetIndex((cb.DataSource as DataTable), 
                            this.propertySet.GetProperty(op).Value);
                        if (idx > cb.Items.Count - 1) idx = 0;

                        cb.SelectedIndex = idx;
                        cb.DisplayMember = "OPDESCR";

                        SwProperty p = this.propertySet.GetProperty(op);
                        p.ID = (cb.SelectedItem as DataRowView).Row.ItemArray[0].ToString();
                        p.Value = (cb.SelectedItem as DataRowView).Row.ItemArray[1].ToString();
                        p.ResValue = (cb.SelectedItem as DataRowView).Row.ItemArray[2].ToString();

                        p.Table = "CUT_PARTS";
                        p.Field = string.Format("OP{0}ID", p.Ctl.Name.Split('p')[1]);
                    }
                }
            }
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
            if (dt != null)
            {
                int count = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    count++;

                    if (dr.ItemArray[1].ToString() == val)
                        return count;
                }
            }
            return -1;
        }

        private void AddProp(ComboBox cb, string prp_Name)
        {
            if (notMachine)
            {
                SwProperty p = this.propertySet.GetProperty(prp_Name);
                p.ID = (cb.SelectedItem as DataRowView).Row.ItemArray[0].ToString();
                p.Value = (cb.SelectedItem as DataRowView).Row.ItemArray[1].ToString();
                p.ResValue = (cb.SelectedItem as DataRowView).Row.ItemArray[2].ToString();

                p.Table = "CUT_PARTS";
                p.Field = string.Format("OP{0}ID", p.Ctl.Name.Split('p')[1]);
            }
            else
            {
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

        public Control GetOp1Box()
        {
            return this.cbOp1;
        }

        public Control GetOp2Box()
        {
            return this.cbOp2;
        }

        public Control GetOp3Box()
        {
            return this.cbOp3;
        }

        public Control GetOp4Box()
        {
            return this.cbOp4;
        }

        public Control GetOp5Box()
        {
            return this.cbOp5;
        }



        private string _opType;

        public string OpType
        {
            get { return _opType; }
            set { _opType = value; }
        }

        private void cbOp2_TextUpdate(object sender, EventArgs e)
        {
            //string t = string.Empty;
            //foreach (SwProperty p in this.propertySet)
            //{
            //    t += string.Format("{0} as {1} = {2}\n", p.Name, p.Type.ToString(), p.Value);
            //}
            //System.Windows.Forms.MessageBox.Show(t);
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

        private void cbOp1_MouseEnter(object sender, EventArgs e)
        {
            this.notMachine = true;
        }

        private void cbOp1_MouseLeave(object sender, EventArgs e)
        {
            this.notMachine = false;
        }

        private void cbOp2_MouseEnter(object sender, EventArgs e)
        {
            this.notMachine = true;
        }

        private void cbOp2_MouseLeave(object sender, EventArgs e)
        {
            this.notMachine = false;
        }

        private void cbOp3_MouseEnter(object sender, EventArgs e)
        {
            this.notMachine = true;
        }

        private void cbOp3_MouseLeave(object sender, EventArgs e)
        {
            this.notMachine = false;
        }

        private void cbOp4_MouseEnter(object sender, EventArgs e)
        {
            this.notMachine = true;
        }

        private void cbOp4_MouseLeave(object sender, EventArgs e)
        {
            this.notMachine = false;
        }

        private void cbOp5_MouseEnter(object sender, EventArgs e)
        {
            this.notMachine = true;
        }

        private void cbOp5_MouseLeave(object sender, EventArgs e)
        {
            this.notMachine = false;
        }
    }
}
