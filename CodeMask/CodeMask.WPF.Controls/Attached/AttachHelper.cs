using System;
using System.Windows;
using System.Windows.Controls;

namespace CodeMask.WPF.Controls.Attached
{
    /// <summary>
    /// Class AttachBase
    /// </summary>
    public class AttachBase : DependencyObject
    {
        #region 附加属性CornerRadius

        /// <summary>
        /// The corner radius property
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(AttachBase), new PropertyMetadata(OnCornerRadiusPropertyChanged));

        /// <summary>
        /// Called when [corner radius property changed].
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnCornerRadiusPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        /// <summary>
        /// Sets the corner radius.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void SetCornerRadius(DependencyObject element, CornerRadius value)
        {
            element.SetValue(CornerRadiusProperty, value);
        }

        /// <summary>
        /// Gets the corner radius.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>CornerRadius.</returns>
        public static CornerRadius GetCornerRadius(DependencyObject element)
        {
            return (CornerRadius)element.GetValue(CornerRadiusProperty);
        }

        #endregion 附加属性CornerRadius

        #region 附加属性Margin

        /// <summary>
        /// The margin property
        /// </summary>
        public static readonly DependencyProperty MarginProperty = DependencyProperty.RegisterAttached("Margin", typeof(Thickness), typeof(AttachBase), new PropertyMetadata(new Thickness(0), OnMarginPropertyChanged));

        /// <summary>
        /// Called when [margin property changed].
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnMarginPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        /// <summary>
        /// Sets the margin.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void SetMargin(DependencyObject element, Thickness value)
        {
            element.SetValue(MarginProperty, value);
        }

        /// <summary>
        /// Gets the margin.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>Thickness.</returns>
        public static Thickness GetMargin(DependencyObject element)
        {
            return (Thickness)element.GetValue(MarginProperty);
        }

        #endregion 附加属性Margin

        #region 附加属性Width

        /// <summary>
        /// The width property
        /// </summary>
        public static readonly DependencyProperty WidthProperty = DependencyProperty.RegisterAttached("Width", typeof(double), typeof(AttachBase), new PropertyMetadata(Double.PositiveInfinity, OnWidthPropertyChanged));

        /// <summary>
        /// Called when [width property changed].
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnWidthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        /// <summary>
        /// Sets the width.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void SetWidth(DependencyObject element, double value)
        {
            element.SetValue(WidthProperty, value);
        }

        /// <summary>
        /// Gets the width.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>System.Double.</returns>
        public static double GetWidth(DependencyObject element)
        {
            return (double)element.GetValue(WidthProperty);
        }

        #endregion 附加属性Width

        #region 附加属性ChildWidth

        /// <summary>
        /// The ChildWidth property
        /// </summary>
        public static readonly DependencyProperty ChildWidthProperty = DependencyProperty.RegisterAttached("ChildWidth", typeof(double), typeof(AttachBase), new PropertyMetadata(Double.PositiveInfinity, OnChildWidthPropertyChanged));

        /// <summary>
        /// Called when [ChildWidth property changed].
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnChildWidthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        /// <summary>
        /// Sets the ChildWidth.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void SetChildWidth(DependencyObject element, double value)
        {
            element.SetValue(ChildWidthProperty, value);
        }

        /// <summary>
        /// Gets the ChildWidth.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>System.Double.</returns>
        public static double GetChildWidth(DependencyObject element)
        {
            return (double)element.GetValue(ChildWidthProperty);
        }

        #endregion 附加属性ChildWidth

        #region 附加属性Height

        /// <summary>
        /// The height property
        /// </summary>
        public static readonly DependencyProperty HeightProperty = DependencyProperty.RegisterAttached("Height", typeof(double), typeof(AttachBase), new PropertyMetadata(Double.PositiveInfinity, OnHeightPropertyChanged));

        /// <summary>
        /// Called when [height property changed].
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnHeightPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        /// <summary>
        /// Sets the height.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void SetHeight(DependencyObject element, double value)
        {
            element.SetValue(HeightProperty, value);
        }

        /// <summary>
        /// Gets the height.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>System.Double.</returns>
        public static double GetHeight(DependencyObject element)
        {
            return (double)element.GetValue(HeightProperty);
        }

        #endregion 附加属性Height

        #region 附加属性ChildHeight

        /// <summary>
        /// The ChildHeight property
        /// </summary>
        public static readonly DependencyProperty ChildHeightProperty = DependencyProperty.RegisterAttached("ChildHeight", typeof(double), typeof(AttachBase), new PropertyMetadata(Double.PositiveInfinity, OnChildHeightPropertyChanged));

        /// <summary>
        /// Called when [ChildHeight property changed].
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnChildHeightPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        /// <summary>
        /// Sets the ChildHeight.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void SetChildHeight(DependencyObject element, double value)
        {
            element.SetValue(ChildHeightProperty, value);
        }

        /// <summary>
        /// Gets the ChildHeight.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>System.Double.</returns>
        public static double GetChildHeight(DependencyObject element)
        {
            return (double)element.GetValue(ChildHeightProperty);
        }

        #endregion 附加属性ChildHeight

        #region 附加属性ChildSpace

        /// <summary>
        /// The ChildSpace property
        /// </summary>
        public static readonly DependencyProperty ChildSpaceProperty = DependencyProperty.RegisterAttached("ChildSpace", typeof(double), typeof(AttachBase), new PropertyMetadata(Double.PositiveInfinity, OnChildSpacePropertyChanged));

        /// <summary>
        /// Called when [ChildSpace property changed].
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnChildSpacePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        /// <summary>
        /// Sets the ChildSpace.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void SetChildSpace(DependencyObject element, double value)
        {
            element.SetValue(ChildSpaceProperty, value);
        }

        /// <summary>
        /// Gets the ChildSpace.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>System.Double.</returns>
        public static double GetChildSpace(DependencyObject element)
        {
            return (double)element.GetValue(ChildSpaceProperty);
        }

        #endregion 附加属性ChildSpace

        #region 附加属性Tag

        /// <summary>
        /// The tag property
        /// </summary>
        public static readonly DependencyProperty TagProperty = DependencyProperty.RegisterAttached("Tag", typeof(object), typeof(AttachBase), new PropertyMetadata(null, OnTagPropertyChanged));

        /// <summary>
        /// Called when [tag property changed].
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnTagPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        /// <summary>
        /// Sets the tag.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void SetTag(DependencyObject element, object value)
        {
            element.SetValue(TagProperty, value);
        }

        /// <summary>
        /// Gets the tag.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>System.Object.</returns>
        public static object GetTag(DependencyObject element)
        {
            return (object)element.GetValue(TagProperty);
        }

        #endregion 附加属性Tag

        #region Orientation

        /// <summary>
        /// Gets the orientation.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>Orientation.</returns>
        public static Orientation GetOrientation(DependencyObject obj)
        {
            return (Orientation)obj.GetValue(OrientationProperty);
        }

        /// <summary>
        /// Sets the orientation.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        public static void SetOrientation(DependencyObject obj, Orientation value)
        {
            obj.SetValue(OrientationProperty, value);
        }

        /// <summary>
        /// The orientation property
        /// </summary>
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.RegisterAttached("Orientation", typeof(Orientation), typeof(AttachBase), new PropertyMetadata(Orientation.Vertical));

        #endregion Orientation
    }
}