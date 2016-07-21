/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System.Windows;
using System.Windows.Controls;
using CodeMask.WPF.AvalonDock.Layout;

namespace AvalonDock.MVVMTestApp
{
    internal class PanesTemplateSelector : DataTemplateSelector
    {
        public DataTemplate FileViewTemplate { get; set; }

        public DataTemplate FileStatsViewTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var itemAsLayoutContent = item as LayoutContent;

            if (item is FileViewModel)
                return FileViewTemplate;

            if (item is FileStatsViewModel)
                return FileStatsViewTemplate;

            return base.SelectTemplate(item, container);
        }
    }
}