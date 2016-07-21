﻿/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CodeMask.WPF.AvalonDock.Layout;

namespace CodeMask.WPF.AvalonDock.Controls
{
    public class LayoutDocumentTabItem : Control
    {
        private bool _isMouseDown;
        private Point _mouseDownPoint;
        private List<TabItem> _otherTabs;

        private List<Rect> _otherTabsScreenArea;
        private DocumentPaneTabPanel _parentDocumentTabPanel;
        private Rect _parentDocumentTabPanelScreenArea;

        static LayoutDocumentTabItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (LayoutDocumentTabItem),
                new FrameworkPropertyMetadata(typeof (LayoutDocumentTabItem)));
        }

        private void UpdateDragDetails()
        {
            _parentDocumentTabPanel = this.FindLogicalAncestor<DocumentPaneTabPanel>();
            _parentDocumentTabPanelScreenArea = _parentDocumentTabPanel.GetScreenArea();
            _otherTabs = _parentDocumentTabPanel.Children.Cast<TabItem>().Where(ch =>
                ch.Visibility != Visibility.Collapsed).ToList();
            var currentTabScreenArea = this.FindLogicalAncestor<TabItem>().GetScreenArea();
            _otherTabsScreenArea = _otherTabs.Select(ti =>
            {
                var screenArea = ti.GetScreenArea();
                return new Rect(screenArea.Left, screenArea.Top, currentTabScreenArea.Width, screenArea.Height);
            }).ToList();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            Model.IsActive = true;

            if (e.ClickCount == 1)
            {
                _mouseDownPoint = e.GetPosition(this);
                _isMouseDown = true;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (_isMouseDown)
            {
                var ptMouseMove = e.GetPosition(this);

                if (Math.Abs(ptMouseMove.X - _mouseDownPoint.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(ptMouseMove.Y - _mouseDownPoint.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    UpdateDragDetails();
                    CaptureMouse();
                    _isMouseDown = false;
                }
            }

            if (IsMouseCaptured)
            {
                var mousePosInScreenCoord = this.PointToScreenDPI(e.GetPosition(this));
                if (!_parentDocumentTabPanelScreenArea.Contains(mousePosInScreenCoord))
                {
                    ReleaseMouseCapture();
                    var manager = Model.Root.Manager;
                    manager.StartDraggingFloatingWindowForContent(Model);
                }
                else
                {
                    var indexOfTabItemWithMouseOver =
                        _otherTabsScreenArea.FindIndex(r => r.Contains(mousePosInScreenCoord));
                    if (indexOfTabItemWithMouseOver >= 0)
                    {
                        var targetModel = _otherTabs[indexOfTabItemWithMouseOver].Content as LayoutContent;
                        var container = Model.Parent;
                        var containerPane = Model.Parent as ILayoutPane;
                        var childrenList = container.Children.ToList();
                        containerPane.MoveChild(childrenList.IndexOf(Model), childrenList.IndexOf(targetModel));
                        Model.IsActive = true;
                        _parentDocumentTabPanel.UpdateLayout();
                        UpdateDragDetails();
                    }
                }
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (IsMouseCaptured)
                ReleaseMouseCapture();
            _isMouseDown = false;

            base.OnMouseLeftButtonUp(e);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            _isMouseDown = false;
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            _isMouseDown = false;
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle)
            {
                if (LayoutItem.CloseCommand.CanExecute(null))
                    LayoutItem.CloseCommand.Execute(null);
            }

            base.OnMouseDown(e);
        }

        #region Model

        /// <summary>
        ///     Model Dependency Property
        /// </summary>
        public static readonly DependencyProperty ModelProperty =
            DependencyProperty.Register("Model", typeof (LayoutContent), typeof (LayoutDocumentTabItem),
                new FrameworkPropertyMetadata(null,
                    OnModelChanged));

        /// <summary>
        ///     Gets or sets the Model property.  This dependency property
        ///     indicates the layout content model attached to the tab item.
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
            ((LayoutDocumentTabItem) d).OnModelChanged(e);
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
            = DependencyProperty.RegisterReadOnly("LayoutItem", typeof (LayoutItem), typeof (LayoutDocumentTabItem),
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