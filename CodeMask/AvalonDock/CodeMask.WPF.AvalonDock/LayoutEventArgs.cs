/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System;
using CodeMask.WPF.AvalonDock.Layout;

namespace CodeMask.WPF.AvalonDock
{
    internal class LayoutEventArgs : EventArgs
    {
        public LayoutEventArgs(LayoutRoot layoutRoot)
        {
            LayoutRoot = layoutRoot;
        }

        public LayoutRoot LayoutRoot { get; private set; }
    }
}