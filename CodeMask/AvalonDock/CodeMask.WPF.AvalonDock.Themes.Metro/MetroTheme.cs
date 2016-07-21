/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System;

namespace CodeMask.WPF.AvalonDock.Themes
{
    public class MetroTheme : Theme
    {
        public override Uri GetResourceUri()
        {
            return new Uri(
                "/CodeMask.WPF.AvalonDock.Themes.Metro;component/Theme.xaml",
                UriKind.Relative);
        }
    }
}