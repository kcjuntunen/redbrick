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
        }

        private void FillBoxes()
        {
            System.Windows.Forms.MessageBox.Show(this.PropertySet.ToString());
            System.Windows.Forms.MessageBox.Show(this.RevSet.ToString());
            this.tbItemNo.Text = this.PropertySet.GetProperty("ItemNo").Value;
            this.tbItemNoRes.Text = this.PropertySet.GetProperty("ItemNo").ResValue;
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