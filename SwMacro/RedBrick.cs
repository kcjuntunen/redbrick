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
        private DepartmentSelector ds = new DepartmentSelector();
        private ConfigurationSpecific cs = new ConfigurationSpecific();
        private GeneralProperties gp = new GeneralProperties();
        private MachineProperties mp = new MachineProperties();
        private Ops op;

        public RedBrick()
        {

            InitializeComponent();
            this.SetLocation();
            this.InitComponents();
            this.SetupEvents();
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

            op = new Ops(ds.OpType);
            if (this.operationsToolStripMenuItem.Checked)
            {
                this.tbMainTable.Controls.Add(op, 2, 1);
                this.tbMainTable.SetRowSpan(op, 2);
                this.op.Dock = DockStyle.Fill;
            }

            this.tbMainTable.Controls.Add(new Button(), 0, 3);
            this.tbMainTable.Controls.Add(new Button(), 1, 3);
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