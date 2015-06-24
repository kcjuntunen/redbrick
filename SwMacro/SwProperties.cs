using System;
using System.Collections.Generic;
using System.Text;

using SolidWorks.Interop.swconst;
using SolidWorks.Interop.sldworks;

namespace redbrick.csproj
{
    class SwProperties : IEnumerable<SwProperty>
    {
        public SwProperty cutlistMaterial = new SwProperty("CUTLIST MATERIAL", swCustomInfoType_e.swCustomInfoNumber, "TBD MATERIAL", false);
        public SwProperty edgeFront = new SwProperty("EDGE FRONT (L)", swCustomInfoType_e.swCustomInfoNumber, string.Empty, false);
        public SwProperty edgeBack = new SwProperty("EDGE BACK (L)", swCustomInfoType_e.swCustomInfoNumber, string.Empty, false);
        public SwProperty edgeLeft = new SwProperty("EDGE LEFT (W)", swCustomInfoType_e.swCustomInfoNumber, string.Empty, false);
        public SwProperty edgeRight = new SwProperty("EDGE RIGHT (W)", swCustomInfoType_e.swCustomInfoNumber, string.Empty, false);

        public SwProperty descr = new SwProperty("Description", swCustomInfoType_e.swCustomInfoText, string.Empty, true);
        public SwProperty length = new SwProperty("LENGTH", swCustomInfoType_e.swCustomInfoText, "\"D1@Sketch1\"", true);
        public SwProperty width = new SwProperty("WIDTH", swCustomInfoType_e.swCustomInfoText, "\"D2@Sketch1\"", true);
        public SwProperty thick = new SwProperty("THICKNESS", swCustomInfoType_e.swCustomInfoText, "\"D1@Boss-Extrude1\"", true);
        public SwProperty wThick = new SwProperty("WALL THICKNESS", swCustomInfoType_e.swCustomInfoText, "\"Thickness@Sheet-Metal1\"", true);
        public SwProperty comment = new SwProperty("COMMENT", swCustomInfoType_e.swCustomInfoText, string.Empty, true);
        public SwProperty blnkQty = new SwProperty("BLANK QTY", swCustomInfoType_e.swCustomInfoDouble, "1", true);
        public SwProperty cnc1 = new SwProperty("CNC1", swCustomInfoType_e.swCustomInfoText, "NA", true);
        public SwProperty cnc2 = new SwProperty("CNC2", swCustomInfoType_e.swCustomInfoText, "NA", true);
        public SwProperty overL = new SwProperty("OVERL", swCustomInfoType_e.swCustomInfoDouble, "0.0", true);
        public SwProperty overW = new SwProperty("OVERW", swCustomInfoType_e.swCustomInfoDouble, "0.0", true);
        public SwProperty op1 = new SwProperty("OP1", swCustomInfoType_e.swCustomInfoText, string.Empty, true);
        public SwProperty op2 = new SwProperty("OP2", swCustomInfoType_e.swCustomInfoText, string.Empty, true);
        public SwProperty op3 = new SwProperty("OP3", swCustomInfoType_e.swCustomInfoText, string.Empty, true);
        public SwProperty op4 = new SwProperty("OP4", swCustomInfoType_e.swCustomInfoText, string.Empty, true);
        public SwProperty op5 = new SwProperty("OP5", swCustomInfoType_e.swCustomInfoText, string.Empty, true);
        public SwProperty dept = new SwProperty("DEPARTMENT", swCustomInfoType_e.swCustomInfoText, "WOOD", true);
        public SwProperty updCNC = new SwProperty("UPDATE CNC", swCustomInfoType_e.swCustomInfoYesOrNo, "No", true);
        public SwProperty inc = new SwProperty("INCLUDE IN CUTLIST", swCustomInfoType_e.swCustomInfoYesOrNo, "Yes", true);

        public SwProperty prtNo = new SwProperty("PartNo", swCustomInfoType_e.swCustomInfoText, "$PRP:\"SW-File Name\"", true);
        public SwProperty swMat = new SwProperty("MATERIAL", swCustomInfoType_e.swCustomInfoText, "SW-Material@{0}", true);
        public SwProperty weight = new SwProperty("WEIGHT", swCustomInfoType_e.swCustomInfoText, "SW-Mass@{0}", true);
        public SwProperty vol = new SwProperty("VOLUME", swCustomInfoType_e.swCustomInfoText, "SW-Volume@{0}", true);

        //protected SldWorks swApp;

        public SwProperties()
        {
            //sw = this.swApp;

        }

        public IEnumerator<SwProperty> GetEnumerator()
        {
            yield return this.cutlistMaterial;
            yield return this.edgeFront;
            yield return this.edgeBack;
            yield return this.edgeLeft;
            yield return this.edgeRight;

            yield return this.prtNo;
            yield return this.swMat;
            yield return this.weight;
            yield return this.vol;

            yield return this.descr;
            yield return this.length;
            yield return this.width;
            yield return this.thick;
            yield return this.wThick;
            yield return this.comment;
            yield return this.blnkQty;
            yield return this.cnc1;
            yield return this.cnc2;
            yield return this.overL;
            yield return this.overW;
            yield return this.op1;
            yield return this.op2;
            yield return this.op3;
            yield return this.op4;
            yield return this.op5;
            yield return this.dept;
            yield return this.updCNC;
            yield return this.inc;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private int _clID;

        public int CutlistID
        {
            get { return _clID; }
            set { _clID = value; }
        }
	
    }
}
