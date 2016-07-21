/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System.Linq;
using System.Windows;
using System.Windows.Media;
using CodeMask.WPF.AvalonDock.Layout;

namespace CodeMask.WPF.AvalonDock.Controls
{
    internal class DocumentPaneGroupDropTarget : DropTarget<LayoutDocumentPaneGroupControl>
    {
        private readonly LayoutDocumentPaneGroupControl _targetPane;

        internal DocumentPaneGroupDropTarget(LayoutDocumentPaneGroupControl paneControl, Rect detectionRect,
            DropTargetType type)
            : base(paneControl, detectionRect, type)
        {
            _targetPane = paneControl;
        }

        protected override void Drop(LayoutDocumentFloatingWindow floatingWindow)
        {
            var targetModel = _targetPane.Model as ILayoutPane;

            switch (Type)
            {
                case DropTargetType.DocumentPaneGroupDockInside:

                    #region DropTargetType.DocumentPaneGroupDockInside

                {
                    var paneGroupModel = targetModel as LayoutDocumentPaneGroup;
                    var paneModel = paneGroupModel.Children[0] as LayoutDocumentPane;
                    var sourceModel = floatingWindow.RootDocument;

                    paneModel.Children.Insert(0, sourceModel);
                }
                    break;

                    #endregion
            }
            base.Drop(floatingWindow);
        }

        protected override void Drop(LayoutAnchorableFloatingWindow floatingWindow)
        {
            var targetModel = _targetPane.Model as ILayoutPane;

            switch (Type)
            {
                case DropTargetType.DocumentPaneGroupDockInside:

                    #region DropTargetType.DocumentPaneGroupDockInside

                {
                    var paneGroupModel = targetModel as LayoutDocumentPaneGroup;
                    var paneModel = paneGroupModel.Children[0] as LayoutDocumentPane;
                    var layoutAnchorablePaneGroup = floatingWindow.RootPanel;

                    var i = 0;
                    foreach (
                        var anchorableToImport in
                            layoutAnchorablePaneGroup.Descendents().OfType<LayoutAnchorable>().ToArray())
                    {
                        paneModel.Children.Insert(i, anchorableToImport);
                        i++;
                    }
                }
                    break;

                    #endregion
            }

            base.Drop(floatingWindow);
        }

        public override Geometry GetPreviewPath(OverlayWindow overlayWindow, LayoutFloatingWindow floatingWindowModel)
        {
            switch (Type)
            {
                case DropTargetType.DocumentPaneGroupDockInside:

                    #region DropTargetType.DocumentPaneGroupDockInside

                {
                    var targetScreenRect = TargetElement.GetScreenArea();
                    targetScreenRect.Offset(-overlayWindow.Left, -overlayWindow.Top);

                    return new RectangleGeometry(targetScreenRect);
                }

                    #endregion
            }

            return null;
        }
    }
}