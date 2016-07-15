using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CodeMask.WPF.Converters
{
    /// <summary>
    ///     Class NullToVisibilityCollapsedConverter
    /// </summary>
    public sealed class NullToVisibilityCollapsedConverter : IValueConverter
    {
        /// <summary>
        ///     转换值。
        /// </summary>
        /// <param name="value">绑定源生成的值。</param>
        /// <param name="targetType">绑定目标属性的类型。</param>
        /// <param name="parameter">要使用的转换器参数。</param>
        /// <param name="culture">要用在转换器中的区域性。</param>
        /// <returns>转换后的值。如果该方法返回 null，则使用有效的 null 值。</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibility;
            var flag = false;
            if (parameter != null)
            {
                flag = bool.Parse((string) parameter);
            }
            if (flag)
            {
                visibility = (value != null ? Visibility.Collapsed : Visibility.Visible);
                return visibility;
            }
            visibility = (value != null ? Visibility.Visible : Visibility.Collapsed);
            return visibility;
        }

        /// <summary>
        ///     转换值。
        /// </summary>
        /// <param name="value">绑定目标生成的值。</param>
        /// <param name="targetType">要转换到的类型。</param>
        /// <param name="parameter">要使用的转换器参数。</param>
        /// <param name="culture">要用在转换器中的区域性。</param>
        /// <returns>转换后的值。如果该方法返回 null，则使用有效的 null 值。</returns>
        /// <exception cref="System.NotSupportedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}