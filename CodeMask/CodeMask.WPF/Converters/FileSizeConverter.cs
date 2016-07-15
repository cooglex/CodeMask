using System;
using System.Globalization;
using System.Windows.Data;

namespace CodeMask.WPF.Converters
{
    /// <summary>
    ///     Class ByteLengthConverter
    /// </summary>
    public class FileSizeConverter : IValueConverter
    {
        /// <summary>
        ///     Default Scale is 1024 bytes to a kilobyte, etc.
        /// </summary>
        private const int Scale = 1024;

        /// <summary>
        ///     Suffixes used to denote magnitude in descending order
        /// </summary>
        private static readonly string[] Units = {"TB", "GB", "MB", "KB", "Bytes"};

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
            var bytes = (int) value;
            return FormatBytes(bytes);
        }

        /// <summary>
        ///     转换值。
        /// </summary>
        /// <param name="value">绑定目标生成的值。</param>
        /// <param name="targetType">要转换到的类型。</param>
        /// <param name="parameter">要使用的转换器参数。</param>
        /// <param name="culture">要用在转换器中的区域性。</param>
        /// <returns>转换后的值。如果该方法返回 null，则使用有效的 null 值。</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Formats the bytes.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns>System.String.</returns>
        public static string FormatBytes(long bytes)
        {
            var min = (long) Math.Pow(Scale, Units.Length - 1);
            // Starting with the biggest order of magnitude, work through until the number bytes is greater the minimum number of bytes that can be display (whole number) with that scale
            foreach (var order in Units)
            {
                if (bytes > min)
                {
                    return string.Format("{0:##.##} {1}", decimal.Divide(bytes, min), order);
                }
                min /= Scale;
            }
            return "0 Bytes";
        }
    }
}