/************************************************************************

   AvalonDock







 

  

  **********************************************************************/

using System;

namespace CodeMask.WPF.AvalonDock.Themes
{
    public class Vs2013LightTheme : Theme
    {
        public override Uri GetResourceUri()
        {
            return new Uri(
                "/CodeMask.WPF.AvalonDock.Themes.VS2013;component/LightTheme.xaml",
                UriKind.Relative);
        }
    }
}