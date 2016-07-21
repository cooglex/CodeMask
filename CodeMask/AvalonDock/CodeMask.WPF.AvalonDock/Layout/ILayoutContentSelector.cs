/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

namespace CodeMask.WPF.AvalonDock.Layout
{
    public interface ILayoutContentSelector
    {
        int SelectedContentIndex { get; set; }

        LayoutContent SelectedContent { get; }

        int IndexOf(LayoutContent content);
    }
}