/************************************************************************

   AvalonDock







 

  

  **********************************************************************/

using System;

namespace CodeMask.WPF.AvalonDock.Themes
{
    public class Vs2013DarkTheme : Theme
    {
        public override Uri GetResourceUri()
        {
            return new Uri(
                "/CodeMask.WPF.AvalonDock.Themes.VS2013;component/DarkTheme.xaml",
                UriKind.Relative);
        }
    }
}