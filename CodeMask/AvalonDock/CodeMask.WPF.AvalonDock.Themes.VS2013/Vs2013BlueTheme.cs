/************************************************************************

   AvalonDock







 

  

  **********************************************************************/

using System;

namespace CodeMask.WPF.AvalonDock.Themes
{
    public class Vs2013BlueTheme : Theme
    {
        public override Uri GetResourceUri()
        {
            return new Uri(
                "/CodeMask.WPF.AvalonDock.Themes.VS2013;component/BlueTheme.xaml",
                UriKind.Relative);
        }
    }
}