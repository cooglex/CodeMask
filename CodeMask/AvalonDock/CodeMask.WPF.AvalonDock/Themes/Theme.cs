/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System;
using System.Windows;

namespace CodeMask.WPF.AvalonDock.Themes
{
    public abstract class Theme : DependencyObject
    {
        public abstract Uri GetResourceUri();
    }
}