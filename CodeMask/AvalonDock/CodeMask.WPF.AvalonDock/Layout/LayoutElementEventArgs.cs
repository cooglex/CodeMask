/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System;

namespace CodeMask.WPF.AvalonDock.Layout
{
    public class LayoutElementEventArgs : EventArgs
    {
        public LayoutElementEventArgs(LayoutElement element)
        {
            Element = element;
        }


        public LayoutElement Element { get; private set; }
    }
}