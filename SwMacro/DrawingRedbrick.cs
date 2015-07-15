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
            this.PropertySet = new DrawingProperties(this._swApp);
            this.RevSet = new DrawingRevs(this._swApp);
            this.GetData();
            this.FillBoxes();
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
            this.PropertySet.GetProperty("PartNo").Ctl = this.tbItemNo;
            this.PropertySet.GetProperty("CUSTOMER").Ctl = this.cbCustomer;
            this.PropertySet.GetProperty("DrawnBy").Ctl = this.cbAuthor;
            this.PropertySet.GetProperty("DATE").Ctl = this.dpDate;

            for (int i = 1; i < 6; i++)
            {
                if (this.PropertySet.Contains("M" + i.ToString()))
                {
                    foreach (Control c in this.tableLayoutPanel3.Controls)
	                {
                        if (c.Name.ToUpper().Contains("M" + i.ToString()) )
	                    {
                            this.PropertySet.GetProperty("M" + i.ToString()).Ctl = c;	 
	                    }

                        if (c.Name.ToUpper().Contains("FINISH" + i.ToString()))
                        {
                            this.PropertySet.GetProperty("FINISH " + i.ToString()).Ctl = c;
                        }
	                }
                }
            }

            this.PropertySet.UpdateFields();
            this.RevSet.UpdateListBox();
            //this.tbItemNo.Text = this.PropertySet.GetProperty("PartNo").Value;
            this.tbItemNoRes.Text = this.PropertySet.GetProperty("PartNo").ResValue;
            //this.cbCustomer.Text = this.PropertySet.GetProperty("CUSTOMER").Value;
            //this.cbAuthor.Text = this.PropertySet.GetProperty("DrawnBy").Value;
            //this.dpDate.Text = this.PropertySet.GetProperty("DATE").Value;
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
	
    }
}