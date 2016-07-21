/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System.Windows;

namespace CodeMask.WPF.AvalonDock.Controls
{
    internal interface IOverlayWindowDropTarget
    {
        Rect ScreenDetectionArea { get; }

        OverlayWindowDropTargetType Type { get; }
    }
}