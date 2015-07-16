using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System.Runtime.InteropServices;
using System;

namespace redbrick.csproj
{
    public partial class SolidWorksMacro
    {
        public void Main()
        {
            ModelDoc2 md = (ModelDoc2)this.swApp.ActiveDoc;

            System.Diagnostics.Debug.WriteLine(DateTime.Now.ToLongTimeString() + " -- "  + md.GetType().ToString());

            switch (md.GetType())
            {
                case (int)swDocumentTypes_e.swDocASSEMBLY:
                    break;
                case (int)swDocumentTypes_e.swDocDRAWING:
                    DrawingRedbrick drb = new DrawingRedbrick(this.swApp);
                    drb.ShowDialog();
                    break;
                case (int)swDocumentTypes_e.swDocPART:
                    RedBrick rb = new RedBrick(this.swApp);
                    rb.ShowDialog();
                    break;
                case (int)swDocumentTypes_e.swDocSDM:
                    break;
                case (int)swDocumentTypes_e.swDocNONE:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        ///  The SldWorks swApp variable is pre-assigned for you.
        /// </summary>
        public SldWorks swApp;
    }
}


