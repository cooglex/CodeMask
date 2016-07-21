/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System.ComponentModel;
using CodeMask.WPF.AvalonDock.Layout;

namespace CodeMask.WPF.AvalonDock
{
    public class DocumentClosingEventArgs : CancelEventArgs
    {
        public DocumentClosingEventArgs(LayoutDocument document)
        {
            Document = document;
        }

        public LayoutDocument Document { get; private set; }
    }
}