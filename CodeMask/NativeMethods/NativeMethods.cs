using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Windows;
using System.Windows.Interop;

namespace NativeMethodsPack
{
    /// <summary>
    /// Class NativeMethods
    /// </summary>
    public partial class NativeMethods
    {
        /// <summary>
        /// 该函数分发一个消息给窗口程序。通常消息从GetMessage函数获得。消息被分发到回调函数（过程函数)，作用是消息传递给操作系统，然后操作系统去调用我们的回调函数，也就是说我们在窗体的过程函数中处理消息。
        /// </summary>
        /// <param name="lpmsg">指向含有消息的MSG结构的指针。</param>
        /// <returns>返回值是窗口程序返回的值。尽管返回值的含义依赖于被调度的消息，但返回值通常被忽略。</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr DispatchMessage([In] ref MSG lpmsg);

        #region Window Class Functions

        /// <summary>
        /// 该函数获得指定窗口所属的类的类名。
        /// </summary>
        /// <param name="hWnd">窗口的句柄及间接给出的窗口所属的类。</param>
        /// <param name="lpClassName">指向接收窗口类名字符串的缓冲区的指针。</param>
        /// <param name="nMaxCount">指定由参数lpClassName指示的缓冲区的字节数。如果类名字符串大于缓冲区的长度，则多出的部分被截断。</param>
        /// <returns>如果函数成功，返回值为拷贝到指定缓冲区的字符个数：如果函数失败，返回值为0。若想获得更多错误信息，请调用GetLastError函数。</returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        /// <summary>
        /// Sets the window long32.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="nIndex">Index of the n.</param>
        /// <param name="dwNewLong">The dw new long.</param>
        /// <returns>System.Int32.</returns>
        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        public static extern int SetWindowLong32(IntPtr hWnd, int nIndex, int dwNewLong);

        /// <summary>
        /// Gets the window long PTR32.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="nIndex">Index of the n.</param>
        /// <returns>System.Int32.</returns>
        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        public static extern int GetWindowLongPtr32(IntPtr hWnd, int nIndex);

        #endregion Window Class Functions

        #region Window Functions

        /// <summary>
        /// 该函数依据所需客户矩形的大小，计算需要的窗口矩形的大小。计算出的窗口矩形随后可以传递给CreateWindow函数，用于创建一个客户区所需大小的窗口。
        /// </summary>
        /// <param name="lpRect">指向RECT结构的指针，该结构包含所需客户区域的左上角和右下角的坐标。函数返回时，该结构容纳所需客户区域的窗口的左上角和右下角的坐标。</param>
        /// <param name="dwStyle">指定将被计算尺寸的窗口的窗口风格。</param>
        /// <param name="bMenu">指示窗口是否有菜单。如窗口有菜单，则设为1（非零）。</param>
        /// <returns>如果函数成功，返回值为非零；如果函数失败，返回值为零。获取错误信息，参看GetLastError。</returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern Boolean AdjustWindowRect(ref RECT lpRect, UInt32 dwStyle, bool bMenu);

        /// <summary>
        /// 该函数依据所需客户矩形大小，计算需要的窗口矩形的大小。计算出的窗口矩形随后可以传送给CreateWindowEx函数，用于创建一个客户区所需大小的窗口。
        /// </summary>
        /// <param name="lpRect">指向RECT结构的指针，该结构包含所需客户区域的左上角和右下角的坐标。函数返回时，该结构包含容纳所需客户区域的窗口的左上角和右下角的坐标。</param>
        /// <param name="dwStyle">指定将被计算尺寸的窗口的窗口风格。</param>
        /// <param name="bMenu">指示窗口是否有菜单。</param>
        /// <param name="dwExStyle">指定将被计算尺寸的窗口的扩展窗口风格。</param>
        /// <returns>如果函数成功，返回值为非零；如果函数失败，返回值为零。若想获得更多错误信息，请调用GetLastError函数。</returns>
        [DllImport("user32.dll")]
        public static extern bool AdjustWindowRectEx(ref RECT lpRect, uint dwStyle,
          bool bMenu, uint dwExStyle);

        /// <summary>
        /// 排列一个父窗口的最小化子窗口（在vb里使用：用于在桌面排列图标，用GetDesktopWindow函数获得桌面窗口的一个句柄）。
        /// </summary>
        /// <param name="hWnd">父窗口的句柄。</param>
        /// <returns>图标行的高度；如失败，则返回零。</returns>
        [DllImport("user32.dll")]
        public static extern uint ArrangeIconicWindows(IntPtr hWnd);

        /// <summary>
        /// 该函数将指定的窗口设置到Z序的顶部。如果窗口为顶层窗口，则该窗口被激活；如果窗口为子窗口，则相应的顶级父窗口被激活。
        /// </summary>
        /// <param name="hWnd">设置到Z序的顶部的窗口句柄。</param>
        /// <returns>如果函数成功，返回值为非零；如果函数失败，返回值为零。若想获得更多错误信息，请调用GetLastError函数。</returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool BringWindowToTop(IntPtr hWnd);

        /// <summary>
        /// Cascades the windows.
        /// </summary>
        /// <param name="hwndParent">The HWND parent.</param>
        /// <param name="wHow">The w how.</param>
        /// <param name="lpRect">The lp rect.</param>
        /// <param name="cKids">The c kids.</param>
        /// <param name="lpKids">The lp kids.</param>
        /// <returns>System.UInt16.</returns>
        [DllImport("user32.dll")]
        public static extern ushort CascadeWindows(IntPtr hwndParent, uint wHow,
          IntPtr lpRect, uint cKids, IntPtr[] lpKids);

        /// <summary>
        /// 返回父窗口中包含了指定点的第一个子窗口的句柄。
        /// </summary>
        /// <param name="hWndParent">hWnd 父窗口的句柄。</param>
        /// <param name="Point">指定一个POINT结构，该结构定义了被检查的点的坐标。</param>
        /// <returns>发现包含了指定点的第一个子窗口的句柄。如未发现任何窗口，则返回hWnd（父窗口的句柄）。如指定点位于父窗口外部，则返回零。</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr ChildWindowFromPoint(IntPtr hWndParent, Point Point);

        /// <summary>
        /// 该函数确定属于父窗口的哪一个子窗口（如果存在的话）包含着指定的点。该函数可以忽略不可见的、禁止的和透明的子窗口。
        /// </summary>
        /// <param name="hWndParent">父窗口句柄。</param>
        /// <param name="pt">指定一个POINT结构，该结构定义了被检查的点的坐标。</param>
        /// <param name="uFlags">指明忽略的子窗口的类型。</param>
        /// <returns>返回值为包含该点并且满足由uFlags定义的规则的第一个子窗口的句柄。如果该点在父窗口内，但在任一满足条件的子窗口外，则返回值为父窗口句柄。如果该点在父窗口之外或函数失败，则返回值为NULL。</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr ChildWindowFromPointEx(IntPtr hWndParent, Point pt, uint uFlags);

