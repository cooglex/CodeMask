/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System.Windows;

namespace CodeMask.WPF.AvalonDock.Controls
{
    public class DockingManagerOverlayArea : OverlayArea
    {
        private readonly DockingManager _manager;

        internal DockingManagerOverlayArea(IOverlayWindow overlayWindow, DockingManager manager)
            : base(overlayWindow)
        {
            _manager = manager;

            SetScreenDetectionArea(new Rect(
                _manager.PointToScreenDPI(new Point()),
                _manager.TransformActualSizeToAncestor()));
        }
    }
}