using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using SolidWorks.Interop.swconst;

namespace redbrick.csproj
{
    public partial class MachineProperties : UserControl
    {
        SwProperties propertySet;
        public MachineProperties(ref SwProperties prop)
        {
            this.propertySet = prop;
            InitializeComponent();
            this.LinkControls();
        }

        private void LinkControls()
        {
            this.LinkControlToProperty("BLANK QTY", this.tbPPB);
            this.LinkControlToProperty("CNC1", this.tbCNC1);
            this.LinkControlToProperty("CNC2", this.tbCNC2);
            this.LinkControlToProperty("OVERL", this.tbOverL);
            this.LinkControlToProperty("OVERW", this.tbOverW);

            //this.tbBlankL = double.Parse(this.propertySet.GetProperty("LENGTH").ResValue) -
            //    (this.propertySet.cutlistData.GetEdgeThickness(this.propertySet.GetProperty("")) + this.propertySet.cutlistData.GetEdgeThickness());
            // yadda yadda
        }

        private void LinkControlToProperty(string property, Control c)
        {
            SwProperty p = this.propertySet.GetProperty(property);
            if (this.propertySet.Contains(p))
            {
                p.Ctl = c;
            }
            else
            {
                SwProperty x = new SwProperty(property, swCustomInfoType_e.swCustomInfoText, string.Empty, true);
                x.Ctl = c;
            }
        }

        public TextBox GetCNC1Box()
        {
            return this.tbCNC1;
        }

        public TextBox GetCNC2Box()
        {
            return this.tbCNC2;
        }

        public TextBox GetPartsPerBlankBox()
        {
            return this.tbPPB;
        }

        public TextBox GetOverLBox()
        {
            return this.tbOverL;
        }

        public TextBox GetOverWBox()
        {
            return this.tbOverW;
        }

        public TextBox GetBlankLBox()
        {
            return this.tbBlankL;
        }

        public TextBox GetBlankWBox()
        {
            return this.tbBlankW;
        }

        private void tbOverL_TextChanged(object sender, EventArgs e)
        {
            this.tbOverL.Text = string.Format("{0:0.000}", this.tbOverL.Text);
        }

        private void tbOverW_TextChanged(object sender, EventArgs e)
        {
            this.tbOverW.Text = string.Format("{0:0.000}", this.tbOverW.Text);
        }
    }
}