        /// <summary>
        /// 该函数最小化指定的窗口，但并不销毁该窗口。
        /// </summary>
        /// <param name="hWnd">将要最小化的窗口的句柄。</param>
        /// <returns>如果函数成功，返回值为非零；如果函数失败，返回值为零。若想获得更多错误信息，请调用GetLastError函数。</returns>
        [DllImport("user32.dll")]
        public static extern bool CloseWindow(IntPtr hWnd);

        //[DllImport("user32.dll", SetLastError = true)]
        //public static extern IntPtr CreateWindowEx(
        //  WindowStylesEx dwExStyle,
        //  string lpClassName,
        //  string lpWindowName,
        //  WindowStyles dwStyle,
        //  int x,
        //  int y,
        //  int nWidth,
        //  int nHeight,
        //  IntPtr hWndParent,
        //  IntPtr hMenu,
        //  IntPtr hInstance,
        //  IntPtr lpParam);
        /// <summary>
        /// Creates the window ex.
        /// </summary>
        /// <param name="dwExStyle">The dw ex style.</param>
        /// <param name="lpClassName">Name of the lp class.</param>
        /// <param name="lpWindowName">Name of the lp window.</param>
        /// <param name="dwStyle">The dw style.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="nWidth">Width of the n.</param>
        /// <param name="nHeight">Height of the n.</param>
        /// <param name="hWndParent">The h WND parent.</param>
        /// <param name="hMenu">The h menu.</param>
        /// <param name="hInstance">The h instance.</param>
        /// <param name="lpParam">The lp param.</param>
        /// <returns>IntPtr.</returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr CreateWindowEx(
          int dwExStyle,
          IntPtr lpClassName,
          string lpWindowName,
          int dwStyle,
          int x,
          int y,
          int nWidth,
          int nHeight,
          IntPtr hWndParent,
          IntPtr hMenu,
          IntPtr hInstance,
          IntPtr lpParam);

