
using System.Windows;

namespace CodeMask.WPF.Controls.Attached
{
    /// <summary>
    /// UniformGrid
    /// </summary>
    public class AttachUniformGrid
    {
        #region Rows
        /// <summary>
        /// Gets the rows.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>System.Int32.</returns>
        public static int GetRows(DependencyObject obj)
        {
            return (int)obj.GetValue(RowsProperty);
        }

        /// <summary>
        /// Sets the rows.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        public static void SetRows(DependencyObject obj, int value)
        {
            obj.SetValue(RowsProperty, value);
        }
        /// <summary>
        /// The rows property
        /// </summary>
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.RegisterAttached("Rows", typeof(int), typeof(AttachUniformGrid), new PropertyMetadata(1));
        #endregion

        #region Columns
        /// <summary>
        /// Gets the columns.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>System.Int32.</returns>
        public static int GetColumns(DependencyObject obj)
        {
            return (int)obj.GetValue(ColumnsProperty);
        }

        /// <summary>
        /// Sets the columns.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        public static void SetColumns(DependencyObject obj, int value)
        {
            obj.SetValue(ColumnsProperty, value);
        }

        /// <summary>
        /// The columns property
        /// </summary>
        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.RegisterAttached("Columns", typeof(int), typeof(AttachUniformGrid), new PropertyMetadata(1));

        #endregion

    }
}
