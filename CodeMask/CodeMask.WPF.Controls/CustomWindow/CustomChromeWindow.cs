using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using NativeMethodsPack;

namespace CodeMask.WPF.Controls.CustomWindow
{
    /// <summary>
    /// Class CustomChromeWindow
    /// </summary>
    public class CustomChromeWindow : System.Windows.Window
    {
        #region Var

        /// <summary>
        /// The minimize animation duration milliseconds
        /// </summary>
        private const int MinimizeAnimationDurationMilliseconds = 200;

        /// <summary>
        /// The corner radius property
        /// </summary>
        public readonly static DependencyProperty CornerRadiusProperty;

        /// <summary>
        /// The last window placement
        /// </summary>
        private int lastWindowPlacement;

        /// <summary>
        /// The _defer glow changes count
        /// </summary>
        private int _deferGlowChangesCount;

        /// <summary>
        /// The _is glow visible
        /// </summary>
        private bool _isGlowVisible;

        /// <summary>
        /// The _make glow visible timer
        /// </summary>
        private DispatcherTimer _makeGlowVisibleTimer;

        /// <summary>
        /// The _is non client strip visible
        /// </summary>
        private bool _isNonClientStripVisible;

        /// <summary>
        /// The _glow windows
        /// </summary>
        private readonly GlowWindow[] _glowWindows = new GlowWindow[4];

        /// <summary>
        /// The active glow color property
        /// </summary>
        public readonly static DependencyProperty ActiveGlowColorProperty;

        /// <summary>
        /// The inactive glow color property
        /// </summary>
        public readonly static DependencyProperty InactiveGlowColorProperty;

        /// <summary>
        /// The non client fill color property
        /// </summary>
        public readonly static DependencyProperty NonClientFillColorProperty;

        /// <summary>
        /// The toggle maximize restore window
        /// </summary>
        public static RoutedCommand ToggleMaximizeRestoreWindow;

        /// <summary>
        /// The minimize window
        /// </summary>
        public static RoutedCommand MinimizeWindow;

        /// <summary>
        /// The close window
        /// </summary>
        public static RoutedCommand CloseWindow;

        /// <summary>
        /// Gets the close image.
        /// </summary>
        /// <value>The close image.</value>
        public static Image CloseImage
        {
            get
            {
                return GetImage("Resources/CloseImage.png");
            }
        }

        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <returns>Image.</returns>
        private static Image GetImage(string str)
        {
            BitmapImage source = new BitmapImage(CustomChromeWindow.MakePackUri(typeof(CustomChromeWindow).Assembly, str));
            Image img = new Image() { Source = source, SnapsToDevicePixels = true };
            img.Width = source.Width;
            img.Height = source.Height;
            return img;
        }

        /// <summary>
        /// Gets the minimize image.
        /// </summary>
        /// <value>The minimize image.</value>
        public static Image MinimizeImage
        {
            get
            {
                return GetImage("Resources/MinimizeImage.png");
            }
        }

        /// <summary>
        /// Gets the maximize image.
        /// </summary>
        /// <value>The maximize image.</value>
        public static Image MaximizeImage
        {
            get
            {
                return GetImage("Resources/MaximizeImage.png");
            }
        }

        /// <summary>
        /// Gets the maximize restore image.
        /// </summary>
        /// <value>The maximize restore image.</value>
        public static Image MaximizeRestoreImage
        {
            get
            {
                return GetImage("Resources/MaximizeRestoreImage.png");
            }
        }

        #endregion Var

        #region Properties

