/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using CodeMask.WPF.AvalonDock.Layout;
using CodeMask.WPF.AvalonDock.Themes;
using Microsoft.Windows.Shell;

namespace CodeMask.WPF.AvalonDock.Controls
{
    public abstract class LayoutFloatingWindowControl : Window, ILayoutControl
    {
        private readonly ILayoutElement _model;
        private bool _attachDrag;


        private DragService _dragService;

        private HwndSource _hwndSrc;
        private HwndSourceHook _hwndSrcHook;

        private bool _internalCloseFlag;

        static LayoutFloatingWindowControl()
        {
            ContentProperty.OverrideMetadata(typeof (LayoutFloatingWindowControl),
                new FrameworkPropertyMetadata(null, null, CoerceContentValue));
            AllowsTransparencyProperty.OverrideMetadata(typeof (LayoutFloatingWindowControl),
                new FrameworkPropertyMetadata(false));
            ShowInTaskbarProperty.OverrideMetadata(typeof (LayoutFloatingWindowControl),
                new FrameworkPropertyMetadata(false));
        }

        protected LayoutFloatingWindowControl(ILayoutElement model)
        {
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
            _model = model;
            UpdateThemeResources();
        }


        protected bool CloseInitiatedByUser
        {
            get { return !_internalCloseFlag; }
        }

        internal bool KeepContentVisibleOnClose { get; set; }

        public abstract ILayoutElement Model { get; }

        private static object CoerceContentValue(DependencyObject sender, object content)
        {
            return new FloatingWindowContentHost(sender as LayoutFloatingWindowControl) {Content = content as UIElement};
        }

