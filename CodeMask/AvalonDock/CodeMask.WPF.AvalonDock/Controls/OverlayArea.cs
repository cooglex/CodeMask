/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System.Windows;

namespace CodeMask.WPF.AvalonDock.Controls
{
    public abstract class OverlayArea : IOverlayWindowArea
    {
        private IOverlayWindow _overlayWindow;

        private Rect? _screenDetectionArea;

        internal OverlayArea(IOverlayWindow overlayWindow)
        {
            _overlayWindow = overlayWindow;
        }

        Rect IOverlayWindowArea.ScreenDetectionArea
        {
            get { return _screenDetectionArea.Value; }
        }

        protected void SetScreenDetectionArea(Rect rect)
        {
            _screenDetectionArea = rect;
        }
    }
}