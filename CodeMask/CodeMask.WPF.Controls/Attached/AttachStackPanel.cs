using System.Windows;
using System.Windows.Controls;

namespace CodeMask.WPF.Controls.Attached
{
    /// <summary>
    /// StackPanel
    /// </summary>
    public class AttachStackPanel
    {
        #region Orientation
        /// <summary>
        /// Gets the orientation.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>Orientation.</returns>
        public static Orientation GetOrientation(DependencyObject obj)
        {
            return (Orientation)obj.GetValue(OrientationProperty);
        }

        /// <summary>
        /// Sets the orientation.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        public static void SetOrientation(DependencyObject obj, int value)
        {
            obj.SetValue(OrientationProperty, value);
        }
        /// <summary>
        /// The orientation property
        /// </summary>
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.RegisterAttached("Orientation", typeof(Orientation), typeof(AttachStackPanel), new PropertyMetadata(Orientation.Horizontal));
        #endregion
    }
}
