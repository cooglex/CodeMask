/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System.Windows;

namespace CodeMask.WPF.AvalonDock.Controls
{
    public class OverlayWindowDropTarget : IOverlayWindowDropTarget
    {
        private readonly Rect _screenDetectionArea;

        private readonly OverlayWindowDropTargetType _type;
        private IOverlayWindowArea _overlayArea;

        internal OverlayWindowDropTarget(IOverlayWindowArea overlayArea, OverlayWindowDropTargetType targetType,
            FrameworkElement element)
        {
            _overlayArea = overlayArea;
            _type = targetType;
            _screenDetectionArea = new Rect(element.TransformToDeviceDPI(new Point()),
                element.TransformActualSizeToAncestor());
        }

        Rect IOverlayWindowDropTarget.ScreenDetectionArea
        {
            get { return _screenDetectionArea; }
        }

        OverlayWindowDropTargetType IOverlayWindowDropTarget.Type
        {
            get { return _type; }
        }
    }
}