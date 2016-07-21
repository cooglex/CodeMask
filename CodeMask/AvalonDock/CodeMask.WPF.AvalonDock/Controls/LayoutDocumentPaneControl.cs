/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using CodeMask.WPF.AvalonDock.Layout;

namespace CodeMask.WPF.AvalonDock.Controls
{
    public class LayoutDocumentPaneControl : TabControl, ILayoutControl //, ILogicalChildrenContainer
    {
        private readonly List<object> _logicalChildren = new List<object>();

        private readonly LayoutDocumentPane _model;

        static LayoutDocumentPaneControl()
        {
            FocusableProperty.OverrideMetadata(typeof (LayoutDocumentPaneControl), new FrameworkPropertyMetadata(false));
        }


        internal LayoutDocumentPaneControl(LayoutDocumentPane model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            _model = model;
            SetBinding(ItemsSourceProperty, new Binding("Model.Children") {Source = this});
            SetBinding(FlowDirectionProperty, new Binding("Model.Root.Manager.FlowDirection") {Source = this});

            LayoutUpdated += OnLayoutUpdated;
        }

        protected override IEnumerator LogicalChildren
        {
            get { return _logicalChildren.GetEnumerator(); }
        }

        public ILayoutElement Model
        {
            get { return _model; }
        }

        private void OnLayoutUpdated(object sender, EventArgs e)
        {
            var modelWithAtcualSize = _model as ILayoutPositionableElementWithActualSize;
            modelWithAtcualSize.ActualWidth = ActualWidth;
            modelWithAtcualSize.ActualHeight = ActualHeight;
        }

        protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnGotKeyboardFocus(e);
            Trace.WriteLine(string.Format("OnGotKeyboardFocus({0}, {1})", e.Source, e.NewFocus));


            //if (_model.SelectedContent != null)
            //    _model.SelectedContent.IsActive = true;
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);

            if (_model.SelectedContent != null)
                _model.SelectedContent.IsActive = true;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            if (!e.Handled && _model.SelectedContent != null)
                _model.SelectedContent.IsActive = true;
        }

        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseRightButtonDown(e);

            if (!e.Handled && _model.SelectedContent != null)
                _model.SelectedContent.IsActive = true;
        }
    }
}