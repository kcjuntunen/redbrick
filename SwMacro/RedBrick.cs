using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace redbrick.csproj
{
    public partial class RedBrick : Form
    {
        public RedBrick()
        {
            InitializeComponent();
            ConfigurationSpecific cs = new ConfigurationSpecific();
            GeneralProperties gp = new GeneralProperties();
            //this.Controls.Add(cs);
            this.flPanel.Controls.Add(cs);
            this.flPanel.Controls.Add(gp);
            //cs.Dock = DockStyle.Fill;
        }
    }
}