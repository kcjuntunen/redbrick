using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace redbrick.csproj
{
    public partial class ConfigurationSpecific : UserControl
    {
        private CutlistData cd;
        public ConfigurationSpecific()
        {
            InitializeComponent();
            cd = new CutlistData();

            this.fillMat(this.cbMat, cd.Materials);

            ComboBox[] cc = {this.cbEf, this.cbEb, this.cbEl, this.cbEr};
            foreach (ComboBox c in cc)
            {
                fillEdg(c, cd.Edges);
            }
        }

        private void fillMat(ComboBox cb, DataSet ds)
        {
            cb.DataSource = ds.Tables[0];
            cb.DisplayMember = "DESCR";
        }

        private void fillEdg(ComboBox c, DataSet ds)
        {
            c.DataSource = ds.Tables[0];
            c.DisplayMember = "DESCR";
        }

        public void ToggleFields(string opType)
        {
            bool wood = (opType != "METAL");
            lEf.Visible = wood;
            leFColor.Visible = wood;
            cbEf.Visible = wood;

            lEb.Visible = wood;
            leBColor.Visible = wood;
            cbEb.Visible = wood;

            lEl.Visible = wood;
            leLColor.Visible = wood;
            cbEl.Visible = wood;

            lEr.Visible = wood;
            leRColor.Visible = wood;
            cbEr.Visible = wood;
        }

        private void cbMat_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lMatColor.Text = (this.cbMat.SelectedValue as System.Data.DataRowView)["COLOR"].ToString();
        }

        private void cbEf_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.leFColor.Text = (this.cbEf.SelectedValue as System.Data.DataRowView)["COLOR"].ToString();
        }

        private void cbEb_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.leBColor.Text = (this.cbEb.SelectedValue as System.Data.DataRowView)["COLOR"].ToString();
        }

        private void cbEl_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.leLColor.Text = (this.cbEl.SelectedValue as System.Data.DataRowView)["COLOR"].ToString();
        }

        private void cbEr_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.leRColor.Text = (this.cbEr.SelectedValue as System.Data.DataRowView)["COLOR"].ToString();
        }
    }
}
