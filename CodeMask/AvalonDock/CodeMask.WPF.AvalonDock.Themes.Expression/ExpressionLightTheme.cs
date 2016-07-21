/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System;

namespace CodeMask.WPF.AvalonDock.Themes
{
    public class ExpressionLightTheme : Theme
    {
        public override Uri GetResourceUri()
        {
            return new Uri(
                "/CodeMask.WPF.AvalonDock.Themes.Expression;component/LightTheme.xaml",
                UriKind.Relative);
        }
    }
}