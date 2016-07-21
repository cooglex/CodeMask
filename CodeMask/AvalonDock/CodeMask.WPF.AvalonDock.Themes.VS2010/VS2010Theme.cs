/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System;

namespace CodeMask.WPF.AvalonDock.Themes
{
    public class VS2010Theme : Theme
    {
        public override Uri GetResourceUri()
        {
            return new Uri(
                "/CodeMask.WPF.AvalonDock.Themes.VS2010;component/Theme.xaml",
                UriKind.Relative);
        }
    }
}