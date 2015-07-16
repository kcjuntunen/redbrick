using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace redbrick.csproj
{
    public partial class MachineProperties : UserControl
    {
        public MachineProperties()
        {
            InitializeComponent();
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
