/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using CodeMask.WPF.AvalonDock.Layout;

namespace CodeMask.WPF.AvalonDock.Controls
{
    public class LayoutAnchorablePaneControl : TabControl, ILayoutControl //, ILogicalChildrenContainer
    {
        private readonly LayoutAnchorablePane _model;

        static LayoutAnchorablePaneControl()
        {
            FocusableProperty.OverrideMetadata(typeof (LayoutAnchorablePaneControl),
                new FrameworkPropertyMetadata(false));
        }

        public LayoutAnchorablePaneControl(LayoutAnchorablePane model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            _model = model;

            SetBinding(ItemsSourceProperty, new Binding("Model.Children") {Source = this});
            SetBinding(FlowDirectionProperty, new Binding("Model.Root.Manager.FlowDirection") {Source = this});

            LayoutUpdated += OnLayoutUpdated;
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
            _model.SelectedContent.IsActive = true;

            base.OnGotKeyboardFocus(e);
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