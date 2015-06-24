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
    public partial class RedBrick : Form
    {
        private SldWorks swApp;
        private ModelDoc2 md;
        private CustomPropertyManager spP;
        private CustomPropertyManager glP;

        public List<SwProperty> propertySet = new List<SwProperty>();

        private DepartmentSelector ds = new DepartmentSelector();
        private ConfigurationSpecific cs = new ConfigurationSpecific();
        private GeneralProperties gp = new GeneralProperties();
        private MachineProperties mp = new MachineProperties();
        private Ops op;

        //private SwProperties propertySet = new SwProperties();

        public RedBrick(SldWorks sw)
        {
            this.swApp = sw;
            this.md = (ModelDoc2)swApp.ActiveDoc;
            this.spP = (CustomPropertyManager)this.md.Extension.get_CustomPropertyManager(this.md.ConfigurationManager.ActiveConfiguration.Name);
            this.glP = (CustomPropertyManager)this.md.Extension.get_CustomPropertyManager(string.Empty);

            InitializeComponent();
            this.SetLocation();
            this.InitComponents();
            this.SetupEvents();
            //this.getPartData();
        }

        private void SetupEvents()
        {
            ds.CheckedChanged += new EventHandler(ds_CheckedChanged);
        }

        void ds_CheckedChanged(object sender, EventArgs e)
        {
            op.RefreshOps(ds.OpType);
            cs.ToggleFields(ds.OpType);
            gp.ToggleFields(ds.OpType);
        }

        private void SetLocation()
        {
            this.Top = Properties.Settings.Default.Top;
            this.Left = Properties.Settings.Default.Left;
        }

        private void InitComponents()
        {
            this.tbMainTable.Controls.Add(ds, 0, 0);
            this.ds.Dock = DockStyle.Fill;

            if (this.configurationSpecificPropertiesToolStripMenuItem.Checked)
            {
                this.tbMainTable.Controls.Add(cs, 0, 1);
                this.tbMainTable.SetRowSpan(cs, 2);
                this.cs.Dock = DockStyle.Fill;
            }

            if (this.globalPropertiesToolStripMenuItem.Checked)
            {
                this.tbMainTable.Controls.Add(gp, 1, 1);
                this.gp.Dock = DockStyle.Fill;
            }

            if (this.machinePropertiesToolStripMenuItem.Checked)
            {
                this.tbMainTable.Controls.Add(mp, 1, 2);
                this.mp.Dock = DockStyle.Fill;
            }

            op = new Ops(this.swApp, this.propertySet);
            if (this.operationsToolStripMenuItem.Checked)
            {
                this.tbMainTable.Controls.Add(op, 2, 1);
                this.tbMainTable.SetRowSpan(op, 2);
                this.op.Dock = DockStyle.Fill;
            }

            this.tbMainTable.Controls.Add(new Button(), 0, 3);
            this.tbMainTable.Controls.Add(new Button(), 1, 3);

            //this.assignControlsToProperties();
        }

        //private void assignControlsToProperties()
        //{
        //    this.propertySet.cutlistMaterial.Ctl = this.cs.GetCutlistMatBox();
        //    this.propertySet.edgeFront.Ctl = this.cs.GetEdgeFrontBox();
        //    this.propertySet.edgeBack.Ctl = this.cs.GetEdgeBackBox();
        //    this.propertySet.edgeLeft.Ctl = this.cs.GetEdgeLeftBox();
        //    this.propertySet.edgeRight.Ctl = this.cs.GetEdgeRightBox();

        //    this.propertySet.descr.Ctl = this.gp.GetDescriptionBox();
        //    this.propertySet.length.Ctl = this.gp.GetLengthBox();
        //    this.propertySet.width.Ctl = this.gp.GetWidthBox();
        //    this.propertySet.thick.Ctl = this.gp.GetThicknessBox();
        //    this.propertySet.wThick.Ctl = this.gp.GetWallThicknessBox();
        //    this.propertySet.comment.Ctl = this.gp.GetCommentBox();

        //    this.propertySet.cnc1.Ctl = this.mp.GetCNC1Box();
        //    this.propertySet.cnc2.Ctl = this.mp.GetCNC2Box();
        //    this.propertySet.blnkQty.Ctl = this.mp.GetPartsPerBlankBox();
        //    this.propertySet.overL.Ctl = this.mp.GetOverLBox();
        //    this.propertySet.overW.Ctl = this.mp.GetOverRBox();
        //}

        private void getPartData()
        {
            int res;
            bool UseCached = false;
            string ValOut;
            string ResolvedValOut;
            bool WasResolved;

            foreach (SwProperty prop in this.propertySet)
            {
                res = glP.Get5(prop.Name, UseCached, out ValOut, out ResolvedValOut, out WasResolved);

                if (res != (int)swCustomInfoGetResult_e.swCustomInfoGetResult_NotPresent)
                {
                    prop.Value = ValOut;
                    prop.ResValue = ResolvedValOut;
                    if (prop.Ctl != null)
                    {
                        //System.Windows.Forms.MessageBox.Show(prop.Ctl.Name);
                        prop.Ctl.Text = ValOut;
                    }
                }
            }


            foreach (SwProperty prop in this.propertySet)
            {
                res = spP.Get5(prop.Name, UseCached, out ValOut, out ResolvedValOut, out WasResolved);

                if (res != (int)swCustomInfoGetResult_e.swCustomInfoGetResult_NotPresent)
                {
                    prop.Value = ValOut;
                    prop.ResValue = ResolvedValOut;
                    if (prop.Ctl != null)
                    {
                        (prop.Ctl as ComboBox).Text = ValOut;
                    }
                }
            }

            string x = string.Empty;
            foreach (SwProperty prop in this.propertySet)
            { 
                x += string.Format("{0}: {1}: {2}\n", prop.Name, prop.Value, prop.ResValue);
            }
            System.Windows.Forms.MessageBox.Show(x);
        }

        private void RedBrick_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Top = this.Top;
            Properties.Settings.Default.Left = this.Left;
            Properties.Settings.Default.Save();
        }

        private void configurationSpecificPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.configurationSpecificPropertiesToolStripMenuItem.Checked)
            {
                this.configurationSpecificPropertiesToolStripMenuItem.Checked = false;
                this.tbMainTable.Controls.Remove(cs);
            }
            else
            {
                this.configurationSpecificPropertiesToolStripMenuItem.Checked = true;
                this.tbMainTable.Controls.Add(cs, 0, 1);
                cs.Dock = DockStyle.Fill;
            }
        }

        private void globalPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.globalPropertiesToolStripMenuItem.Checked)
            {
                this.globalPropertiesToolStripMenuItem.Checked = false;
                this.tbMainTable.Controls.Remove(gp);
            }
            else
            {
                this.globalPropertiesToolStripMenuItem.Checked = true;
                this.tbMainTable.Controls.Add(gp, 1, 1);
                gp.Dock = DockStyle.Fill;
            }
        }

        private void machinePropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.machinePropertiesToolStripMenuItem.Checked)
            {
                this.machinePropertiesToolStripMenuItem.Checked = false;
                this.tbMainTable.Controls.Remove(mp);
            }
            else
            {
                this.machinePropertiesToolStripMenuItem.Checked = true;
                this.tbMainTable.Controls.Add(mp, 1, 2);
                mp.Dock = DockStyle.Fill;
            }
        }

        private void operationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.operationsToolStripMenuItem.Checked)
            {
                this.operationsToolStripMenuItem.Checked = false;
                this.tbMainTable.Controls.Remove(op);
            }
            else
            {
                this.operationsToolStripMenuItem.Checked = true;
                this.tbMainTable.Controls.Add(op, 2, 1);
                op.Dock = DockStyle.Fill;
            }
        }
    }
}