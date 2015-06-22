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
            RedBrick rb = new RedBrick();
            rb.ShowDialog();
        }

        /// <summary>
        ///  The SldWorks swApp variable is pre-assigned for you.
        /// </summary>
        public SldWorks swApp;
    }
}


