using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace redbrick.csproj
{
    public partial class ConfigurationSpecific : UserControl
    {
        private CutlistData cd;
        //private List<ComboBox> cc = new List<ComboBox>();
        public ConfigurationSpecific()
        {
            InitializeComponent();
            cd = new CutlistData();

            this.fillComboBoxes();
        }

        private void fillComboBoxes()
        {
            Thread t = new Thread(new ThreadStart(this.fillMat));
            try
            {
                t.Start();
            }
            catch (ThreadStateException tse)
            {
                System.Windows.Forms.MessageBox.Show(tse.Message);
            }
            catch (OutOfMemoryException oome)
            {
                System.Windows.Forms.MessageBox.Show(oome.Message);
            }

            ComboBox[] cc = { this.cbEf, this.cbEb, this.cbEl, this.cbEr };
            foreach (ComboBox c in cc)
            {
                t = new Thread(new ParameterizedThreadStart(this.fillEdg));
                try
                {
                    t.Start((object)c);
                }
                catch (ThreadStateException tse)
                {
                    System.Windows.Forms.MessageBox.Show(tse.Message);
                }
                catch (OutOfMemoryException oome)
                {
                    System.Windows.Forms.MessageBox.Show(oome.Message);
                }
            }
        }

        private void fillMat()
        {
            //System.Windows.Forms.MessageBox.Show("fillMat()");
            this.cbMat.DataSource = cd.Materials.Tables[0];
            this.cbMat.DisplayMember = "DESCR";
        }

        private void fillEdg(object occ)
        {
            ComboBox c = (ComboBox)occ;
            c.DataSource = cd.Edges.Tables[0];
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

        public ComboBox GetCutlistMatBox()
        {
            return this.cbMat;
        }

        public ComboBox GetEdgeFrontBox()
        {
            return this.cbEf;
        }

        public ComboBox GetEdgeBackBox()
        {
            return this.cbEb;
        }

        public ComboBox GetEdgeLeftBox()
        {
            return this.cbEl;
        }

        public ComboBox GetEdgeRightBox()
        {
            return this.cbEr;
        }

    }
}
