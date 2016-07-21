/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System.Windows;

namespace CodeMask.WPF.AvalonDock.Controls
{
    internal abstract class DropTargetBase : DependencyObject
    {
        #region IsDraggingOver

        /// <summary>
        ///     IsDraggingOver Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty IsDraggingOverProperty =
            DependencyProperty.RegisterAttached("IsDraggingOver", typeof (bool), typeof (DropTargetBase),
                new FrameworkPropertyMetadata(false));

        /// <summary>
        ///     Gets the IsDraggingOver property.  This dependency property
        ///     indicates if user is dragging a window over the target element.
        /// </summary>
        public static bool GetIsDraggingOver(DependencyObject d)
        {
            return (bool) d.GetValue(IsDraggingOverProperty);
        }

        /// <summary>
        ///     Sets the IsDraggingOver property.  This dependency property
        ///     indicates if user is dragging away a window from the target element.
        /// </summary>
        public static void SetIsDraggingOver(DependencyObject d, bool value)
        {
            d.SetValue(IsDraggingOverProperty, value);
        }

        #endregion
    }
}