/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System;

namespace CodeMask.WPF.AvalonDock.Themes
{
    public class ExpressionDarkTheme : Theme
    {
        public override Uri GetResourceUri()
        {
            return new Uri(
                "/CodeMask.WPF.AvalonDock.Themes.Expression;component/DarkTheme.xaml",
                UriKind.Relative);
        }
    }
}