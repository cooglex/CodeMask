/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System.ComponentModel;

namespace CodeMask.WPF.AvalonDock.Layout
{
    public interface ILayoutElement : INotifyPropertyChanged, INotifyPropertyChanging
    {
        ILayoutContainer Parent { get; }
        ILayoutRoot Root { get; }
    }
}