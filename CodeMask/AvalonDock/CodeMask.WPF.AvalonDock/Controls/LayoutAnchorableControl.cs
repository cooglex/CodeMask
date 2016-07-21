/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CodeMask.WPF.AvalonDock.Layout;

namespace CodeMask.WPF.AvalonDock.Controls
{
    public class LayoutAnchorableControl : Control
    {
        static LayoutAnchorableControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (LayoutAnchorableControl),
                new FrameworkPropertyMetadata(typeof (LayoutAnchorableControl)));
            FocusableProperty.OverrideMetadata(typeof (LayoutAnchorableControl), new FrameworkPropertyMetadata(false));
        }


        protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            if (Model != null)
                Model.IsActive = true;

            base.OnGotKeyboardFocus(e);
        }

        #region Model

        /// <summary>
        ///     Model Dependency Property
        /// </summary>
        public static readonly DependencyProperty ModelProperty =
            DependencyProperty.Register("Model", typeof (LayoutAnchorable), typeof (LayoutAnchorableControl),
                new FrameworkPropertyMetadata(null,
                    OnModelChanged));

        /// <summary>
        ///     Gets or sets the Model property.  This dependency property
        ///     indicates the model attached to this view.
        /// </summary>
        public LayoutAnchorable Model
        {
            get { return (LayoutAnchorable) GetValue(ModelProperty); }
            set { SetValue(ModelProperty, value); }
        }

        /// <summary>
        ///     Handles changes to the Model property.
        /// </summary>
        private static void OnModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((LayoutAnchorableControl) d).OnModelChanged(e);
        }

        /// <summary>
        ///     Provides derived classes an opportunity to handle changes to the Model property.
        /// </summary>
        protected virtual void OnModelChanged(DependencyPropertyChangedEventArgs e)
        {
            if (Model != null)
                SetLayoutItem(Model.Root.Manager.GetLayoutItemFromModel(Model));
            else
                SetLayoutItem(null);
        }

        #endregion

        #region LayoutItem

        /// <summary>
        ///     LayoutItem Read-Only Dependency Property
        /// </summary>
        private static readonly DependencyPropertyKey LayoutItemPropertyKey
            = DependencyProperty.RegisterReadOnly("LayoutItem", typeof (LayoutItem), typeof (LayoutAnchorableControl),
                new FrameworkPropertyMetadata((LayoutItem) null));

        public static readonly DependencyProperty LayoutItemProperty
            = LayoutItemPropertyKey.DependencyProperty;

        /// <summary>
        ///     Gets the LayoutItem property.  This dependency property
        ///     indicates the LayoutItem attached to this tag item.
        /// </summary>
        public LayoutItem LayoutItem
        {
            get { return (LayoutItem) GetValue(LayoutItemProperty); }
        }

        /// <summary>
        ///     Provides a secure method for setting the LayoutItem property.
        ///     This dependency property indicates the LayoutItem attached to this tag item.
        /// </summary>
        /// <param name="value">The new value for the property.</param>
        protected void SetLayoutItem(LayoutItem value)
        {
            SetValue(LayoutItemPropertyKey, value);
        }

        #endregion
    }
}