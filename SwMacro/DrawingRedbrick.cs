using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace redbrick.csproj
{
    public partial class DrawingRedbrick : Form
    {
        public DrawingRedbrick(SldWorks sw)
        {
            this._swApp = sw;
            InitializeComponent();
            this.fillMat();
            this.fillAuthor();

            this.SetLocation();

            this.PropertySet = new DrawingProperties(this._swApp);
            this.RevSet = new DrawingRevs(this._swApp);
            this.GetData();
            this.FillBoxes();
        }

        private void SetLocation()
        {
            this.Top = Properties.Settings.Default.Top;
            this.Left = Properties.Settings.Default.Left;
        }

        private void GetData()
        {
            this.PropertySet.Read();
            this.RevSet.Read();
            this.RevSet.listBox = this.lbRevs;

            //System.Windows.Forms.MessageBox.Show(this.RevSet.ToString());
        }

        private void FillBoxes()
        {
            SwProperty partNo = this.PropertySet.GetProperty("PartNo");
            SwProperty custo = this.PropertySet.GetProperty("CUSTOMER");
            SwProperty by = this.PropertySet.GetProperty("DrawnBy");
            SwProperty d = this.PropertySet.GetProperty("DATE");

            if (partNo != null)
            {
                partNo.Ctl = this.tbItemNo;
            }
            else
            {
                partNo = new SwProperty("PartNo", swCustomInfoType_e.swCustomInfoText, string.Empty, true);
                partNo.SwApp = this.SwApp;
                partNo.Ctl = this.tbItemNo;
                this.PropertySet.Add(partNo);
            }

            if (custo != null)
            {
                custo.Ctl = this.cbCustomer;
            }
            else
            {
                custo = new SwProperty("CUSTOMER", swCustomInfoType_e.swCustomInfoText, string.Empty, true);
                custo.SwApp = this.SwApp;
                custo.Ctl = this.cbCustomer;
                this.PropertySet.Add(custo);
            }

            if (by != null)
            {
                by.Ctl = this.cbAuthor;
            }
            else
            {
                by = new SwProperty("DrawnBy", swCustomInfoType_e.swCustomInfoText, string.Empty, true);
                by.SwApp = this.SwApp;
                by.Ctl = this.cbAuthor;
                this.PropertySet.Add(by);
            }

            if (d != null)
            {
                d.Ctl = this.dpDate;
            }
            else
            {
                d = new SwProperty("DATE", swCustomInfoType_e.swCustomInfoDate, string.Empty, true);
                d.SwApp = this.SwApp;
                d.Ctl = this.dpDate;
                this.PropertySet.Add(d);
            }

            for (int i = 1; i < 6; i++)
            {
                if (this.PropertySet.Contains("M" + i.ToString()))
                {
                    foreach (Control c in this.tableLayoutPanel3.Controls)
                    {
                        if (c.Name.ToUpper().Contains("M" + i.ToString()))
                            this.PropertySet.GetProperty("M" + i.ToString()).Ctl = c;


                        if (c.Name.ToUpper().Contains("FINISH" + i.ToString()))
                            this.PropertySet.GetProperty("FINISH " + i.ToString()).Ctl = c;
                    }
                }
                else
                {
                    foreach (Control c in this.tableLayoutPanel3.Controls)
                    {
                        if (c.Name.ToUpper().Contains("M" + i.ToString()))
                        {
                            SwProperty mx = new SwProperty("M" + i.ToString(), swCustomInfoType_e.swCustomInfoText, string.Empty, true);
                            mx.SwApp = this.SwApp;
                            mx.Ctl = c;
                            //System.Diagnostics.Debug.Print("M: " + mx.Value);
                            this.PropertySet.Add(mx);
                        }

                        if (c.Name.ToUpper().Contains("FINISH" + i.ToString()))
                        {
                            SwProperty fx = new SwProperty("FINISH " + i.ToString(), swCustomInfoType_e.swCustomInfoText, string.Empty, true);
                            fx.SwApp = this.SwApp;
                            fx.Ctl = c;
                            //System.Diagnostics.Debug.Print("F: " + fx.Value);
                            this.PropertySet.Add(fx);
                        }
                    }
                }
            }

            this.PropertySet.UpdateFields();
            this.RevSet.UpdateListBox();
            this.tbItemNoRes.Text = this.PropertySet.GetProperty("PartNo").ResValue;
        }

        private void fillMat()
        {
            System.Collections.Specialized.StringCollection sc = Properties.Settings.Default.Materials;
            foreach (string s in sc)
	        {
        	    this.cbM1.Items.Add(s);	    
                this.cbM2.Items.Add(s);
                this.cbM3.Items.Add(s);
                this.cbM4.Items.Add(s);
                this.cbM5.Items.Add(s);
	        }
        }

        private void fillAuthor()
        {
            System.Collections.Specialized.StringCollection sc = Properties.Settings.Default.Authors;
            foreach (string s in sc)
            {
                this.cbAuthor.Items.Add(s);
            }
        }

        private DrawingProperties _propSet;

	    public DrawingProperties PropertySet
	    {
		    get { return _propSet;}
		    set { _propSet = value;}
	    }

        private DrawingRevs _revSet;

        public DrawingRevs RevSet
        {
            get { return _revSet; }
            set { _revSet = value; }
        }
	
	
        private SldWorks _swApp;

        public SldWorks SwApp
        {
            get { return _swApp; }
            set { _swApp = value; }
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            this.PropertySet.ReadControls();
            this.PropertySet.Write(this.SwApp);

            this.RevSet.ReadControls();
            this.RevSet.Write(this.SwApp);
            (this.SwApp.ActiveDoc as ModelDoc2).ForceRebuild3(true);
            this.Close();
        }

        private void DrawingRedbrick_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Left = this.Left;
            Properties.Settings.Default.Top = this.Top;
            Properties.Settings.Default.Save();
        }
    }
}