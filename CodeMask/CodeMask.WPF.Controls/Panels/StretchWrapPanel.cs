using System;
using System.Windows;
using System.Windows.Controls;

namespace CodeMask.WPF.Controls.Panels
{
    /// <summary>
    /// </summary>
    public class StretchWrapPanel : Panel
    {
        #region private methods

        private Size GetItemDesiredSize(UIElement element)
        {
            var itemSize = new Size();
            itemSize.Width = double.IsInfinity(element.DesiredSize.Width) ? 0 : element.DesiredSize.Width;
            itemSize.Height = double.IsInfinity(element.DesiredSize.Height) ? 0 : element.DesiredSize.Height;
            return itemSize;
        }

        #endregion Private Methods

        #region override methods

        protected override Size ArrangeOverride(Size finalSize)
        {
            var lineCount = 0;
            double lineWidth = 0;
            double lineHeight = 0;

            double top = 0;
            double left = 0;
            double width = 0;
            double height = 0;
            double gap = 0;

            for (var u = 0; u <= Children.Count - 1; u++)
            {
                var curr = Children[u];
                var currSize = GetItemDesiredSize(curr);
                lineCount++;

                lineWidth = lineWidth + currSize.Width;
                lineHeight = Math.Max(lineHeight, currSize.Height);

                var isLast = Children[Children.Count - 1].Equals(curr);

                if (isLast || (lineWidth + GetItemDesiredSize(Children[u + 1]).Width) >= finalSize.Width)
                {
                    gap = (isLast && !IsLastRowJustified)
                        ? 0
                        : lineCount <= 1 ? 0 : (finalSize.Width - lineWidth)/(lineCount - 1);
                    for (var i = (u - lineCount) + 1; i <= u; i++)
                    {
                        var element = Children[i];
                        width = GetItemDesiredSize(element).Width;
                        height = GetItemDesiredSize(element).Height;

                        element.Arrange(new Rect(left, top, width, height));

                        left += (width + gap);
                    }

                    top += lineHeight;
                    left = 0;

                    // reset our line markers
                    lineWidth = 0;
                    lineHeight = 0;
                    lineCount = 0;
                }
            }

            return finalSize;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            double lineWidth = 0;
            double lineHeight = 0;
            double totalWidth = 0;
            double totalHeight = 0;

            var asBigAsYouWant = new Size(double.PositiveInfinity, double.PositiveInfinity);

            foreach (UIElement element in Children)
            {
                element.Measure(asBigAsYouWant);
                var itemSize = GetItemDesiredSize(element);

                // reset the line if we run out of room
                if ((Children[Children.Count - 1].Equals(element)) ||
                    (lineWidth + GetItemDesiredSize(Children[Children.IndexOf(element) + 1]).Width > availableSize.Width))
                {
                    if (lineWidth > totalWidth) totalWidth = lineWidth;
                    lineWidth = 0;

                    totalHeight += lineHeight;
                    lineHeight = 0;
                }

                lineWidth += itemSize.Width;

                // take the tallest item in the line as our lineHeight
                if (itemSize.Height > lineHeight) lineHeight = itemSize.Height;
            }

            return new Size(availableSize.Width, totalHeight);
        }

        #endregion Protected Methods

        #region properties

        #region IsLastRowJustified

        /// <summary>
        ///     IsLastRowJustified依赖属性。
        /// </summary>
        public static readonly DependencyProperty IsLastRowJustifiedProperty =
            DependencyProperty.Register("IsLastRowJustified", typeof (bool), typeof (StretchWrapPanel),
                new FrameworkPropertyMetadata(true));

        /// <summary>
        /// </summary>
        public bool IsLastRowJustified
        {
            get { return (bool) GetValue(IsLastRowJustifiedProperty); }
            set { SetValue(IsLastRowJustifiedProperty, value); }
        }

        #endregion

        #endregion
    }
}