        /// <summary>
        /// Gets or sets the color of the active glow.
        /// </summary>
        /// <value>The color of the active glow.</value>
        public Color ActiveGlowColor
        {
            get
            {
                return (Color)GetValue(CustomChromeWindow.ActiveGlowColorProperty);
            }
            set
            {
                SetValue(CustomChromeWindow.ActiveGlowColorProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the corner radius.
        /// </summary>
        /// <value>The corner radius.</value>
        public int CornerRadius
        {
            get
            {
                return (int)GetValue(CustomChromeWindow.CornerRadiusProperty);
            }
            set
            {
                SetValue(CustomChromeWindow.CornerRadiusProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the inactive glow.
        /// </summary>
        /// <value>The color of the inactive glow.</value>
        public Color InactiveGlowColor
        {
            get
            {
                return (Color)GetValue(CustomChromeWindow.InactiveGlowColorProperty);
            }
            set
            {
                SetValue(CustomChromeWindow.InactiveGlowColorProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is glow visible.
        /// </summary>
        /// <value><c>true</c> if this instance is glow visible; otherwise, <c>false</c>.</value>
        private bool IsGlowVisible
        {
            get
            {
                return this._isGlowVisible;
            }
            set
            {
                if (this._isGlowVisible != value)
                {
                    this._isGlowVisible = value;
                    for (int i = 0; i < (int)this._glowWindows.Length; i++)
                    {
                        this.GetOrCreateGlowWindow(i).IsVisible = value;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the loaded glow windows.
        /// </summary>
        /// <value>The loaded glow windows.</value>
        private IEnumerable<GlowWindow> LoadedGlowWindows
        {
            get
            {
                //GlowWindow[] glowWindowArray = this._glowWindows;
                //return (IEnumerable<GlowWindow>)glowWindowArray.Where<GlowWindow>((GlowWindow w) => w != null);
                return (from w in this._glowWindows
                        where w != null
                        select w);
            }
        }

        /// <summary>
        /// Gets or sets the color of the non client fill.
        /// </summary>
        /// <value>The color of the non client fill.</value>
        public Color NonClientFillColor
        {
            get
            {
                return (Color)GetValue(CustomChromeWindow.NonClientFillColorProperty);
            }
            set
            {
                SetValue(CustomChromeWindow.NonClientFillColorProperty, value);
            }
        }

        /// <summary>
        /// Gets the pressed mouse buttons.
        /// </summary>
        /// <value>The pressed mouse buttons.</value>
        private static int PressedMouseButtons
        {
            get
            {
                int num = 0;
                if (NativeMethodsPack.NativeMethods.IsKeyPressed(VIRTUALKEY.VK_LBUTTON))
                {
                    num = num | 1;
                }
                if (NativeMethodsPack.NativeMethods.IsKeyPressed(VIRTUALKEY.VK_RBUTTON))
                {
                    num = num | 2;
                }
                if (NativeMethodsPack.NativeMethods.IsKeyPressed(VIRTUALKEY.VK_MBUTTON))
                {
                    num = num | 16;
                }
                if (NativeMethodsPack.NativeMethods.IsKeyPressed(VIRTUALKEY.VK_XBUTTON1))
                {
                    num = num | 32;
                }
                if (NativeMethodsPack.NativeMethods.IsKeyPressed(VIRTUALKEY.VK_XBUTTON2))
                {
                    num = num | 64;
                }
                return num;
            }
        }

        /// <summary>
        /// Gets a value indicating whether [should show glow].
        /// </summary>
        /// <value><c>true</c> if [should show glow]; otherwise, <c>false</c>.</value>
        protected virtual bool ShouldShowGlow
        {
            get
            {
                IntPtr handle = (new WindowInteropHelper(this)).Handle;
                if (!NativeMethodsPack.NativeMethods.IsWindowVisible(handle) || NativeMethodsPack.NativeMethods.IsIconic(handle) || NativeMethodsPack.NativeMethods.IsZoomed(handle))
                {
                    return false;
                }
                else
                {
                    return ResizeMode != ResizeMode.NoResize;
                }
            }
        }

        #endregion Properties

        #region Construtor

        /// <summary>
        /// Initializes static members of the <see cref="CustomChromeWindow"/> class.
        /// </summary>
        static CustomChromeWindow()
        {
            CustomChromeWindow.CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(int), typeof(CustomChromeWindow), new FrameworkPropertyMetadata(0, new PropertyChangedCallback(CustomChromeWindow.OnCornerRadiusChanged)));
            CustomChromeWindow.ActiveGlowColorProperty = DependencyProperty.Register("ActiveGlowColor", typeof(Color), typeof(CustomChromeWindow), new FrameworkPropertyMetadata(Colors.Transparent, new PropertyChangedCallback(CustomChromeWindow.OnGlowColorChanged)));
            CustomChromeWindow.InactiveGlowColorProperty = DependencyProperty.Register("InactiveGlowColor", typeof(Color), typeof(CustomChromeWindow), new FrameworkPropertyMetadata(Colors.Transparent, new PropertyChangedCallback(CustomChromeWindow.OnGlowColorChanged)));
            CustomChromeWindow.NonClientFillColorProperty = DependencyProperty.Register("NonClientFillColor", typeof(Color), typeof(CustomChromeWindow), new FrameworkPropertyMetadata(Colors.Black));
            System.Windows.Window.ResizeModeProperty.OverrideMetadata(typeof(CustomChromeWindow), new FrameworkPropertyMetadata(new PropertyChangedCallback(CustomChromeWindow.OnResizeModeChanged)));

            CustomChromeWindow.ToggleMaximizeRestoreWindow = new RoutedCommand("ToggleMaximizeRestoreWindow", typeof(CustomChromeWindow));
            CustomChromeWindow.MinimizeWindow = new RoutedCommand("MinimizeWindow", typeof(CustomChromeWindow));
            CustomChromeWindow.CloseWindow = new RoutedCommand("CloseWindow", typeof(CustomChromeWindow));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomChromeWindow"/> class.
        /// </summary>
        public CustomChromeWindow()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            CommandManager.RegisterClassCommandBinding(typeof(UIElement), new CommandBinding(CustomChromeWindow.ToggleMaximizeRestoreWindow, OnToggleMaximizeRestoreWindow));
            CommandManager.RegisterClassCommandBinding(typeof(UIElement), new CommandBinding(CustomChromeWindow.MinimizeWindow, OnMinimizeWindow));
            CommandManager.RegisterClassCommandBinding(typeof(UIElement), new CommandBinding(CustomChromeWindow.CloseWindow, OnCloseWindow));
        }

        #endregion Construtor

        #region SystemBtnCommands

        #region ToggleMaximizeRestoreWindow

        /// <summary>
        /// Determines whether this instance [can toggle maximize restore window] the specified args.
        /// </summary>
        /// <param name="args">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        /// <returns><c>true</c> if this instance [can toggle maximize restore window] the specified args; otherwise, <c>false</c>.</returns>
        private bool CanToggleMaximizeRestoreWindow(ExecutedRoutedEventArgs args)
        {
            return args.Parameter is System.Windows.Window;
        }

        /// <summary>
        /// Called when [toggle maximize restore window].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void OnToggleMaximizeRestoreWindow(object sender, ExecutedRoutedEventArgs args)
        {
            if (this.CanToggleMaximizeRestoreWindow(args))
            {
                System.Windows.Window wnd = (System.Windows.Window)args.Parameter;
                if (wnd.WindowState == WindowState.Maximized)
                {
                    wnd.WindowState = WindowState.Normal;
                }
                else
                {
                    wnd.WindowState = WindowState.Maximized;
                }
            }
        }

        #endregion ToggleMaximizeRestoreWindow

        #region MinimizeWindow

        /// <summary>
        /// Determines whether this instance [can minimize window] the specified args.
        /// </summary>
        /// <param name="args">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        /// <returns><c>true</c> if this instance [can minimize window] the specified args; otherwise, <c>false</c>.</returns>
        private bool CanMinimizeWindow(ExecutedRoutedEventArgs args)
        {
            return args.Parameter is System.Windows.Window;
        }

        /// <summary>
        /// Called when [minimize window].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void OnMinimizeWindow(object sender, ExecutedRoutedEventArgs args)
        {
            if (this.CanMinimizeWindow(args))
            {
                System.Windows.Window parameter = (System.Windows.Window)args.Parameter;
                parameter.WindowState = WindowState.Minimized;
            }
        }

        #endregion MinimizeWindow

        #region CloseWindow

        /// <summary>
        /// Determines whether this instance [can close window] the specified args.
        /// </summary>
        /// <param name="args">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        /// <returns><c>true</c> if this instance [can close window] the specified args; otherwise, <c>false</c>.</returns>
        private bool CanCloseWindow(ExecutedRoutedEventArgs args)
        {
            return args.Parameter is System.Windows.Window;
        }

        /// <summary>
        /// Called when [close window].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void OnCloseWindow(object sender, ExecutedRoutedEventArgs args)
        {
            if (this.CanCloseWindow(args))
            {
                ((System.Windows.Window)args.Parameter).Close();
            }
        }

        #endregion CloseWindow

        #endregion SystemBtnCommands

        #region Methods

        /// <summary>
        /// Calls the def window proc without visible style.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="msg">The MSG.</param>
        /// <param name="wParam">The w param.</param>
        /// <param name="lParam">The l param.</param>
        /// <param name="handled">if set to <c>true</c> [handled].</param>
        /// <returns>IntPtr.</returns>
        private IntPtr CallDefWindowProcWithoutVisibleStyle(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            bool flag = NativeMethodsPack.NativeMethods.ModifyStyle(hWnd, WS.WS_VISIBLE, 0);
            IntPtr intPtr = NativeMethodsPack.NativeMethods.DefWindowProc(hWnd, msg, wParam, lParam);
            if (flag)
            {
                NativeMethodsPack.NativeMethods.ModifyStyle(hWnd, 0, WS.WS_VISIBLE);
            }
            handled = true;
            return intPtr;
        }

        /// <summary>
        /// Changes the owner.
        /// </summary>
        /// <param name="newOwner">The new owner.</param>
        public void ChangeOwner(IntPtr newOwner)
        {
            WindowInteropHelper windowInteropHelper = new WindowInteropHelper(this) { Owner = newOwner };
            foreach (GlowWindow loadedGlowWindow in this.LoadedGlowWindows)
            {
                loadedGlowWindow.ChangeOwner(newOwner);
            }
            this.UpdateZOrderOfThisAndOwner();
        }

        /// <summary>
        /// Clears the clip region.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        private void ClearClipRegion(IntPtr hWnd)
        {
            NativeMethodsPack.NativeMethods.SetWindowRgn(hWnd, IntPtr.Zero, NativeMethodsPack.NativeMethods.IsWindowVisible(hWnd));
        }

        /// <summary>
        /// Computes the corner radius rect region.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="cornerRadius">The corner radius.</param>
        /// <returns>IntPtr.</returns>
        protected IntPtr ComputeCornerRadiusRectRegion(Int32Rect rect, CornerRadius cornerRadius)
        {
            if (((cornerRadius.TopLeft == cornerRadius.TopRight) && (cornerRadius.TopLeft == cornerRadius.BottomLeft)) && (cornerRadius.BottomLeft == cornerRadius.BottomRight))
            {
                return this.ComputeRoundRectRegion(rect.X, rect.Y, rect.Width, rect.Height, (int)cornerRadius.TopLeft);
            }

            IntPtr topLeftRect = IntPtr.Zero;
            IntPtr topRightRect = IntPtr.Zero;
            IntPtr bottomLeftRect = IntPtr.Zero;
            IntPtr bottomRightRect = IntPtr.Zero;
            IntPtr _topLeftRect = IntPtr.Zero;
            IntPtr _topRightRect = IntPtr.Zero;
            IntPtr _bottomLeftRect = IntPtr.Zero;
            IntPtr _bottomRightRect = IntPtr.Zero;
            IntPtr _rect = IntPtr.Zero;
            IntPtr _returnRect = IntPtr.Zero;
            try
            {
                topLeftRect = this.ComputeRoundRectRegion(rect.X, rect.Y, rect.Width, rect.Height, (int)cornerRadius.TopLeft);
                topRightRect = this.ComputeRoundRectRegion(rect.X, rect.Y, rect.Width, rect.Height, (int)cornerRadius.TopRight);
                bottomLeftRect = this.ComputeRoundRectRegion(rect.X, rect.Y, rect.Width, rect.Height, (int)cornerRadius.BottomLeft);
                bottomRightRect = this.ComputeRoundRectRegion(rect.X, rect.Y, rect.Width, rect.Height, (int)cornerRadius.BottomRight);
                POINT pOINT = new POINT();
                pOINT.x = rect.X + rect.Width / 2;
                pOINT.y = rect.Y + rect.Height / 2;
                _topLeftRect = NativeMethodsPack.NativeMethods.CreateRectRgn(rect.X, rect.Y, pOINT.x + 1, pOINT.y + 1);
                _topRightRect = NativeMethodsPack.NativeMethods.CreateRectRgn(pOINT.x - 1, rect.Y, rect.X + rect.Width, pOINT.y + 1);
                _bottomLeftRect = NativeMethodsPack.NativeMethods.CreateRectRgn(rect.X, pOINT.y - 1, pOINT.x + 1, rect.Y + rect.Height);
                _bottomRightRect = NativeMethodsPack.NativeMethods.CreateRectRgn(pOINT.x - 1, pOINT.y - 1, rect.X + rect.Width, rect.Y + rect.Height);
                _rect = NativeMethodsPack.NativeMethods.CreateRectRgn(0, 0, 1, 1);
                _returnRect = NativeMethodsPack.NativeMethods.CreateRectRgn(0, 0, 1, 1);
                NativeMethodsPack.NativeMethods.CombineRgn(_returnRect, topLeftRect, _topLeftRect, CR_MODE.RGN_AND);
                NativeMethodsPack.NativeMethods.CombineRgn(_rect, topRightRect, _topRightRect, CR_MODE.RGN_AND);
                NativeMethodsPack.NativeMethods.CombineRgn(_returnRect, _returnRect, _rect, CR_MODE.RGN_OR);
                NativeMethodsPack.NativeMethods.CombineRgn(_rect, bottomLeftRect, _bottomLeftRect, CR_MODE.RGN_AND);
                NativeMethodsPack.NativeMethods.CombineRgn(_returnRect, _returnRect, _rect, CR_MODE.RGN_OR);
                NativeMethodsPack.NativeMethods.CombineRgn(_rect, bottomRightRect, _bottomRightRect, CR_MODE.RGN_AND);
                NativeMethodsPack.NativeMethods.CombineRgn(_returnRect, _returnRect, _rect, CR_MODE.RGN_OR);
            }
            finally
            {
                if (topLeftRect != IntPtr.Zero)
                {
                    NativeMethodsPack.NativeMethods.DeleteObject(topLeftRect);
                }
                if (topRightRect != IntPtr.Zero)
                {
                    NativeMethodsPack.NativeMethods.DeleteObject(topRightRect);
                }
                if (bottomLeftRect != IntPtr.Zero)
                {
                    NativeMethodsPack.NativeMethods.DeleteObject(bottomLeftRect);
                }
                if (bottomRightRect != IntPtr.Zero)
                {
                    NativeMethodsPack.NativeMethods.DeleteObject(bottomRightRect);
                }
                if (_topLeftRect != IntPtr.Zero)
                {
                    NativeMethodsPack.NativeMethods.DeleteObject(_topLeftRect);
                }
                if (_topRightRect != IntPtr.Zero)
                {
                    NativeMethodsPack.NativeMethods.DeleteObject(_topRightRect);
                }
                if (_bottomLeftRect != IntPtr.Zero)
                {
                    NativeMethodsPack.NativeMethods.DeleteObject(_bottomLeftRect);
                }
                if (_bottomRightRect != IntPtr.Zero)
                {
                    NativeMethodsPack.NativeMethods.DeleteObject(_bottomRightRect);
                }
                if (_rect != IntPtr.Zero)
                {
                    NativeMethodsPack.NativeMethods.DeleteObject(_rect);
                }
            }
            return _returnRect;
        }

        /// <summary>
        /// Computes the round rect region.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="top">The top.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="cornerRadius">The corner radius.</param>
        /// <returns>IntPtr.</returns>
        private IntPtr ComputeRoundRectRegion(int left, int top, int width, int height, int cornerRadius)
        {
            int widthEllipse = (int)((double)(2 * cornerRadius) * DpiHelper.LogicalToDeviceUnitsScalingFactorX);
            int heightEllipse = (int)((double)(2 * cornerRadius) * DpiHelper.LogicalToDeviceUnitsScalingFactorY);
            return NativeMethodsPack.NativeMethods.CreateRoundRectRgn(left, top, left + width + 1, top + height + 1, widthEllipse, heightEllipse);
        }

        /// <summary>
        /// Creates the glow window handles.
        /// </summary>
        private void CreateGlowWindowHandles()
        {
            for (int i = 0; i < (int)this._glowWindows.Length; i++)
            {
                this.GetOrCreateGlowWindow(i).EnsureHandle();
            }
        }

        /// <summary>
        /// Defers the glow changes.
        /// </summary>
        /// <returns>IDisposable.</returns>
        private IDisposable DeferGlowChanges()
        {
            return new ChangeScope(this);
        }

        /// <summary>
        /// Destroys the glow windows.
        /// </summary>
        private void DestroyGlowWindows()
        {
            for (int i = 0; i < (int)this._glowWindows.Length; i++)
            {
                using (this._glowWindows[i])
                {
                    this._glowWindows[i] = null;
                }
            }
        }

        /// <summary>
        /// Ends the defer glow changes.
        /// </summary>
        private void EndDeferGlowChanges()
        {
            foreach (GlowWindow loadedGlowWindow in this.LoadedGlowWindows)
            {
                loadedGlowWindow.CommitChanges();
            }
        }

        /// <summary>
        /// Gets the client rect relative to window rect.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <returns>RECT.</returns>
        private static RECT GetClientRectRelativeToWindowRect(IntPtr hWnd)
        {
            RECT windowRect, clientRect;
            NativeMethodsPack.NativeMethods.GetWindowRect(hWnd, out windowRect);
            NativeMethodsPack.NativeMethods.GetClientRect(hWnd, out clientRect);
            POINT pt = new POINT() { x = 0, y = 0 };
            NativeMethodsPack.NativeMethods.ClientToScreen(hWnd, ref pt);
            clientRect.Offset(pt.x - windowRect.Left, pt.y - windowRect.Top);
            return clientRect;
        }

        /// <summary>
        /// Gets the or create glow window.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <returns>GlowWindow.</returns>
        private GlowWindow GetOrCreateGlowWindow(int direction)
        {
            if (this._glowWindows[direction] == null)
            {
                this._glowWindows[direction] = new GlowWindow(this, (Dock)direction);
                this._glowWindows[direction].ActiveGlowColor = this.ActiveGlowColor;
                this._glowWindows[direction].InactiveGlowColor = this.InactiveGlowColor;
                this._glowWindows[direction].IsActive = IsActive;
            }
            return this._glowWindows[direction];
        }

        /// <summary>
        /// Gets the window info.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <returns>WINDOWINFO.</returns>
        private WINDOWINFO GetWindowInfo(IntPtr hWnd)
        {
            WINDOWINFO wINDOWINFO = new WINDOWINFO();
            wINDOWINFO.cbSize = (uint)Marshal.SizeOf(wINDOWINFO);
            NativeMethodsPack.NativeMethods.GetWindowInfo(hWnd, ref wINDOWINFO);
            return wINDOWINFO;
        }

        /// <summary>
        /// Monitors the info from window.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <returns>MONITORINFO.</returns>
        private static MONITORINFO MonitorInfoFromWindow(IntPtr hWnd)
        {
            IntPtr intPtr = NativeMethodsPack.NativeMethods.MonitorFromWindow(hWnd, 2);
            MONITORINFO mONITORINFO = new MONITORINFO();
            mONITORINFO.cbSize = (uint)Marshal.SizeOf(typeof(MONITORINFO));
            NativeMethodsPack.NativeMethods.GetMonitorInfo(intPtr, ref mONITORINFO);
            return mONITORINFO;
        }

        /// <summary>
        /// Called when [corner radius changed].
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="args">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnCornerRadiusChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ((CustomChromeWindow)obj).UpdateClipRegion(CustomChromeWindow.ClipRegionChangeType.FromPropertyChange);
        }

        /// <summary>
        /// Called when [delayed visibility timer tick].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnDelayedVisibilityTimerTick(object sender, EventArgs e)
        {
            this.StopTimer();
            this.UpdateGlowWindowPositions(false);
        }

        /// <summary>
        /// Called when [glow color changed].
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="args">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnGlowColorChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ((CustomChromeWindow)obj).UpdateGlowColors();
        }

        /// <summary>
        /// Called when [resize mode changed].
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnResizeModeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((CustomChromeWindow)obj).UpdateGlowVisibility(false);
        }

        /// <summary>
        /// Raises the non client mouse message as client.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="msg">The MSG.</param>
        /// <param name="wParam">The w param.</param>
        /// <param name="lParam">The l param.</param>
        private static void RaiseNonClientMouseMessageAsClient(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam)
        {
            POINT point = new POINT()
            {
                x = NativeMethodsPack.NativeMethods.GetXLParam(lParam.ToInt32()),
                y = NativeMethodsPack.NativeMethods.GetYLParam(lParam.ToInt32())
            };
            NativeMethodsPack.NativeMethods.ScreenToClient(hWnd, ref point);
            NativeMethodsPack.NativeMethods.SendMessage(hWnd, (uint)(msg + WINDOWMESSAGES.WM_LBUTTONDOWN - WINDOWMESSAGES.WM_NCLBUTTONDOWN), new IntPtr(CustomChromeWindow.PressedMouseButtons), NativeMethodsPack.NativeMethods.MakeParam(point.x, point.y));
        }

        /// <summary>
        /// Sets the round rect.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        protected void SetRoundRect(IntPtr hWnd, int width, int height)
        {
            IntPtr intPtr = this.ComputeRoundRectRegion(0, 0, width, height, this.CornerRadius);
            NativeMethodsPack.NativeMethods.SetWindowRgn(hWnd, intPtr, NativeMethodsPack.NativeMethods.IsWindowVisible(hWnd));
        }

        /// <summary>
        /// Shows the window menu.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="element">The element.</param>
        /// <param name="elementPoint">The element point.</param>
        /// <param name="elementSize">Size of the element.</param>
        public static void ShowWindowMenu(HwndSource source, Visual element, Point elementPoint, Size elementSize)
        {
            if ((elementPoint.X >= 0 && elementPoint.X <= elementSize.Width) && (elementPoint.Y >= 0 && elementPoint.Y <= elementSize.Height))
            {
                Point screen = element.PointToScreen(elementPoint);
                CustomChromeWindow.ShowWindowMenu(source, screen, true);
            }
        }

        /// <summary>
        /// Shows the window menu.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="screenPoint">The screen point.</param>
        /// <param name="canMinimize">if set to <c>true</c> [can minimize].</param>
        protected static void ShowWindowMenu(HwndSource source, Point screenPoint, bool canMinimize)
        {
            uint num1 = (uint)(canMinimize ? 0 : 1);
            int systemMetrics = NativeMethodsPack.NativeMethods.GetSystemMetrics(SM.SM_MENUDROPALIGNMENT);
            IntPtr systemMenu = NativeMethodsPack.NativeMethods.GetSystemMenu(source.Handle, false);
            WINDOWPLACEMENT windowPlacement = NativeMethodsPack.NativeMethods.GetWindowPlacement(source.Handle);
            bool flag = NativeMethodsPack.NativeMethods.ModifyStyle(source.Handle, 268435456, 0);
            if (windowPlacement.ShowCmd != SW_CMD.SW_SHOWNORMAL)
            {
                if (windowPlacement.ShowCmd == SW_CMD.SW_MAXIMIZE)
                {
                    NativeMethodsPack.NativeMethods.EnableMenuItem(systemMenu, SC.SC_RESTORE, 0);
                    NativeMethodsPack.NativeMethods.EnableMenuItem(systemMenu, SC.SC_MOVE, 1);
                    NativeMethodsPack.NativeMethods.EnableMenuItem(systemMenu, SC.SC_SIZE, 1);
                    NativeMethodsPack.NativeMethods.EnableMenuItem(systemMenu, SC.SC_MAXIMIZE, 1);
                    NativeMethodsPack.NativeMethods.EnableMenuItem(systemMenu, SC.SC_MINIMIZE, num1);
                    NativeMethodsPack.NativeMethods.EnableMenuItem(systemMenu, SC.SC_CLOSE, 0);
                }
            }
            else
            {
                NativeMethodsPack.NativeMethods.EnableMenuItem(systemMenu, SC.SC_RESTORE, 1);
                NativeMethodsPack.NativeMethods.EnableMenuItem(systemMenu, SC.SC_MOVE, 0);
                NativeMethodsPack.NativeMethods.EnableMenuItem(systemMenu, SC.SC_SIZE, 0);
                NativeMethodsPack.NativeMethods.EnableMenuItem(systemMenu, SC.SC_MAXIMIZE, 0);
                NativeMethodsPack.NativeMethods.EnableMenuItem(systemMenu, SC.SC_MINIMIZE, num1);
                NativeMethodsPack.NativeMethods.EnableMenuItem(systemMenu, SC.SC_CLOSE, 0);
            }
            if (flag)
            {
                NativeMethodsPack.NativeMethods.ModifyStyle(source.Handle, 0, (int)WS.WS_VISIBLE);
            }
            uint num2 = (uint)(systemMetrics | 256 | 128 | 2);
            int num3 = NativeMethodsPack.NativeMethods.TrackPopupMenuEx(systemMenu, num2, (int)screenPoint.X, (int)screenPoint.Y, source.Handle, IntPtr.Zero);
            if (num3 != 0)
            {
                NativeMethodsPack.NativeMethods.PostMessage(source.Handle, 274, new IntPtr(num3), IntPtr.Zero);
            }
        }

        /// <summary>
        /// Stops the timer.
        /// </summary>
        private void StopTimer()
        {
            if (this._makeGlowVisibleTimer != null)
            {
                this._makeGlowVisibleTimer.Stop();
                this._makeGlowVisibleTimer.Tick -= new EventHandler(this.OnDelayedVisibilityTimerTick);
                this._makeGlowVisibleTimer = null;
            }
        }

        /// <summary>
        /// Updates the clip region.
        /// </summary>
        /// <param name="regionChangeType">Type of the region change.</param>
        protected void UpdateClipRegion(ClipRegionChangeType regionChangeType = ClipRegionChangeType.FromPropertyChange)
        {
            RECT rECT;
            HwndSource hwndSource = (HwndSource)PresentationSource.FromVisual(this);
            if (hwndSource != null)
            {
                NativeMethodsPack.NativeMethods.GetWindowRect(hwndSource.Handle, out rECT);
                WINDOWPLACEMENT windowPlacement = NativeMethodsPack.NativeMethods.GetWindowPlacement(hwndSource.Handle);
                this.UpdateClipRegion(hwndSource.Handle, windowPlacement, regionChangeType, rECT);
            }
        }

        /// <summary>
        /// Updates the clip region.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="placement">The placement.</param>
        /// <param name="changeType">Type of the change.</param>
        /// <param name="currentBounds">The current bounds.</param>
        private void UpdateClipRegion(IntPtr hWnd, WINDOWPLACEMENT placement, ClipRegionChangeType changeType, RECT currentBounds)
        {
            this.UpdateClipRegionCore(hWnd, placement.ShowCmd, changeType, currentBounds.ToInt32Rect());
            this.lastWindowPlacement = (int)placement.ShowCmd;
        }

        /// <summary>
        /// Updates the state of the glow active.
        /// </summary>
        private void UpdateGlowActiveState()
        {
            IDisposable disposable = this.DeferGlowChanges();
            using (disposable)
            {
                foreach (GlowWindow loadedGlowWindow in this.LoadedGlowWindows)
                {
                    loadedGlowWindow.IsActive = IsActive;
                }
            }
        }

        /// <summary>
        /// Updates the glow colors.
        /// </summary>
        private void UpdateGlowColors()
        {
            IDisposable disposable = this.DeferGlowChanges();
            using (disposable)
            {
                foreach (GlowWindow loadedGlowWindow in this.LoadedGlowWindows)
                {
                    loadedGlowWindow.ActiveGlowColor = this.ActiveGlowColor;
                    loadedGlowWindow.InactiveGlowColor = this.InactiveGlowColor;
                }
            }
        }

        /// <summary>
        /// Updates the glow visibility.
        /// </summary>
        /// <param name="delayIfNecessary">if set to <c>true</c> [delay if necessary].</param>
        private void UpdateGlowVisibility(bool delayIfNecessary)
        {
            if (ShouldShowGlow != this.IsGlowVisible)
            {
                if ((SystemParameters.MinimizeAnimation && ShouldShowGlow) && delayIfNecessary)
                {
                    if (this._makeGlowVisibleTimer != null)
                    {
                        this._makeGlowVisibleTimer.Stop();
                    }
                    else
                    {
                        this._makeGlowVisibleTimer = new DispatcherTimer();
                        this._makeGlowVisibleTimer.Interval = TimeSpan.FromMilliseconds(200.0);
                        this._makeGlowVisibleTimer.Tick += new EventHandler(this.OnDelayedVisibilityTimerTick);
                    }
                    this._makeGlowVisibleTimer.Start();
                }
                else
                {
                    this.StopTimer();
                    this.IsGlowVisible = ShouldShowGlow;
                }
            }
        }

        /// <summary>
        /// Updates the glow window positions.
        /// </summary>
        /// <param name="delayIfNecessary">if set to <c>true</c> [delay if necessary].</param>
        private void UpdateGlowWindowPositions(bool delayIfNecessary)
        {
            IDisposable disposable = this.DeferGlowChanges();
            using (disposable)
            {
                this.UpdateGlowVisibility(delayIfNecessary);
                foreach (GlowWindow loadedGlowWindow in this.LoadedGlowWindows)
                {
                    loadedGlowWindow.UpdateWindowPos();
                }
            }
        }

        /// <summary>
        /// Updates the maximized clip region.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        private void UpdateMaximizedClipRegion(IntPtr hWnd)
        {
            RECT clientRectRelativeToWindowRect = CustomChromeWindow.GetClientRectRelativeToWindowRect(hWnd);
            if (this._isNonClientStripVisible)
            {
                clientRectRelativeToWindowRect.Bottom = clientRectRelativeToWindowRect.Bottom + 1;
            }
            IntPtr intPtr = NativeMethodsPack.NativeMethods.CreateRectRgnIndirect(ref clientRectRelativeToWindowRect);
            NativeMethodsPack.NativeMethods.SetWindowRgn(hWnd, intPtr, NativeMethodsPack.NativeMethods.IsWindowVisible(hWnd));
        }

        /// <summary>
        /// Updates the Z order of owner.
        /// </summary>
        /// <param name="hwndOwner">The HWND owner.</param>
        private void UpdateZOrderOfOwner(IntPtr hwndOwner)
        {
            IntPtr lastOwnedWindow = IntPtr.Zero;
            NativeMethodsPack.NativeMethods.EnumThreadWindows(NativeMethodsPack.NativeMethods.GetCurrentThreadId(), (IntPtr hwnd, IntPtr lParam) =>
            {
                if (NativeMethodsPack.NativeMethods.GetWindow(hwnd, GW_CMD.GW_OWNER) == hwndOwner)
                {
                    lastOwnedWindow = hwnd;
                }
                return true;
            }, IntPtr.Zero);
            if (lastOwnedWindow != IntPtr.Zero && NativeMethodsPack.NativeMethods.GetWindow(hwndOwner, GW_CMD.GW_HWNDPREV) != IntPtr.Zero)
            {
                NativeMethodsPack.NativeMethods.SetWindowPos(hwndOwner, lastOwnedWindow, 0, 0, 0, 0, SWP_FLAGS.SWP_NOACTIVATE | SWP_FLAGS.SWP_NOMOVE | SWP_FLAGS.SWP_NOSIZE);
            }
        }

        /// <summary>
        /// Updates the Z order of this and owner.
        /// </summary>
        private void UpdateZOrderOfThisAndOwner()
        {
            WindowInteropHelper windowInteropHelper = new WindowInteropHelper(this);
            IntPtr handle = windowInteropHelper.Handle;
            foreach (GlowWindow loadedGlowWindow in this.LoadedGlowWindows)
            {
                IntPtr window = NativeMethodsPack.NativeMethods.GetWindow(loadedGlowWindow.Handle, GW_CMD.GW_HWNDPREV);
                if (window != handle)
                {
                    NativeMethodsPack.NativeMethods.SetWindowPos(loadedGlowWindow.Handle, handle, 0, 0, 0, 0, SWP_FLAGS.SWP_NOACTIVATE | SWP_FLAGS.SWP_NOMOVE | SWP_FLAGS.SWP_NOSIZE);
                }
                handle = loadedGlowWindow.Handle;
            }
            IntPtr owner = windowInteropHelper.Owner;
            if (owner != IntPtr.Zero)
            {
                this.UpdateZOrderOfOwner(owner);
            }
        }

        /// <summary>
        /// Makes the pack URI.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="path">The path.</param>
        /// <returns>Uri.</returns>
        private static Uri MakePackUri(Assembly assembly, string path)
        {
            string name = assembly.GetName().Name;
            return new Uri(string.Format("pack://application:,,,/{0};component/{1}", name, path), UriKind.Absolute);
        }

        /// <summary>
        /// Gets the on screen position.
        /// </summary>
        /// <param name="floatRect">The float rect.</param>
        /// <returns>Rect.</returns>
        private static Rect GetOnScreenPosition(Rect floatRect)
        {
            Rect rect;
            Rect logicalUnits;
            Rect width = floatRect;
            floatRect = floatRect.LogicalToDeviceUnits();
            Screen.FindMaximumSingleMonitorRectangle(floatRect, out rect, out logicalUnits);
            if (!floatRect.IntersectsWith(logicalUnits))
            {
                Screen.FindMonitorRectsFromPoint(NativeMethodsPack.NativeMethods.GetCursorPos(), out rect, out logicalUnits);
                logicalUnits = logicalUnits.DeviceToLogicalUnits();
                if (width.Width > logicalUnits.Width)
                {
                    width.Width = logicalUnits.Width;
                }
                if (width.Height > logicalUnits.Height)
                {
                    width.Height = logicalUnits.Height;
                }
                if (logicalUnits.Right <= width.X)
                {
                    width.X = logicalUnits.Right - width.Width;
                }
                if (logicalUnits.Left > width.X + width.Width)
                {
                    width.X = logicalUnits.Left;
                }
                if (logicalUnits.Bottom <= width.Y)
                {
                    width.Y = logicalUnits.Bottom - width.Height;
                }
                if (logicalUnits.Top > width.Y + width.Height)
                {
                    width.Y = logicalUnits.Top;
                }
            }
            return width;
        }

        /// <summary>
        /// Hits the test visible elements.
        /// </summary>
        /// <param name="visual">The visual.</param>
        /// <param name="resultCallback">The result callback.</param>
        /// <param name="parameters">The parameters.</param>
        private static void HitTestVisibleElements(Visual visual, HitTestResultCallback resultCallback, HitTestParameters parameters)
        {
            VisualTreeHelper.HitTest(visual, new HitTestFilterCallback(ExcludeNonVisualElements), resultCallback, parameters);
        }

        /// <summary>
        /// Excludes the non visual elements.
        /// </summary>
        /// <param name="potentialHitTestTarget">The potential hit test target.</param>
        /// <returns>HitTestFilterBehavior.</returns>
        private static HitTestFilterBehavior ExcludeNonVisualElements(DependencyObject potentialHitTestTarget)
        {
            if (potentialHitTestTarget as Visual != null)
            {
                UIElement uIElement = potentialHitTestTarget as UIElement;
                if (uIElement == null || uIElement.IsVisible && uIElement.IsEnabled)
                {
                    return HitTestFilterBehavior.Continue;
                }
                else
                {
                    return HitTestFilterBehavior.ContinueSkipSelfAndChildren;
                }
            }
            else
            {
                return HitTestFilterBehavior.ContinueSkipSelfAndChildren;
            }
        }

        /// <summary>
        /// Gets the visual or logical parent.
        /// </summary>
        /// <param name="sourceElement">The source element.</param>
        /// <returns>DependencyObject.</returns>
        private DependencyObject GetVisualOrLogicalParent(DependencyObject sourceElement)
        {
            if (sourceElement != null)
            {
                if (sourceElement as Visual == null)
                {
                    return LogicalTreeHelper.GetParent(sourceElement);
                }
                else
                {
                    DependencyObject parent = VisualTreeHelper.GetParent(sourceElement);
                    DependencyObject dependencyObject = parent;
                    if (parent == null)
                    {
                        dependencyObject = LogicalTreeHelper.GetParent(sourceElement);
                    }
                    return dependencyObject;
                }
            }
            else
            {
                return null;
            }
        }

        #region WindowMessages

        /// <summary>
        /// Wms the activate.
        /// </summary>
        /// <param name="wParam">The w param.</param>
        /// <param name="lParam">The l param.</param>
        private void WmActivate(IntPtr wParam, IntPtr lParam)
        {
            WindowInteropHelper windowInteropHelper = new WindowInteropHelper(this);
            if (windowInteropHelper.Owner != IntPtr.Zero)
            {
                NativeMethodsPack.NativeMethods.SendMessage(windowInteropHelper.Owner, (uint)NativeMethodsPack.NativeMethods.NOTIFYOWNERACTIVATE, wParam, lParam);
            }
        }

        /// <summary>
        /// Wms the nc activate.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="wParam">The w param.</param>
        /// <param name="lParam">The l param.</param>
        /// <param name="handled">if set to <c>true</c> [handled].</param>
        /// <returns>IntPtr.</returns>
        private IntPtr WmNcActivate(IntPtr hWnd, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            handled = true;
            return NativeMethodsPack.NativeMethods.DefWindowProc(hWnd, WINDOWMESSAGES.WM_NCACTIVATE, wParam, NativeMethodsPack.NativeMethods.HRGN_NONE);
        }

        /// <summary>
        /// Wms the size of the nc calc.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="wParam">The w param.</param>
        /// <param name="lParam">The l param.</param>
        /// <param name="handled">if set to <c>true</c> [handled].</param>
        /// <returns>IntPtr.</returns>
        private IntPtr WmNcCalcSize(IntPtr hWnd, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            this._isNonClientStripVisible = false;
            if (NativeMethodsPack.NativeMethods.GetWindowPlacement(hWnd).ShowCmd == SW_CMD.SW_MAXIMIZE)
            {
                RECT rect = (RECT)Marshal.PtrToStructure(lParam, typeof(RECT));
                NativeMethodsPack.NativeMethods.DefWindowProc(hWnd, WINDOWMESSAGES.WM_NCCALCSIZE, wParam, lParam);
                RECT rectStructure = (RECT)Marshal.PtrToStructure(lParam, typeof(RECT));
                MONITORINFO mONITORINFO = CustomChromeWindow.MonitorInfoFromWindow(hWnd);
                if (mONITORINFO.rcMonitor.Height == mONITORINFO.rcWork.Height && mONITORINFO.rcMonitor.Width == mONITORINFO.rcWork.Width)
                {
                    this._isNonClientStripVisible = true;
                    rectStructure.Bottom--;
                }
                rectStructure.Top = (int)(rect.Top + this.GetWindowInfo(hWnd).cyWindowBorders);
                rectStructure.Left = (int)(rect.Left + this.GetWindowInfo(hWnd).cyWindowBorders);
                Marshal.StructureToPtr(rectStructure, lParam, true);
            }
            handled = true;
            return IntPtr.Zero;
        }

        /// <summary>
        /// Wms the nc hit test.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="lParam">The l param.</param>
        /// <param name="handled">if set to <c>true</c> [handled].</param>
        /// <returns>IntPtr.</returns>
        private IntPtr WmNcHitTest(IntPtr hWnd, IntPtr lParam, ref bool handled)
        {
            if (PresentationSource.FromDependencyObject(this) == null)
            {
                return new IntPtr(0);
            }
            Point point = new Point((double)NativeMethodsPack.NativeMethods.GetXLParam(lParam.ToInt32()), (double)NativeMethodsPack.NativeMethods.GetYLParam(lParam.ToInt32()));
            Point point2 = PointFromScreen(point);
            DependencyObject visualHit = null;
            CustomChromeWindow.HitTestVisibleElements(this, delegate(HitTestResult target)
            {
                visualHit = target.VisualHit;
                return HitTestResultBehavior.Stop;
            }, new PointHitTestParameters(point2));
            int num = 0;
            while (visualHit != null)
            {
                INonClientArea area = visualHit as INonClientArea;
                if (area != null)
                {
                    num = area.HitTest(point);
                    if (num != 0)
                    {
                        break;
                    }
                }
                visualHit = GetVisualOrLogicalParent(visualHit);
            }
            if (num == 0)
            {
                num = 1;
            }
            handled = true;
            return new IntPtr(num);
        }

        /// <summary>
        /// Wms the nc paint.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="wParam">The w param.</param>
        /// <param name="lParam">The l param.</param>
        /// <param name="handled">if set to <c>true</c> [handled].</param>
        /// <returns>IntPtr.</returns>
        private IntPtr WmNcPaint(IntPtr hWnd, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (this._isNonClientStripVisible)
            {
                IntPtr intPtr = (wParam == new IntPtr(1)) ? IntPtr.Zero : wParam;
                IntPtr dCEx = NativeMethodsPack.NativeMethods.GetDCEx(hWnd, intPtr, 155);
                if (dCEx != IntPtr.Zero)
                {
                    try
                    {
                        Color nonClientFillColor = this.NonClientFillColor;
                        int b = nonClientFillColor.B << 16 | nonClientFillColor.G << 8 | nonClientFillColor.R;
                        IntPtr intPtr1 = NativeMethodsPack.NativeMethods.CreateSolidBrush(b);
                        try
                        {
                            RECT clientRectRelativeToWindowRect = CustomChromeWindow.GetClientRectRelativeToWindowRect(hWnd);
                            clientRectRelativeToWindowRect.Top = clientRectRelativeToWindowRect.Bottom;
                            clientRectRelativeToWindowRect.Bottom = clientRectRelativeToWindowRect.Top + 1;
                            NativeMethodsPack.NativeMethods.FillRect(dCEx, ref clientRectRelativeToWindowRect, intPtr1);
                        }
                        finally
                        {
                            NativeMethodsPack.NativeMethods.DeleteObject(intPtr1);
                        }
                    }
                    finally
                    {
                        NativeMethodsPack.NativeMethods.ReleaseDC(hWnd, dCEx);
                    }
                }
            }
            handled = true;
            return IntPtr.Zero;
        }

        /// <summary>
        /// Wms the window pos changed.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="lParam">The l param.</param>
        private void WmWindowPosChanged(IntPtr hWnd, IntPtr lParam)
        {
            WINDOWPOS windowpos = (WINDOWPOS)Marshal.PtrToStructure(lParam, typeof(WINDOWPOS));
            WINDOWPLACEMENT windowPlacement = NativeMethodsPack.NativeMethods.GetWindowPlacement(hWnd);
            RECT currentBounds = new RECT(windowpos.x, windowpos.y, windowpos.x + windowpos.cx, windowpos.y + windowpos.cy);
            if ((windowpos.flags & SWP_FLAGS.SWP_NOSIZE) != SWP_FLAGS.SWP_NOSIZE)
            {
                this.UpdateClipRegion(hWnd, windowPlacement, ClipRegionChangeType.FromSize, currentBounds);
            }
            else if ((windowpos.flags & SWP_FLAGS.SWP_NOMOVE) != SWP_FLAGS.SWP_NOMOVE)
            {
                this.UpdateClipRegion(hWnd, windowPlacement, ClipRegionChangeType.FromPosition, currentBounds);
            }
            this.OnWindowPosChanged(hWnd, windowPlacement.ShowCmd, windowPlacement.NormalPosition.ToInt32Rect());
            this.UpdateGlowWindowPositions((windowpos.flags & SWP_FLAGS.SWP_SHOWWINDOW) == 0);
            this.UpdateZOrderOfThisAndOwner();
        }

        /// <summary>
        /// Wms the window pos changing.
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        /// <param name="lParam">The l param.</param>
        private void WmWindowPosChanging(IntPtr hwnd, IntPtr lParam)
        {
            WINDOWPOS windowpos = (WINDOWPOS)Marshal.PtrToStructure(lParam, typeof(WINDOWPOS));
            if ((windowpos.flags & SWP_FLAGS.SWP_NOMOVE) == 0 && (windowpos.flags & SWP_FLAGS.SWP_NOSIZE) == 0 && windowpos.cx > 0 && windowpos.cy > 0)
            {
                Rect rect = new Rect((double)windowpos.x, (double)windowpos.y, (double)windowpos.cx, (double)windowpos.cy);
                Rect onScreenPosition = CustomChromeWindow.GetOnScreenPosition(rect.DeviceToLogicalUnits()).LogicalToDeviceUnits();
                windowpos.x = (int)onScreenPosition.X;
                windowpos.y = (int)onScreenPosition.Y;
                Marshal.StructureToPtr(windowpos, lParam, true);
            }
        }

        #endregion WindowMessages

        #endregion Methods

        #region virtual Methods

        /// <summary>
        /// HWNDs the source hook.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="msg">The MSG.</param>
        /// <param name="wParam">The w param.</param>
        /// <param name="lParam">The l param.</param>
        /// <param name="handled">if set to <c>true</c> [handled].</param>
        /// <returns>IntPtr.</returns>
        protected virtual IntPtr HwndSourceHook(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case WINDOWMESSAGES.WM_WINDOWPOSCHANGING:
                    this.WmWindowPosChanging(hWnd, lParam);
                    break;

                case WINDOWMESSAGES.WM_WINDOWPOSCHANGED:
                    this.WmWindowPosChanged(hWnd, lParam);
                    break;

                case WINDOWMESSAGES.WM_SETTEXT:
                case WINDOWMESSAGES.WM_SETICON:
                    return this.CallDefWindowProcWithoutVisibleStyle(hWnd, msg, wParam, lParam, ref handled);

                case WINDOWMESSAGES.WM_ACTIVATE:
                    this.WmActivate(wParam, lParam);
                    break;

                case WINDOWMESSAGES.WM_NCCALCSIZE:
                    return this.WmNcCalcSize(hWnd, wParam, lParam, ref handled);

                case WINDOWMESSAGES.WM_NCHITTEST:
                    return this.WmNcHitTest(hWnd, lParam, ref handled);

                case WINDOWMESSAGES.WM_NCPAINT:
                    return this.WmNcPaint(hWnd, wParam, lParam, ref handled);

                case WINDOWMESSAGES.WM_NCACTIVATE:
                    return this.WmNcActivate(hWnd, wParam, lParam, ref handled);

                case WINDOWMESSAGES.WM_NCRBUTTONDOWN:
                case WINDOWMESSAGES.WM_NCRBUTTONUP:
                case WINDOWMESSAGES.WM_NCRBUTTONDBLCLK:
                    RaiseNonClientMouseMessageAsClient(hWnd, msg, wParam, lParam);
                    handled = true;
                    break;

                case WINDOWMESSAGES.WM_NCUAHDRAWCAPTION:
                case WINDOWMESSAGES.WM_NCUAHDRAWFRAME:
                    handled = true;
                    break;
            }
            return IntPtr.Zero;
        }

        /// <summary>
        /// Called when [window pos changed].
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="showCmd">The show CMD.</param>
        /// <param name="rcNormalPosition">The rc normal position.</param>
        protected virtual void OnWindowPosChanged(IntPtr hWnd, int showCmd, Int32Rect rcNormalPosition)
        {
        }

        /// <summary>
        /// Updates the clip region core.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="showCmd">The show CMD.</param>
        /// <param name="changeType">Type of the change.</param>
        /// <param name="currentBounds">The current bounds.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        protected virtual bool UpdateClipRegionCore(IntPtr hWnd, int showCmd, CustomChromeWindow.ClipRegionChangeType changeType, Int32Rect currentBounds)
        {
            if (showCmd == SW_CMD.SW_MAXIMIZE)
            {
                this.UpdateMaximizedClipRegion(hWnd);
                return true;
            }
            if (((changeType != ClipRegionChangeType.FromSize) && (changeType != ClipRegionChangeType.FromPropertyChange)) && (this.lastWindowPlacement == (int)showCmd))
            {
                return false;
            }
            if (this.CornerRadius < 0)
            {
                this.ClearClipRegion(hWnd);
            }
            else
            {
                this.SetRoundRect(hWnd, currentBounds.Width, currentBounds.Height);
            }
            return true;
        }

        #endregion virtual Methods

        #region override Window Methods

        /// <summary>
        /// 引发 <see cref="E:System.Windows.Window.Activated" /> 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 <see cref="T:System.EventArgs" />。</param>
        protected override void OnActivated(EventArgs e)
        {
            this.UpdateGlowActiveState();
            base.OnActivated(e);
        }

        /// <summary>
        /// 引发 <see cref="E:System.Windows.Window.Closed" /> 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 <see cref="T:System.EventArgs" />。</param>
        protected override void OnClosed(EventArgs e)
        {
            this.StopTimer();
            this.DestroyGlowWindows();
            base.OnClosed(e);
        }

        /// <summary>
        /// 引发 <see cref="E:System.Windows.Window.Deactivated" /> 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 <see cref="T:System.EventArgs" />。</param>
        protected override void OnDeactivated(EventArgs e)
        {
            this.UpdateGlowActiveState();
            base.OnDeactivated(e);
        }

        /// <summary>
        /// 引发 <see cref="E:System.Windows.Window.SourceInitialized" /> 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 <see cref="T:System.EventArgs" />。</param>
        protected override void OnSourceInitialized(EventArgs e)
        {
            HwndSource.FromHwnd(new WindowInteropHelper(this).Handle).AddHook(new HwndSourceHook(this.HwndSourceHook));
            this.CreateGlowWindowHandles();
            base.OnSourceInitialized(e);

            //权宜之计，消除第一次启动显示窗体默认标题栏问题
            Top += 1;
            Top -= 1;
        }

        #endregion override Window Methods

        #region private class or enum

        /// <summary>
        /// Class ChangeScope
        /// </summary>
        private class ChangeScope : DisposableObject
        {
            /// <summary>
            /// The _window
            /// </summary>
            private readonly CustomChromeWindow _window;

            /// <summary>
            /// Initializes a new instance of the <see cref="ChangeScope"/> class.
            /// </summary>
            /// <param name="window">The window.</param>
            public ChangeScope(CustomChromeWindow window)
            {
                this._window = window;
                this._window._deferGlowChangesCount++;
            }

            #region DisposableObject

            /// <summary>
            /// Disposes the managed resources.
            /// </summary>
            protected override void DisposeManagedResources()
            {
                if (this._window._deferGlowChangesCount-- == 0)
                {
                    this._window.EndDeferGlowChanges();
                }
            }

            #endregion DisposableObject
        }

        /// <summary>
        /// Enum ClipRegionChangeType
        /// </summary>
        protected enum ClipRegionChangeType
        {
            /// <summary>
            /// From size
            /// </summary>
            FromSize,
            /// <summary>
            /// From position
            /// </summary>
            FromPosition,
            /// <summary>
            /// From property change
            /// </summary>
            FromPropertyChange,
            /// <summary>
            /// From undock single tab
            /// </summary>
            FromUndockSingleTab
        }

        /// <summary>
        /// Class GlowBitmap
        /// </summary>
        private sealed class GlowBitmap : DisposableObject
        {
            #region var

            /// <summary>
            /// The glow bitmap part count
            /// </summary>
            public const int GlowBitmapPartCount = 16;

            /// <summary>
            /// The bytes per pixel bgra32
            /// </summary>
            private const int BytesPerPixelBgra32 = 4;

            /// <summary>
            /// The _transparency masks
            /// </summary>
            private readonly static CachedBitmapInfo[] _transparencyMasks;

            /// <summary>
            /// The _h bitmap
            /// </summary>
            private readonly IntPtr _hBitmap;

            /// <summary>
            /// The _pbits
            /// </summary>
            private readonly IntPtr _pbits;

            /// <summary>
            /// The _bitmap info
            /// </summary>
            private readonly BITMAPINFO _bitmapInfo;

            #endregion var

            #region properties

            /// <summary>
            /// Gets the DI bits.
            /// </summary>
            /// <value>The DI bits.</value>
            public IntPtr DIBits { get { return this._pbits; } }

            /// <summary>
            /// Gets the handle.
            /// </summary>
            /// <value>The handle.</value>
            public IntPtr Handle { get { return this._hBitmap; } }

            /// <summary>
            /// Gets the height.
            /// </summary>
            /// <value>The height.</value>
            public int Height { get { return -this._bitmapInfo.biHeight; } }

            /// <summary>
            /// Gets the width.
            /// </summary>
            /// <value>The width.</value>
            public int Width { get { return this._bitmapInfo.biWidth; } }

            #endregion properties

            #region Constructor

            /// <summary>
            /// Initializes static members of the <see cref="GlowBitmap"/> class.
            /// </summary>
            static GlowBitmap()
            {
                GlowBitmap._transparencyMasks = new CachedBitmapInfo[16];
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="GlowBitmap"/> class.
            /// </summary>
            /// <param name="hdcScreen">The HDC screen.</param>
            /// <param name="width">The width.</param>
            /// <param name="height">The height.</param>
            public GlowBitmap(IntPtr hdcScreen, int width, int height)
            {
                this._bitmapInfo.biSize = Marshal.SizeOf(typeof(BITMAPINFOHEADER));
                this._bitmapInfo.biPlanes = 1;
                this._bitmapInfo.biBitCount = 32;
                this._bitmapInfo.biCompression = 0;
                this._bitmapInfo.biXPelsPerMeter = 0;
                this._bitmapInfo.biYPelsPerMeter = 0;
                this._bitmapInfo.biWidth = width;
                this._bitmapInfo.biHeight = -height;
                this._hBitmap = NativeMethodsPack.NativeMethods.CreateDIBSection(hdcScreen, ref this._bitmapInfo, 0, out this._pbits, IntPtr.Zero, 0);
            }

            #endregion Constructor

            #region Methods

            /// <summary>
            /// Creates the specified drawing context.
            /// </summary>
            /// <param name="drawingContext">The drawing context.</param>
            /// <param name="bitmapPart">The bitmap part.</param>
            /// <param name="color">The color.</param>
            /// <returns>GlowBitmap.</returns>
            public static GlowBitmap Create(GlowDrawingContext drawingContext, GlowBitmapPart bitmapPart, Color color)
            {
                CachedBitmapInfo orCreateAlphaMask = GlowBitmap.GetOrCreateAlphaMask(bitmapPart);
                GlowBitmap glowBitmap = new GlowBitmap(drawingContext.ScreenDC, orCreateAlphaMask.Width, orCreateAlphaMask.Height);
                for (int i = 0; i < (int)orCreateAlphaMask.DIBits.Length; i = i + 4)
                {
                    byte dIBits = orCreateAlphaMask.DIBits[i + 3];
                    byte num = GlowBitmap.PremultiplyAlpha(color.R, dIBits);
                    byte num1 = GlowBitmap.PremultiplyAlpha(color.G, dIBits);
                    byte num2 = GlowBitmap.PremultiplyAlpha(color.B, dIBits);
                    Marshal.WriteByte(glowBitmap.DIBits, i, num2);
                    Marshal.WriteByte(glowBitmap.DIBits, i + 1, num1);
                    Marshal.WriteByte(glowBitmap.DIBits, i + 2, num);
                    Marshal.WriteByte(glowBitmap.DIBits, i + 3, dIBits);
                }
                return glowBitmap;
            }

            /// <summary>
            /// Gets the or create alpha mask.
            /// </summary>
            /// <param name="bitmapPart">The bitmap part.</param>
            /// <returns>CachedBitmapInfo.</returns>
            private static CachedBitmapInfo GetOrCreateAlphaMask(GlowBitmapPart bitmapPart)
            {
                int num = (int)bitmapPart;
                if (GlowBitmap._transparencyMasks[num] == null)
                {
                    BitmapImage bitmapImage = new BitmapImage(CustomChromeWindow.MakePackUri(typeof(GlowBitmap).Assembly, string.Concat("Resources/", bitmapPart.ToString(), ".png")));
                    byte[] numArray = new byte[4 * bitmapImage.PixelWidth * bitmapImage.PixelHeight];
                    int pixelWidth = 4 * bitmapImage.PixelWidth;
                    bitmapImage.CopyPixels(numArray, pixelWidth, 0);
                    GlowBitmap._transparencyMasks[num] = new CachedBitmapInfo(numArray, bitmapImage.PixelWidth, bitmapImage.PixelHeight);
                }
                return GlowBitmap._transparencyMasks[num];
            }

            /// <summary>
            /// Premultiplies the alpha.
            /// </summary>
            /// <param name="channel">The channel.</param>
            /// <param name="alpha">The alpha.</param>
            /// <returns>System.Byte.</returns>
            private static byte PremultiplyAlpha(byte channel, byte alpha)
            {
                return (byte)((double)(channel * alpha) / 255);
            }

            #endregion Methods

            /// <summary>
            /// Class CachedBitmapInfo
            /// </summary>
            private sealed class CachedBitmapInfo
            {
                /// <summary>
                /// The width
                /// </summary>
                public readonly int Width;

                /// <summary>
                /// The height
                /// </summary>
                public readonly int Height;

                /// <summary>
                /// The DI bits
                /// </summary>
                public readonly byte[] DIBits;

                /// <summary>
                /// Initializes a new instance of the <see cref="CachedBitmapInfo"/> class.
                /// </summary>
                /// <param name="diBits">The di bits.</param>
                /// <param name="width">The width.</param>
                /// <param name="height">The height.</param>
                public CachedBitmapInfo(byte[] diBits, int width, int height)
                {
                    this.Width = width;
                    this.Height = height;
                    this.DIBits = diBits;
                }
            }

            #region GlowBitmap

            /// <summary>
            /// Disposes the native resources.
            /// </summary>
            protected override void DisposeNativeResources()
            {
                NativeMethodsPack.NativeMethods.DeleteObject(this._hBitmap);
            }

            #endregion GlowBitmap
        }

        /// <summary>
        /// Enum GlowBitmapPart
        /// </summary>
        private enum GlowBitmapPart
        {
            /// <summary>
            /// The corner top left
            /// </summary>
            CornerTopLeft,
            /// <summary>
            /// The corner top right
            /// </summary>
            CornerTopRight,
            /// <summary>
            /// The corner bottom left
            /// </summary>
            CornerBottomLeft,
            /// <summary>
            /// The corner bottom right
            /// </summary>
            CornerBottomRight,
            /// <summary>
            /// The top left
            /// </summary>
            TopLeft,
            /// <summary>
            /// The top
            /// </summary>
            Top,
            /// <summary>
            /// The top right
            /// </summary>
            TopRight,
            /// <summary>
            /// The left top
            /// </summary>
            LeftTop,
            /// <summary>
            /// The left
            /// </summary>
            Left,
            /// <summary>
            /// The left bottom
            /// </summary>
            LeftBottom,
            /// <summary>
            /// The bottom left
            /// </summary>
            BottomLeft,
            /// <summary>
            /// The bottom
            /// </summary>
            Bottom,
            /// <summary>
            /// The bottom right
            /// </summary>
            BottomRight,
            /// <summary>
            /// The right top
            /// </summary>
            RightTop,
            /// <summary>
            /// The right
            /// </summary>
            Right,
            /// <summary>
            /// The right bottom
            /// </summary>
            RightBottom
        }

        /// <summary>
        /// Class GlowDrawingContext
        /// </summary>
        private sealed class GlowDrawingContext : DisposableObject
        {
            #region var

            /// <summary>
            /// The HDC screen
            /// </summary>
            private readonly IntPtr hdcScreen;

            /// <summary>
            /// The HDC window
            /// </summary>
            private readonly IntPtr hdcWindow;

            /// <summary>
            /// The HDC background
            /// </summary>
            private readonly IntPtr hdcBackground;

            /// <summary>
            /// The window bitmap
            /// </summary>
            private readonly GlowBitmap windowBitmap;

            /// <summary>
            /// The blend
            /// </summary>
            public BLENDFUNCTION Blend;

            #endregion var

            #region properties

            /// <summary>
            /// Gets the background DC.
            /// </summary>
            /// <value>The background DC.</value>
            public IntPtr BackgroundDC { get { return this.hdcBackground; } }

            /// <summary>
            /// Gets the screen DC.
            /// </summary>
            /// <value>The screen DC.</value>
            public IntPtr ScreenDC { get { return this.hdcScreen; } }

            /// <summary>
            /// Gets the window DC.
            /// </summary>
            /// <value>The window DC.</value>
            public IntPtr WindowDC { get { return this.hdcWindow; } }

            /// <summary>
            /// Gets the height.
            /// </summary>
            /// <value>The height.</value>
            public int Height { get { return this.windowBitmap.Height; } }

            /// <summary>
            /// Gets the width.
            /// </summary>
            /// <value>The width.</value>
            public int Width { get { return this.windowBitmap.Width; } }

            #endregion properties

            #region Constructor

            /// <summary>
            /// Initializes a new instance of the <see cref="GlowDrawingContext"/> class.
            /// </summary>
            /// <param name="width">The width.</param>
            /// <param name="height">The height.</param>
            public GlowDrawingContext(int width, int height)
            {
                this.hdcScreen = NativeMethodsPack.NativeMethods.GetDC(IntPtr.Zero);
                if (this.hdcScreen != IntPtr.Zero)
                {
                    this.hdcWindow = NativeMethodsPack.NativeMethods.CreateCompatibleDC(this.hdcScreen);
                    if (this.hdcWindow != IntPtr.Zero)
                    {
                        this.hdcBackground = NativeMethodsPack.NativeMethods.CreateCompatibleDC(this.hdcScreen);
                        if (this.hdcBackground != IntPtr.Zero)
                        {
                            this.Blend.BlendOp = 0;
                            this.Blend.BlendFlags = 0;
                            this.Blend.SourceConstantAlpha = 255;
                            this.Blend.AlphaFormat = 1;
                            this.windowBitmap = new GlowBitmap(this.ScreenDC, width, height);
                            NativeMethodsPack.NativeMethods.SelectObject(this.hdcWindow, this.windowBitmap.Handle);
                        }
                    }
                }
            }

            #endregion Constructor

            #region DisposableObject

            /// <summary>
            /// Disposes the managed resources.
            /// </summary>
            protected override void DisposeManagedResources()
            {
                this.windowBitmap.Dispose();
            }

            /// <summary>
            /// Disposes the native resources.
            /// </summary>
            protected override void DisposeNativeResources()
            {
                if (this.hdcScreen != IntPtr.Zero)
                {
                    NativeMethodsPack.NativeMethods.ReleaseDC(IntPtr.Zero, this.hdcScreen);
                }
                if (this.hdcWindow != IntPtr.Zero)
                {
                    NativeMethodsPack.NativeMethods.DeleteDC(this.hdcWindow);
                }
                if (this.hdcBackground != IntPtr.Zero)
                {
                    NativeMethodsPack.NativeMethods.DeleteDC(this.hdcBackground);
                }
            }

            #endregion DisposableObject
        }

        /// <summary>
        /// Class GlowWindow
        /// </summary>
        private sealed class GlowWindow : HwndWrapper
        {
            #region Var

            /// <summary>
            /// The glow window class name
            /// </summary>
            private const string GlowWindowClassName = "VisualStudioGlowWindow";

            /// <summary>
            /// The glow depth
            /// </summary>
            private const int GlowDepth = 9;

            /// <summary>
            /// The corner grip thickness
            /// </summary>
            private const int CornerGripThickness = 18;

            /// <summary>
            /// The _target window
            /// </summary>
            private readonly CustomChromeWindow _targetWindow;

            /// <summary>
            /// The _orientation
            /// </summary>
            private readonly Dock _orientation;

            /// <summary>
            /// The _active glow bitmaps
            /// </summary>
            private readonly GlowBitmap[] _activeGlowBitmaps;

            /// <summary>
            /// The _inactive glow bitmaps
            /// </summary>
            private readonly GlowBitmap[] _inactiveGlowBitmaps;

            /// <summary>
            /// The _shared window class atom
            /// </summary>
            private static ushort _sharedWindowClassAtom;

            /// <summary>
            /// The _shared WND proc
            /// </summary>
            private static WndProc _sharedWndProc;

            /// <summary>
            /// The _left
            /// </summary>
            private int _left;

            /// <summary>
            /// The _top
            /// </summary>
            private int _top;

            /// <summary>
            /// The _width
            /// </summary>
            private int _width;

            /// <summary>
            /// The _height
            /// </summary>
            private int _height;

            /// <summary>
            /// The _is visible
            /// </summary>
            private bool _isVisible;

            /// <summary>
            /// The _is active
            /// </summary>
            private bool _isActive;

            /// <summary>
            /// The _active glow color
            /// </summary>
            private Color _activeGlowColor;

            /// <summary>
            /// The _inactive glow color
            /// </summary>
            private Color _inactiveGlowColor;

            /// <summary>
            /// The _invalidated values
            /// </summary>
            private FieldInvalidationTypes _invalidatedValues;

            /// <summary>
            /// The _pending delay render
            /// </summary>
            private bool _pendingDelayRender;

            #endregion Var

            #region Properties

            /// <summary>
            /// Gets or sets the color of the active glow.
            /// </summary>
            /// <value>The color of the active glow.</value>
            public Color ActiveGlowColor
            {
                get
                {
                    return this._activeGlowColor;
                }
                set
                {
                    this.UpdateProperty<Color>(ref this._activeGlowColor, value, FieldInvalidationTypes.ActiveColor | FieldInvalidationTypes.Render);
                }
            }

            /// <summary>
            /// Gets or sets the height.
            /// </summary>
            /// <value>The height.</value>
            public int Height
            {
                get
                {
                    return this._height;
                }
                set
                {
                    this.UpdateProperty<int>(ref this._height, value, FieldInvalidationTypes.Size | FieldInvalidationTypes.Render);
                }
            }

            /// <summary>
            /// Gets or sets the color of the inactive glow.
            /// </summary>
            /// <value>The color of the inactive glow.</value>
            public Color InactiveGlowColor
            {
                get
                {
                    return this._inactiveGlowColor;
                }
                set
                {
                    this.UpdateProperty<Color>(ref this._inactiveGlowColor, value, FieldInvalidationTypes.InactiveColor | FieldInvalidationTypes.Render);
                }
            }

            /// <summary>
            /// Gets or sets a value indicating whether this instance is active.
            /// </summary>
            /// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>
            public bool IsActive
            {
                get
                {
                    return this._isActive;
                }
                set
                {
                    this.UpdateProperty<bool>(ref this._isActive, value, FieldInvalidationTypes.Render);
                }
            }

            /// <summary>
            /// Gets a value indicating whether this instance is deferring changes.
            /// </summary>
            /// <value><c>true</c> if this instance is deferring changes; otherwise, <c>false</c>.</value>
            private bool IsDeferringChanges
            {
                get
                {
                    return this._targetWindow._deferGlowChangesCount > 0;
                }
            }

            /// <summary>
            /// Gets a value indicating whether this instance is position valid.
            /// </summary>
            /// <value><c>true</c> if this instance is position valid; otherwise, <c>false</c>.</value>
            private bool IsPositionValid
            {
                get
                {
                    return (this._invalidatedValues & (FieldInvalidationTypes.Location | FieldInvalidationTypes.Size | FieldInvalidationTypes.Visibility)) == FieldInvalidationTypes.None;
                }
            }

            /// <summary>
            /// Gets or sets a value indicating whether this instance is visible.
            /// </summary>
            /// <value><c>true</c> if this instance is visible; otherwise, <c>false</c>.</value>
            public bool IsVisible
            {
                get
                {
                    return this._isVisible;
                }
                set
                {
                    this.UpdateProperty<bool>(ref this._isVisible, value, FieldInvalidationTypes.Render | FieldInvalidationTypes.Visibility);
                }
            }

            /// <summary>
            /// Gets or sets the left.
            /// </summary>
            /// <value>The left.</value>
            public int Left
            {
                get
                {
                    return this._left;
                }
                set
                {
                    this.UpdateProperty<int>(ref this._left, value, FieldInvalidationTypes.Location);
                }
            }

            /// <summary>
            /// Gets the shared window class atom.
            /// </summary>
            /// <value>The shared window class atom.</value>
            private static ushort SharedWindowClassAtom
            {
                get
                {
                    if (GlowWindow._sharedWindowClassAtom == 0)
                    {
                        WNDCLASS zero = new WNDCLASS();
                        zero.cbClsExtra = 0;
                        zero.cbWndExtra = 0;
                        zero.hbrBackground = IntPtr.Zero;
                        zero.hCursor = IntPtr.Zero;
                        zero.hIcon = IntPtr.Zero;
                        WndProc wndProc = new WndProc(NativeMethodsPack.NativeMethods.DefWindowProc);
                        GlowWindow._sharedWndProc = wndProc;
                        zero.lpfnWndProc = wndProc;
                        zero.lpszClassName = "VisualStudioGlowWindow";
                        zero.lpszMenuName = null;
                        zero.style = 0;
                        GlowWindow._sharedWindowClassAtom = NativeMethodsPack.NativeMethods.RegisterClass(ref zero);
                    }
                    return GlowWindow._sharedWindowClassAtom;
                }
            }

            /// <summary>
            /// Gets the target window handle.
            /// </summary>
            /// <value>The target window handle.</value>
            private IntPtr TargetWindowHandle
            {
                get
                {
                    return (new WindowInteropHelper(this._targetWindow)).Handle;
                }
            }

            /// <summary>
            /// Gets or sets the top.
            /// </summary>
            /// <value>The top.</value>
            public int Top
            {
                get
                {
                    return this._top;
                }
                set
                {
                    this.UpdateProperty<int>(ref this._top, value, FieldInvalidationTypes.Location);
                }
            }

            /// <summary>
            /// Gets or sets the width.
            /// </summary>
            /// <value>The width.</value>
            public int Width
            {
                get
                {
                    return this._width;
                }
                set
                {
                    this.UpdateProperty<int>(ref this._width, value, FieldInvalidationTypes.Size | FieldInvalidationTypes.Render);
                }
            }

            #endregion Properties

            #region override HwndWrapper Properties

            /// <summary>
            /// Gets a value indicating whether this instance is window subclassed.
            /// </summary>
            /// <value><c>true</c> if this instance is window subclassed; otherwise, <c>false</c>.</value>
            protected override bool IsWindowSubclassed
            {
                get
                {
                    return true;
                }
            }

            #endregion override HwndWrapper Properties

            #region Constructor

            /// <summary>
            /// Initializes a new instance of the <see cref="GlowWindow"/> class.
            /// </summary>
            /// <param name="owner">The owner.</param>
            /// <param name="orientation">The orientation.</param>
            public GlowWindow(CustomChromeWindow owner, Dock orientation)
            {
                this._activeGlowBitmaps = new GlowBitmap[16];
                this._inactiveGlowBitmaps = new GlowBitmap[16];
                this._activeGlowColor = Colors.Transparent;
                this._inactiveGlowColor = Colors.Transparent;

                //Validate.IsNotNull(owner, "owner");
                this._targetWindow = owner;
                this._orientation = orientation;
            }

            #endregion Constructor

            #region override HwndWrapper Methods

            /// <summary>
            /// Creates the window class core.
            /// </summary>
            /// <returns>System.UInt16.</returns>
            protected override ushort CreateWindowClassCore()
            {
                return GlowWindow.SharedWindowClassAtom;
            }

            /// <summary>
            /// Creates the window core.
            /// </summary>
            /// <returns>IntPtr.</returns>
            protected override IntPtr CreateWindowCore()
            {
                return NativeMethodsPack.NativeMethods.CreateWindowEx(WS.WS_EX_LAYERED | WS.WS_EX_TOOLWINDOW, new IntPtr(base.WindowClassAtom), string.Empty, -2046820352, 0, 0, 0, 0, (new WindowInteropHelper(this._targetWindow)).Owner, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
            }

            /// <summary>
            /// Destroys the window class core.
            /// </summary>
            protected override void DestroyWindowClassCore()
            {
            }

            /// <summary>
            /// Disposes the managed resources.
            /// </summary>
            protected override void DisposeManagedResources()
            {
                this.ClearCache(this._activeGlowBitmaps);
                this.ClearCache(this._inactiveGlowBitmaps);
            }

            /// <summary>
            /// WNDs the proc.
            /// </summary>
            /// <param name="hwnd">The HWND.</param>
            /// <param name="msg">The MSG.</param>
            /// <param name="wParam">The w param.</param>
            /// <param name="lParam">The l param.</param>
            /// <returns>IntPtr.</returns>
            protected override IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam)
            {
                switch (msg)
                {
                    case 0xa1:
                    case 0xa3:
                    case 0xa4:
                    case 0xa6:
                    case 0xa7:
                    case 0xa9:
                    case 0xab:
                    case 0xad:
                        {
                            IntPtr targetWindowHandle = this.TargetWindowHandle;
                            NativeMethodsPack.NativeMethods.SendMessage(targetWindowHandle, 6, new IntPtr(2), IntPtr.Zero);
                            NativeMethodsPack.NativeMethods.SendMessage(targetWindowHandle, (uint)msg, wParam, IntPtr.Zero);
                            return IntPtr.Zero;
                        }
                    case 0x84:
                        return new IntPtr(this.WmNcHitTest(lParam));

                    case 0x7e:
                        if (this.IsVisible)
                        {
                            this.RenderLayeredWindow();
                        }
                        break;

                    case 6:
                        return IntPtr.Zero;

                    case 70:
                        {
                            WINDOWPOS structure = (WINDOWPOS)Marshal.PtrToStructure(lParam, typeof(WINDOWPOS));
                            structure.flags |= 0x10;
                            Marshal.StructureToPtr(structure, lParam, true);
                            break;
                        }
                }
                return base.WndProc(hwnd, msg, wParam, lParam);
            }

            #endregion override HwndWrapper Methods

            #region Methods

            /// <summary>
            /// Begins the delayed render.
            /// </summary>
            private void BeginDelayedRender()
            {
                if (!this._pendingDelayRender)
                {
                    this._pendingDelayRender = true;
                    CompositionTarget.Rendering += this.CommitDelayedRender;
                }
            }

            /// <summary>
            /// Cancels the delayed render.
            /// </summary>
            private void CancelDelayedRender()
            {
                if (this._pendingDelayRender)
                {
                    this._pendingDelayRender = false;
                    CompositionTarget.Rendering -= this.CommitDelayedRender;
                }
            }

            /// <summary>
            /// Changes the owner.
            /// </summary>
            /// <param name="newOwner">The new owner.</param>
            public void ChangeOwner(IntPtr newOwner)
            {
                NativeMethodsPack.NativeMethods.SetWindowLong(base.Handle, GWL_INDEX.GWL_HWNDPARENT, newOwner);
            }

            /// <summary>
            /// Clears the cache.
            /// </summary>
            /// <param name="cache">The cache.</param>
            private void ClearCache(GlowBitmap[] cache)
            {
                for (int i = 0; i < (int)cache.Length; i++)
                {
                    GlowBitmap glowBitmap = cache[i];
                    using (glowBitmap)
                    {
                        cache[i] = null;
                    }
                }
            }

            /// <summary>
            /// Commits the changes.
            /// </summary>
            public void CommitChanges()
            {
                this.InvalidateCachedBitmaps();
                this.UpdateWindowPosCore();
                this.UpdateLayeredWindowCore();
                this._invalidatedValues = FieldInvalidationTypes.None;
            }

            /// <summary>
            /// Commits the delayed render.
            /// </summary>
            /// <param name="sender">The sender.</param>
            /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
            private void CommitDelayedRender(object sender, EventArgs e)
            {
                this.CancelDelayedRender();
                if (this.IsVisible)
                {
                    this.RenderLayeredWindow();
                }
            }

            /// <summary>
            /// Draws the bottom.
            /// </summary>
            /// <param name="drawingContext">The drawing context.</param>
            private void DrawBottom(GlowDrawingContext drawingContext)
            {
                GlowBitmap orCreateBitmap = this.GetOrCreateBitmap(drawingContext, GlowBitmapPart.BottomLeft);
                GlowBitmap glowBitmap = this.GetOrCreateBitmap(drawingContext, GlowBitmapPart.Bottom);
                GlowBitmap orCreateBitmap1 = this.GetOrCreateBitmap(drawingContext, GlowBitmapPart.BottomRight);
                int num = 9;
                int width = num + orCreateBitmap.Width;
                int width1 = drawingContext.Width - 9 - orCreateBitmap1.Width;
                int num1 = width1 - width;
                NativeMethodsPack.NativeMethods.SelectObject(drawingContext.BackgroundDC, orCreateBitmap.Handle);
                NativeMethodsPack.NativeMethods.AlphaBlend(drawingContext.WindowDC, num, 0, orCreateBitmap.Width, orCreateBitmap.Height, drawingContext.BackgroundDC, 0, 0, orCreateBitmap.Width, orCreateBitmap.Height, drawingContext.Blend);
                if (num1 > 0)
                {
                    NativeMethodsPack.NativeMethods.SelectObject(drawingContext.BackgroundDC, glowBitmap.Handle);
                    NativeMethodsPack.NativeMethods.AlphaBlend(drawingContext.WindowDC, width, 0, num1, glowBitmap.Height, drawingContext.BackgroundDC, 0, 0, glowBitmap.Width, glowBitmap.Height, drawingContext.Blend);
                }
                NativeMethodsPack.NativeMethods.SelectObject(drawingContext.BackgroundDC, orCreateBitmap1.Handle);
                NativeMethodsPack.NativeMethods.AlphaBlend(drawingContext.WindowDC, width1, 0, orCreateBitmap1.Width, orCreateBitmap1.Height, drawingContext.BackgroundDC, 0, 0, orCreateBitmap1.Width, orCreateBitmap1.Height, drawingContext.Blend);
            }

            /// <summary>
            /// Draws the left.
            /// </summary>
            /// <param name="drawingContext">The drawing context.</param>
            private void DrawLeft(GlowDrawingContext drawingContext)
            {
                GlowBitmap orCreateBitmap = this.GetOrCreateBitmap(drawingContext, GlowBitmapPart.CornerTopLeft);
                GlowBitmap glowBitmap = this.GetOrCreateBitmap(drawingContext, GlowBitmapPart.LeftTop);
                GlowBitmap orCreateBitmap1 = this.GetOrCreateBitmap(drawingContext, GlowBitmapPart.Left);
                GlowBitmap glowBitmap1 = this.GetOrCreateBitmap(drawingContext, GlowBitmapPart.LeftBottom);
                GlowBitmap orCreateBitmap2 = this.GetOrCreateBitmap(drawingContext, GlowBitmapPart.CornerBottomLeft);
                int height = orCreateBitmap.Height;
                int num = height + glowBitmap.Height;
                int height1 = drawingContext.Height - orCreateBitmap2.Height;
                int num1 = height1 - glowBitmap1.Height;
                int num2 = num1 - num;
                NativeMethodsPack.NativeMethods.SelectObject(drawingContext.BackgroundDC, orCreateBitmap.Handle);
                NativeMethodsPack.NativeMethods.AlphaBlend(drawingContext.WindowDC, 0, 0, orCreateBitmap.Width, orCreateBitmap.Height, drawingContext.BackgroundDC, 0, 0, orCreateBitmap.Width, orCreateBitmap.Height, drawingContext.Blend);
                NativeMethodsPack.NativeMethods.SelectObject(drawingContext.BackgroundDC, glowBitmap.Handle);
                NativeMethodsPack.NativeMethods.AlphaBlend(drawingContext.WindowDC, 0, height, glowBitmap.Width, glowBitmap.Height, drawingContext.BackgroundDC, 0, 0, glowBitmap.Width, glowBitmap.Height, drawingContext.Blend);
                if (num2 > 0)
                {
                    NativeMethodsPack.NativeMethods.SelectObject(drawingContext.BackgroundDC, orCreateBitmap1.Handle);
                    NativeMethodsPack.NativeMethods.AlphaBlend(drawingContext.WindowDC, 0, num, orCreateBitmap1.Width, num2, drawingContext.BackgroundDC, 0, 0, orCreateBitmap1.Width, orCreateBitmap1.Height, drawingContext.Blend);
                }
                NativeMethodsPack.NativeMethods.SelectObject(drawingContext.BackgroundDC, glowBitmap1.Handle);
                NativeMethodsPack.NativeMethods.AlphaBlend(drawingContext.WindowDC, 0, num1, glowBitmap1.Width, glowBitmap1.Height, drawingContext.BackgroundDC, 0, 0, glowBitmap1.Width, glowBitmap1.Height, drawingContext.Blend);
                NativeMethodsPack.NativeMethods.SelectObject(drawingContext.BackgroundDC, orCreateBitmap2.Handle);
                NativeMethodsPack.NativeMethods.AlphaBlend(drawingContext.WindowDC, 0, height1, orCreateBitmap2.Width, orCreateBitmap2.Height, drawingContext.BackgroundDC, 0, 0, orCreateBitmap2.Width, orCreateBitmap2.Height, drawingContext.Blend);
            }

            /// <summary>
            /// Draws the right.
            /// </summary>
            /// <param name="drawingContext">The drawing context.</param>
            private void DrawRight(GlowDrawingContext drawingContext)
            {
                GlowBitmap orCreateBitmap = this.GetOrCreateBitmap(drawingContext, GlowBitmapPart.CornerTopRight);
                GlowBitmap glowBitmap = this.GetOrCreateBitmap(drawingContext, GlowBitmapPart.RightTop);
                GlowBitmap orCreateBitmap1 = this.GetOrCreateBitmap(drawingContext, GlowBitmapPart.Right);
                GlowBitmap glowBitmap1 = this.GetOrCreateBitmap(drawingContext, GlowBitmapPart.RightBottom);
                GlowBitmap orCreateBitmap2 = this.GetOrCreateBitmap(drawingContext, GlowBitmapPart.CornerBottomRight);
                int height = orCreateBitmap.Height;
                int num = height + glowBitmap.Height;
                int height1 = drawingContext.Height - orCreateBitmap2.Height;
                int num1 = height1 - glowBitmap1.Height;
                int num2 = num1 - num;
                NativeMethodsPack.NativeMethods.SelectObject(drawingContext.BackgroundDC, orCreateBitmap.Handle);
                NativeMethodsPack.NativeMethods.AlphaBlend(drawingContext.WindowDC, 0, 0, orCreateBitmap.Width, orCreateBitmap.Height, drawingContext.BackgroundDC, 0, 0, orCreateBitmap.Width, orCreateBitmap.Height, drawingContext.Blend);
                NativeMethodsPack.NativeMethods.SelectObject(drawingContext.BackgroundDC, glowBitmap.Handle);
                NativeMethodsPack.NativeMethods.AlphaBlend(drawingContext.WindowDC, 0, height, glowBitmap.Width, glowBitmap.Height, drawingContext.BackgroundDC, 0, 0, glowBitmap.Width, glowBitmap.Height, drawingContext.Blend);
                if (num2 > 0)
                {
                    NativeMethodsPack.NativeMethods.SelectObject(drawingContext.BackgroundDC, orCreateBitmap1.Handle);
                    NativeMethodsPack.NativeMethods.AlphaBlend(drawingContext.WindowDC, 0, num, orCreateBitmap1.Width, num2, drawingContext.BackgroundDC, 0, 0, orCreateBitmap1.Width, orCreateBitmap1.Height, drawingContext.Blend);
                }
                NativeMethodsPack.NativeMethods.SelectObject(drawingContext.BackgroundDC, glowBitmap1.Handle);
                NativeMethodsPack.NativeMethods.AlphaBlend(drawingContext.WindowDC, 0, num1, glowBitmap1.Width, glowBitmap1.Height, drawingContext.BackgroundDC, 0, 0, glowBitmap1.Width, glowBitmap1.Height, drawingContext.Blend);
                NativeMethodsPack.NativeMethods.SelectObject(drawingContext.BackgroundDC, orCreateBitmap2.Handle);
                NativeMethodsPack.NativeMethods.AlphaBlend(drawingContext.WindowDC, 0, height1, orCreateBitmap2.Width, orCreateBitmap2.Height, drawingContext.BackgroundDC, 0, 0, orCreateBitmap2.Width, orCreateBitmap2.Height, drawingContext.Blend);
            }

            /// <summary>
            /// Draws the top.
            /// </summary>
            /// <param name="drawingContext">The drawing context.</param>
            private void DrawTop(GlowDrawingContext drawingContext)
            {
                GlowBitmap orCreateBitmap = this.GetOrCreateBitmap(drawingContext, GlowBitmapPart.TopLeft);
                GlowBitmap glowBitmap = this.GetOrCreateBitmap(drawingContext, GlowBitmapPart.Top);
                GlowBitmap orCreateBitmap1 = this.GetOrCreateBitmap(drawingContext, GlowBitmapPart.TopRight);
                int num = 9;
                int width = num + orCreateBitmap.Width;
                int width1 = drawingContext.Width - 9 - orCreateBitmap1.Width;
                int num1 = width1 - width;
                NativeMethodsPack.NativeMethods.SelectObject(drawingContext.BackgroundDC, orCreateBitmap.Handle);
                NativeMethodsPack.NativeMethods.AlphaBlend(drawingContext.WindowDC, num, 0, orCreateBitmap.Width, orCreateBitmap.Height, drawingContext.BackgroundDC, 0, 0, orCreateBitmap.Width, orCreateBitmap.Height, drawingContext.Blend);
                if (num1 > 0)
                {
                    NativeMethodsPack.NativeMethods.SelectObject(drawingContext.BackgroundDC, glowBitmap.Handle);
                    NativeMethodsPack.NativeMethods.AlphaBlend(drawingContext.WindowDC, width, 0, num1, glowBitmap.Height, drawingContext.BackgroundDC, 0, 0, glowBitmap.Width, glowBitmap.Height, drawingContext.Blend);
                }
                NativeMethodsPack.NativeMethods.SelectObject(drawingContext.BackgroundDC, orCreateBitmap1.Handle);
                NativeMethodsPack.NativeMethods.AlphaBlend(drawingContext.WindowDC, width1, 0, orCreateBitmap1.Width, orCreateBitmap1.Height, drawingContext.BackgroundDC, 0, 0, orCreateBitmap1.Width, orCreateBitmap1.Height, drawingContext.Blend);
            }

            /// <summary>
            /// Gets the or create bitmap.
            /// </summary>
            /// <param name="drawingContext">The drawing context.</param>
            /// <param name="bitmapPart">The bitmap part.</param>
            /// <returns>GlowBitmap.</returns>
            private GlowBitmap GetOrCreateBitmap(GlowDrawingContext drawingContext, GlowBitmapPart bitmapPart)
            {
                GlowBitmap[] glowBitmapArray;
                Color inactiveGlowColor;
                if (!this.IsActive)
                {
                    glowBitmapArray = this._inactiveGlowBitmaps;
                    inactiveGlowColor = this.InactiveGlowColor;
                }
                else
                {
                    glowBitmapArray = this._activeGlowBitmaps;
                    inactiveGlowColor = this.ActiveGlowColor;
                }
                int num = (int)bitmapPart;
                if (glowBitmapArray[num] == null)
                {
                    glowBitmapArray[num] = GlowBitmap.Create(drawingContext, bitmapPart, inactiveGlowColor);
                }
                return glowBitmapArray[num];
            }

            /// <summary>
            /// Invalidates the cached bitmaps.
            /// </summary>
            private void InvalidateCachedBitmaps()
            {
                if (this._invalidatedValues.HasFlag(FieldInvalidationTypes.ActiveColor))
                {
                    this.ClearCache(this._activeGlowBitmaps);
                }
                if (this._invalidatedValues.HasFlag(FieldInvalidationTypes.InactiveColor))
                {
                    this.ClearCache(this._inactiveGlowBitmaps);
                }
            }

            /// <summary>
            /// Renders the layered window.
            /// </summary>
            private void RenderLayeredWindow()
            {
                using (GlowDrawingContext glowDrawingContext = new GlowDrawingContext(this.Width, this.Height))
                {
                    Dock dock = this._orientation;
                    switch (dock)
                    {
                        case Dock.Left:
                            {
                                this.DrawLeft(glowDrawingContext);
                                break;
                            }
                        case Dock.Top:
                            {
                                this.DrawTop(glowDrawingContext);
                                break;
                            }
                        case Dock.Right:
                            {
                                this.DrawRight(glowDrawingContext);
                                break;
                            }
                        default:
                            {
                                this.DrawBottom(glowDrawingContext);
                                break;
                            }
                    }
                    POINT left = new POINT();
                    left.x = this.Left;
                    left.y = this.Top;
                    POINT pOINT = left;
                    SIZE width = new SIZE();
                    width.cx = this.Width;
                    width.cy = this.Height;
                    SIZE sIZE = width;
                    POINT pOINT1 = new POINT();
                    pOINT1.x = 0;
                    pOINT1.y = 0;
                    POINT pOINT2 = pOINT1;
                    NativeMethodsPack.NativeMethods.UpdateLayeredWindow(base.Handle, glowDrawingContext.ScreenDC, ref pOINT, ref sIZE, glowDrawingContext.WindowDC, ref pOINT2, 0, ref glowDrawingContext.Blend, 2);
                }
            }

            /// <summary>
            /// Updates the layered window core.
            /// </summary>
            private void UpdateLayeredWindowCore()
            {
                if (this.IsVisible && this._invalidatedValues.HasFlag(FieldInvalidationTypes.Render))
                {
                    if (this.IsPositionValid)
                    {
                        this.BeginDelayedRender();
                    }
                    else
                    {
                        this.CancelDelayedRender();
                        this.RenderLayeredWindow();
                    }
                }
            }

            /// <summary>
            /// Updates the property.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="field">The field.</param>
            /// <param name="value">The value.</param>
            /// <param name="invalidatedValues">The invalidated values.</param>
            private void UpdateProperty<T>(ref T field, T value, FieldInvalidationTypes invalidatedValues)
            where T : struct
            {
                if (!field.Equals(value))
                {
                    field = value;
                    GlowWindow glowWindow = this;
                    glowWindow._invalidatedValues = glowWindow._invalidatedValues | invalidatedValues;
                    if (this.IsDeferringChanges)
                    {
                        this.CommitChanges();
                    }
                }
            }

            /// <summary>
            /// Updates the window pos.
            /// </summary>
            public void UpdateWindowPos()
            {
                RECT rECT;
                IntPtr targetWindowHandle = this.TargetWindowHandle;
                NativeMethodsPack.NativeMethods.GetWindowRect(targetWindowHandle, out rECT);
                NativeMethodsPack.NativeMethods.GetWindowPlacement(targetWindowHandle);
                if (this.IsVisible)
                {
                    Dock dock = this._orientation;
                    switch (dock)
                    {
                        case Dock.Left:
                            {
                                this.Left = rECT.Left - 9;
                                this.Top = rECT.Top - 9;
                                this.Width = 9;
                                this.Height = rECT.Height + 18;
                                return;
                            }
                        case Dock.Top:
                            {
                                this.Left = rECT.Left - 9;
                                this.Top = rECT.Top - 9;
                                this.Width = rECT.Width + 18;
                                this.Height = 9;
                                return;
                            }
                        case Dock.Right:
                            {
                                this.Left = rECT.Right;
                                this.Top = rECT.Top - 9;
                                this.Width = 9;
                                this.Height = rECT.Height + 18;
                                return;
                            }
                        default:
                            {
                                this.Left = rECT.Left - 9;
                                this.Top = rECT.Bottom;
                                this.Width = rECT.Width + 18;
                                this.Height = 9;
                                break;
                            }
                    }
                }
            }

            /// <summary>
            /// Updates the window pos core.
            /// </summary>
            private void UpdateWindowPosCore()
            {
                if (this._invalidatedValues.HasFlag(FieldInvalidationTypes.Location) || this._invalidatedValues.HasFlag(FieldInvalidationTypes.Size) || this._invalidatedValues.HasFlag(FieldInvalidationTypes.Visibility))
                {
                    int num = 532;
                    if (this._invalidatedValues.HasFlag(FieldInvalidationTypes.Visibility))
                    {
                        if (!this.IsVisible)
                        {
                            num = num | 131;
                        }
                        else
                        {
                            num = num | 64;
                        }
                    }
                    if (!this._invalidatedValues.HasFlag(FieldInvalidationTypes.Location))
                    {
                        num = num | 2;
                    }
                    if (!this._invalidatedValues.HasFlag(FieldInvalidationTypes.Size))
                    {
                        num = num | 1;
                    }
                    NativeMethodsPack.NativeMethods.SetWindowPos(base.Handle, IntPtr.Zero, this.Left, this.Top, this.Width, this.Height, (uint)num);
                }
            }

            /// <summary>
            /// Wms the nc hit test.
            /// </summary>
            /// <param name="lParam">The l param.</param>
            /// <returns>System.Int32.</returns>
            private int WmNcHitTest(IntPtr lParam)
            {
                RECT rECT;
                int xLParam = NativeMethodsPack.NativeMethods.GetXLParam(lParam.ToInt32());
                int yLParam = NativeMethodsPack.NativeMethods.GetYLParam(lParam.ToInt32());
                NativeMethodsPack.NativeMethods.GetWindowRect(base.Handle, out rECT);
                Dock dock = this._orientation;
                switch (dock)
                {
                    case Dock.Left:
                        {
                            if (yLParam - 18 >= rECT.Top)
                            {
                                if (yLParam + 18 <= rECT.Bottom)
                                {
                                    return 10;
                                }
                                else
                                {
                                    return 16;
                                }
                            }
                            else
                            {
                                return 13;
                            }
                        }
                    case Dock.Top:
                        {
                            if (xLParam - 18 >= rECT.Left)
                            {
                                if (xLParam + 18 <= rECT.Right)
                                {
                                    return 12;
                                }
                                else
                                {
                                    return 14;
                                }
                            }
                            else
                            {
                                return 13;
                            }
                        }
                    case Dock.Right:
                        {
                            if (yLParam - 18 >= rECT.Top)
                            {
                                if (yLParam + 18 <= rECT.Bottom)
                                {
                                    return 11;
                                }
                                else
                                {
                                    return 17;
                                }
                            }
                            else
                            {
                                return 14;
                            }
                        }
                    default:
                        {
                            if (xLParam - 18 < rECT.Left)
                            {
                                break;
                            }
                            if (xLParam + 18 <= rECT.Right)
                            {
                                return 15;
                            }
                            else
                            {
                                return 17;
                            }
                        }
                }
                return 16;
            }

            #endregion Methods

            /// <summary>
            /// Enum FieldInvalidationTypes
            /// </summary>
            [Flags]
            private enum FieldInvalidationTypes
            {
                /// <summary>
                /// The none
                /// </summary>
                None = 0,
                /// <summary>
                /// The location
                /// </summary>
                Location = 1,
                /// <summary>
                /// The size
                /// </summary>
                Size = 2,
                /// <summary>
                /// The active color
                /// </summary>
                ActiveColor = 4,
                /// <summary>
                /// The inactive color
                /// </summary>
                InactiveColor = 8,
                /// <summary>
                /// The render
                /// </summary>
                Render = 16,
                /// <summary>
                /// The visibility
                /// </summary>
                Visibility = 32
            }
        }

        #endregion private class or enum
    }
}