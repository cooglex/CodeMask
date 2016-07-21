/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System;

namespace CodeMask.WPF.AvalonDock.Themes
{
    public class GenericTheme : Theme
    {
        public override Uri GetResourceUri()
        {
            return new Uri(
                "/CodeMask.WPF.AvalonDock;component/Themes/generic.xaml",
                UriKind.Relative);
        }
    }
}