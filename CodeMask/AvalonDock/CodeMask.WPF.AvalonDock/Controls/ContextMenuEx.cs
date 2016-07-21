/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CodeMask.WPF.AvalonDock.Controls
{
    public class ContextMenuEx : ContextMenu
    {
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new MenuItemEx();
        }

        protected override void OnOpened(RoutedEventArgs e)
        {
            BindingOperations.GetBindingExpression(this, ItemsSourceProperty).UpdateTarget();

            base.OnOpened(e);
        }
    }
}