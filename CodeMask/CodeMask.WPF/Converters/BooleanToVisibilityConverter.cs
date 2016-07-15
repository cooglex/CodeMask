using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CodeMask.WPF.Converters
{
    /// <summary>
    ///     Class BooleanToVisibilityConverter
    /// </summary>
    /// <example>
    ///     <code>
    ///           <![CDATA[
    /// <local:BooleanToVisibilityConverter x:Key="HiddenIfTrue" TriggerValue="True" IsHidden="True"/>
    ///          <!--Hides control if boolean value is false-->
    ///           <local:BooleanToVisibilityConverter x:Key="HiddenIfFalse" TriggerValue="False" IsHidden="True"/>
    ///           <!--Collapses control if boolean value is true-->
    ///           <local:BooleanToVisibilityConverter x:Key="CollapsedIfTrue" TriggerValue="True" IsHidden="False"/>
    ///           <!--Collapses control if boolean value is false-->
    ///           <local:BooleanToVisibilityConverter x:Key="CollapsedIfFalse" TriggerValue="False" IsHidden="False"/>
    ///           ]]>
    ///      </code>
    /// </example>
    public class BooleanToVisibilityConverter : IValueConverter
    {
        //Set to true if you just want to hide the control
        //else set to false if you want to collapse the control
        /// <summary>
        ///     The is hidden
        /// </summary>
        private bool isHidden;

        //Set to true if you want to show control when boolean value is true
        //Set to false if you want to hide/collapse control when value is true
        /// <summary>
        ///     The trigger value
        /// </summary>
        private bool triggerValue;

        /// <summary>
        ///     Gets or sets a value indicating whether [trigger value].
        /// </summary>
        /// <value><c>true</c> if [trigger value]; otherwise, <c>false</c>.</value>
        public bool TriggerValue
        {
            get { return triggerValue; }
            set { triggerValue = value; }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is hidden.
        /// </summary>
        /// <value><c>true</c> if this instance is hidden; otherwise, <c>false</c>.</value>
        public bool IsHidden
        {
            get { return isHidden; }
            set { isHidden = value; }
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
            return GetVisibility(value);
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
        ///     Gets the visibility.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Object.</returns>
        private object GetVisibility(object value)
        {
            if (!(value is bool))
                return DependencyProperty.UnsetValue;
            var objValue = (bool) value;
            if ((objValue && TriggerValue && IsHidden) || (!objValue && !TriggerValue && IsHidden))
            {
                return Visibility.Hidden;
            }
            if ((objValue && TriggerValue && !IsHidden) || (!objValue && !TriggerValue && !IsHidden))
            {
                return Visibility.Collapsed;
            }
            return Visibility.Visible;
        }
    }
}