        internal virtual void UpdateThemeResources(Theme oldTheme = null)
        {
            if (oldTheme != null)
            {
                var resourceDictionaryToRemove =
                    Resources.MergedDictionaries.FirstOrDefault(r => r.Source == oldTheme.GetResourceUri());
                if (resourceDictionaryToRemove != null)
                    Resources.MergedDictionaries.Remove(
                        resourceDictionaryToRemove);
            }

            var manager = _model.Root.Manager;
            if (manager.Theme != null)
            {
                Resources.MergedDictionaries.Add(new ResourceDictionary {Source = manager.Theme.GetResourceUri()});
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            if (Content != null)
            {
                var host = Content as FloatingWindowContentHost;
                host.Dispose();

                if (_hwndSrc != null)
                {
                    _hwndSrc.RemoveHook(_hwndSrcHook);
                    _hwndSrc.Dispose();
                    _hwndSrc = null;
                }
            }

            base.OnClosed(e);
        }

        internal void AttachDrag(bool onActivated = true)
        {
            if (onActivated)
            {
                _attachDrag = true;
                Activated += OnActivated;
            }
            else
            {
                var windowHandle = new WindowInteropHelper(this).Handle;
                var lParam = new IntPtr(((int) Left & 0xFFFF) | (((int) Top) << 16));
                Win32Helper.SendMessage(windowHandle, Win32Helper.WM_NCLBUTTONDOWN, new IntPtr(Win32Helper.HT_CAPTION),
                    lParam);
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Loaded -= OnLoaded;

            this.SetParentToMainWindowOf(Model.Root.Manager);


            _hwndSrc = PresentationSource.FromDependencyObject(this) as HwndSource;
            _hwndSrcHook = FilterMessage;
            _hwndSrc.AddHook(_hwndSrcHook);
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            Unloaded -= OnUnloaded;

            if (_hwndSrc != null)
            {
                _hwndSrc.RemoveHook(_hwndSrcHook);
                _hwndSrc.Dispose();
                _hwndSrc = null;
            }
        }

        private void OnActivated(object sender, EventArgs e)
        {
            Activated -= OnActivated;

            if (_attachDrag && Mouse.LeftButton == MouseButtonState.Pressed)
            {
                var windowHandle = new WindowInteropHelper(this).Handle;
                var mousePosition = this.PointToScreenDPI(Mouse.GetPosition(this));
                var clientArea = Win32Helper.GetClientRect(windowHandle);
                var windowArea = Win32Helper.GetWindowRect(windowHandle);

                Left = mousePosition.X - windowArea.Width/2.0;
                Top = mousePosition.Y - (windowArea.Height - clientArea.Height)/2.0;
                _attachDrag = false;

                var lParam = new IntPtr(((int) mousePosition.X & 0xFFFF) | (((int) mousePosition.Y) << 16));
                Win32Helper.SendMessage(windowHandle, Win32Helper.WM_NCLBUTTONDOWN, new IntPtr(Win32Helper.HT_CAPTION),
                    lParam);
            }
        }


        protected override void OnInitialized(EventArgs e)
        {
            CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand,
                (s, args) => SystemCommands.CloseWindow((Window) args.Parameter)));
            CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand,
                (s, args) => SystemCommands.MaximizeWindow((Window) args.Parameter)));
            CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand,
                (s, args) => SystemCommands.MinimizeWindow((Window) args.Parameter)));
            CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand,
                (s, args) => SystemCommands.RestoreWindow((Window) args.Parameter)));
            //Debug.Assert(this.Owner != null);
            base.OnInitialized(e);
        }

        private void UpdatePositionAndSizeOfPanes()
        {
            foreach (var posElement in Model.Descendents().OfType<ILayoutElementForFloatingWindow>())
            {
                posElement.FloatingLeft = Left;
                posElement.FloatingTop = Top;
                posElement.FloatingWidth = Width;
                posElement.FloatingHeight = Height;
            }
        }

        private void UpdateMaximizedState(bool isMaximized)
        {
            foreach (var posElement in Model.Descendents().OfType<ILayoutElementForFloatingWindow>())
            {
                posElement.IsMaximized = isMaximized;
            }
        }


        protected virtual IntPtr FilterMessage(
            IntPtr hwnd,
            int msg,
            IntPtr wParam,
            IntPtr lParam,
            ref bool handled
            )
        {
            handled = false;

            switch (msg)
            {
                case Win32Helper.WM_ACTIVATE:
                    if (((int) wParam & 0xFFFF) == Win32Helper.WA_INACTIVE)
                    {
                        if (lParam == this.GetParentWindowHandle())
                        {
                            Win32Helper.SetActiveWindow(_hwndSrc.Handle);
                            handled = true;
                        }
                    }
                    break;
                case Win32Helper.WM_EXITSIZEMOVE:
                    UpdatePositionAndSizeOfPanes();

                    if (_dragService != null)
                    {
                        bool dropFlag;
                        var mousePosition = this.TransformToDeviceDPI(Win32Helper.GetMousePosition());
                        _dragService.Drop(mousePosition, out dropFlag);
                        _dragService = null;
                        SetIsDragging(false);

                        if (dropFlag)
                            InternalClose();
                    }

                    break;
                case Win32Helper.WM_MOVING:
                {
                    UpdateDragPosition();
                }
                    break;
                case Win32Helper.WM_LBUTTONUP:
                    //set as handled right button click on title area (after showing context menu)
                    if (_dragService != null && Mouse.LeftButton == MouseButtonState.Released)
                    {
                        _dragService.Abort();
                        _dragService = null;
                        SetIsDragging(false);
                    }
                    break;
                case Win32Helper.WM_SYSCOMMAND:
                    var wMaximize = new IntPtr(Win32Helper.SC_MAXIMIZE);
                    var wRestore = new IntPtr(Win32Helper.SC_RESTORE);
                    if (wParam == wMaximize || wParam == wRestore)
                    {
                        UpdateMaximizedState(wParam == wMaximize);
                    }
                    break;
            }


            return IntPtr.Zero;
        }

        private void UpdateDragPosition()
        {
            if (_dragService == null)
            {
                _dragService = new DragService(this);
                SetIsDragging(true);
            }

            var mousePosition = this.TransformToDeviceDPI(Win32Helper.GetMousePosition());
            _dragService.UpdateMouseLocation(mousePosition);
        }

        internal void InternalClose()
        {
            _internalCloseFlag = true;
            Close();
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
        }

        protected class FloatingWindowContentHost : HwndHost
        {
            private readonly LayoutFloatingWindowControl _owner;
            private DockingManager _manager;
            private Border _rootPresenter;


            private HwndSource _wpfContentHost;

            public FloatingWindowContentHost(LayoutFloatingWindowControl owner)
            {
                _owner = owner;
                var manager = _owner.Model.Root.Manager;
            }

            public Visual RootVisual
            {
                get { return _rootPresenter; }
            }

            protected override HandleRef BuildWindowCore(HandleRef hwndParent)
            {
                _wpfContentHost = new HwndSource(new HwndSourceParameters
                {
                    ParentWindow = hwndParent.Handle,
                    WindowStyle =
                        Win32Helper.WS_CHILD | Win32Helper.WS_VISIBLE | Win32Helper.WS_CLIPSIBLINGS |
                        Win32Helper.WS_CLIPCHILDREN,
                    Width = 1,
                    Height = 1
                });

                _rootPresenter = new Border {Child = new AdornerDecorator {Child = Content}, Focusable = true};
                _rootPresenter.SetBinding(Border.BackgroundProperty, new Binding("Background") {Source = _owner});
                _wpfContentHost.RootVisual = _rootPresenter;
                _wpfContentHost.SizeToContent = SizeToContent.Manual;
                _manager = _owner.Model.Root.Manager;
                _manager.InternalAddLogicalChild(_rootPresenter);

                return new HandleRef(this, _wpfContentHost.Handle);
            }


            protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
            {
                Trace.WriteLine("FloatingWindowContentHost.GotKeyboardFocus");
                base.OnGotKeyboardFocus(e);
            }

            protected override void DestroyWindowCore(HandleRef hwnd)
            {
                _manager.InternalRemoveLogicalChild(_rootPresenter);
                if (_wpfContentHost != null)
                {
                    _wpfContentHost.Dispose();
                    _wpfContentHost = null;
                }
            }

            protected override IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
            {
                switch (msg)
                {
                    case Win32Helper.WM_SETFOCUS:
                        Trace.WriteLine("FloatingWindowContentHost.WM_SETFOCUS");
                        break;
                    case Win32Helper.WM_KILLFOCUS:
                        Trace.WriteLine("FloatingWindowContentHost.WM_KILLFOCUS");
                        break;
                }
                return base.WndProc(hwnd, msg, wParam, lParam, ref handled);
            }

            protected override Size MeasureOverride(Size constraint)
            {
                if (Content == null)
                    return base.MeasureOverride(constraint);

                Content.Measure(constraint);
                return Content.DesiredSize;
            }

            #region Content

            /// <summary>
            ///     Content Dependency Property
            /// </summary>
            public static readonly DependencyProperty ContentProperty =
                DependencyProperty.Register("Content", typeof (UIElement), typeof (FloatingWindowContentHost),
                    new FrameworkPropertyMetadata(null,
                        OnContentChanged));

            /// <summary>
            ///     Gets or sets the Content property.  This dependency property
            ///     indicates ....
            /// </summary>
            public UIElement Content
            {
                get { return (UIElement) GetValue(ContentProperty); }
                set { SetValue(ContentProperty, value); }
            }

            /// <summary>
            ///     Handles changes to the Content property.
            /// </summary>
            private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FloatingWindowContentHost) d).OnContentChanged(e);
            }

            /// <summary>
            ///     Provides derived classes an opportunity to handle changes to the Content property.
            /// </summary>
            protected virtual void OnContentChanged(DependencyPropertyChangedEventArgs e)
            {
                if (_rootPresenter != null)
                    _rootPresenter.Child = Content;
            }

            #endregion
        }

        #region IsDragging

        /// <summary>
        ///     IsDragging Read-Only Dependency Property
        /// </summary>
        private static readonly DependencyPropertyKey IsDraggingPropertyKey
            = DependencyProperty.RegisterReadOnly("IsDragging", typeof (bool), typeof (LayoutFloatingWindowControl),
                new FrameworkPropertyMetadata(false,
                    OnIsDraggingChanged));

        public static readonly DependencyProperty IsDraggingProperty
            = IsDraggingPropertyKey.DependencyProperty;

        /// <summary>
        ///     Gets the IsDragging property.  This dependency property
        ///     indicates that this floating window is being dragged.
        /// </summary>
        public bool IsDragging
        {
            get { return (bool) GetValue(IsDraggingProperty); }
        }

        /// <summary>
        ///     Provides a secure method for setting the IsDragging property.
        ///     This dependency property indicates that this floating window is being dragged.
        /// </summary>
        /// <param name="value">The new value for the property.</param>
        protected void SetIsDragging(bool value)
        {
            SetValue(IsDraggingPropertyKey, value);
        }

        /// <summary>
        ///     Handles changes to the IsDragging property.
        /// </summary>
        private static void OnIsDraggingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((LayoutFloatingWindowControl) d).OnIsDraggingChanged(e);
        }

        /// <summary>
        ///     Provides derived classes an opportunity to handle changes to the IsDragging property.
        /// </summary>
        protected virtual void OnIsDraggingChanged(DependencyPropertyChangedEventArgs e)
        {
            //Trace.WriteLine("IsDragging={0}", e.NewValue);
        }

        #endregion

        #region IsMaximized

        /// <summary>
        ///     IsMaximized Read-Only Dependency Property
        /// </summary>
        private static readonly DependencyPropertyKey IsMaximizedPropertyKey
            = DependencyProperty.RegisterReadOnly("IsMaximized", typeof (bool), typeof (LayoutFloatingWindowControl),
                new FrameworkPropertyMetadata(false));

        public static readonly DependencyProperty IsMaximizedProperty
            = IsMaximizedPropertyKey.DependencyProperty;

        /// <summary>
        ///     Gets the IsMaximized property.  This dependency property
        ///     indicates if the window is maximized.
        /// </summary>
        public bool IsMaximized
        {
            get { return (bool) GetValue(IsMaximizedProperty); }
        }

        /// <summary>
        ///     Provides a secure method for setting the IsMaximized property.
        ///     This dependency property indicates if the window is maximized.
        /// </summary>
        /// <param name="value">The new value for the property.</param>
        protected void SetIsMaximized(bool value)
        {
            SetValue(IsMaximizedPropertyKey, value);
        }

        protected override void OnStateChanged(EventArgs e)
        {
            SetIsMaximized(WindowState == WindowState.Maximized);
            base.OnStateChanged(e);
        }

        #endregion
    }
}