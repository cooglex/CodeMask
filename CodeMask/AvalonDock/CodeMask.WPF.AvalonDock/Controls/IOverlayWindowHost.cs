/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System.Collections.Generic;
using System.Windows;

namespace CodeMask.WPF.AvalonDock.Controls
{
    internal interface IOverlayWindowHost
    {
        DockingManager Manager { get; }
        bool HitTest(Point dragPoint);

        IOverlayWindow ShowOverlayWindow(LayoutFloatingWindowControl draggingWindow);

        void HideOverlayWindow();

        IEnumerable<IDropArea> GetDropAreas(LayoutFloatingWindowControl draggingWindow);
    }
}