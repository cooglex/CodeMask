/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System.Windows;
using System.Windows.Media;
using CodeMask.WPF.AvalonDock.Layout;

namespace CodeMask.WPF.AvalonDock.Controls
{
    internal interface IDropTarget
    {
        DropTargetType Type { get; }
        Geometry GetPreviewPath(OverlayWindow overlayWindow, LayoutFloatingWindow floatingWindow);

        bool HitTest(Point dragPoint);

        void Drop(LayoutFloatingWindow floatingWindow);

        void DragEnter();

        void DragLeave();
    }
}