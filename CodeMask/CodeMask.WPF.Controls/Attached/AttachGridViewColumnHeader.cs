using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CodeMask.WPF.Controls.Attached
{
    /// <summary>
    /// Class AttachGridViewColumnHeader
    /// </summary>
    public class AttachGridViewColumnHeader
    {

        /// <summary>
        /// Gets the width of the set last column.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>DataGridLength.</returns>
        [AttachedPropertyBrowsableForType(typeof(GridViewColumnHeader))]
        public static DataGridLength GetSetLastColumnWidth(DependencyObject obj)
        {
            return (DataGridLength)obj.GetValue(SetLastColumnWidthProperty);
        }

        /// <summary>
        /// Sets the width of the set last column.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        public static void SetSetLastColumnWidth(DependencyObject obj, DataGridLength value)
        {
            obj.SetValue(SetLastColumnWidthProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsLast.  This enables animation, styling, binding, etc...
        /// <summary>
        /// The set last column width property
        /// </summary>
        public static readonly DependencyProperty SetLastColumnWidthProperty =
            DependencyProperty.RegisterAttached("SetLastColumnWidth", typeof(DataGridLength), typeof(AttachGridViewColumnHeader), new PropertyMetadata(OnSetLastColumnWidthPropertyChanged));

        /// <summary>
        /// Called when [set last column width property changed].
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnSetLastColumnWidthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GridViewColumnHeader item = (GridViewColumnHeader)d;
            if (item != null)
            {
                GridViewHeaderRowPresenter dataGrid = item.FindParent<GridViewHeaderRowPresenter>();
                if (dataGrid != null)
                {
                    if (dataGrid.Columns.Count != 0 && dataGrid.Columns.Last() == item.Column)
                    {
                        //item.Width = AttachGridViewColumnHeader.GetSetLastColumnWidth(item);
                    }
                }
            }
        }
    }
}
