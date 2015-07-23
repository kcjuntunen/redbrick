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
        public SwProperties propertySet;

        public Ops(ref SwProperties prop)
        {
            this.cd = prop.cutlistData;
            this.propertySet = prop;

            InitializeComponent();
            
            this.OpType = "WOOD";
        }

        private void fillBox(object occ)
        {
            ComboBox c = (ComboBox)occ;
            c.DataSource = cd.GetOps(this.OpType).Tables[0];
            c.DisplayMember = "OPDESCR";
            c.ValueMember = "OPNAME";
        }

        public void GetProperties()
        {
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
                        System.Diagnostics.Debug.Print(this.propertySet.GetProperty(op).Value);
                        cb.SelectedIndex = idx;
                        cb.DisplayMember = "OPDESCR";
                        SwProperty p = this.propertySet.GetProperty(op);
                        p.ID = (cb.SelectedItem as DataRowView).Row.ItemArray[0].ToString();
                        p.Value = (cb.SelectedItem as DataRowView).Row.ItemArray[1].ToString();
                        p.ResValue = (cb.SelectedItem as DataRowView).Row.ItemArray[2].ToString();

                        p.Table = "CUT_PARTS";
                        p.Field = string.Format("OP{0}ID", c.Name.Split('p')[1]);
                    }
                }
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
            this.GetProperties();
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

                    if (dr.ItemArray[1].ToString().Trim().ToUpper() == val.Trim().ToUpper())
                        return count;
                }
            }
            return -1;
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


    }
}
