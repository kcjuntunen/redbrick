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

        //public List<SwProperty> propertySet = new List<SwProperty>();

        private DepartmentSelector ds = new DepartmentSelector();
        private ConfigurationSpecific cs = new ConfigurationSpecific();
        private GeneralProperties gp = new GeneralProperties();
        private MachineProperties mp = new MachineProperties();
        public Ops op;

        private SwProperties propertySet;

        public RedBrick(SldWorks sw)
        {
            this.swApp = sw;
            this.md = (ModelDoc2)swApp.ActiveDoc;
            this.spP = (CustomPropertyManager)this.md.Extension.get_CustomPropertyManager(this.md.ConfigurationManager.ActiveConfiguration.Name);
            this.glP = (CustomPropertyManager)this.md.Extension.get_CustomPropertyManager(string.Empty);
            this.propertySet = new SwProperties(this.swApp);

            InitializeComponent();
            this.SetLocation();
            this.InitComponents();
            this.SetupEvents();
            this.getPartData();

            this.propertySet.ReadProperties();
            //this.op.GetProperties();
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

            op = new Ops(this.swApp, ref this.propertySet);
            if (this.operationsToolStripMenuItem.Checked)
            {
                this.tbMainTable.Controls.Add(op, 2, 1);
                this.tbMainTable.SetRowSpan(op, 2);
                this.op.Dock = DockStyle.Fill;
            }

            Button bOK = new Button();
            bOK.Text = "OK";
            bOK.Click += new EventHandler(bOK_Click);

            this.tbMainTable.Controls.Add(bOK, 0, 3);
            this.tbMainTable.Controls.Add(new Button(), 1, 3);
            this.getPartData();
        }

        void bOK_Click(object sender, EventArgs e)
        {
            this.propertySet.ReadControls();
            this.propertySet.Write();
            this.getPartData();
        }

        private void getPartData()
        {
            int res;
            bool UseCached = false;
            string ValOut;
            string ResolvedValOut;
            bool WasResolved;
            int typ;


            string[] globalNames = (string[])glP.GetNames();
            if (globalNames != null)
	        {
                foreach (string s in globalNames)
                {
                    res = glP.Get5(s, UseCached, out ValOut, out ResolvedValOut, out WasResolved);
                    typ = glP.GetType2(s);
                    SwProperty p = new SwProperty(s, (swCustomInfoType_e)typ, ValOut, true);
                    p.ResValue = ResolvedValOut;
                    this.propertySet.Add(p);
                }
            }
            
            string[] specNames = (string[])spP.GetNames();
            if (spP != null)
            {
                foreach (string s in specNames)
                {
                    res = spP.Get5(s, UseCached, out ValOut, out ResolvedValOut, out WasResolved);
                    typ = spP.GetType2(s);
                    SwProperty p = new SwProperty(s, (swCustomInfoType_e)typ, ValOut, false);
                    p.ResValue = ResolvedValOut;
                    this.propertySet.Add(p);
                }
            }

            this.LinkControlToProperty("CUTLIST MATERIAL", this.cs.GetCutlistMatBox());
            this.LinkControlToProperty("EDGE FRONT (L)", this.cs.GetEdgeFrontBox());
            this.LinkControlToProperty("EDGE BACK (L)", this.cs.GetEdgeBackBox());
            this.LinkControlToProperty("EDGE LEFT (W)", this.cs.GetEdgeLeftBox());
            this.LinkControlToProperty("EDGE RIGHT (W)", this.cs.GetEdgeRightBox());

            this.LinkControlToProperty("Description", this.gp.GetDescriptionBox());
            this.LinkControlToProperty("LENGTH", this.gp.GetLengthBox());
            this.gp.UpdateLengthRes(propertySet.GetProperty("LENGTH"));
            this.LinkControlToProperty("WIDTH", gp.GetWidthBox());
            this.gp.UpdateWidthRes(propertySet.GetProperty("WIDTH"));
            this.LinkControlToProperty("THICKNESS", gp.GetThicknessBox());
            this.gp.UpdateThickRes(propertySet.GetProperty("THICKNESS"));
            this.LinkControlToProperty("WALL THICKNESS", gp.GetWallThicknessBox());
            this.gp.UpdateWallThickRes(propertySet.GetProperty("WALL THICKNESS"));
            this.LinkControlToProperty("COMMENT", gp.GetCommentBox());
            this.LinkControlToProperty("BLANK QTY", mp.GetPartsPerBlankBox());
            this.LinkControlToProperty("CNC1", mp.GetCNC1Box());
            this.LinkControlToProperty("CNC2", mp.GetCNC2Box());
            this.LinkControlToProperty("OVERL", mp.GetOverLBox());
            this.LinkControlToProperty("OVERW", mp.GetOverWBox());
            this.LinkControlToProperty("OP1", op.GetOp1Box());
            this.LinkControlToProperty("OP2", op.GetOp2Box());
            this.LinkControlToProperty("OP3", op.GetOp3Box());
            this.LinkControlToProperty("OP4", op.GetOp4Box());
            this.LinkControlToProperty("OP5", op.GetOp5Box());


            //string t = string.Empty;
            //foreach (SwProperty px in this.propertySet)
            //    t += string.Format("{0} as {1} = {2}, ID {3}\n", px.Name, px.Type.ToString(), px.ResValue, px.ID);
        }

        private void LinkControlToProperty(string property, Control c)
        {
            SwProperty p = this.propertySet.GetProperty(property);
            if (this.propertySet.Contains(p))
            {
                //System.Diagnostics.Debug.Print(p.ToString());
                p.SwApp = this.swApp;
                p.Ctl = c;
            }
            else
            {
                SwProperty x = new SwProperty(property, swCustomInfoType_e.swCustomInfoText, string.Empty, true);
                x.SwApp = this.swApp;
                x.Ctl = c;
            }
        }

        private void RedBrick_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.propertySet.Write(this.swApp);
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

        private void RedBrick_Shown(object sender, EventArgs e)
        {
            if (this.propertySet.Contains("DEPARTMENT"))
                if (this.propertySet.GetProperty("DEPARTMENT").Value.ToUpper() == "METAL")
                {
                    this.ds_CheckedChanged(this, e);
                }
            op.GetProperties();
        }
    }
}