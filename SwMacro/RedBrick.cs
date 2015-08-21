using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;

using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace redbrick.csproj
{
    [ComVisible(true)]
    [ProgId(SWTASKPANE_PROGID)]
    public partial class RedBrick : Form
    {
        public const string SWTASKPANE_PROGID = "amstore.redbrick";
        private SldWorks swApp;
        private ModelDoc2 md;

        private DepartmentSelector ds;
        private ConfigurationSpecific cs;
        private GeneralProperties gp;
        private MachineProperties mp;
        private Ops op;

        public RedBrick(SldWorks sw)
        {
            go(sw);
        }

        private void go(SldWorks sw)
        {
#if DEBUG
            System.Diagnostics.Debug.Print("Started " + DateTime.Now.ToLongTimeString());
#endif
            this.swApp = sw;
            this.md = (ModelDoc2)swApp.ActiveDoc;

            int _dt = md.GetType();
            this.DocType = (swDocumentTypes_e)_dt;


            this.propertySet = new SwProperties(this.swApp);

            switch (_dt)
            {
                case (int)swDocumentTypes_e.swDocASSEMBLY:
                    this.AcquiredProperties.CreateDefaultPartSet();
                    break;
                case (int)swDocumentTypes_e.swDocDRAWING:
                    //this.AcquiredProperties.CreateDefaultDrawingSet();
                    this.AcquiredProperties.GetPropertyData();
                    DrawingRedbrick drb = new DrawingRedbrick(this.swApp);
                    InitializeComponent();
                    this.tbMainTable.ColumnCount = 1;
                    this.tbMainTable.RowCount = 3;
                    this.InitDrawing();
                    this.tbMainTable.Controls.Add(drb);
                    drb.Dock = DockStyle.Fill;
                    break;
                case (int)swDocumentTypes_e.swDocPART:
                    //this.AcquiredProperties.CreateDefaultPartSet();
                    this.AcquiredProperties.GetPropertyData();
                    InitializeComponent();
                    this.tbMainTable.ColumnCount = 3;
                    this.tbMainTable.RowCount = 3;
                    this.InitModel();
                    //this.propertySet.ReadProperties();
                    break;
                case (int)swDocumentTypes_e.swDocSDM:
                    break;
                case (int)swDocumentTypes_e.swDocNONE:
                    break;
                default:
                    break;
            }
        }


        public void InitModel()
        {
            this.SetWindowProperties();
            this.InitComponents();
            this.SetupEvents();
        }

        public void InitDrawing()
        {
            this.SetWindowProperties();
        }

        private void SetupEvents()
        {
            ds.CheckedChanged += new EventHandler(ds_CheckedChanged);
            cs.GetEdgeFrontBox().SelectedIndexChanged += new EventHandler(RedBrick_SelectedIndexChanged);
            cs.GetEdgeBackBox().SelectedIndexChanged += new EventHandler(RedBrick_SelectedIndexChanged);
            cs.GetEdgeLeftBox().SelectedIndexChanged += new EventHandler(RedBrick_SelectedIndexChanged);
            cs.GetEdgeRightBox().SelectedIndexChanged += new EventHandler(RedBrick_SelectedIndexChanged);
            mp.GetOverLBox().TextChanged += new EventHandler(RedBrick_SelectedIndexChanged);
            mp.GetOverWBox().TextChanged += new EventHandler(RedBrick_SelectedIndexChanged);
            gp.GetLengthBox().TextChanged += new EventHandler(RedBrick_SelectedIndexChanged);
            gp.GetWidthBox().TextChanged += new EventHandler(RedBrick_SelectedIndexChanged);
        }

        void ds_CheckedChanged(object sender, EventArgs e)
        {
            op.RefreshOps(ds.OpType);
            cs.ToggleFields(ds.OpType);
            gp.ToggleFields(ds.OpType);
        }

        private void SetWindowProperties()
        {
            this.Top = Properties.Settings.Default.Top;
            this.Left = Properties.Settings.Default.Left;
            this.Width = Properties.Settings.Default.Width;
            this.Height = Properties.Settings.Default.Height;
            if (this.AcquiredProperties.GetProperty("PartNo") != null)
                this.Text = "Editing " + this.AcquiredProperties.GetProperty("PartNo").ResValue + " ...";
            else
                this.Text = "Editing " + this.swApp.ActiveDoc.ToString() + " ...";
        }

        private void InitComponents()
        {
            ds = new DepartmentSelector(ref this.propertySet);
            cs = new ConfigurationSpecific(ref this.propertySet);
            gp = new GeneralProperties(ref this.propertySet);
            mp = new MachineProperties(ref this.propertySet);
            op = new Ops(ref this.propertySet);

            this.tbMainTable.Controls.Add(ds, 0, 0);
            this.ds.Dock = DockStyle.Fill;

            this.tbMainTable.Controls.Add(cs, 0, 1);
            this.tbMainTable.SetRowSpan(cs, 2);
            this.cs.Dock = DockStyle.Fill;
            

            this.tbMainTable.Controls.Add(gp, 1, 1);
            this.gp.Dock = DockStyle.Fill;
            this.tbMainTable.Controls.Add(mp, 1, 2);
            this.mp.Dock = DockStyle.Fill;

            this.tbMainTable.Controls.Add(op, 2, 1);
            this.tbMainTable.SetRowSpan(op, 2);
            this.op.Dock = DockStyle.Fill;
            this.op.OpType = this.ds.OpType;
            this.ds_CheckedChanged(this, new EventArgs());

            Button bOK = new Button();
            bOK.Text = "OK";
            bOK.Click += new EventHandler(bOK_Click);

            this.tbMainTable.Controls.Add(bOK, 0, 3);
            this.tbMainTable.Controls.Add(new Button(), 1, 3);
            //this.getPartData();

        }

        void RedBrick_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateBlankSize();
        }

        void UpdateBlankSize()
        {
            double blankL;
            double blankW;
            blankL = gp.PartLength + mp.OverL + cs.EdgeDiffL;
            blankW = gp.PartWidth + mp.OverW + cs.EdgeDiffW;
            mp.GetBlankLBox().Text = blankL.ToString();
            mp.GetBlankWBox().Text = blankW.ToString();
#if DEBUG
            System.Diagnostics.Debug.Print(string.Format("{0} + {1} + {2} = {3}", gp.PartLength, mp.OverL, cs.EdgeDiffL, blankL));
            System.Diagnostics.Debug.Print(string.Format("{0} + {1} + {2} = {3}", gp.PartWidth, mp.OverW, cs.EdgeDiffW, blankW));
#endif
        }

        private void InitDrawgingComponents()
        {
            Button bOK = new Button();
            bOK.Text = "OK";
            bOK.Click += new EventHandler(bOK_Click);


            this.tbMainTable.Controls.Add(bOK, 0, 1);
            //this.tbMainTable.Controls.Add(new Button(), 1, 1);
        }

        void bOK_Click(object sender, EventArgs e)
        {
            this.propertySet.ReadControls();
            this.propertySet.Write();
            this.Close();
        }

        private void LinkControls()
        {

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

            TextBox tbBlankL = mp.GetBlankLBox();
            TextBox tbBlankW = mp.GetBlankWBox();
            double l = gp.PartLength - cs.EdgeDiffL;
            double w = gp.PartWidth - cs.EdgeDiffW;
            tbBlankL.Text = l.ToString();
            tbBlankW.Text = w.ToString();
        }

        private void LinkControlToProperty(string property, Control c)
        {
            SwProperty p = this.propertySet.GetProperty(property);
            if (this.propertySet.Contains(p))
            {
#if DEBUG
                System.Diagnostics.Debug.Print(p.ToString());
#endif
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
            Properties.Settings.Default.Width = this.Width;
            Properties.Settings.Default.Height = this.Height;
            Properties.Settings.Default.Save();
        }

        //private void configurationSpecificPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (this.configurationSpecificPropertiesToolStripMenuItem.Checked)
        //    {
        //        this.configurationSpecificPropertiesToolStripMenuItem.Checked = false;
        //        this.tbMainTable.Controls.Remove(cs);
        //    }
        //    else
        //    {
        //        this.configurationSpecificPropertiesToolStripMenuItem.Checked = true;
        //        this.tbMainTable.Controls.Add(cs, 0, 1);
        //        cs.Dock = DockStyle.Fill;
        //    }
        //}

        //private void globalPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (this.globalPropertiesToolStripMenuItem.Checked)
        //    {
        //        this.globalPropertiesToolStripMenuItem.Checked = false;
        //        this.tbMainTable.Controls.Remove(gp);
        //    }
        //    else
        //    {
        //        this.globalPropertiesToolStripMenuItem.Checked = true;
        //        this.tbMainTable.Controls.Add(gp, 1, 1);
        //        gp.Dock = DockStyle.Fill;
        //    }
        //}

        //private void machinePropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (this.machinePropertiesToolStripMenuItem.Checked)
        //    {
        //        this.machinePropertiesToolStripMenuItem.Checked = false;
        //        this.tbMainTable.Controls.Remove(mp);
        //    }
        //    else
        //    {
        //        this.machinePropertiesToolStripMenuItem.Checked = true;
        //        this.tbMainTable.Controls.Add(mp, 1, 2);
        //        mp.Dock = DockStyle.Fill;
        //    }
        //}

        //private void operationsToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (this.operationsToolStripMenuItem.Checked)
        //    {
        //        this.operationsToolStripMenuItem.Checked = false;
        //        this.tbMainTable.Controls.Remove(op);
        //    }
        //    else
        //    {
        //        this.operationsToolStripMenuItem.Checked = true;
        //        this.tbMainTable.Controls.Add(op, 2, 1);
        //        op.Dock = DockStyle.Fill;
        //    }
        //}

        private void RedBrick_Shown(object sender, EventArgs e)
        {
            //if (this.DocType != swDocumentTypes_e.swDocDRAWING)
            //{
            //    if (this.propertySet.Contains("DEPARTMENT"))
            //        if (this.propertySet.GetProperty("DEPARTMENT").Value.ToUpper() == "METAL")
            //        {
            //            System.Windows.Forms.MessageBox.Show(this.propertySet.GetProperty("DEPARTMENT").Value.ToUpper());
            //            this.ds_CheckedChanged(this, e);
            //        }
            //    op.GetProperties();   
            //}
        }

        private swDocumentTypes_e _docType;

        public swDocumentTypes_e DocType
        {
            get { return _docType; }
            set { _docType = value; }
        }

        private SwProperties propertySet;

        public SwProperties AcquiredProperties
        {
            get { return propertySet; }
            set { propertySet = value; }
        }
	
    }
}