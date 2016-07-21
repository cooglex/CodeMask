/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System.Windows;

namespace CodeMask.WPF.AvalonDock.Controls
{
    public class AnchorablePaneControlOverlayArea : OverlayArea
    {
        private readonly LayoutAnchorablePaneControl _anchorablePaneControl;

        internal AnchorablePaneControlOverlayArea(
            IOverlayWindow overlayWindow,
            LayoutAnchorablePaneControl anchorablePaneControl)
            : base(overlayWindow)
        {
            _anchorablePaneControl = anchorablePaneControl;
            SetScreenDetectionArea(new Rect(
                _anchorablePaneControl.PointToScreenDPI(new Point()),
                _anchorablePaneControl.TransformActualSizeToAncestor()));
        }
    }
}