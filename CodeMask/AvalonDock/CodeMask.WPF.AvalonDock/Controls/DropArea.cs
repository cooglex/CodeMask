/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System.Windows;

namespace CodeMask.WPF.AvalonDock.Controls
{
    public enum DropAreaType
    {
        DockingManager,

        DocumentPane,

        DocumentPaneGroup,

        AnchorablePane
    }


    public interface IDropArea
    {
        Rect DetectionRect { get; }
        DropAreaType Type { get; }
    }

    public class DropArea<T> : IDropArea where T : FrameworkElement
    {
        internal DropArea(T areaElement, DropAreaType type)
        {
            AreaElement = areaElement;
            DetectionRect = areaElement.GetScreenArea();
            Type = type;
        }

        public T AreaElement { get; }

        public Rect DetectionRect { get; }

        public DropAreaType Type { get; }
    }
}