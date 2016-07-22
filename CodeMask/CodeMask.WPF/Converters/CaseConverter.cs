using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace CodeMask.WPF.Converters
{
    /// <summary>
    ///     大小写转换器。
    /// </summary>
    [ValueConversion(typeof (string), typeof (string))]
    public class CaseConverter : CoreConverter<CaseConverter>, IValueConverter
    {
        /// <summary>
        /// </summary>
        public CaseConverter()
        {
        }


        /// <summary>
        /// </summary>
        /// <param name="casing"></param>
        public CaseConverter(CharacterCasing casing)
        {
            Casing = casing;
        }


        /// <summary>
        /// </summary>
        /// <param name="sourceCasing"></param>
        /// <param name="targetCasing"></param>
        public CaseConverter(CharacterCasing sourceCasing, CharacterCasing targetCasing)
        {
            SourceCasing = sourceCasing;
            TargetCasing = targetCasing;
        }

        /// <summary>
        /// </summary>
        [ConstructorArgument("sourceCasing")]
        public CharacterCasing SourceCasing { get; set; }

        /// <summary>
        /// </summary>
        [ConstructorArgument("targetCasing")]
        public CharacterCasing TargetCasing { get; set; }

        /// <summary>
        /// </summary>
        [ConstructorArgument("casing")]
        public CharacterCasing Casing
        {
            set
            {
                SourceCasing = value;
                TargetCasing = value;
            }
        }

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
            var str = value as string;

            if (str != null)
            {
                culture = culture ?? CultureInfo.CurrentCulture;

                switch (TargetCasing)
                {
                    case CharacterCasing.Lower:
                        return str.ToLower(culture);
                    case CharacterCasing.Upper:
                        return str.ToUpper(culture);
                    case CharacterCasing.Normal:
                        return str;
                }
            }

            return DependencyProperty.UnsetValue;
        }

        /// <summary>
        ///     转换值。
        /// </summary>
        /// <param name="value">绑定目标生成的值。</param>
        /// <param name="targetType">要转换到的类型。</param>
        /// <param name="parameter">要使用的转换器参数。</param>
        /// <param name="culture">要用在转换器中的区域性。</param>
        /// <returns>转换后的值。如果该方法返回 null，则使用有效的 null 值。</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value as string;

            if (str != null)
            {
                culture = culture ?? CultureInfo.CurrentCulture;

                switch (SourceCasing)
                {
                    case CharacterCasing.Lower:
                        return str.ToLower(culture);
                    case CharacterCasing.Upper:
                        return str.ToUpper(culture);
                    case CharacterCasing.Normal:
                        return str;
                }
            }

            return DependencyProperty.UnsetValue;
        }


        /// <summary>
        ///     返回默认的转换器对象。
        /// </summary>
        /// <param name="serviceProvider">服务提供者。</param>
        /// <returns>转换器对象。</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}