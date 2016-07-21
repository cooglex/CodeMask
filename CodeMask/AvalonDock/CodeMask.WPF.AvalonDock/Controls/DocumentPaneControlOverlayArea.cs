/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System.Windows;

namespace CodeMask.WPF.AvalonDock.Controls
{
    public class DocumentPaneControlOverlayArea : OverlayArea
    {
        private readonly LayoutDocumentPaneControl _documentPaneControl;


        internal DocumentPaneControlOverlayArea(
            IOverlayWindow overlayWindow,
            LayoutDocumentPaneControl documentPaneControl)
            : base(overlayWindow)
        {
            _documentPaneControl = documentPaneControl;
            SetScreenDetectionArea(new Rect(
                _documentPaneControl.PointToScreenDPI(new Point()),
                _documentPaneControl.TransformActualSizeToAncestor()));
        }
    }
}