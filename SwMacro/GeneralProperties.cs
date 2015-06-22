using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace redbrick.csproj
{
    public partial class GeneralProperties : UserControl
    {
        public GeneralProperties()
        {
            InitializeComponent();
        }

        public void ToggleFields(string opType)
        {
            bool wood = (opType != "METAL");
            this.labResWallThickness.Visible = wood;
            this.lWallThickness.Visible = wood;
            this.tbWallThick.Visible = wood;
        }
    }
}
