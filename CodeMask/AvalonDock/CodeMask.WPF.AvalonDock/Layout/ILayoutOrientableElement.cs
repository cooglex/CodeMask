/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System.Windows.Controls;

namespace CodeMask.WPF.AvalonDock.Layout
{
    public interface ILayoutOrientableGroup : ILayoutGroup
    {
        Orientation Orientation { get; set; }
    }
}