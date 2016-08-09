using System.Windows;
using System.Windows.Controls;

namespace CodeMask.WPF.Controls.Attached
{
    public class AttachContentControl
    {
        #region ContentKey

        public static DependencyProperty ContentKeyProperty = DependencyProperty.RegisterAttached("ContentKey",
            typeof (string), typeof (AttachContentControl),
            new PropertyMetadata(null, ContentKeyPropertyChangedCallback));

        public static string GetContentKey(DependencyObject dependencyObject)
        {
            return (string) dependencyObject.GetValue(ContentKeyProperty);
        }

        public static void SetContentKey(DependencyObject dependencyObject, string value)
        {
            dependencyObject.SetValue(ContentKeyProperty, value);
        }

        private static void ContentKeyPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                var control = (d as ContentControl);
                control.SetResourceReference(ContentControl.ContentProperty, e.NewValue);
            }
        }

        #endregion

        #region TagKey

        public static DependencyProperty TagKeyProperty = DependencyProperty.RegisterAttached("TagKey",
            typeof (string), typeof (AttachContentControl), new PropertyMetadata(null, TagKeyPropertyChangedCallback));

        public static string GetTagKey(DependencyObject dependencyObject)
        {
            return (string) dependencyObject.GetValue(TagKeyProperty);
        }

        public static void SetTagKey(DependencyObject dependencyObject, string value)
        {
            dependencyObject.SetValue(TagKeyProperty, value);
        }

        private static void TagKeyPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                var control = (d as ContentControl);
                control.SetResourceReference(FrameworkElement.TagProperty, e.NewValue);
            }
        }

        #endregion
    }
}