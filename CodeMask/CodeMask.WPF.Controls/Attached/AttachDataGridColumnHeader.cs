using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace CodeMask.WPF.Controls.Attached
{
    /// <summary>
    /// Class Extensions
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// 查找给定元素指定类型的父类
        /// </summary>
        /// <typeparam name="T">父类型</typeparam>
        /// <param name="obj">The obj.</param>
        /// <returns>父类</returns>
        public static T FindParent<T>(this DependencyObject obj) where T : DependencyObject
        {
            return obj.GetAncestors().OfType<T>().FirstOrDefault();
        }

        /// <summary>
        /// 查找可视对象的所有迭代父类
        /// </summary>
        /// <param name="element">要查找父对象的元素</param>
        /// <returns>可视父类集合</returns>
        /// <remarks>返回的集合包括传入的参数element.</remarks>
        public static IEnumerable<DependencyObject> GetAncestors(this DependencyObject element)
        {
            do
            {
                yield return element;
                element = VisualTreeHelper.GetParent(element);
            } while (element != null);
        }
    }

    /// <summary>
    /// Class AttachDataGridColumnHeader
    /// </summary>
    public class AttachDataGridColumnHeader
    {
        /// <summary>
        /// Gets the width of the set last column.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>DataGridLength.</returns>
        [AttachedPropertyBrowsableForType(typeof(DataGridColumnHeader))]
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
            DependencyProperty.RegisterAttached("SetLastColumnWidth", typeof(DataGridLength), typeof(AttachDataGridColumnHeader), new PropertyMetadata(OnSetLastColumnWidthPropertyChanged));

        /// <summary>
        /// Called when [set last column width property changed].
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnSetLastColumnWidthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataGridColumnHeader item = (DataGridColumnHeader)d;
            if (item != null)
            {
                DataGrid dataGrid = item.FindParent<DataGrid>();
                if (dataGrid != null)
                {
                    if (dataGrid.Columns.Count != 0 && dataGrid.Columns.Last() == item.Column)
                    {
                        item.Column.Width = AttachDataGridColumnHeader.GetSetLastColumnWidth(item);
                    }
                }
            }
        }
    }
}