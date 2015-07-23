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
    public partial class GeneralProperties : UserControl
    {
        SwProperties propertySet;

        public GeneralProperties(ref SwProperties prop)
        {
            this.propertySet = prop;
            InitializeComponent();
            this.LinkControls();
        }

        private void LinkControls()
        {
            this.LinkControlToProperty("Description", this.tbDescription);
            this.LinkControlToProperty("LENGTH", this.tbLength);
            this.UpdateLengthRes(propertySet.GetProperty("LENGTH"));
            this.LinkControlToProperty("WIDTH", this.tbWidth);
            this.UpdateWidthRes(propertySet.GetProperty("WIDTH"));
            this.LinkControlToProperty("THICKNESS", this.tbThick);
            this.UpdateThickRes(propertySet.GetProperty("THICKNESS"));
            this.LinkControlToProperty("WALL THICKNESS", this.tbWallThick);
            this.UpdateWallThickRes(propertySet.GetProperty("WALL THICKNESS"));
            this.LinkControlToProperty("COMMENT", this.tbComment);
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

        private void tbLength_TextChanged(object sender, EventArgs e)
        {
            this.propertySet.GetProperty("LENGTH").Write();
            this.propertySet.GetProperty("LENGTH").Get();
            this.labResLength.Text = this.propertySet.GetProperty("LENGTH").ResValue;
        }	
    }
}
