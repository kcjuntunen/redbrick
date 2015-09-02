using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using SolidWorks.Interop.sldworks;

namespace redbrick.csproj
{
    public partial class tvRevs : UserControl
    {
        private DrawingRevs dr;

        public tvRevs(SldWorks sw)
        {
            dr = new DrawingRevs(sw);
            dr.Read();

            InitializeComponent();
            Init();
        }

        private void Init()
        {
            foreach (DrawingRev r in dr)
            {
                TreeNode tnList = new TreeNode(r.List.Value);
                TreeNode tnDate = new TreeNode(r.Date.Value);
                TreeNode tnECO;
                int test;
                if (!r.Eco.Value.Contains("NA") && !(r.Eco.Value == string.Empty))
                {
                    CutlistData cd = new CutlistData();
                    eco e = cd.GetECOData(r.Eco.Value);
                    TreeNode tnC = new TreeNode("Changes: " + e.Changes, 0, 0);
                    TreeNode tnD = new TreeNode("Error Description: " + e.ErrDescription, 0, 0);
                    TreeNode tnRB = new TreeNode("Requested by: " + e.RequestedBy, 0, 0);
                    TreeNode tnR = new TreeNode("Revision Description:" + e.Revision, 0, 0);
                    TreeNode tnS = new TreeNode("Status: " + e.Status, 0, 0);
                    TreeNode[] ts = { tnC, tnD, tnRB, tnR, tnS };

                    tnECO = new TreeNode(r.Eco.Value, ts);
                }
                else
                {
                    tnECO = new TreeNode(r.Eco.Value);
                }


                TreeNode tnDesc = new TreeNode(r.Description.Value);
                TreeNode[] tt = { tnECO, tnDesc, tnList, tnDate };

                TreeNode tn = new TreeNode(r.Revision.Value, tt);
                this.tvRevisions.Nodes.Add(tn);
                this.tvRevisions.LabelEdit = true;
            }
            
        }
    }
}
