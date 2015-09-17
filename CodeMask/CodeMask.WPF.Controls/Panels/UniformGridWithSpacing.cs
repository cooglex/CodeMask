using System;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace CodeMask.WPF.Controls.Panels
{
    /// <summary>
    ///     可以指定子元素之间间隔距离的UniformGrid。
    /// </summary>
    public class UniformGridWithSpacing : UniformGrid
    {
        #region override methods

        /// <summary>
        ///     通过测量所有子元素计算 <see cref="System.Windows.Controls.Primitives.UniformGrid" /> 的期望大小。
        /// </summary>
        /// <param name="constraint">网格可用区域的 <see cref="System.Windows.Size" />。</param>
        /// <returns>基于网格的子内容和 <paramref name="constraint" /> 参数的期望 <see cref="System.Windows.Size" />。</returns>
        protected override Size MeasureOverride(Size constraint)
        {
            var s = base.MeasureOverride(constraint);
            return new Size(s.Width + Math.Max(0, Columns - 1)*SpaceBetweenColumns,
                s.Height + Math.Max(0, Rows - 1)*SpaceBetweenRows);
        }

        /// <summary>
        ///     通过在所有子元素之间平均分配空间来定义 <see cref="System.Windows.Controls.Primitives.UniformGrid" /> 的布局。
        /// </summary>
        /// <param name="arrangeSize">供网格使用的区域的 <see cref="System.Windows.Size" />。</param>
        /// <returns>为显示可见子元素而呈现的网格的实际 <see cref="System.Windows.Size" />。</returns>
        protected override Size ArrangeOverride(Size arrangeSize)
        {
            var spaceBetweenColumns = SpaceBetweenColumns;
            var spaceBetweenRows = SpaceBetweenRows;
            var rows = Math.Max(1, Rows);
            var columns = Math.Max(1, Columns);
            var rect = new Rect(0, 0,
                (arrangeSize.Width - spaceBetweenColumns*(columns - 1))/columns,
                (arrangeSize.Height - spaceBetweenRows*(rows - 1))/rows);
            var currentColumn = FirstColumn;
            rect.X += currentColumn*(rect.Width + spaceBetweenColumns);
            foreach (UIElement element in InternalChildren)
            {
                element.Arrange(rect);
                if (element.Visibility != Visibility.Collapsed)
                {
                    if (++currentColumn >= columns)
                    {
                        currentColumn = 0;
                        rect.X = 0;
                        rect.Y += rect.Height + spaceBetweenRows;
                    }
                    else
                    {
                        rect.X += rect.Width + spaceBetweenColumns;
                    }
                }
            }
            return arrangeSize;
        }

        #endregion

        #region properties

        #region SpaceBetweenColumns

        /// <summary>
        ///     获取或设置两列之间的间隔距离。
        /// </summary>
        /// <value>两列之间的间隔距离。</value>
        public double SpaceBetweenColumns
        {
            get { return (double) GetValue(SpaceBetweenColumnsProperty); }
            set { SetValue(SpaceBetweenColumnsProperty, value); }
        }

        /// <summary>
        ///     两列之间的间隔距离属性。
        /// </summary>
        public static readonly DependencyProperty SpaceBetweenColumnsProperty =
            DependencyProperty.Register("SpaceBetweenColumns", typeof (double), typeof (UniformGridWithSpacing),
                new FrameworkPropertyMetadata(7.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        #endregion

        #region SpaceBetweenRows

        /// <summary>
        ///     获取或设置两行之间的间隔距离。
        /// </summary>
        /// <value>两行之间的间隔距离。</value>
        public double SpaceBetweenRows
        {
            get { return (double) GetValue(SpaceBetweenRowsProperty); }
            set { SetValue(SpaceBetweenRowsProperty, value); }
        }

        /// <summary>
        ///     两行之间的间隔距离属性。
        /// </summary>
        public static readonly DependencyProperty SpaceBetweenRowsProperty =
            DependencyProperty.Register("SpaceBetweenRows", typeof (double), typeof (UniformGridWithSpacing),
                new FrameworkPropertyMetadata(5.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        #endregion

        #endregion
    }
}