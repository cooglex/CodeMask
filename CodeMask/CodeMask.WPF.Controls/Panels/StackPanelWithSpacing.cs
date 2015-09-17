using System;
using System.Windows;
using System.Windows.Controls;

namespace CodeMask.WPF.Controls.Panels
{
    /// <summary>
    ///     可以指定子元素布局间距的StackPanel。
    /// </summary>
    public class StackPanelWithSpacing : Panel
    {
        #region Properties

        #region Orientation

        /// <summary>
        ///     获取或设置面板的布局方向。
        /// </summary>
        /// <value>布局方向。</value>
        public Orientation Orientation
        {
            get { return (Orientation) GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        /// <summary>
        ///     布局方向属性。
        /// </summary>
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof (Orientation), typeof (StackPanelWithSpacing),
                new FrameworkPropertyMetadata(Orientation.Vertical, FrameworkPropertyMetadataOptions.AffectsMeasure));

        #endregion

        #region SpaceBetweenItems

        /// <summary>
        ///     获取或设置面板的子元素之间的间隔距离。
        /// </summary>
        /// <value>两个子元素之间的间隔距离。</value>
        public double SpaceBetweenItems
        {
            get { return (double) GetValue(SpaceBetweenItemsProperty); }
            set { SetValue(SpaceBetweenItemsProperty, value); }
        }

        /// <summary>
        ///     两个子元素之间的间隔距离属性。
        /// </summary>
        public static readonly DependencyProperty SpaceBetweenItemsProperty =
            DependencyProperty.Register("SpaceBetweenItems", typeof (double), typeof (StackPanelWithSpacing),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        #endregion

        #endregion

        #region override properties

        /// <summary>
        ///     获取一个值，该值指示此 <see cref="System.Windows.Controls.Panel" /> 是否在单个维度中排列其子代。
        /// </summary>
        /// <value>如果有逻辑方向，为<c>true</c> ; 否则, <c>false</c>.</value>
        protected override bool HasLogicalOrientation => true;

        /// <summary>
        ///     获取面板的布局方向 <see cref="System.Windows.Controls.Orientation" />。
        /// </summary>
        /// <value>布局方向。</value>
        protected override Orientation LogicalOrientation => Orientation;

        #endregion

        #region override methods

        /// <summary>
        ///     当在派生类中重写时，请测量子元素在布局中所需的大小，然后确定 <see cref="System.Windows.FrameworkElement" /> 派生类的大小。
        /// </summary>
        /// <param name="availableSize">此元素可以赋给子元素的可用大小。 可以指定无穷大值，这表示元素的大小将调整为内容的可用大小。</param>
        /// <returns>此元素在布局过程中所需的大小，这是由此元素根据对其子元素大小的计算而确定的。</returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            var orientation = Orientation;
            if (orientation == Orientation.Horizontal)
            {
                availableSize.Width = double.PositiveInfinity;
            }
            else
            {
                availableSize.Height = double.PositiveInfinity;
            }
            var spaceBetweenItems = SpaceBetweenItems;
            double pos = 0;
            double maxWidth = 0;
            var hasVisibleItems = false;
            foreach (UIElement element in InternalChildren)
            {
                element.Measure(availableSize);
                var desiredSize = element.DesiredSize;
                if (orientation == Orientation.Horizontal)
                {
                    maxWidth = Math.Max(maxWidth, desiredSize.Height);
                    pos += desiredSize.Width;
                }
                else
                {
                    maxWidth = Math.Max(maxWidth, desiredSize.Width);
                    pos += desiredSize.Height;
                }
                if (element.Visibility != Visibility.Collapsed)
                {
                    pos += spaceBetweenItems;
                    hasVisibleItems = true;
                }
            }
            if (hasVisibleItems)
                pos -= spaceBetweenItems;
            if (orientation == Orientation.Horizontal)
                return new Size(pos, maxWidth);
            return new Size(maxWidth, pos);
        }

        /// <summary>
        ///     在派生类中重写时，请为 <see cref="T:System.Windows.FrameworkElement" /> 派生类定位子元素并确定大小。
        /// </summary>
        /// <param name="finalSize">父级中此元素应用来排列自身及其子元素的最终区域。</param>
        /// <returns>所用的实际大小。</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            var orientation = Orientation;
            var spaceBetweenItems = SpaceBetweenItems;
            double pos = 0;
            foreach (UIElement element in InternalChildren)
            {
                if (orientation == Orientation.Horizontal)
                {
                    element.Arrange(new Rect(pos, 0, element.DesiredSize.Width, finalSize.Height));
                    pos += element.DesiredSize.Width;
                }
                else
                {
                    element.Arrange(new Rect(0, pos, finalSize.Width, element.DesiredSize.Height));
                    pos += element.DesiredSize.Height;
                }
                if (element.Visibility != Visibility.Collapsed)
                {
                    pos += spaceBetweenItems;
                }
            }
            return finalSize;
        }

        #endregion
    }
}