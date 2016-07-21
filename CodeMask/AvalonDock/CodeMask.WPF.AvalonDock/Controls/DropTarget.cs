/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using CodeMask.WPF.AvalonDock.Layout;

namespace CodeMask.WPF.AvalonDock.Controls
{
    internal abstract class DropTarget<T> : DropTargetBase, IDropTarget where T : FrameworkElement
    {
        protected DropTarget(T targetElement, Rect detectionRect, DropTargetType type)
        {
            TargetElement = targetElement;
            DetectionRects = new[] {detectionRect};
            Type = type;
        }

        protected DropTarget(T targetElement, IEnumerable<Rect> detectionRects, DropTargetType type)
        {
            TargetElement = targetElement;
            DetectionRects = detectionRects.ToArray();
            Type = type;
        }

        public Rect[] DetectionRects { get; }

        public T TargetElement { get; }

        public DropTargetType Type { get; }


        public void Drop(LayoutFloatingWindow floatingWindow)
        {
            var root = floatingWindow.Root;
            var currentActiveContent = floatingWindow.Root.ActiveContent;
            var fwAsAnchorable = floatingWindow as LayoutAnchorableFloatingWindow;

            if (fwAsAnchorable != null)
            {
                Drop(fwAsAnchorable);
            }
            else
            {
                var fwAsDocument = floatingWindow as LayoutDocumentFloatingWindow;
                Drop(fwAsDocument);
            }

            Dispatcher.BeginInvoke(new Action(() =>
            {
                currentActiveContent.IsSelected = false;
                currentActiveContent.IsActive = false;
                currentActiveContent.IsActive = true;
            }), DispatcherPriority.Background);
        }

        public virtual bool HitTest(Point dragPoint)
        {
            return DetectionRects.Any(dr => dr.Contains(dragPoint));
        }

        public abstract Geometry GetPreviewPath(OverlayWindow overlayWindow, LayoutFloatingWindow floatingWindow);


        public void DragEnter()
        {
            SetIsDraggingOver(TargetElement, true);
        }

        public void DragLeave()
        {
            SetIsDraggingOver(TargetElement, false);
        }

        protected virtual void Drop(LayoutAnchorableFloatingWindow floatingWindow)
        {
        }

        protected virtual void Drop(LayoutDocumentFloatingWindow floatingWindow)
        {
        }
    }
}