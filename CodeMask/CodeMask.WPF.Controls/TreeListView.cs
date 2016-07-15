using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CodeMask.WPF.Controls
{
    /// <summary>
    /// Class LevelToIndentConverter
    /// </summary>
    public class LevelToIndentConverter : IValueConverter
    {
        /// <summary>
        /// Converts the specified o.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <param name="type">The type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>System.Object.</returns>
        public object Convert(object o, Type type, object parameter,
                              CultureInfo culture)
        {
            return new Thickness((int)o * c_IndentSize, 0, 0, 0);
        }

        /// <summary>
        /// Converts the back.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <param name="type">The type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>System.Object.</returns>
        /// <exception cref="System.NotSupportedException"></exception>
        public object ConvertBack(object o, Type type, object parameter,
                                  CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// The c_ indent size
        /// </summary>
        private const double c_IndentSize = 18.0;
    }

    /// <summary>
    /// Class TreeListView
    /// </summary>
    public class TreeListView : TreeView
    {
        /// <summary>
        /// Initializes static members of the <see cref="TreeListView"/> class.
        /// </summary>
        static TreeListView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TreeListView), new FrameworkPropertyMetadata(typeof(TreeListView)));
        }

        /// <summary>
        /// 创建用于显示内容的 <see cref="T:System.Windows.Controls.TreeViewItem" />。
        /// </summary>
        /// <returns>一个用作内容容器的新 <see cref="T:System.Windows.Controls.TreeViewItem" />。</returns>
        protected override DependencyObject
                           GetContainerForItemOverride()
        {
            return new TreeListViewItem();
        }

        /// <summary>
        /// 确定指定项是否是其自己的容器，或可以作为其自己的容器。
        /// </summary>
        /// <param name="item">要计算的对象。</param>
        /// <returns>如果 <paramref name="item" /> 为 <see cref="T:System.Windows.Controls.TreeViewItem" />，则为 true；否则为 false。</returns>
        protected override bool
                           IsItemItsOwnContainerOverride(object item)
        {
            return item is TreeListViewItem;
        }



        /// <summary>
        /// Gets or sets the columns header.
        /// </summary>
        /// <value>The columns header.</value>
        public GridViewColumnCollection ColumnsHeader
        {
            get { return (GridViewColumnCollection)GetValue(ColumnsHeaderProperty); }
            set { SetValue(ColumnsHeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ColumnsHeader.  This enables animation, styling, binding, etc...
        /// <summary>
        /// The columns header property
        /// </summary>
        public static readonly DependencyProperty ColumnsHeaderProperty =
            DependencyProperty.Register("ColumnsHeader", typeof(GridViewColumnCollection), typeof(TreeListView), new PropertyMetadata());


    }

    /// <summary>
    /// Class TreeListViewItem
    /// </summary>
    public class TreeListViewItem : TreeViewItem
    {
        /// <summary>
        /// Initializes static members of the <see cref="TreeListViewItem"/> class.
        /// </summary>
        static TreeListViewItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TreeListViewItem), new FrameworkPropertyMetadata(typeof(TreeListViewItem)));
        }

        /// <summary>
        /// 当未处理的 <see cref="E:System.Windows.Input.Mouse.MouseDown" /> 附加事件在其路由中到达派生自此类的元素时，调用此方法。实现此方法可为此事件添加类处理。
        /// </summary>
        /// <param name="e">包含事件数据的 <see cref="T:System.Windows.Input.MouseButtonEventArgs" />。此事件数据报告有关按下的鼠标按钮和已处理状态的详细信息。</param>
        protected override void OnMouseDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
        }

        /// <summary>
        /// Item's hierarchy in the tree
        /// </summary>
        /// <value>The level.</value>
        public int Level
        {
            get
            {
                if (_level == -1)
                {
                    TreeListViewItem parent =
                        ItemsControl.ItemsControlFromItemContainer(this)
                            as TreeListViewItem;
                    _level = (parent != null) ? parent.Level + 1 : 0;
                }
                return _level;
            }
        }

        /// <summary>
        /// 创建新的 <see cref="T:System.Windows.Controls.TreeViewItem" /> 以用于显示对象。
        /// </summary>
        /// <returns>一个新的 <see cref="T:System.Windows.Controls.TreeViewItem" />。</returns>
        protected override DependencyObject
                           GetContainerForItemOverride()
        {
            return new TreeListViewItem();
        }

        /// <summary>
        /// 确定对象是否为 <see cref="T:System.Windows.Controls.TreeViewItem" />。
        /// </summary>
        /// <param name="item">要计算的对象。</param>
        /// <returns>如果 <paramref name="item" /> 为 <see cref="T:System.Windows.Controls.TreeViewItem" />，则为 true；否则为 false。</returns>
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is TreeListViewItem;
        }

        /// <summary>
        /// The _level
        /// </summary>
        private int _level = -1;
    }
}