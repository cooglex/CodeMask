using System;
using System.Windows;
using System.Windows.Controls;

namespace CodeMask.WPF.Controls.Panels
{
    /// <summary>
    /// 
    /// </summary>
    public class UniformWrapPanel : WrapPanel
    {
        #region override methods

        /// <summary>
        /// </summary>
        /// <param name="availableSize"></param>
        /// <returns></returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            if (Children.Count > 0 && IsAutoUniform)
            {
                if (Orientation == Orientation.Horizontal)
                {
                    var totalWidth = availableSize.Width;
                    ItemWidth = 0.0;
                    foreach (UIElement el in Children)
                    {
                        el.Measure(availableSize);
                        var next = el.DesiredSize;
                        if (!(double.IsInfinity(next.Width) || double.IsNaN(next.Width)))
                        {
                            ItemWidth = Math.Max(next.Width, ItemWidth);
                        }
                    }
                }
                else
                {
                    var totalHeight = availableSize.Height;
                    ItemHeight = 0.0;
                    foreach (UIElement el in Children)
                    {
                        el.Measure(availableSize);
                        var next = el.DesiredSize;
                        if (!(double.IsInfinity(next.Height) || double.IsNaN(next.Height)))
                        {
                            ItemHeight = Math.Max(next.Height, ItemHeight);
                        }
                    }
                }
            }
            return base.MeasureOverride(availableSize);
        }

        #endregion

        #region properties

        /// <summary>
        /// </summary>
        public bool IsAutoUniform
        {
            get { return (bool) GetValue(IsAutoUniformProperty); }
            set { SetValue(IsAutoUniformProperty, value); }
        }

        /// <summary>
        ///     IsAutoUniform依赖属性。
        /// </summary>
        public static readonly DependencyProperty IsAutoUniformProperty = DependencyProperty.Register("IsAutoUniform",
            typeof (bool), typeof (UniformWrapPanel), new FrameworkPropertyMetadata(true, IsAutoUniformChanged));


        private static void IsAutoUniformChanged(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            if (sender is UniformWrapPanel)
            {
                ((UniformWrapPanel) sender).InvalidateVisual();
            }
        }

        #endregion
    }
}