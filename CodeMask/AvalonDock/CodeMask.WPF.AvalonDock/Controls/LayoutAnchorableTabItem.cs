﻿/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CodeMask.WPF.AvalonDock.Layout;

namespace CodeMask.WPF.AvalonDock.Controls
{
    public class LayoutAnchorableTabItem : Control
    {
        /// <summary>
        ///     Model Dependency Property
        /// </summary>
        public static readonly DependencyProperty ModelProperty =
            DependencyProperty.Register("Model", typeof (LayoutContent), typeof (LayoutAnchorableTabItem),
                new FrameworkPropertyMetadata(null,
                    OnModelChanged));

        private static LayoutAnchorableTabItem _draggingItem;

        private bool _isMouseDown;

        static LayoutAnchorableTabItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (LayoutAnchorableTabItem),
                new FrameworkPropertyMetadata(typeof (LayoutAnchorableTabItem)));
        }

        internal static bool IsDraggingItem()
        {
            return _draggingItem != null;
        }

        internal static LayoutAnchorableTabItem GetDraggingItem()
        {
            return _draggingItem;
        }

        internal static void ResetDraggingItem()
        {
            _draggingItem = null;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            _isMouseDown = true;
            _draggingItem = this;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.LeftButton != MouseButtonState.Pressed)
            {
                _isMouseDown = false;
                _draggingItem = null;
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            _isMouseDown = false;

            base.OnMouseLeftButtonUp(e);

            Model.IsActive = true;
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);

            if (_isMouseDown && e.LeftButton == MouseButtonState.Pressed)
            {
                _draggingItem = this;
            }

            _isMouseDown = false;
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);

            if (_draggingItem != null &&
                _draggingItem != this &&
                e.LeftButton == MouseButtonState.Pressed)
            {
                //Trace.WriteLine("Dragging item from {0} to {1}", _draggingItem, this);

                var model = Model;
                var container = model.Parent;
                var containerPane = model.Parent as ILayoutPane;
                var childrenList = container.Children.ToList();
                containerPane.MoveChild(childrenList.IndexOf(_draggingItem.Model), childrenList.IndexOf(model));
            }
        }

        protected override void OnPreviewGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnPreviewGotKeyboardFocus(e);
        }

        #region Model

        /// <summary>
        ///     Gets or sets the Model property.  This dependency property
        ///     indicates model attached to the anchorable tab item.
        /// </summary>
        public LayoutContent Model
        {
            get { return (LayoutContent) GetValue(ModelProperty); }
            set { SetValue(ModelProperty, value); }
        }

        /// <summary>
        ///     Handles changes to the Model property.
        /// </summary>
        private static void OnModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((LayoutAnchorableTabItem) d).OnModelChanged(e);
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
            //UpdateLogicalParent();
        }

        #endregion

        #region LayoutItem

        /// <summary>
        ///     LayoutItem Read-Only Dependency Property
        /// </summary>
        private static readonly DependencyPropertyKey LayoutItemPropertyKey
            = DependencyProperty.RegisterReadOnly("LayoutItem", typeof (LayoutItem), typeof (LayoutAnchorableTabItem),
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