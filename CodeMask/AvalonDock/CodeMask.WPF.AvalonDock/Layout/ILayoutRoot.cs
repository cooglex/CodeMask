/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System.Collections.ObjectModel;

namespace CodeMask.WPF.AvalonDock.Layout
{
    public interface ILayoutRoot
    {
        DockingManager Manager { get; }

        LayoutPanel RootPanel { get; }

        LayoutAnchorSide TopSide { get; }
        LayoutAnchorSide LeftSide { get; }
        LayoutAnchorSide RightSide { get; }
        LayoutAnchorSide BottomSide { get; }

        LayoutContent ActiveContent { get; set; }

        ObservableCollection<LayoutFloatingWindow> FloatingWindows { get; }
        ObservableCollection<LayoutAnchorable> Hidden { get; }

        void CollectGarbage();
    }
}