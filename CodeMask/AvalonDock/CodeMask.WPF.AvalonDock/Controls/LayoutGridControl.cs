/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using CodeMask.WPF.AvalonDock.Layout;

namespace CodeMask.WPF.AvalonDock.Controls
{
    public abstract class LayoutGridControl<T> : Grid, ILayoutControl where T : class, ILayoutPanelElement
    {
        private readonly ReentrantFlag _fixingChildrenDockLengths = new ReentrantFlag();

        private readonly LayoutPositionableGroup<T> _model;
        private ChildrenTreeChange? _asyncRefreshCalled;

        private bool _initialized;

        private Orientation _orientation;

        internal LayoutGridControl(LayoutPositionableGroup<T> model, Orientation orientation)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            _model = model;
            _orientation = orientation;

            FlowDirection = FlowDirection.LeftToRight;
        }

        public Orientation Orientation
        {
            get { return (_model as ILayoutOrientableGroup).Orientation; }
        }

        private bool AsyncRefreshCalled
        {
            get { return _asyncRefreshCalled != null; }
        }

        public ILayoutElement Model
        {
            get { return _model; }
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            _model.ChildrenTreeChanged += (s, args) =>
            {
                if (_asyncRefreshCalled.HasValue &&
                    _asyncRefreshCalled.Value == args.Change)
                    return;
                _asyncRefreshCalled = args.Change;
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    _asyncRefreshCalled = null;
                    UpdateChildren();
                }), DispatcherPriority.Normal, null);
            };

            LayoutUpdated += OnLayoutUpdated;
        }

        private void OnLayoutUpdated(object sender, EventArgs e)
        {
            var modelWithAtcualSize = _model as ILayoutPositionableElementWithActualSize;
            modelWithAtcualSize.ActualWidth = ActualWidth;
            modelWithAtcualSize.ActualHeight = ActualHeight;

            if (!_initialized)
            {
                _initialized = true;
                UpdateChildren();
            }
        }

        private void UpdateChildren()
        {
            var alreadyContainedChildren = Children.OfType<ILayoutControl>().ToArray();

            DetachOldSplitters();
            DetachPropertChangeHandler();

            Children.Clear();
            ColumnDefinitions.Clear();
            RowDefinitions.Clear();

            if (_model == null ||
                _model.Root == null)
                return;

            var manager = _model.Root.Manager;
            if (manager == null)
                return;


            foreach (ILayoutElement child in _model.Children)
            {
                var foundContainedChild = alreadyContainedChildren.FirstOrDefault(chVM => chVM.Model == child);
                if (foundContainedChild != null)
                    Children.Add(foundContainedChild as UIElement);
                else
                    Children.Add(manager.CreateUIElementForModel(child));
            }

            CreateSplitters();

            UpdateRowColDefinitions();

            AttachNewSplitters();
            AttachPropertyChangeHandler();
        }

        private void AttachPropertyChangeHandler()
        {
            foreach (var child in InternalChildren.OfType<ILayoutControl>())
            {
                child.Model.PropertyChanged += OnChildModelPropertyChanged;
            }
        }

        private void DetachPropertChangeHandler()
        {
            foreach (var child in InternalChildren.OfType<ILayoutControl>())
            {
                child.Model.PropertyChanged -= OnChildModelPropertyChanged;
            }
        }

        private void OnChildModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (AsyncRefreshCalled)
                return;

            if (_fixingChildrenDockLengths.CanEnter && e.PropertyName == "DockWidth" &&
                Orientation == Orientation.Horizontal)
            {
                if (ColumnDefinitions.Count == InternalChildren.Count)
                {
                    var changedElement = sender as ILayoutPositionableElement;
                    var childFromModel =
                        InternalChildren.OfType<ILayoutControl>().First(ch => ch.Model == changedElement) as UIElement;
                    var indexOfChild = InternalChildren.IndexOf(childFromModel);
                    ColumnDefinitions[indexOfChild].Width = changedElement.DockWidth;
                }
            }
            else if (_fixingChildrenDockLengths.CanEnter && e.PropertyName == "DockHeight" &&
                     Orientation == Orientation.Vertical)
            {
                if (RowDefinitions.Count == InternalChildren.Count)
                {
                    var changedElement = sender as ILayoutPositionableElement;
                    var childFromModel =
                        InternalChildren.OfType<ILayoutControl>().First(ch => ch.Model == changedElement) as
                            UIElement;
                    var indexOfChild = InternalChildren.IndexOf(childFromModel);
                    RowDefinitions[indexOfChild].Height = changedElement.DockHeight;
                }
            }
            else if (e.PropertyName == "IsVisible")
            {
                UpdateRowColDefinitions();
            }
        }


        private void UpdateRowColDefinitions()
        {
            var root = _model.Root;
            if (root == null)
                return;
            var manager = root.Manager;
            if (manager == null)
                return;

            FixChildrenDockLengths();

            //Debug.Assert(InternalChildren.Count == _model.ChildrenCount + (_model.ChildrenCount - 1));

            #region Setup GridRows/Cols

            RowDefinitions.Clear();
            ColumnDefinitions.Clear();
            if (Orientation == Orientation.Horizontal)
            {
                var iColumn = 0;
                var iChild = 0;
                for (var iChildModel = 0; iChildModel < _model.Children.Count; iChildModel++, iColumn++, iChild++)
                {
                    var childModel = _model.Children[iChildModel] as ILayoutPositionableElement;
                    ColumnDefinitions.Add(new ColumnDefinition
                    {
                        Width = childModel.IsVisible ? childModel.DockWidth : new GridLength(0.0, GridUnitType.Pixel),
                        MinWidth = childModel.IsVisible ? childModel.DockMinWidth : 0.0
                    });
                    SetColumn(InternalChildren[iChild], iColumn);

                    //append column for splitter
                    if (iChild < InternalChildren.Count - 1)
                    {
                        iChild++;
                        iColumn++;

                        var nextChildModelVisibleExist = false;
                        for (var i = iChildModel + 1; i < _model.Children.Count; i++)
                        {
                            var nextChildModel = _model.Children[i] as ILayoutPositionableElement;
                            if (nextChildModel.IsVisible)
                            {
                                nextChildModelVisibleExist = true;
                                break;
                            }
                        }

                        ColumnDefinitions.Add(new ColumnDefinition
                        {
                            Width =
                                childModel.IsVisible && nextChildModelVisibleExist
                                    ? new GridLength(manager.GridSplitterWidth)
                                    : new GridLength(0.0, GridUnitType.Pixel)
                        });
                        SetColumn(InternalChildren[iChild], iColumn);
                    }
                }
            }
            else //if (_model.Orientation == Orientation.Vertical)
            {
                var iRow = 0;
                var iChild = 0;
                for (var iChildModel = 0; iChildModel < _model.Children.Count; iChildModel++, iRow++, iChild++)
                {
                    var childModel = _model.Children[iChildModel] as ILayoutPositionableElement;
                    RowDefinitions.Add(new RowDefinition
                    {
                        Height = childModel.IsVisible ? childModel.DockHeight : new GridLength(0.0, GridUnitType.Pixel),
                        MinHeight = childModel.IsVisible ? childModel.DockMinHeight : 0.0
                    });
                    SetRow(InternalChildren[iChild], iRow);

                    //if (RowDefinitions.Last().Height.Value == 0.0)
                    //    System.Diagnostics.Debugger.Break();

                    //append row for splitter (if necessary)
                    if (iChild < InternalChildren.Count - 1)
                    {
                        iChild++;
                        iRow++;

                        var nextChildModelVisibleExist = false;
                        for (var i = iChildModel + 1; i < _model.Children.Count; i++)
                        {
                            var nextChildModel = _model.Children[i] as ILayoutPositionableElement;
                            if (nextChildModel.IsVisible)
                            {
                                nextChildModelVisibleExist = true;
                                break;
                            }
                        }

                        RowDefinitions.Add(new RowDefinition
                        {
                            Height =
                                childModel.IsVisible && nextChildModelVisibleExist
                                    ? new GridLength(manager.GridSplitterHeight)
                                    : new GridLength(0.0, GridUnitType.Pixel)
                        });
                        //if (RowDefinitions.Last().Height.Value == 0.0)
                        //    System.Diagnostics.Debugger.Break();
                        SetRow(InternalChildren[iChild], iRow);
                    }
                }
            }

            #endregion
        }

        protected void FixChildrenDockLengths()
        {
            using (_fixingChildrenDockLengths.Enter())
                OnFixChildrenDockLengths();
        }

        protected abstract void OnFixChildrenDockLengths();

        #region Splitters

        private void CreateSplitters()
        {
            for (var iChild = 1; iChild < Children.Count; iChild++)
            {
                var splitter = new LayoutGridResizerControl();
                splitter.Cursor = Orientation == Orientation.Horizontal ? Cursors.SizeWE : Cursors.SizeNS;
                Children.Insert(iChild, splitter);
                iChild++;
            }
        }

        private void DetachOldSplitters()
        {
            foreach (var splitter in Children.OfType<LayoutGridResizerControl>())
            {
                splitter.DragStarted -= OnSplitterDragStarted;
                splitter.DragDelta -= OnSplitterDragDelta;
                splitter.DragCompleted -= OnSplitterDragCompleted;
            }
        }

        private void AttachNewSplitters()
        {
            foreach (var splitter in Children.OfType<LayoutGridResizerControl>())
            {
                splitter.DragStarted += OnSplitterDragStarted;
                splitter.DragDelta += OnSplitterDragDelta;
                splitter.DragCompleted += OnSplitterDragCompleted;
            }
        }

        private void OnSplitterDragStarted(object sender, DragStartedEventArgs e)
        {
            var resizer = sender as LayoutGridResizerControl;
            ShowResizerOverlayWindow(resizer);
        }

        private void OnSplitterDragDelta(object sender, DragDeltaEventArgs e)
        {
            var splitter = sender as LayoutGridResizerControl;
            var rootVisual = this.FindVisualTreeRoot() as Visual;

            var trToWnd = TransformToAncestor(rootVisual);
            var transformedDelta = trToWnd.Transform(new Point(e.HorizontalChange, e.VerticalChange)) -
                                   trToWnd.Transform(new Point());

            if (Orientation == Orientation.Horizontal)
            {
                Canvas.SetLeft(_resizerGhost,
                    MathHelper.MinMax(_initialStartPoint.X + transformedDelta.X, 0.0,
                        _resizerWindowHost.Width - _resizerGhost.Width));
            }
            else
            {
                Canvas.SetTop(_resizerGhost,
                    MathHelper.MinMax(_initialStartPoint.Y + transformedDelta.Y, 0.0,
                        _resizerWindowHost.Height - _resizerGhost.Height));
            }
        }

        private void OnSplitterDragCompleted(object sender, DragCompletedEventArgs e)
        {
            var splitter = sender as LayoutGridResizerControl;
            var rootVisual = this.FindVisualTreeRoot() as Visual;

            var trToWnd = TransformToAncestor(rootVisual);
            var transformedDelta = trToWnd.Transform(new Point(e.HorizontalChange, e.VerticalChange)) -
                                   trToWnd.Transform(new Point());

            double delta;
            if (Orientation == Orientation.Horizontal)
                delta = Canvas.GetLeft(_resizerGhost) - _initialStartPoint.X;
            else
                delta = Canvas.GetTop(_resizerGhost) - _initialStartPoint.Y;

            var indexOfResizer = InternalChildren.IndexOf(splitter);

            var prevChild = InternalChildren[indexOfResizer - 1] as FrameworkElement;
            var nextChild = GetNextVisibleChild(indexOfResizer);

            var prevChildActualSize = prevChild.TransformActualSizeToAncestor();
            var nextChildActualSize = nextChild.TransformActualSizeToAncestor();

            var prevChildModel = (ILayoutPositionableElement) (prevChild as ILayoutControl).Model;
            var nextChildModel = (ILayoutPositionableElement) (nextChild as ILayoutControl).Model;

            if (Orientation == Orientation.Horizontal)
            {
                //Trace.WriteLine(string.Format("PrevChild From {0}", prevChildModel.DockWidth));
                if (prevChildModel.DockWidth.IsStar)
                {
                    prevChildModel.DockWidth =
                        new GridLength(
                            prevChildModel.DockWidth.Value*(prevChildActualSize.Width + delta)/prevChildActualSize.Width,
                            GridUnitType.Star);
                }
                else
                {
                    prevChildModel.DockWidth = new GridLength(prevChildModel.DockWidth.Value + delta, GridUnitType.Pixel);
                }
                //Trace.WriteLine(string.Format("PrevChild To {0}", prevChildModel.DockWidth));

                //Trace.WriteLine(string.Format("NextChild From {0}", nextChildModel.DockWidth));
                if (nextChildModel.DockWidth.IsStar)
                {
                    nextChildModel.DockWidth =
                        new GridLength(
                            nextChildModel.DockWidth.Value*(nextChildActualSize.Width - delta)/nextChildActualSize.Width,
                            GridUnitType.Star);
                }
                else
                {
                    nextChildModel.DockWidth = new GridLength(nextChildModel.DockWidth.Value - delta, GridUnitType.Pixel);
                }
                //Trace.WriteLine(string.Format("NextChild To {0}", nextChildModel.DockWidth));
            }
            else
            {
                //Trace.WriteLine(string.Format("PrevChild From {0}", prevChildModel.DockHeight));
                if (prevChildModel.DockHeight.IsStar)
                {
                    prevChildModel.DockHeight =
                        new GridLength(
                            prevChildModel.DockHeight.Value*(prevChildActualSize.Height + delta)/
                            prevChildActualSize.Height, GridUnitType.Star);
                }
                else
                {
                    prevChildModel.DockHeight = new GridLength(prevChildModel.DockHeight.Value + delta,
                        GridUnitType.Pixel);
                }
                //Trace.WriteLine(string.Format("PrevChild To {0}", prevChildModel.DockHeight));

                //Trace.WriteLine(string.Format("NextChild From {0}", nextChildModel.DockHeight));
                if (nextChildModel.DockHeight.IsStar)
                {
                    nextChildModel.DockHeight =
                        new GridLength(
                            nextChildModel.DockHeight.Value*(nextChildActualSize.Height - delta)/
                            nextChildActualSize.Height, GridUnitType.Star);
                }
                else
                {
                    nextChildModel.DockHeight = new GridLength(nextChildModel.DockHeight.Value - delta,
                        GridUnitType.Pixel);
                }
                //Trace.WriteLine(string.Format("NextChild To {0}", nextChildModel.DockHeight));
            }

            HideResizerOverlayWindow();
        }

        private Border _resizerGhost;
        private Window _resizerWindowHost;
        private Vector _initialStartPoint;

        private FrameworkElement GetNextVisibleChild(int index)
        {
            for (var i = index + 1; i < InternalChildren.Count; i++)
            {
                if (InternalChildren[i] is LayoutGridResizerControl)
                    continue;

                if (Orientation == Orientation.Horizontal)
                {
                    if (ColumnDefinitions[i].Width.IsStar || ColumnDefinitions[i].Width.Value > 0)
                        return InternalChildren[i] as FrameworkElement;
                }
                else
                {
                    if (RowDefinitions[i].Height.IsStar || RowDefinitions[i].Height.Value > 0)
                        return InternalChildren[i] as FrameworkElement;
                }
            }

            return null;
        }

        private void ShowResizerOverlayWindow(LayoutGridResizerControl splitter)
        {
            _resizerGhost = new Border
            {
                Background = splitter.BackgroundWhileDragging,
                Opacity = splitter.OpacityWhileDragging
            };

            var indexOfResizer = InternalChildren.IndexOf(splitter);

            var prevChild = InternalChildren[indexOfResizer - 1] as FrameworkElement;
            var nextChild = GetNextVisibleChild(indexOfResizer);

            var prevChildActualSize = prevChild.TransformActualSizeToAncestor();
            var nextChildActualSize = nextChild.TransformActualSizeToAncestor();

            var prevChildModel = (ILayoutPositionableElement) (prevChild as ILayoutControl).Model;
            var nextChildModel = (ILayoutPositionableElement) (nextChild as ILayoutControl).Model;

            var ptTopLeftScreen = prevChild.PointToScreenDPIWithoutFlowDirection(new Point());

            Size actualSize;

            if (Orientation == Orientation.Horizontal)
            {
                actualSize = new Size(
                    prevChildActualSize.Width - prevChildModel.DockMinWidth + splitter.ActualWidth +
                    nextChildActualSize.Width - nextChildModel.DockMinWidth,
                    nextChildActualSize.Height);

                _resizerGhost.Width = splitter.ActualWidth;
                _resizerGhost.Height = actualSize.Height;
                ptTopLeftScreen.Offset(prevChildModel.DockMinWidth, 0.0);
            }
            else
            {
                actualSize = new Size(
                    prevChildActualSize.Width,
                    prevChildActualSize.Height - prevChildModel.DockMinHeight + splitter.ActualHeight +
                    nextChildActualSize.Height - nextChildModel.DockMinHeight);

                _resizerGhost.Height = splitter.ActualHeight;
                _resizerGhost.Width = actualSize.Width;

                ptTopLeftScreen.Offset(0.0, prevChildModel.DockMinHeight);
            }

            _initialStartPoint = splitter.PointToScreenDPIWithoutFlowDirection(new Point()) - ptTopLeftScreen;

            if (Orientation == Orientation.Horizontal)
            {
                Canvas.SetLeft(_resizerGhost, _initialStartPoint.X);
            }
            else
            {
                Canvas.SetTop(_resizerGhost, _initialStartPoint.Y);
            }

            var panelHostResizer = new Canvas
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch
            };

            panelHostResizer.Children.Add(_resizerGhost);


            _resizerWindowHost = new Window
            {
                SizeToContent = SizeToContent.Manual,
                ResizeMode = ResizeMode.NoResize,
                WindowStyle = WindowStyle.None,
                ShowInTaskbar = false,
                AllowsTransparency = true,
                Background = null,
                Width = actualSize.Width,
                Height = actualSize.Height,
                Left = ptTopLeftScreen.X,
                Top = ptTopLeftScreen.Y,
                ShowActivated = false,
                //Owner = Window.GetWindow(this),
                Content = panelHostResizer
            };
            _resizerWindowHost.Loaded += (s, e) => { _resizerWindowHost.SetParentToMainWindowOf(this); };
            _resizerWindowHost.Show();
        }

        private void HideResizerOverlayWindow()
        {
            if (_resizerWindowHost != null)
            {
                _resizerWindowHost.Close();
                _resizerWindowHost = null;
            }
        }

        #endregion
    }
}