        /// <summary>
        /// Creates the window ex.
        /// </summary>
        /// <param name="dwExStyle">The dw ex style.</param>
        /// <param name="className">Name of the class.</param>
        /// <param name="lpWindowName">Name of the lp window.</param>
        /// <param name="dwStyle">The dw style.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="nWidth">Width of the n.</param>
        /// <param name="nHeight">Height of the n.</param>
        /// <param name="hWndParent">The h WND parent.</param>
        /// <param name="hMenu">The h menu.</param>
        /// <param name="hInstance">The h instance.</param>
        /// <param name="lpParam">The lp param.</param>
        /// <returns>IntPtr.</returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr CreateWindowEx(
            int dwExStyle,
            string className,
            string lpWindowName,
            int dwStyle,
            int x,
            int y,
            int nWidth,
            int nHeight,
            IntPtr hWndParent,
            IntPtr hMenu,
            IntPtr hInstance,
            IntPtr lpParam);

        /// <summary>
        /// 销毁指定的窗口。这个函数通过发送WM_DESTROY 消息和 WM_NCDESTROY 消息使窗口无效并移除其键盘焦点。这个函数还销毁窗口的菜单，清空线程的消息队列，销毁与窗口过程相关的定时器，解除窗口对剪贴板的拥有权，打断剪贴板器的查看链。
        /// </summary>
        /// <param name="hwnd">将被销毁的窗口的句柄。</param>
        /// <returns>如果函数成功，返回值为非零：如果函数失败，返回值为零。若想获得更多错误信息，请调用GetLastError函数。</returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DestroyWindow(IntPtr hwnd);

        /// <summary>
        /// Enums the child windows.
        /// </summary>
        /// <param name="hwndParent">The HWND parent.</param>
        /// <param name="lpEnumFunc">The lp enum func.</param>
        /// <param name="lParam">The l param.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr hwndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);

        /// <summary>
        /// 该函数获取指定窗口的父窗口的句柄。
        /// </summary>
        /// <param name="hwnd">窗口的句柄。（按值传递）</param>
        /// <param name="gaFlags">指窗口类型。</param>
        /// <returns>该函数返回父窗口句柄</returns>
        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern IntPtr GetAncestor(IntPtr hwnd, uint gaFlags);

        /// <summary>
        /// 该函数获取窗口客户区的坐标。客户区坐标指定客户区的左上角和右下角。由于客户区坐标是相对窗口客户区的左上角而言的，因此左上角坐标为（0，0）。
        /// </summary>
        /// <param name="hWnd">程序窗口的句柄。</param>
        /// <param name="lpRect">是一个指针，指向一个RECT类型的rectangle结构。该结构有四个LONG字段,分别为left、top、right和bottom。GetClientRect将这四个字段设定为窗口显示区域的尺寸。left和top字段通常设定为0。right和bottom字段设定为显示区域的宽度和高度（像素点数）。 也可以是一个CRect对象指针。CRect对象有多个参数，与RECT用法相同。</param>
        /// <returns>如果函数成功，返回一个非零值。
        /// 如果函数失败，返回零。要得到更多的错误信息，请使用GetLastError函数。</returns>
        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        /// <summary>
        /// 获取一个前台窗口的句柄。该系统分配给其他线程比它的前台窗口的线程创建一个稍微更高的优先级。
        /// </summary>
        /// <returns>返回值是一个前台窗口的句柄。在某些情况下，如一个窗口失去激活时，前台窗口可以是NULL。</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        /// <summary>
        /// Gets the layered window attributes.
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        /// <param name="crKey">The cr key.</param>
        /// <param name="bAlpha">The b alpha.</param>
        /// <param name="dwFlags">The dw flags.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetLayeredWindowAttributes(IntPtr hwnd, out uint crKey, out byte bAlpha, out uint dwFlags);

        /// <summary>
        /// 该函数获得一个指定子窗口的父窗口句柄。
        /// </summary>
        /// <param name="hWnd">子窗口句柄。</param>
        /// <returns>如果函数成功，返回值为父窗口句柄。如果窗口无父窗口，则函数返回NULL。若想获得更多错误信息，请调用GetLastError函数。</returns>
        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetParent(IntPtr hWnd);

        //[DllImport("user32.dll")]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //private static extern bool GetTitleBarInfo(IntPtr hwnd, ref TITLEBARINFO pti);

        /// <summary>
        /// 该函数检查与特定父窗口相联的子窗口z序，并返回在z序顶部的子窗口的句柄。
        /// </summary>
        /// <param name="hWnd">被查序的父窗口的句柄。如果该参数为NULL，函数返回Z序顶部的窗口句柄。</param>
        /// <returns>如果函数成功，返回值为在Z序顶部的子窗口句柄。如果指定的窗口无子窗口，返回值为NULL。
        /// 若想获得更多错误信息，请调用GetLastError函数。</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetTopWindow(IntPtr hWnd);

        /// <summary>
        /// 该函数返回与指定窗口有特定关系（如Z序或所有者）的窗口句柄。
        /// </summary>
        /// <param name="hWnd">窗口句柄。要获得的窗口句柄是依据nCmd参数值相对于这个窗口的句柄。</param>
        /// <param name="uCmd">说明指定窗口与要获得句柄的窗口之间的关系。</param>
        /// <returns>如果函数成功，返回值为窗口句柄；如果与指定窗口有特定关系的窗口不存在，则返回值为NULL。</returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

        /// <summary>
        /// 检索有关窗口的信息。
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        /// <param name="pwi">为 WINDOWINFO 结构的指针。</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowInfo(IntPtr hwnd, ref WINDOWINFO pwi);

        /// <summary>
        /// 该函数返回指定窗口的显示状态以及被恢复的、最大化的和最小化的窗口位置。
        /// </summary>
        /// <param name="hWnd">窗口句柄。</param>
        /// <param name="lpwndpl">指向WINDOWPLACEMENT结构的指针，该结构存贮显示状态和位置信息。</param>
        /// <returns>如果函数成功，返回值为非零；如果函数失败，返回值为零。若想获得更多错误信息，请调用GetlastError函数。</returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowPlacement(IntPtr hWnd, out WINDOWPLACEMENT lpwndpl);

        /// <summary>
        /// 该函数返回指定窗口的边框矩形的尺寸。该尺寸以相对于屏幕坐标左上角的屏幕坐标给出。
        /// </summary>
        /// <param name="hwnd">窗口句柄。</param>
        /// <param name="lpRect">指向一个RECT结构的指针，该结构接收窗口的左上角和右下角的屏幕坐标。</param>
        /// <returns>如果函数成功，返回值为非零：如果函数失败，返回值为零。若想获得更多错误信息，请调用GetLastError函数。</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(HandleRef hwnd, out RECT lpRect);

        /// <summary>
        /// 该函数将指定窗口的标题条文本（如果存在）拷贝到一个缓存区内。如果指定的窗口是一个控件，则拷贝控件的文本。但是，GetWindowText不能接收其他应用程序中控件的文本。
        /// </summary>
        /// <param name="hWnd">带文本的窗口或控件的句柄。</param>
        /// <param name="lpString">指向接收文本的缓冲区的指针。</param>
        /// <param name="nMaxCount">指定要保存在缓冲区内的字符的最大个数，其中包含NULL字符。如果文本超过界限，它就被截断。</param>
        /// <returns>如果函数成功，返回值是拷贝的字符串的字符个数，不包括中断的空字符；如果窗口无标题栏或文本，或标题栏为空，或窗口或控制的句柄无效，则返回值为零。若想获得更多错误信息，请调用GetLastError函数。</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        /// <summary>
        /// 该函数返回指定窗口的标题文本（如果存在）的字符长度。如果指定窗口是一个控件，函数将返回控制内文本的长度。但是GetWindowTextLength函数不能返回在其他应用程序中的控制的文本长度。
        /// </summary>
        /// <param name="hWnd">窗口或控制的句柄。</param>
        /// <returns>如果函数成功，返回值为文本的字符长度。在一定的条件下，返回值可能比实际的文本长度大。请参看说明。如果窗口无文本，返回值为零。若想获得更多错误信息，请调用GetLastError函数。</returns>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        /// <summary>
        /// 该函数测试一个窗口是否是指定父窗口的子窗口或后代窗口。如果该父窗口是在父窗口的链表上则子窗口是指定父窗口的直接后代。父窗口链表从原始层叠窗口或弹出窗口一直连到该子窗口。
        /// </summary>
        /// <param name="hWndParent">父窗口句柄。</param>
        /// <param name="hWnd">将被测试的窗口句柄。</param>
        /// <returns>如果窗口是指定窗口的子窗口或后代窗口，则退回值为非零。如果窗口不是指定窗口的子窗口或后代窗口，则退回值为零。</returns>
        [DllImport("user32.dll")]
        public static extern bool IsChild(IntPtr hWndParent, IntPtr hWnd);

        /// <summary>
        /// 该函数确定给定窗口是否是最小化（图标化）的窗口。
        /// </summary>
        /// <param name="hWnd">被测试窗口的句柄。</param>
        /// <returns>如果窗口未最小化，返回值为零；如果窗口已最小化，返回值为非零。</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsIconic(IntPtr hWnd);

        /// <summary>
        /// 该函数获得给定窗口的可视状态。
        /// </summary>
        /// <param name="hWnd">被测试窗口的句柄。</param>
        /// <returns>如果指定的窗口及其父窗口具有WS_VISIBLE风格，返回值为非零；如果指定的窗口及其父窗口不具有WS_VISIBLE风格，返回值为零。由于返回值表明了窗口是否具有Ws_VISIBLE风格，因此，即使该窗口被其他窗口遮盖，函数返回值也为非零。</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        /// <summary>
        /// 该函数确定窗口是否是最大化的窗口。
        /// </summary>
        /// <param name="hWnd">被测试窗口的句柄。</param>
        /// <returns>如果窗口己最大化，则返回值为非零；如果窗口未最大化，则返回值为零。</returns>
        [DllImport("user32.dll")]
        public static extern bool IsZoomed(IntPtr hWnd);

        /// <summary>
        /// The MoveWindow function changes the position and dimensions of the specified window. For a top-level window, the position and dimensions are relative to the upper-left corner of the screen. For a child window, they are relative to the upper-left corner of the parent window's client area.
        /// </summary>
        /// <param name="hWnd">Handle to the window.</param>
        /// <param name="X">Specifies the new position of the left side of the window.</param>
        /// <param name="Y">Specifies the new position of the top of the window.</param>
        /// <param name="nWidth">Specifies the new width of the window.</param>
        /// <param name="nHeight">Specifies the new height of the window.</param>
        /// <param name="bRepaint">Specifies whether the window is to be repainted. If this parameter is TRUE, the window receives a message. If the parameter is FALSE, no repainting of any kind occurs. This applies to the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent window uncovered as a result of moving a child window.</param>
        /// <returns>If the function succeeds, the return value is nonzero.
        /// <para>If the function fails, the return value is zero. To get extended error information, call GetLastError.</para></returns>
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        /// <summary>
        /// Opens the icon.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        [DllImport("user32.dll")]
        public static extern bool OpenIcon(IntPtr hWnd);

        // For Windows Mobile, replace user32.dll with coredll.dll
        /// <summary>
        /// Sets the foreground window.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        /// <summary>
        /// Sets the foreground window.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        [SecurityCritical, SuppressUnmanagedCodeSecurity, DllImport("user32", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetForegroundWindow(HandleRef hWnd);
        /// <summary>
        /// Sets the layered window attributes.
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        /// <param name="crKey">The cr key.</param>
        /// <param name="bAlpha">The b alpha.</param>
        /// <param name="dwFlags">The dw flags.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        [DllImport("user32.dll")]
        public static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        /// <summary>
        /// Sets the parent.
        /// </summary>
        /// <param name="hWndChild">The h WND child.</param>
        /// <param name="hWndNewParent">The h WND new parent.</param>
        /// <returns>IntPtr.</returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        /// <summary>
        /// Sets the show state and the restored, minimized, and maximized positions of the specified window.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="lpwndpl">A pointer to a WINDOWPLACEMENT structure that specifies the new show state and window positions.
        /// <para>
        /// Before calling SetWindowPlacement, set the length member of the WINDOWPLACEMENT structure to sizeof(WINDOWPLACEMENT). SetWindowPlacement fails if the length member is not set correctly.
        /// </para></param>
        /// <returns>If the function succeeds, the return value is nonzero.
        /// <para>
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </para></returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPlacement(IntPtr hWnd,
          [In] ref WINDOWPLACEMENT lpwndpl);

        /// <summary>
        /// Sets the window pos.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="hWndInsertAfter">The h WND insert after.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="cx">The cx.</param>
        /// <param name="cy">The cy.</param>
        /// <param name="uFlags">The u flags.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        /// <summary>
        /// Sets the window text.
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        /// <param name="lpString">The lp string.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool SetWindowText(IntPtr hwnd, String lpString);

        /// <summary>
        /// 该函数设置指定窗口的显示状态。
        /// </summary>
        /// <param name="hWnd">窗口句柄。</param>
        /// <param name="nCmdShow">指定窗口如何显示。如果发送应用程序的程序提供了STARTUPINFO结构，则应用程序第一次调用ShowWindow时该参数被忽略。否则，在第一次调用ShowWindow函数时，该值应为在函数WinMain中nCmdShow参数。</param>
        /// <returns>如果窗口之前可见，则返回值为非零。如果窗口之前被隐藏，则返回值为零。</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        /// <summary>
        /// 该函数允许/禁止指定的窗口或控件接受鼠标和键盘的输入，当输入被禁止时，窗口不响应鼠标和按键的输入，输入允许时，窗口接受所有的输入。
        /// </summary>
        /// <param name="hWnd">被允许/禁止的窗口句柄。</param>
        /// <param name="bEnable">定义窗口是被允许，还是被禁止。若该参数为TRUE，则窗口被允许。若该参数为FALSE，则窗口被禁止。</param>
        /// <returns>如果窗口原来是被禁止的，返回值不为零；如果窗口原来不是被禁止的，返回值为零。若想获得更多的错误信息，可调用GetLastError函数。</returns>
        [DllImport("user32.dll")]
        public static extern bool EnableWindow(IntPtr hWnd, bool bEnable);

        /// <summary>
        /// Finds the window.
        /// </summary>
        /// <param name="lpClassName">Name of the lp class.</param>
        /// <param name="lpWindowName">Name of the lp window.</param>
        /// <returns>IntPtr.</returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        #endregion Window Functions

        #region Menu Functions

        /// <summary>
        /// 该函数重画指定菜单的菜单条。如果系统创建窗口以后菜单条被修改，则必须调用此函数来画修改了的菜单条。
        /// </summary>
        /// <param name="hWnd">其菜单条需要被重画的窗口的句柄。</param>
        /// <returns>如果函数调用成功，返回非零值：如果函数调用失败，返回值是零。若想获得更多的错误信息，请调用GetLastError函数。</returns>
        [DllImport("user32.dll")]
        public static extern bool DrawMenuBar(IntPtr hWnd);

        /// <summary>
        /// 允许或禁止指定的菜单条目。
        /// </summary>
        /// <param name="hMenu">菜单句柄。</param>
        /// <param name="uIDEnableItem">欲允许或禁止的一个菜单条目的标识符。如果在wEnable参数中设置了MF_BYCOMMAND标志，这个参数就代表欲改变菜单条目的命令ID。如设置的是MF_BYPOSITION，则这个参数代表菜单条目在菜单中的位置（第一个条目肯定是零）。</param>
        /// <param name="uEnable">The u enable.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        [DllImport("user32.dll")]
        public static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

        /// <summary>
        /// 该函数允许应用程序为复制或修改而访问窗口菜单（系统菜单或控制菜单）。
        /// </summary>
        /// <param name="hWnd">拥有窗口菜单拷贝的窗口的句柄。</param>
        /// <param name="bRevert">指定将执行的操作。如果此参数为FALSE，GetSystemMenu返回当前使用窗口菜单的拷贝的句柄。该拷贝初始时与窗口菜单相同，但可以被修改。
        /// 如果此参数为TRUE，GetSystemMenu重置窗口菜单到缺省状态。如果存在先前的窗口菜单，将被销毁。</param>
        /// <returns>如果参数bRevert为FALSE，返回值是窗口菜单的拷贝的句柄：如果参数bRevert为TRUE，返回值是NULL。</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        #endregion Menu Functions

        #region Message and Message Queue Functions

        /// <summary>
        /// 该函数将指定的消息发送到一个或多个窗口。此函数为指定的窗口调用窗口程序，直到窗口程序处理完消息再返回。而和函数PostMessage不同，PostMessage是将一个消息寄送到一个线程的消息队列后就立即返回。
        /// </summary>
        /// <param name="hWnd">其窗口程序将接收消息的窗口的句柄。如果此参数为HWND_BROADCAST，则消息将被发送到系统中所有顶层窗口，包括无效或不可见的非自身拥有的窗口、被覆盖的窗口和弹出式窗口，但消息不被发送到子窗口。</param>
        /// <param name="Msg">指定被发送的消息。</param>
        /// <param name="wParam">指定附加的消息特定信息。</param>
        /// <param name="lParam">指定附加的消息特定信息。</param>
        /// <returns>返回值指定消息处理的结果，依赖于所发送的消息。</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="Msg">The MSG.</param>
        /// <param name="wParam">The w param.</param>
        /// <param name="lParam">The l param.</param>
        /// <returns>IntPtr.</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, ref COPYDATASTRUCT lParam);

        /// <summary>
        /// 该函数将虚拟键消息转换为字符消息。字符消息被寄送到调用线程的消息队列里，当下一次线程调用函数GetMessage或PeekMessage时被读出。
        /// </summary>
        /// <param name="lpMsg">指向含有消息的MSG结构的指针，该结构里含有用函数GetMessage或PeekMessage从调用线程的消息队列里取得的消息信息。</param>
        /// <returns>如果消息被转换（即，字符消息被寄送到调用线程的消息队列里），返回非零值。如果消息是WM_KEYDOWN，WM_KEYUP WM_SYSKEYDOWN或WM_SYSKEYUP，返回非零值，不考虑转换。如果消息没被转换（即，字符消息没被寄送到调用线程的消息队列里），返回值是零。</returns>
        [DllImport("user32.dll")]
        public static extern bool TranslateMessage([In] ref MSG lpMsg);

        #endregion Message and Message Queue Functions

        #region Keyboard Input Functions

        /// <summary>
        /// Gets the state of the async key.
        /// </summary>
        /// <param name="vKey">The v key.</param>
        /// <returns>System.UInt16.</returns>
        [DllImport("user32.dll")]
        public static extern ushort GetAsyncKeyState(int vKey);

        #endregion Keyboard Input Functions

        /// <summary>
        /// Shows the owned popups.
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        /// <param name="fShow">if set to <c>true</c> [f show].</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        [DllImport("user32.dll", CharSet = CharSet.None)]
        public static extern bool ShowOwnedPopups(IntPtr hwnd, bool fShow);

        /// <summary>
        /// Systems the parameters info.
        /// </summary>
        /// <param name="uiAction">The UI action.</param>
        /// <param name="uiParam">The UI param.</param>
        /// <param name="pvParam">The pv param.</param>
        /// <param name="fWinIni">The f win ini.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern bool SystemParametersInfo(int uiAction, int uiParam, ref NONCLIENTMETRICS pvParam, int fWinIni);

        /// <summary>
        /// 该函数在属于当前线程的指定窗口里设置鼠标捕获。一旦窗口捕获了鼠标，所有鼠标输入都针对该窗口，无论光标是否在窗口的边界内。同一时刻只能有一个窗口捕获鼠标。如果鼠标光标在另一个线程创建的窗口上，只有当鼠标键按下时系统才将鼠标输入指向指定的窗口。
        /// </summary>
        /// <param name="hWnd">当前线程里要捕获鼠标的窗口句柄。</param>
        /// <returns>返回值是上次捕获鼠标的窗口句柄。如果不存在那样的句柄，返回值是NULL。</returns>
        [DllImport("user32.dll", CharSet = CharSet.None)]
        public static extern IntPtr SetCapture(IntPtr hWnd);

        /// <summary>
        /// 该函数确定光标的形状。
        /// </summary>
        /// <param name="hCursor">光标的句柄，该光标由CreateCursor函数载入。如果该参数为NULL，则该光标从屏幕上移开。</param>
        /// <returns>如果有前一个光标，则返回值是前光标的句柄；如果没有前光标，则返回值是NULL。</returns>
        [DllImport("user32.dll", CharSet = CharSet.None)]
        public static extern IntPtr SetCursor(IntPtr hCursor);

        /// <summary>
        /// Sets the event.
        /// </summary>
        /// <param name="hEvent">The h event.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.None)]
        public static extern bool SetEvent(IntPtr hEvent);

        /// <summary>
        /// Sets the prop.
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        /// <param name="propName">Name of the prop.</param>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        [DllImport("user32.dll", CharSet = CharSet.None)]
        public static extern bool SetProp(IntPtr hwnd, string propName, IntPtr value);

        /// <summary>
        /// 销毁一个图标并且释放该图标所占用的内存。
        /// </summary>
        /// <param name="hIcon">图标的句柄</param>
        /// <returns>如果销毁图标成功则返回值为true;如果销毁图标失败返回值为false。</returns>
        [DllImport("user32.dll", CharSet = CharSet.None)]
        public static extern bool DestroyIcon(IntPtr hIcon);

        /// <summary>
        /// Enums the display monitors.
        /// </summary>
        /// <param name="hdc">The HDC.</param>
        /// <param name="lprcClip">The LPRC clip.</param>
        /// <param name="lpfnEnum">The LPFN enum.</param>
        /// <param name="dwData">The dw data.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        [DllImport("user32.dll", CharSet = CharSet.None)]
        public static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, EnumMonitorsDelegate lpfnEnum, IntPtr dwData);

        /// <summary>
        /// Enums the thread windows.
        /// </summary>
        /// <param name="dwThreadId">The dw thread id.</param>
        /// <param name="lpfn">The LPFN.</param>
        /// <param name="lParam">The l param.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        [DllImport("user32.dll", CharSet = CharSet.None)]
        public static extern bool EnumThreadWindows(uint dwThreadId, EnumWindowsProc lpfn, IntPtr lParam);

        /// <summary>
        /// 该函数检取光标的位置，以屏幕坐标表示。
        /// </summary>
        /// <param name="point">POINT结构指针，该结构接收光标的屏幕坐标。</param>
        /// <returns>如果成功，返回值非零；如果失败，返回值为零。若想获得更多错误信息，请调用GetLastError函数。</returns>
        [DllImport("user32.dll", CharSet = CharSet.None)]
        public static extern bool GetCursorPos(ref POINT point);

        /// <summary>
        /// Clients to screen.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="point">The point.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        [DllImport("user32.dll", CharSet = CharSet.None)]
        public static extern bool ClientToScreen(IntPtr hWnd, ref POINT point);

        /// <summary>
        /// Monitors from point.
        /// </summary>
        /// <param name="pt">The pt.</param>
        /// <param name="flags">The flags.</param>
        /// <returns>IntPtr.</returns>
        [DllImport("user32.dll", CharSet = CharSet.None)]
        public static extern IntPtr MonitorFromPoint(POINT pt, int flags);

        /// <summary>
        /// Pts the in rect.
        /// </summary>
        /// <param name="lprc">The LPRC.</param>
        /// <param name="pt">The pt.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        [DllImport("user32.dll", CharSet = CharSet.None)]
        public static extern bool PtInRect(ref RECT lprc, POINT pt);

        /// <summary>
        /// Screens to client.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="point">The point.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        [DllImport("user32.dll", CharSet = CharSet.None)]
        public static extern bool ScreenToClient(IntPtr hWnd, ref POINT point);

        /// <summary>
        /// Updates the layered window.
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        /// <param name="hdcDest">The HDC dest.</param>
        /// <param name="pptDest">The PPT dest.</param>
        /// <param name="psize">The psize.</param>
        /// <param name="hdcSrc">The HDC SRC.</param>
        /// <param name="pptSrc">The PPT SRC.</param>
        /// <param name="crKey">The cr key.</param>
        /// <param name="pblend">The pblend.</param>
        /// <param name="dwFlags">The dw flags.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        [DllImport("user32.dll", CharSet = CharSet.None)]
        public static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDest, ref POINT pptDest, ref SIZE psize, IntPtr hdcSrc, ref POINT pptSrc, uint crKey, ref BLENDFUNCTION pblend, uint dwFlags);

        /// <summary>
        /// Gets the monitor info.
        /// </summary>
        /// <param name="hMonitor">The h monitor.</param>
        /// <param name="monitorInfo">The monitor info.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        [DllImport("user32.dll", CharSet = CharSet.None)]
        public static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO monitorInfo);

        /// <summary>
        /// Windows from point.
        /// </summary>
        /// <param name="pt">The pt.</param>
        /// <returns>IntPtr.</returns>
        [DllImport("user32.dll", CharSet = CharSet.None)]
        public static extern IntPtr WindowFromPoint(POINT pt);

        /// <summary>
        /// 该函数用指定的画刷填充矩形，此函数包括矩形的左上边界，但不包括矩形的右下边界。
        /// </summary>
        /// <param name="hDC">设备环境句柄。</param>
        /// <param name="rect">指向含有将填充矩形的逻辑坐标的RECT结构的指针。</param>
        /// <param name="hbrush">用来填充矩形的画刷的句柄。</param>
        /// <returns>如果函数调用成功，返回值非零；如果函数调用失败，返回值是0。</returns>
        [DllImport("user32.dll", CharSet = CharSet.None)]
        public static extern bool FillRect(IntPtr hDC, ref RECT rect, IntPtr hbrush);

        /// <summary>
        /// 该函数返回指定窗口的边框矩形的尺寸。该尺寸以相对于屏幕坐标左上角的屏幕坐标给出。
        /// </summary>
        /// <param name="hwnd">窗口句柄。</param>
        /// <param name="lpRect">指向一个RECT结构的指针，该结构接收窗口的左上角和右下角的屏幕坐标。</param>
        /// <returns>如果函数成功，返回值为非零：如果函数失败，返回值为零。若想获得更多错误信息，请调用GetLastError函数。</returns>
        [DllImport("user32.dll", CharSet = CharSet.None)]
        public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        /// <summary>
        /// Intersects the rect.
        /// </summary>
        /// <param name="lprcDst">The LPRC DST.</param>
        /// <param name="lprcSrc1">The LPRC SRC1.</param>
        /// <param name="lprcSrc2">The LPRC SRC2.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        [DllImport("user32.dll", CharSet = CharSet.None)]
        public static extern bool IntersectRect(out RECT lprcDst, ref RECT lprcSrc1, ref RECT lprcSrc2);

        /// <summary>
        /// Registers the class.
        /// </summary>
        /// <param name="lpWndClass">The lp WND class.</param>
        /// <returns>System.UInt16.</returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern ushort RegisterClass(ref WNDCLASS lpWndClass);

        /// <summary>
        /// Registers the class ex.
        /// </summary>
        /// <param name="lpWndClass">The lp WND class.</param>
        /// <returns>System.UInt16.</returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern ushort RegisterClassEx(ref WNDCLASSEX lpWndClass);

        /// <summary>
        /// Gets the window thread process id.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="processId">The process id.</param>
        /// <returns>System.UInt32.</returns>
        [DllImport("user32.dll", CharSet = CharSet.None)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        /// <summary>
        /// 该函数调用缺省的窗口过程来为应用程序没有处理的任何窗口消息提供缺省的处理。该函数确保每一个消息得到处理。调用DefWindowProc函数时使用窗口过程接收的相同参数。
        /// </summary>
        /// <param name="hWnd">指向接收消息的窗口过程的句柄。</param>
        /// <param name="msg">指定消息类型。</param>
        /// <param name="wParam">指定其余的、消息特定的信息。该参数的内容与Msg参数值有关。</param>
        /// <param name="lParam">指定其余的、消息特定的信息。该参数的内容与Msg参数值有关。</param>
        /// <returns>返回值就是消息处理结果，它与发送的消息有关。</returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr DefWindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// 该函数检取指定虚拟键的状态。该状态指定此键是UP状态，DOWN状态，还是被触发的（开关每次按下此键时进行切换）。
        /// </summary>
        /// <param name="vKey">欲测试的虚拟键键码。对字母、数字字符（A-Z、a-z、0-9），用它们实际的ASCII值</param>
        /// <returns>System.Int16.</returns>
        [DllImport("user32.dll", CharSet = CharSet.None)]
        public static extern short GetKeyState(int vKey);

        /// <summary>
        /// 函数释放设备上下文环境（DC）供其他应用程序使用。函数的效果与设备上下文环境类型有关。它只释放公用的和设备上下文环境，对于类或私有的则无效。
        /// </summary>
        /// <param name="hWnd">指向要释放的设备上下文环境所在的窗口的句柄。</param>
        /// <param name="hDC">指向要释放的设备上下文环境的句柄。</param>
        /// <returns>System.Int32.</returns>
        [DllImport("User32.dll", CharSet = CharSet.None)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        /// <summary>
        /// 将钩子信息传递到当前钩子链中的下一个子程，一个钩子程序可以调用这个函数之前或之后处理钩子信息。
        /// </summary>
        /// <param name="hhk">当前钩子的句柄。</param>
        /// <param name="code">钩子代码; 就是给下一个钩子要交待的。</param>
        /// <param name="wParam">要传递的参数; 由钩子类型决定是什么参数。</param>
        /// <param name="lParam">要传递的参数; 由钩子类型决定是什么参数。</param>
        /// <returns>IntPtr.</returns>
        [DllImport("user32.dll", CharSet = CharSet.None)]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int code, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// 该函数检索一指定窗口的客户区域或整个屏幕的显示设备上下文环境的句柄，以后可以在GDI函数中使用该句柄来在设备上下文环境中绘图。
        /// </summary>
        /// <param name="hWnd">设备上下文环境被检索的窗口的句柄，如果该值为NULL，GetDC则检索整个屏幕的设备上下文环境。</param>
        /// <returns>如果成功，返回指定窗口客户区的设备上下文环境；如果失败，返回值为Null。</returns>
        [DllImport("User32.dll", CharSet = CharSet.None)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        /// <summary>
        /// 该函数检索指定窗口客户区域或整个屏幕的显示设备上下文环境的句柄，在随后的GDI函数中可以使用该句柄在设备上下文环境中绘图。
        /// </summary>
        /// <param name="hWnd">窗口的句柄，该窗口的设备上下文环境将要被检索，如果该值为NULL，则GetDCEx将检索整个屏幕的设备上下文环境。</param>
        /// <param name="hrgnClip">指定一剪切区域，它可以与设备上下文环境的可见区域相结合。</param>
        /// <param name="dwFlags">指定如何创建设备上下文环境。</param>
        /// <returns>如果成功，返回值是指定窗口设备上下文环境的句柄，如果失败，返回值为Null。HWnd参数的一个无效值会使函数失败。</returns>
        [DllImport("User32.dll", CharSet = CharSet.None)]
        public static extern IntPtr GetDCEx(IntPtr hWnd, IntPtr hrgnClip, int dwFlags);

        /// <summary>
        /// Gets the focus.
        /// </summary>
        /// <returns>IntPtr.</returns>
        [DllImport("user32.dll", CharSet = CharSet.None)]
        public static extern IntPtr GetFocus();

        /// <summary>
        /// 用于得到被定义的系统数据或者系统配置信息。
        /// </summary>
        /// <param name="index">可取SM静态类指定常量。</param>
        /// <returns>System.Int32.</returns>
        [DllImport("user32.dll", CharSet = CharSet.None)]
        public static extern int GetSystemMetrics(int index);

        /// <summary>
        /// 该函数获得有关指定窗口的信息，函数也获得在额外窗口内存中指定偏移位地址的32位度整型值。
        /// </summary>
        /// <param name="hWnd">窗口句柄及间接给出的窗口所属的窗口类。</param>
        /// <param name="nIndex">Index of the n.</param>
        /// <returns>如果函数成功，返回值是所需的32位值；如果函数失败，返回值是0。若想获得更多错误信息请调用 GetLastError函数。</returns>
        [DllImport("user32.dll", CharSet = CharSet.None)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        /// <summary>
        /// Posts the message.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="nMsg">The n MSG.</param>
        /// <param name="wParam">The w param.</param>
        /// <param name="lParam">The l param.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        [DllImport("user32.dll", CharSet = CharSet.None)]
        public static extern bool PostMessage(IntPtr hWnd, int nMsg, IntPtr wParam, IntPtr lParam);
        /// <summary>
        /// Posts the message.
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        /// <param name="msg">The MSG.</param>
        /// <param name="wparam">The wparam.</param>
        /// <param name="lparam">The lparam.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        [SecurityCritical, SuppressUnmanagedCodeSecurity, DllImport("user32", CharSet = CharSet.Auto)]
        public static extern bool PostMessage(HandleRef hwnd, int msg, IntPtr wparam, IntPtr lparam);
        /// <summary>
        /// Monitors from window.
        /// </summary>
        /// <param name="handle">The handle.</param>
        /// <param name="flags">The flags.</param>
        /// <returns>IntPtr.</returns>
        [DllImport("user32.dll", CharSet = CharSet.None)]
        public static extern IntPtr MonitorFromWindow(IntPtr handle, int flags);

        /// <summary>
        /// Sets the window RGN.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="hRgn">The h RGN.</param>
        /// <param name="redraw">if set to <c>true</c> [redraw].</param>
        /// <returns>System.Int32.</returns>
        [DllImport("User32.dll", CharSet = CharSet.None)]
        public static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool redraw);

        /// <summary>
        /// Sets the window long PTR64.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="nIndex">Index of the n.</param>
        /// <param name="dwNewLong">The dw new long.</param>
        /// <returns>IntPtr.</returns>
        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        /// <summary>
        /// Unhooks the windows hook ex.
        /// </summary>
        /// <param name="hhk">The HHK.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        [DllImport("user32.dll", CharSet = CharSet.None)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        /// <summary>
        /// Unregisters the class.
        /// </summary>
        /// <param name="classAtom">The class atom.</param>
        /// <param name="hInstance">The h instance.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        [DllImport("user32.dll", CharSet = CharSet.None)]
        public static extern bool UnregisterClass(IntPtr classAtom, IntPtr hInstance);

        /// <summary>
        /// Tracks the popup menu ex.
        /// </summary>
        /// <param name="hmenu">The hmenu.</param>
        /// <param name="fuFlags">The fu flags.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="hwnd">The HWND.</param>
        /// <param name="lptpm">The LPTPM.</param>
        /// <returns>System.Int32.</returns>
        [DllImport("user32.dll", CharSet = CharSet.None)]
        public static extern int TrackPopupMenuEx(IntPtr hmenu, uint fuFlags, int x, int y, IntPtr hwnd, IntPtr lptpm);

        /// <summary>
        /// Sets the windows hook ex.
        /// </summary>
        /// <param name="hookType">Type of the hook.</param>
        /// <param name="hookProc">The hook proc.</param>
        /// <param name="module">The module.</param>
        /// <param name="threadId">The thread id.</param>
        /// <returns>IntPtr.</returns>
        [DllImport("user32.dll", CharSet = CharSet.None)]
        public static extern IntPtr SetWindowsHookEx(int hookType, WindowsHookProc hookProc, IntPtr module, uint threadId);

        /// <summary>
        /// 该函数对指定的窗口设置键盘焦点。该窗口必须与调用线程的消息队列相关。
        /// </summary>
        /// <param name="hWnd">接收键盘输入的窗口指针。若该参数为NULL，则击键被忽略。</param>
        /// <returns>若函数调用成功，则返回原先拥有键盘焦点的窗口句柄。若hWnd参数无效或窗口未与调用线程的消息队列相关，则返回值为NULL。若要获得更多错误信息，可以调用GetLastError函数。</returns>
        [DllImport("user32.dll", CharSet = CharSet.None)]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        /// <summary>
        /// Registers the window message.
        /// </summary>
        /// <param name="lpString">The lp string.</param>
        /// <returns>System.Int32.</returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int RegisterWindowMessage(string lpString);

        /// <summary>
        /// Gets the current process id.
        /// </summary>
        /// <returns>System.UInt32.</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.None)]
        public static extern uint GetCurrentProcessId();

        /// <summary>
        /// 该函数返回表示屏幕坐标下光标位置的长整数值。此位置表示当上一消息由GetMessage取得时鼠标占用的点。
        /// </summary>
        /// <returns>返回值给出光标位置的X，y坐标。X坐标在低位整数，y坐标在高位整数。</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetMessagePos();

        /// <summary>
        /// 该函数返回由GetMessage从当前线程队列里取得上一消息的消息时间。时间是一个长整数，指定从系统开始到消息创建（即，放入线程消息队列）的占用时间（按毫秒计算）。
        /// </summary>
        /// <returns>返回值为消息时间。</returns>
        [DllImport("user32.dll", CharSet = CharSet.None)]
        public static extern int GetMessageTime();

        /// <summary>
        /// 该函数用于判断指定的窗口是否允许接受键盘或鼠标输入。
        /// </summary>
        /// <param name="hwnd">被测试的窗口句柄。</param>
        /// <returns>若窗口允许接受键盘或鼠标输入，则返回非0值，若窗口不允许接受键盘或鼠标输入，则返回值为0。</returns>
        [DllImport("user32.dll", CharSet = CharSet.None)]
        public static extern bool IsWindowEnabled(IntPtr hwnd);

        /// <summary>
        /// Gets the state of the keyboard.
        /// </summary>
        /// <param name="lpKeyState">State of the lp key.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        [DllImport("user32.dll", CharSet = CharSet.None)]
        public static extern bool GetKeyboardState(byte[] lpKeyState);

        /// <summary>
        /// 该函数激活一个窗口。该窗口必须与调用线程的消息队列相关联。
        /// </summary>
        /// <param name="hWnd">将被激活的最顶层窗口。</param>
        /// <returns>若函数调用成功，则返回原先活动窗口的句柄。若函数调用失败，则返回值为NULL。若要获得更多错误信息，可以调用GetLastError函数。</returns>
        [DllImport("user32.dll", CharSet = CharSet.None)]
        public static extern IntPtr SetActiveWindow(IntPtr hWnd);

        /// <summary>
        /// Alphas the blend.
        /// </summary>
        /// <param name="hdcDest">The HDC dest.</param>
        /// <param name="xoriginDest">The xorigin dest.</param>
        /// <param name="yoriginDest">The yorigin dest.</param>
        /// <param name="wDest">The w dest.</param>
        /// <param name="hDest">The h dest.</param>
        /// <param name="hdcSrc">The HDC SRC.</param>
        /// <param name="xoriginSrc">The xorigin SRC.</param>
        /// <param name="yoriginSrc">The yorigin SRC.</param>
        /// <param name="wSrc">The w SRC.</param>
        /// <param name="hSrc">The h SRC.</param>
        /// <param name="pfn">The PFN.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        [DllImport("msimg32.dll", CharSet = CharSet.None)]
        public static extern bool AlphaBlend(IntPtr hdcDest, int xoriginDest, int yoriginDest, int wDest, int hDest, IntPtr hdcSrc, int xoriginSrc, int yoriginSrc, int wSrc, int hSrc, BLENDFUNCTION pfn);

        #region Gdi32

        /// <summary>
        /// 该函数选择一对象到指定的设备上下文环境中，该新对象替换先前的相同类型的对象。
        /// </summary>
        /// <param name="hdc">设备上下文环境的句柄。</param>
        /// <param name="hgdiobj">被选择的对象的句型，该指定对象必须由如下的函数创建。
        /// 位图：CreateBitmap, CreateBitmapIndirect, CreateCompatible Bitmap, CreateDIBitmap, CreateDIBsection（只有内存设备上下文环境可选择位图，并且在同一时刻只能一个设备上下文环境选择位图）。
        /// 画笔：CreateBrushIndirect, CreateDIBPatternBrush, CreateDIBPatternBrushPt, CreateHatchBrush, CreatePatternBrush, CreateSolidBrush。
        /// 字体：CreateFont, CreateFontIndirect。
        /// 笔：CreatePen, CreatePenIndirect。
        /// 区域：CombineRgn, CreateEllipticRgn, CreateEllipticRgnIndirect, CreatePolygonRgn, CreateRectRgn, CreateRectRgnIndirect。</param>
        /// <returns>如果选择对象不是区域并且函数执行成功，那么返回值是被取代的对象的句柄；如果选择对象是区域并且函数执行成功，返回如下一值；
        /// SIMPLEREGION：区域由单个矩形组成；COMPLEXREGION：区域由多个矩形组成。NULLREGION：区域为空。
        /// 如果发生错误并且选择对象不是一个区域，那么返回值为NULL，否则返回GDI_ERROR。</returns>
        [DllImport("gdi32.dll", CharSet = CharSet.None)]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        /// <summary>
        /// 该函数创建一个与指定设备兼容的内存设备上下文环境（DC）。通过GetDc()获取的HDC直接与相关设备沟通，而本函数创建的DC，则是与内存中的一个表面相关联。
        /// </summary>
        /// <param name="hdc">现有设备上下文环境的句柄，如果该句柄为NULL，该函数创建一个与应用程序的当前显示器兼容的内存设备上下文环境。</param>
        /// <returns>如果成功，则返回内存设备上下文环境的句柄；如果失败，则返回值为NULL。</returns>
        [DllImport("gdi32.dll", CharSet = CharSet.None)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        /// <summary>
        /// 该函数删除指定的设备上下文环境（Dc）。
        /// </summary>
        /// <param name="hdc">设备上下文环境的句柄。</param>
        /// <returns>成功，返回非零值；失败，返回零。</returns>
        [DllImport("gdi32.dll", CharSet = CharSet.None)]
        public static extern bool DeleteDC(IntPtr hdc);

        /// <summary>
        /// 该函数删除一个逻辑笔、画笔、字体、位图、区域或者调色板，释放所有与该对象有关的系统资源，在对象被删除之后，指定的句柄也就失效了。
        /// </summary>
        /// <param name="hObject">逻辑笔、画笔、字体、位图、区域或者调色板的句柄。</param>
        /// <returns>成功，返回非零值；如果指定的句柄无效或者它已被选入设备上下文环境，则返回值为零。</returns>
        [DllImport("gdi32.dll", CharSet = CharSet.None)]
        public static extern bool DeleteObject(IntPtr hObject);

        /// <summary>
        /// 创建一个由lpRect确定的矩形区域。
        /// </summary>
        /// <param name="lprc">要用来创建区域的矩形。</param>
        /// <returns>执行成功则为区域句柄，失败则为0。</returns>
        [DllImport("Gdi32.dll", CharSet = CharSet.None)]
        public static extern IntPtr CreateRectRgnIndirect(ref RECT lprc);

        /// <summary>
        /// 将两个区域组合为一个新区域。
        /// </summary>
        /// <param name="hrngDest">包含组合结果的区域句柄</param>
        /// <param name="hrgnSrc1">源区域1</param>
        /// <param name="hrgnSrc2">源区域2</param>
        /// <param name="fnCombineMode">合并选项</param>
        /// <returns>ERROR         = 0; {错误}
        /// NULLREGION    = 1; {空区域}
        /// SIMPLEREGION  = 2; {单矩形区域}
        /// COMPLEXREGION = 3; {多矩形区域}</returns>
        [DllImport("Gdi32.dll", CharSet = CharSet.None)]
        public static extern int CombineRgn(IntPtr hrngDest, IntPtr hrgnSrc1, IntPtr hrgnSrc2, int fnCombineMode);

        /// <summary>
        /// Creates the rect RGN.
        /// </summary>
        /// <param name="nLeftRect">The n left rect.</param>
        /// <param name="nTopRect">The n top rect.</param>
        /// <param name="nRightRect">The n right rect.</param>
        /// <param name="nBottomRect">The n bottom rect.</param>
        /// <returns>IntPtr.</returns>
        [DllImport("Gdi32.dll", CharSet = CharSet.None)]
        public static extern IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

        /// <summary>
        /// Creates the DIB section.
        /// </summary>
        /// <param name="hdc">The HDC.</param>
        /// <param name="pbmi">The pbmi.</param>
        /// <param name="iUsage">The i usage.</param>
        /// <param name="ppvBits">The PPV bits.</param>
        /// <param name="hSection">The h section.</param>
        /// <param name="dwOffset">The dw offset.</param>
        /// <returns>IntPtr.</returns>
        [DllImport("gdi32.dll", CharSet = CharSet.None)]
        public static extern IntPtr CreateDIBSection(IntPtr hdc, ref BITMAPINFO pbmi, uint iUsage, out IntPtr ppvBits, IntPtr hSection, uint dwOffset);

        /// <summary>
        /// Creates the round rect RGN.
        /// </summary>
        /// <param name="nLeftRect">The n left rect.</param>
        /// <param name="nTopRect">The n top rect.</param>
        /// <param name="nRightRect">The n right rect.</param>
        /// <param name="nBottomRect">The n bottom rect.</param>
        /// <param name="nWidthEllipse">The n width ellipse.</param>
        /// <param name="nHeightEllipse">The n height ellipse.</param>
        /// <returns>IntPtr.</returns>
        [DllImport("Gdi32.dll", CharSet = CharSet.None)]
        public static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        /// <summary>
        /// 该函数创建一个具有指定颜色的逻辑刷子。
        /// </summary>
        /// <param name="colorref">指定刷子的颜色。</param>
        /// <returns>如果该函数执行成功，那么返回值标识一个逻辑实心刷子；如果函数失败，那么返回值为NULL。</returns>
        [DllImport("gdi32.dll", CharSet = CharSet.None)]
        public static extern IntPtr CreateSolidBrush(int colorref);

        /// <summary>
        /// 该函数检索指定设备的设备指定信息。
        /// </summary>
        /// <param name="hdc">设备上下文环境的句柄。</param>
        /// <param name="nIndex">Index of the n.</param>
        /// <returns>System.Int32.</returns>
        [DllImport("Gdi32.dll", CharSet = CharSet.None)]
        public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        #endregion Gdi32

        #region shell32

        /// <summary>
        /// SHs the get file info.
        /// </summary>
        /// <param name="pszPath">The PSZ path.</param>
        /// <param name="dwFileAttributes">The dw file attributes.</param>
        /// <param name="psfi">The psfi.</param>
        /// <param name="cbFileInfo">The cb file info.</param>
        /// <param name="uFlags">The u flags.</param>
        /// <returns>IntPtr.</returns>
        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbFileInfo, SHGFI uFlags);
        /// <summary>
        /// Shell_s the notify icon.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="pnid">The pnid.</param>
        /// <returns>System.Int32.</returns>
        [SecurityCritical, DllImport("shell32", CharSet = CharSet.Auto)]
        public static extern int Shell_NotifyIcon(int message, NOTIFYICONDATA pnid);
        #endregion shell32

        /// <summary>
        /// 获取当前线程一个唯一的线程标识符。
        /// </summary>
        /// <returns>返回当前线程 ID。</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.None)]
        public static extern uint GetCurrentThreadId();

        /// <summary>
        /// 获取一个应用程序或动态链接库的模块句柄。
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        /// <returns>IntPtr.</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr GetModuleHandle(string moduleName);
    }
}