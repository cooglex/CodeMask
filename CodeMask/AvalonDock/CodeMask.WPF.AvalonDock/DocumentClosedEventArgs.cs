/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System;
using CodeMask.WPF.AvalonDock.Layout;

namespace CodeMask.WPF.AvalonDock
{
    public class DocumentClosedEventArgs : EventArgs
    {
        public DocumentClosedEventArgs(LayoutDocument document)
        {
            Document = document;
        }

        public LayoutDocument Document { get; private set; }
    }
}