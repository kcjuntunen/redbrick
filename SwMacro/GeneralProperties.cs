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
            this.labResWallThickness.Visible = !wood;
            this.lWallThickness.Visible = !wood;
            this.tbWallThick.Visible = !wood;
        }

        public TextBox GetDescriptionBox()
        {
            return this.tbDescription;
        }

        public TextBox GetLengthBox()
        {
            return this.tbLength;
        }

        public TextBox GetWidthBox()
        {
            return this.tbWidth;
        }

        public TextBox GetThicknessBox()
        {
            return this.tbThick;
        }

        public TextBox GetWallThicknessBox()
        {
            return this.tbWallThick;
        }

        public TextBox GetCommentBox()
        {
            return this.tbComment;
        }

        public void UpdateLengthRes(SwProperty p)
        {
            this.labResLength.Text = p.ResValue;
        }

        public void UpdateWidthRes(SwProperty p)
        {
            this.labResWidth.Text = p.ResValue;
        }

        public void UpdateThickRes(SwProperty p)
        {
            this.labResThickness.Text = p.ResValue;
        }

        public void UpdateWallThickRes(SwProperty p)
        {
            this.labResWallThickness.Text = p.ResValue;
        }
    }
}
