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

            if (md != null)
            {
                System.Diagnostics.Debug.WriteLine(DateTime.Now.ToLongTimeString() + " -- " + md.GetType().ToString());
                RedBrick rb = new RedBrick(this.swApp);
                rb.ShowDialog();
            }
            else
            {
                swApp.SendMsgToUser2(Properties.Resources.MustOpenDocument, 
                    (int)swMessageBoxIcon_e.swMbStop,
                    (int)swMessageBoxBtn_e.swMbOk);
            }
        }

        /// <summary>
        ///  The SldWorks swApp variable is pre-assigned for you.
        /// </summary>
        public SldWorks swApp;
    }
}


