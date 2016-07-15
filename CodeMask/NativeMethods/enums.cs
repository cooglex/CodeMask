using System;
using System.Runtime.InteropServices;

namespace NativeMethodsPack
{
    #region enum

    /// <summary>
    /// Class SWP_FLAGS
    /// </summary>
    public static class SWP_FLAGS
    {
        /// <summary>
        /// 如果调用进程不拥有窗口，系统会向拥有窗口的线程发出需求。这就防止调用线程在其他线程处理需求的时候发生死锁。
        /// </summary>
        public const int SWP_ASYNCWINDOWPOS = 0x4000;

        /// <summary>
        /// 防止产生WM_SYNCPAINT消息。
        /// </summary>
        public const int SWP_DEFERERASE = 0x2000;

        /// <summary>
        /// 向窗口发送一条WM_NCCALCSIZE消息，即使窗口的大小不会改变。如果没有指定这个标志，则仅当窗口的大小发生变化时才发送WM_NCCALCSIZE消息。
        /// </summary>
        public const int SWP_FRAMECHANGED = 0x20;

        /// <summary>
        /// 隐藏窗口。
        /// </summary>
        public const int SWP_HIDEWINDOW = 0x80;

        /// <summary>
        /// 不激活窗口。如果未设置标志，则窗口被激活，并被设置到其他最高级窗口或非最高级组的顶部（根据参数hWndlnsertAfter设置）。
        /// </summary>
        public const int SWP_NOACTIVATE = 0x10;

        /// <summary>
        /// 废弃这个客户区的内容。如果没有指定这个参数，则客户区的有效内容将被保存，并在窗口的大小或位置改变以后被拷贝回客户区。
        /// </summary>
        public const int SWP_NOCOPYBITS = 0x100;

        /// <summary>
        /// 维持当前位置（忽略X和Y参数）。
        /// </summary>
        public const int SWP_NOMOVE = 2;

        /// <summary>
        /// 不改变拥有者窗口在Z轴次序上的位置。
        /// </summary>
        public const int SWP_NOOWNERZORDER = 0x200;

        /// <summary>
        /// 不重画改变的内容。如果设置了这个标志，则不发生任何重画动作。适用于客户区和非客户区（包括标题栏和滚动条）和任何由于窗回移动而露出的父窗口的所有部分。如果设置了这个标志，应用程序必须明确地使窗口无效并区重画窗口的任何部分和父窗口需要重画的部分。
        /// </summary>
        public const int SWP_NOREDRAW = 8;

        /// <summary>
        /// 防止窗口接收WM_WINDOWPOSCHANGING消息。
        /// </summary>
        public const int SWP_NOSENDCHANGING = 0x400;

        /// <summary>
        /// 维持当前尺寸（忽略cx和Cy参数）。
        /// </summary>
        public const int SWP_NOSIZE = 1;

        /// <summary>
        /// 保持当前的次序（忽略pWndInsertAfter）。
        /// </summary>
        public const int SWP_NOZORDER = 4;

        /// <summary>
        /// 显示窗口。
        /// </summary>
        public const int SWP_SHOWWINDOW = 0x40;
    }

    /// <summary>
    /// Class SWP_HWND
    /// </summary>
    public static class SWP_HWND
    {
        /// <summary>
        /// 将窗口置于Z序的底部。如果参数hWnd标识了一个顶层窗口，则窗口失去顶级位置，并且被置在其他窗口的底部。
        /// </summary>
        public static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

        /// <summary>
        /// 将窗口置于所有非顶层窗口之上（即在所有顶层窗口之后）。如果窗口已经是非顶层窗口则该标志不起作用。
        /// </summary>
        public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);

        /// <summary>
        /// 将窗口置于Z序的顶部。
        /// </summary>
        public static readonly IntPtr HWND_TOP = IntPtr.Zero;

        /// <summary>
        /// 将窗口置于所有非顶层窗口之上。即使窗口未被激活窗口也将保持顶级位置。
        /// </summary>
        public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
    }

    /// <summary>
    /// GetAncestor函数flags参数可取值。
    /// </summary>
    public static class GA_FLAGS
    {
        /// <summary>
        /// 获取父窗口。这不包括所有者，功能同GetParent功能。
        /// </summary>
        public const uint GA_PARENT = 1;

        /// <summary>
        /// 通过遍历父窗口链获取根窗口。
        /// </summary>
        public const uint GA_ROOT = 2;

        /// <summary>
        /// 通过遍历父窗口链和使用GetParent函数返回的所有者窗口来获取根窗口。
        /// </summary>
        public const uint GA_ROOTOWNER = 3;
    }

    /// <summary>
    /// GetWindow函数cmd参数可取值。
    /// </summary>
    public static class GW_CMD
    {
        /// <summary>
        /// 返回的句柄标识了在Z序最高端的相同类型的窗口。如果指定窗口是最高端窗口，则该句柄标识了在Z序最高端的最高端窗口；如果指定窗口是顶层窗口，则该句柄标识了在z序最高端的顶层窗口：如果指定窗口是子窗口，则句柄标识了在Z序最高端的同属窗口。
        /// </summary>
        public const uint GW_HWNDFIRST = 0;

        /// <summary>
        /// 返回的句柄标识了在z序最低端的相同类型的窗口。如果指定窗口是最高端窗口，则该柄标识了在z序最低端的最高端窗口：如果指定窗口是顶层窗口，则该句柄标识了在z序最低端的顶层窗口；如果指定窗口是子窗口，则句柄标识了在Z序最低端的同属窗口。
        /// </summary>
        public const uint GW_HWNDLAST = 1;

        /// <summary>
        /// 返回的句柄标识了在Z序中指定窗口下的相同类型的窗口。如果指定窗口是最高端窗口，则该句柄标识了在指定窗口下的最高端窗口：如果指定窗口是顶层窗口，则该句柄标识了在指定窗口下的顶层窗口；如果指定窗口是子窗口，则句柄标识了在指定窗口下的同属窗口。
        /// </summary>
        public const uint GW_HWNDNEXT = 2;

        /// <summary>
        /// 返回的句柄标识了在Z序中指定窗口上的相同类型的窗口。如果指定窗口是最高端窗口，则该句柄标识了在指定窗口上的最高端窗口；如果指定窗口是顶层窗口，则该句柄标识了在指定窗口上的顶层窗口；如果指定窗口是子窗口，则句柄标识了在指定窗口上的同属窗口。
        /// </summary>
        public const uint GW_HWNDPREV = 3;

        /// <summary>
        /// 返回的句柄标识了指定窗口的所有者窗口（如果存在）。GW_OWNER与GW_CHILD不是相对的参数，没有父窗口的含义，如果想得到父窗口请使用GetParent()。例如：例如有时对话框的控件的GW_OWNER，是不存在的。
        /// </summary>
        public const uint GW_OWNER = 4;

        /// <summary>
        /// 如果指定窗口是父窗口，则获得的是在Z序顶端的子窗口的句柄，否则为NULL。函数仅检查指定父窗口的子窗口，不检查继承窗口。
        /// </summary>
        public const uint GW_CHILD = 5;

        /// <summary>
        /// （WindowsNT 5.0）返回的句柄标识了属于指定窗口的处于使能状态弹出式窗口（检索使用第一个由GW_HWNDNEXT 查找到的满足前述条件的窗口）；如果无使能窗口，则获得的句柄与指定窗口相同。
        /// </summary>
        public const uint GW_ENABLEDPOPUP = 6;
    }

    /// <summary>
    /// ShowWindow函数cmd参数可取值。
    /// </summary>
    public static class SW_CMD
    {
        /// <summary>
        /// 隐藏窗口并激活其他窗口。
        /// </summary>
        public const int SW_HIDE = 0;

        /// <summary>
        /// 激活并显示窗口。 如果窗口处于最小化或最大化，窗口还原为其原始大小和位置 (和 SW_RESTORE相同)。
        /// </summary>
        public const int SW_SHOWNORMAL = 1;

        /// <summary>
        /// 激活窗口并将其最小化。
        /// </summary>
        public const int SW_SHOWMINIMIZED = 2;

        /// <summary>
        /// 最大化指定的窗口。
        /// </summary>
        public const int SW_MAXIMIZE = 3;

        /// <summary>
        /// 激活窗口并将其最大化。
        /// </summary>
        public const int SW_SHOWMAXIMIZED = 3;

        /// <summary>
        /// 以窗口最近一次的大小和状态显示窗口。激活窗口仍然维持激活状态。
        /// </summary>
        public const int SW_SHOWNOACTIVATE = 4;

        /// <summary>
        /// 在窗口原来的位置以原来的尺寸激活和显示窗口。
        /// </summary>
        public const int SW_SHOW = 5;

        /// <summary>
        /// 最小化指定的窗口并且激活在Z序中的下一个顶层窗口。
        /// </summary>
        public const int SW_MINIMIZE = 6;

        /// <summary>
        /// 窗口最小化，激活窗口仍然维持激活状态。
        /// </summary>
        public const int SW_SHOWMINNOACTIVE = 7;

        /// <summary>
        /// 以窗口原来的状态显示窗口。激活窗口仍然维持激活状态。
        /// </summary>
        public const int SW_SHOWNA = 8;

        /// <summary>
        /// 激活并显示窗口。如果窗口最小化或最大化，则系统将窗口恢复到原来的尺寸和位置。在恢复最小化窗口时，应用程序应该指定这个标志。
        /// </summary>
        public const int SW_RESTORE = 9;

        /// <summary>
        /// 依据在STARTUPINFO结构中指定的SW_FLAG标志设定显示状态，STARTUPINFO 结构是由启动应用程序的程序传递给CreateProcess函数的。
        /// </summary>
        public const int SW_SHOWDEFAULT = 10;

        /// <summary>
        /// 在WindowNT5.0中最小化窗口，即使拥有窗口的线程被挂起也会最小化。在从其他线程最小化窗口时才使用这个参数。
        /// </summary>
        public const int SW_FORCEMINIMIZE = 11;
    }

    /// <summary>
    /// CallNextHookEx函数code可取值。
    /// </summary>
    public static class CBT_HOOKACTION
    {
        /// <summary>
        /// The HCB t_ MOVESIZE
        /// </summary>
        public const int HCBT_MOVESIZE = 0;

        /// <summary>
        /// The HCB t_ MINMAX
        /// </summary>
        public const int HCBT_MINMAX = 1;

        /// <summary>
        /// The HCB t_ QS
        /// </summary>
        public const int HCBT_QS = 2;

        /// <summary>
        /// The HCB t_ CREATEWND
        /// </summary>
        public const int HCBT_CREATEWND = 3;

        /// <summary>
        /// The HCB t_ DESTROYWND
        /// </summary>
        public const int HCBT_DESTROYWND = 4;

        /// <summary>
        /// The HCB t_ ACTIVATE
        /// </summary>
        public const int HCBT_ACTIVATE = 5;

        /// <summary>
        /// The HCB t_ CLICKSKIPPED
        /// </summary>
        public const int HCBT_CLICKSKIPPED = 6;

        /// <summary>
        /// The HCB t_ KEYSKIPPED
        /// </summary>
        public const int HCBT_KEYSKIPPED = 7;

        /// <summary>
        /// The HCB t_ SYSCOMMAND
        /// </summary>
        public const int HCBT_SYSCOMMAND = 8;

        /// <summary>
        /// The HCB t_ SETFOCUS
        /// </summary>
        public const int HCBT_SETFOCUS = 9;
    }

    /// <summary>
    /// SetWindowsHookEx函数hookType参数可取值。
    /// </summary>
    public static class SWH_HOOKTYPE
    {
        /// <summary>
        /// 线程级; 截获用户与控件交互的消息。
        /// </summary>
        public const int WH_MSGFILTER = -1;

        /// <summary>
        /// 系统级; 记录所有消息队列从消息队列送出的输入消息, 在消息从队列中清除时发生; 可用于宏记录。
        /// </summary>
        public const int WH_JOURNALRECORD = 0;

        /// <summary>
        /// 系统级; 回放由 WH_JOURNALRECORD 记录的消息, 也就是将这些消息重新送入消息队列。
        /// </summary>
        public const int WH_JOURNALPLAYBACK = 1;

        /// <summary>
        /// 系统级或线程级; 截获键盘消息。
        /// </summary>
        public const int WH_KEYBOARD = 2;

        /// <summary>
        /// 系统级或线程级; 截获从消息队列送出的消息。
        /// </summary>
        public const int WH_GETMESSAGE = 3;

        /// <summary>
        /// 系统级或线程级; 截获发送到目标窗口的消息, 在 SendMessage 调用时发生。
        /// </summary>
        public const int WH_CALLWNDPROC = 4;

        /// <summary>
        /// 系统级或线程级; 截获系统基本消息, 譬如: 窗口的创建、激活、关闭、最大最小化、移动等等。
        /// </summary>
        public const int WH_CBT = 5;

        /// <summary>
        /// 系统级; 截获系统范围内用户与控件交互的消息。
        /// </summary>
        public const int WH_SYSMSGFILTER = 6;

        /// <summary>
        /// 系统级或线程级; 截获鼠标消息。
        /// </summary>
        public const int WH_MOUSE = 7;

        /// <summary>
        /// 系统级或线程级; 截获非标准硬件(非鼠标、键盘)的消息。
        /// </summary>
        public const int WH_HARDWARE = 8;

        /// <summary>
        /// 系统级或线程级; 在其他钩子调用前调用, 用于调试钩子。
        /// </summary>
        public const int WH_DEBUG = 9;

        /// <summary>
        /// 系统级或线程级; 截获发向外壳应用程序的消息。
        /// </summary>
        public const int WH_SHELL = 10;

        /// <summary>
        /// 系统级或线程级; 在程序前台线程空闲时调用。
        /// </summary>
        public const int WH_FOREGROUNDIDLE = 11;

        /// <summary>
        /// 系统级或线程级; 截获目标窗口处理完毕的消息, 在 SendMessage 调用后发生
        /// </summary>
        public const int WH_CALLWNDPROCRET = 12;

        /// <summary>
        /// Hook监视输入到线程消息队列中的键盘消息。
        /// </summary>
        public const int WH_KEYBOARD_LL = 13;

        /// <summary>
        /// Hook监视输入到线程消息队列中的鼠标消息。
        /// </summary>
        public const int WH_MOUSE_LL = 14;
    }

    /// <summary>
    /// Class CR_MODE
    /// </summary>
    public static class CR_MODE
    {
        /// <summary>
        /// The RG n_ MIN
        /// </summary>
        public const int RGN_MIN = 1;

        /// <summary>
        /// The RG n_ AND
        /// </summary>
        public const int RGN_AND = 1;

        /// <summary>
        /// The RG n_ OR
        /// </summary>
        public const int RGN_OR = 2;

        /// <summary>
        /// The RG n_ XOR
        /// </summary>
        public const int RGN_XOR = 3;

        /// <summary>
        /// The RG n_ DIFF
        /// </summary>
        public const int RGN_DIFF = 4;

        /// <summary>
        /// The RG n_ COPY
        /// </summary>
        public const int RGN_COPY = 5;

        /// <summary>
        /// The RG n_ MAX
        /// </summary>
        public const int RGN_MAX = 5;
    }

    /// <summary>
    /// GetWindowLong参数。
    /// </summary>
    public static class GWL_INDEX
    {
        /// <summary>
        /// 扩展窗口风格。
        /// </summary>
        public const int GWL_EXSTYLE = -20;

        /// <summary>
        /// 窗口风格。
        /// </summary>
        public const int GWL_STYLE = -16;

        /// <summary>
        /// 窗口过程的地址，或代表窗口过程的地址的句柄。必须使用CallWindowProc函数调用窗口过程。
        /// </summary>
        public const int GWL_WNDPROC = -4;

        /// <summary>
        /// 应用事例的句柄。
        /// </summary>
        public const int GWL_HINSTANCE = -6;

        /// <summary>
        /// 如果父窗口存在，获得父窗口句柄。
        /// </summary>
        public const int GWL_HWNDPARENT = -8;

        /// <summary>
        /// 窗口标识。
        /// </summary>
        public const int GWL_ID = -12;

        /// <summary>
        /// 获得与窗口有关的32位值。每一个窗口均有一个由创建该窗口的应用程序使用的32位值。
        /// </summary>
        public const int GWL_USERDATA = -21;

        //在hWnd参数标识了一个对话框时也可用下列值：
        /// <summary>
        /// 获得对话框过程的地址，或一个代表对话框过程的地址的句柄。必须使用函数CallWindowProc来调用对话框过程。
        /// </summary>
        public const int GWL_DLGPROC = 4;

        /// <summary>
        /// 获得在对话框过程中一个消息处理的返回值。
        /// </summary>
        public const int GWL_MSGRESULT = 0;

        /// <summary>
        /// 获得应用程序私有的额外信息，例如一个句柄或指针。
        /// </summary>
        public const int GWL_USER = 8;
    }

    /// <summary>
    /// CreateWindow和CreateWindowEx 函数windowStyle参数可取值。
    /// </summary>
    public static class WS
    {
        /// <summary>
        /// 创建一个单边框的窗口。
        /// </summary>
        public const int WS_BORDER = 0x800000;

        /// <summary>
        /// 创建一个有标题框的窗口（包括WS_BODER风格）。
        /// </summary>
        public const int WS_CAPTION = 0xc00000;

        /// <summary>
        /// 创建一个子窗口。这个风格不能与WS_POPUP风格合用。
        /// </summary>
        public const int WS_CHILD = 0x40000000;

        /// <summary>
        /// 与WS_CHILD相同。
        /// </summary>
        public const int WS_CHILDWINDOW = 0x40000000;

        /// <summary>
        /// 当在父窗口内绘图时，排除子窗口区域。在创建父窗口时使用这个风格。
        /// </summary>
        public const int WS_CLIPCHILDREN = 0x2000000;

        /// <summary>
        /// 排除子窗口之间的相对区域，也就是，当一个特定的窗口接收到WM_PAINT消息时，WS_CLIPSIBLINGS 风格将所有层叠窗口排除在绘图之外，只重绘指定的子窗口。如果未指定WS_CLIPSIBLINGS风格，并且子窗口是层叠的，则在重绘子窗口的客户区时，就会重绘邻近的子窗口。
        /// </summary>
        public const int WS_CLIPSIBLINGS = 0x4000000;

        /// <summary>
        /// 创建一个初始状态为禁止的子窗口。一个禁止状态的窗口不能接受来自用户的输入信息。
        /// </summary>
        public const int WS_DISABLED = 0x8000000;

        /// <summary>
        /// 创建一个带对话框边框风格的窗口。这种风格的窗口不能带标题条。
        /// </summary>
        public const int WS_DLGFRAME = 0x400000;

        #region WS_EX

        /// <summary>
        /// 指定以该风格创建的窗口接受一个拖拽文件。
        /// </summary>
        public const int WS_EX_ACCEPTFILES = 0x10;

        /// <summary>
        /// 当窗口可见时，将一个顶层窗口放置到任务条上。
        /// </summary>
        public const int WS_EX_APPWINDOW = 0x40000;

        /// <summary>
        /// 指定窗口有一个带阴影的边界。
        /// </summary>
        public const int WS_EX_CLIENTEDGE = 0x200;

        /// <summary>
        /// The W s_ E x_ COMPOSITED
        /// </summary>
        public const int WS_EX_COMPOSITED = 0x2000000;

        /// <summary>
        /// 在窗口的标题条包含一个问号标志。当用户点击了问号时，鼠标光标变为一个问号的指针、如果点击了一个子窗口，则子窗日接收到WM_HELP消息。子窗口应该将这个消息传递给父窗口过程，父窗口再通过HELP_WM_HELP命令调用WinHelp函数。这个Help应用程序显示一个包含子窗口帮助信息的弹出式窗口。 WS_EX_CONTEXTHELP不能与WS_MAXIMIZEBOX和WS_MINIMIZEBOX同时使用。
        /// </summary>
        public const int WS_EX_CONTEXTHELP = 0x400;

        /// <summary>
        /// 允许用户使用Tab键在窗口的子窗口间搜索。
        /// </summary>
        public const int WS_EX_CONTROLPARENT = 0x10000;

        /// <summary>
        /// 创建一个带双边的窗口；该窗口可以在dwStyle中指定WS_CAPTION风格来创建一个标题栏。
        /// </summary>
        public const int WS_EX_DLGMODALFRAME = 1;

        /// <summary>
        /// The W s_ E x_ LAYERED
        /// </summary>
        public const int WS_EX_LAYERED = 0x80000;

        /// <summary>
        /// The W s_ E x_ LAYOUTRTL
        /// </summary>
        public const int WS_EX_LAYOUTRTL = 0x400000;

        /// <summary>
        /// 窗口具有左对齐属性，这是缺省设置的。
        /// </summary>
        public const int WS_EX_LEFT = 0;

        /// <summary>
        /// 如果外壳语言是如Hebrew，Arabic，或其他支持reading order alignment的语言，则标题条（如果存在）则在客户区的左部分。若是其他语言，在该风格被忽略并且不作为错误处理。
        /// </summary>
        public const int WS_EX_LEFTSCROLLBAR = 0x4000;

        /// <summary>
        /// 窗口文本以LEFT到RIGHT（自左向右）属性的顺序显示。这是缺省设置的。
        /// </summary>
        public const int WS_EX_LTRREADING = 0;

        /// <summary>
        /// 创建一个MD子窗口。
        /// </summary>
        public const int WS_EX_MDICHILD = 0x40;

        /// <summary>
        /// The W s_ E x_ NOACTIVATE
        /// </summary>
        public const int WS_EX_NOACTIVATE = 0x8000000;

        /// <summary>
        /// The W s_ E x_ NOINHERITLAYOUT
        /// </summary>
        public const int WS_EX_NOINHERITLAYOUT = 0x100000;

        /// <summary>
        /// 指明以这个风格创建的窗口在被创建和销毁时不向父窗口发送WM_PARENTNOTFY消息。
        /// </summary>
        public const int WS_EX_NOPARENTNOTIFY = 4;

        /// <summary>
        /// 合并 WS_EX_CLIENTEDGE 和 WS_EX_WINDOWEDGE 样式。
        /// </summary>
        public const int WS_EX_OVERLAPPEDWINDOW = 0x300;

        /// <summary>
        /// WS_EX_WINDOWEDGE, WS_EX_TOOLWINDOW和WS_WX_TOPMOST风格的组合WS_EX_RIGHT:窗口具有普通的右对齐属性，这依赖于窗口类。只有在外壳语言是如Hebrew,Arabic或其他支持读顺序对齐（reading order alignment）的语言时该风格才有效，否则，忽略该标志并且不作为错误处理。
        /// </summary>
        public const int WS_EX_PALETTEWINDOW = 0x188;

        /// <summary>
        /// 为窗口泛型右对齐的属性。 这取决于窗口类。
        /// </summary>
        public const int WS_EX_RIGHT = 0x1000;

        /// <summary>
        /// 垂直滚动条在窗口的右边界。这是缺省设置的。
        /// </summary>
        public const int WS_EX_RIGHTSCROLLBAR = 0;

        /// <summary>
        /// 如果外壳语言是如Hebrew，Arabic，或其他支持读顺序对齐（reading order alignment）的语言，则窗口文本是一自左向右）RIGHT到LEFT顺序的读出顺序。若是其他语言，在该风格被忽略并且不作为错误处理。
        /// </summary>
        public const int WS_EX_RTLREADING = 0x2000;

        /// <summary>
        /// 为不接受用户输入的项创建一个3一维边界风格
        /// </summary>
        public const int WS_EX_STATICEDGE = 0x20000;

        /// <summary>
        /// 创建工具窗口，即窗口是一个游动的工具条。工具窗口的标题条比一般窗口的标题条短，并且窗口标题以小字体显示。工具窗口不在任务栏里显示，当用户按下alt＋Tab键时工具窗口不在对话框里显示。如果工具窗口有一个系统菜单，它的图标也不会显示在标题栏里，但是，可以通过点击鼠标右键或Alt＋Space来显示菜单。
        /// </summary>
        public const int WS_EX_TOOLWINDOW = 0x80;

        /// <summary>
        /// 指明以该风格创建的窗口应放置在所有非最高层窗口的上面并且停留在其L，即使窗口未被激活。使用函数SetWindowPos来设置和移去这个风格。
        /// </summary>
        public const int WS_EX_TOPMOST = 8;

        /// <summary>
        /// 指定用此样式创建的窗口是透明的。 即在 windows 下的任何窗口未由 windows 遮盖。 ，只有在更新后，用此样式创建的窗口接收消息 WM_PAINT 其下方的所有同级窗口。
        /// </summary>
        public const int WS_EX_TRANSPARENT = 0x20;

        /// <summary>
        /// 指定窗口一个凸出的边缘的一个边框。
        /// </summary>
        public const int WS_EX_WINDOWEDGE = 0x100;

        #endregion WS_EX

        /// <summary>
        /// 指定一组控制的第一个控制。这个控制组由第一个控制和随后定义的控制组成，自第二个控制开始每个控制，具有WS_GROUP风格，每个组的第一个控制带有WS_TABSTOP风格，从而使用户可以在组间移动。用户随后可以使用光标在组内的控制间改变键盘焦点。
        /// </summary>
        public const int WS_GROUP = 0x20000;

        /// <summary>
        /// 创建一个有水平滚动条的窗口。
        /// </summary>
        public const int WS_HSCROLL = 0x100000;

        /// <summary>
        /// 创建一个初始状态为最小化状态的窗口。与WS_MINIMIZE风格相同。
        /// </summary>
        public const int WS_ICONIC = 0x20000000;

        /// <summary>
        /// 创建一个初始状态为最大化状态的窗口。
        /// </summary>
        public const int WS_MAXIMIZE = 0x1000000;

        /// <summary>
        /// 创建一个具有最大化按钮的窗口。该风格不能与WS_EX_CONTEXTHELP风格同时出现，同时必须指定WS_SYSMENU风格。
        /// </summary>
        public const int WS_MAXIMIZEBOX = 0x10000;

        /// <summary>
        /// The W s_ MINIMIZE
        /// </summary>
        public const int WS_MINIMIZE = 0x20000000;

        /// <summary>
        /// The W s_ MINIMIZEBOX
        /// </summary>
        public const int WS_MINIMIZEBOX = 0x20000;

        /// <summary>
        /// 产生一个层叠的窗口。一个层叠的窗口有一个标题条和一个边框。与WS_TILED风格相同。
        /// </summary>
        public const int WS_OVERLAPPED = 0;

        /// <summary>
        /// 创建一个具有WS_OVERLAPPED，WS_CAPTION，WS_SYSMENU WS_THICKFRAME，WS_MINIMIZEBOX，WS_MAXIMIZEBOX风格的层叠窗口，与WS_TILEDWINDOW风格相同。
        /// </summary>
        public const int WS_OVERLAPPEDWINDOW = 0xcf0000;

        /// <summary>
        /// 创建一个弹出式窗口。该风格不能与WS_CHLD风格同时使用。
        /// </summary>
        public const int WS_POPUP = -2147483648;

        /// <summary>
        /// 创建一个具有WS_BORDER，WS_POPUP,WS_SYSMENU风格的窗口，WS_CAPTION和WS_POPUPWINDOW必须同时设定才能使窗口某单可见。
        /// </summary>
        public const int WS_POPUPWINDOW = -2138570752;

        /// <summary>
        /// 创建一个可调边框的窗口，与WS_THICKFRAME风格相同。
        /// </summary>
        public const int WS_SIZEBOX = 0x40000;

        /// <summary>
        /// 创建一个在标题条上带有窗口菜单的窗口，必须同时设定WS_CAPTION风格。
        /// </summary>
        public const int WS_SYSMENU = 0x80000;

        /// <summary>
        /// 创建一个控制，这个控制在用户按下Tab键时可以获得键盘焦点。按下Tab键后使键盘焦点转移到下一具有WS_TABSTOP风格的控制。
        /// </summary>
        public const int WS_TABSTOP = 0x10000;

        /// <summary>
        /// 创建一个具有可调边框的窗口，与WS_SIZEBOX风格相同。
        /// </summary>
        public const int WS_THICKFRAME = 0x40000;

        /// <summary>
        /// 产生一个层叠的窗口。一个层叠的窗口有一个标题和一个边框。与WS_OVERLAPPED风格相同。
        /// </summary>
        public const int WS_TILED = 0;

        /// <summary>
        /// 创建一个具有WS_OVERLAPPED，WS_CAPTION，WS_SYSMENU， WS_THICKFRAME，WS_MINIMIZEBOX，WS_MAXMIZEBOX风格的层叠窗口。与WS_OVERLAPPEDWINDOW风格相同。
        /// </summary>
        public const int WS_TILEDWINDOW = 0xcf0000;

        /// <summary>
        /// 创建一个初始状态为可见的窗口。
        /// </summary>
        public const int WS_VISIBLE = 0x10000000;

        /// <summary>
        /// 创建一个有垂直滚动条的窗口。
        /// </summary>
        public const int WS_VSCROLL = 0x200000;
    }

    /// <summary>
    /// Class DCX_FLAGS
    /// </summary>
    public static class DCX_FLAGS
    {
        /// <summary>
        /// 从高速缓存而不是从OWNDC或CLASSDC窗口中返回设备上下文环境。从本质上覆盖CS_OWNDC和CS_CLASSDC。
        /// </summary>
        public const int DCX_CACHE = 2;

        /// <summary>
        /// 排除hWnd参数所标识窗口上的所有子窗口的可见区域。
        /// </summary>
        public const int DCX_CLIPCHILDREN = 8;

        /// <summary>
        /// 排除hWnd参数所标识窗口上的所有兄弟窗口的可见区域。
        /// </summary>
        public const int DCX_CLIPSIBLINGS = 0x10;

        /// <summary>
        /// 从返回设备上下文环境的可见区域中排除由hrgnClip指定的剪切区域。
        /// </summary>
        public const int DCX_EXCLUDERGN = 0x40;

        /// <summary>
        /// 从设备场景剪裁区中排除刷新区域。
        /// </summary>
        public const int DCX_EXCLUDEUPDATE = 0x100;

        /// <summary>
        /// 对hrgnClip指定的剪切区域与返回设备描述的可见区域作交运算。
        /// </summary>
        public const int DCX_INTERSECTRGN = 0x80;

        /// <summary>
        /// 指定区域与设备场景刷新区域相交。
        /// </summary>
        public const int DCX_INTERSECTUPDATE = 0x200;

        /// <summary>
        /// 即使在排除指定窗口的LockWindowUpdate函数调用有效的情况下也许会绘制，该参数用于在跟踪期间进行绘制。
        /// </summary>
        public const int DCX_LOCKWINDOWUPDATE = 0x400;

        /// <summary>
        /// 当设备上下文环境被释放时，并不重置该设备上下文环境的特性为缺省特性。
        /// </summary>
        public const int DCX_NORESETATTRS = 4;

        /// <summary>
        /// 使用父窗口的可见区域，父窗口的WS_CIPCHILDREN和CS_PARENTDC风格被忽略，并把设备上下文环境的原点，设在由hWnd所标识的窗口的左上角。
        /// </summary>
        public const int DCX_PARENTCLIP = 0x20;

        /// <summary>
        /// 返回与窗口矩形而不是与客户矩形相对应的设备上下文环境。
        /// </summary>
        public const int DCX_WINDOW = 1;
    }

    /// <summary>
    /// Class WINDOWMESSAGES
    /// </summary>
    public static class WINDOWMESSAGES
    {
        /// <summary>
        /// The tray mouse message
        /// </summary>
        public const int TrayMouseMessage = 0x800;
        /// <summary>
        /// 一个窗口被激活或失去激活状态。
        /// </summary>
        public const int WM_ACTIVATE = 6;

        /// <summary>
        /// 发此消息给应用程序哪个窗口是激活的，哪个是非激活的。
        /// </summary>
        public const int WM_ACTIVATEAPP = 0x1c;

        /// <summary>
        /// 指定首个AFX消息(MFC)。
        /// </summary>
        public const int WM_AFXFIRST = 0x360;

        /// <summary>
        /// 指定末个afx消息。
        /// </summary>
        public const int WM_AFXLAST = 0x37f;

        /// <summary>
        /// 用于帮助应用程序自定义私有消息,通常形式为:WM_APP + X。
        /// </summary>
        public const int WM_APP = 0x8000;

        /// <summary>
        /// The W m_ APPCOMMAND
        /// </summary>
        public const int WM_APPCOMMAND = 0x319;

        /// <summary>
        /// 通过剪贴板观察窗口发送本消息给剪贴板的所有者,以请求一个CF_OWNERDISPLAY格式的剪贴板的名字。
        /// </summary>
        public const int WM_ASKCBFORMATNAME = 780;

        /// <summary>
        /// 当用户取消程序日志激活状态时,发送本消息给那个应用程序。该消息使用空窗口句柄发送。
        /// </summary>
        public const int WM_CANCELJOURNAL = 0x4b;

        /// <summary>
        /// 发送本消息来取消某种正在进行的模态(操作)(如鼠示捕获),例如:启动一个模态窗口时,父窗会收到本消息;该消息无参数。
        /// </summary>
        public const int WM_CANCELMODE = 0x1f;

        /// <summary>
        /// 当它失去捕获的鼠标时,发送本消息给窗口。
        /// </summary>
        public const int WM_CAPTURECHANGED = 0x215;

        /// <summary>
        /// 当一个窗口从剪贴板观察链中移去时,发送本消息给剪贴板观察链的首个窗口。
        /// </summary>
        public const int WM_CHANGECBCHAIN = 0x30d;

        /// <summary>
        /// The W m_ CHANGEUISTATE
        /// </summary>
        public const int WM_CHANGEUISTATE = 0x127;

        /// <summary>
        /// 按下某按键,并已发出WM_KEYDOWN、WM_KEYUP消息,本消息包含被按下的按键的字符码。
        /// </summary>
        public const int WM_CHAR = 0x102;

        /// <summary>
        /// LBS_WANTKEYBOARDINPUT风格的列表框会发送本消息给其所有者,以便响应WM_CHAR消息。
        /// </summary>
        public const int WM_CHARTOITEM = 0x2f;

        /// <summary>
        /// 点击窗口标题栏或当窗口被激活、移动、大小改变时,会发送本消息给MDI子窗口。
        /// </summary>
        public const int WM_CHILDACTIVATE = 0x22;

        /// <summary>
        /// 应用程序发送本消息给编辑框或组合框,以清除当前选择的内容。
        /// </summary>
        public const int WM_CLEAR = 0x303;

        /// <summary>
        /// 用户关闭窗口时会发送本消息,紧接着会发送WM_DESTROY消息。
        /// </summary>
        public const int WM_CLOSE = 0x10;

        /// <summary>
        /// 用户选择一条菜单命令项或某控件发送一条通知消息给其父窗,或某快捷键被翻译时,本消息被发送。
        /// </summary>
        public const int WM_COMMAND = 0x111;

        /// <summary>
        /// Win3.1中,当串口事件产生时,通讯设备驱动程序发送消息本消息给系统,指示输入输出队列的状态。
        /// </summary>
        public const int WM_COMMNOTIFY = 0x44;

        /// <summary>
        /// 显示内存已经很少了。
        /// </summary>
        public const int WM_COMPACTING = 0x41;

        /// <summary>
        /// 可发送本消息来确定组合框(CBS_SORT)或列表框(LBS_SORT)中新增项的相对位置。
        /// </summary>
        public const int WM_COMPAREITEM = 0x39;

        /// <summary>
        /// 当用户在某窗口中点击右键就发送本消息给该窗口,设置右键菜单。
        /// </summary>
        public const int WM_CONTEXTMENU = 0x7b;

        /// <summary>
        /// 应用程序发送本消息给一个编辑框或组合框,以便用CF_TEXT格式复制当前选择的文本到剪贴板。
        /// </summary>
        public const int WM_COPY = 0x301;

        /// <summary>
        /// 当一个应用程序传递数据给另一个应用程序时发送本消息。
        /// </summary>
        public const int WM_COPYDATA = 0x4a;

        /// <summary>
        /// 新建一个窗口。
        /// </summary>
        public const int WM_CREATE = 1;

        /// <summary>
        /// The W m_ CTLCOLOR
        /// </summary>
        public const int WM_CTLCOLOR = 0x19;

        /// <summary>
        /// 设置按钮的背景色。
        /// </summary>
        public const int WM_CTLCOLORBTN = 0x135;

        /// <summary>
        /// 设置对话框的背景色,通常是在WinnApp中使用SetDialogBkColor函数实现。
        /// </summary>
        public const int WM_CTLCOLORDLG = 310;

        /// <summary>
        /// 当一个编辑框控件将要被绘制时,发送本消息给其父窗;通过响应本消息,所有者窗口可通过使用给定的相关显示设备的句柄来设置编辑框的文本和背景色。
        /// </summary>
        public const int WM_CTLCOLOREDIT = 0x133;

        /// <summary>
        /// 当一个列表框控件将要被绘制前,发送本消息给其父窗;通过响应本消息,所有者窗口可通过使用给定的相关显示设备的句柄来设置列表框的文本和背景色。
        /// </summary>
        public const int WM_CTLCOLORLISTBOX = 0x134;

        /// <summary>
        /// 系统绘制消息框前发送本消息给消息框的所有者窗口,通过响应本消息,所有者窗口可通过使用给定的相关显示设备的句柄来设置消息框的文本和背景色。
        /// </summary>
        public const int WM_CTLCOLORMSGBOX = 0x132;

        /// <summary>
        /// 设置滚动条的背景色。
        /// </summary>
        public const int WM_CTLCOLORSCROLLBAR = 0x137;

        /// <summary>
        /// 设置一个静态控件的背景色。
        /// </summary>
        public const int WM_CTLCOLORSTATIC = 0x138;

        /// <summary>
        /// 应用程序发送本消息给一个编辑框或组合框来删除当前选择的文本。
        /// </summary>
        public const int WM_CUT = 0x300;

        /// <summary>
        /// 当使用TranslateMessage函数翻译WM_KEYUP消息时,发送本消息给拥有键盘焦点的窗口,注:德语键盘上,有些按键只是给字符添加音标的,并不产生字符,故称"死字符"。
        /// </summary>
        public const int WM_DEADCHAR = 0x103;

        /// <summary>
        /// 当列表框或组合框被销毁或通过LB_DELETESTRING、LB_RESETCONTENT、CB_DELETESTRING或CB_RESETCONTENT消息删除某些项时,会发送本消息给这些控件的所有者。
        /// </summary>
        public const int WM_DELETEITEM = 0x2d;

        /// <summary>
        /// 销毁一个窗口。
        /// </summary>
        public const int WM_DESTROY = 2;

        /// <summary>
        /// 当调用EmptyClipboard函数时,发送本消息给剪贴板的所有者。
        /// </summary>
        public const int WM_DESTROYCLIPBOARD = 0x307;

        /// <summary>
        /// 当设备的硬件配置改变时,发送本消息给应用程序或设备驱动程序。
        /// </summary>
        public const int WM_DEVICECHANGE = 0x219;

        /// <summary>
        /// 改变设备模式设置(\"win.ini\")时,处理本消息的应用程序可重新初始化它们的设备模式设置。
        /// </summary>
        public const int WM_DEVMODECHANGE = 0x1b;

        /// <summary>
        /// 当显示器的分辨率改变后,发送本消息给所有窗口。
        /// </summary>
        public const int WM_DISPLAYCHANGE = 0x7e;

        /// <summary>
        /// 当剪贴板的内容变化时,发送本消息给剪贴板观察链的首个窗口;它允许用剪贴板观察窗口来显示剪贴板的新内容。
        /// </summary>
        public const int WM_DRAWCLIPBOARD = 0x308;

        /// <summary>
        /// 按钮、组合框、列表框、菜单的外观改变时会发送本消息给这些控件的所有者。
        /// </summary>
        public const int WM_DRAWITEM = 0x2b;

        /// <summary>
        /// 鼠标拖放时,放下事件产生时发送本消息,比如:文件拖放功能。
        /// </summary>
        public const int WM_DROPFILES = 0x233;

        /// <summary>
        /// 使一个窗口处于可用状态。
        /// </summary>
        public const int WM_ENABLE = 10;

        /// <summary>
        /// 关机或注销时系统会发出WM_QUERYENDSESSION消息,然后将本消息发送给应用程序,通知程序会话结束。
        /// </summary>
        public const int WM_ENDSESSION = 0x16;

        /// <summary>
        /// 当一个模态对话框或菜单进入空闲状态时,发送本消息给它的所有者,一个模态对话框或菜单进入空闲状态就是在处理完一条或几条先前的消息后,没有消息在消息列队中等待。
        /// </summary>
        public const int WM_ENTERIDLE = 0x121;

        /// <summary>
        /// 发送本消息通知应用程序的主窗口已进入菜单循环模式。
        /// </summary>
        public const int WM_ENTERMENULOOP = 0x211;

        /// <summary>
        /// 当某窗口进入移动或调整大小的模式循环时,本消息发送到该窗口。
        /// </summary>
        public const int WM_ENTERSIZEMOVE = 0x231;

        /// <summary>
        /// 当一个窗口的背景必须被擦除时本消息会被触发(如:窗口大小改变时。
        /// </summary>
        public const int WM_ERASEBKGND = 20;

        /// <summary>
        /// 发送本消息通知应用程序的主窗口已退出菜单循环模式。
        /// </summary>
        public const int WM_EXITMENULOOP = 530;

        /// <summary>
        /// 确定用户改变窗口大小或改变窗口位置的事件是何时完成的。
        /// </summary>
        public const int WM_EXITSIZEMOVE = 0x232;

        /// <summary>
        /// 当系统的字体资源库变化时发送本消息给所有顶级窗口。
        /// </summary>
        public const int WM_FONTCHANGE = 0x1d;

        /// <summary>
        /// 发送本消息给某个与对话框程序关联的控件,系统控制方位键和TAB键使输入进入该控件,通过响应本消息应用程序可把它当成一个特殊的输入控件并能处理它。
        /// </summary>
        public const int WM_GETDLGCODE = 0x87;

        /// <summary>
        /// 得到当前控件绘制其文本所用的字体。
        /// </summary>
        public const int WM_GETFONT = 0x31;

        /// <summary>
        /// 确定某热键与某窗口是否相关联。
        /// </summary>
        public const int WM_GETHOTKEY = 0x33;

        /// <summary>
        /// 本消息发送给某个窗口,用于返回与某窗口有关联的大图标或小图标的句柄。
        /// </summary>
        public const int WM_GETICON = 0x7f;

        /// <summary>
        /// 当窗口将要改变大小或位置时,由系统发送本消息给窗口,用户拖动一个可重置大小的窗口时便会发出本消息。
        /// </summary>
        public const int WM_GETMINMAXINFO = 0x24;

        /// <summary>
        /// The W m_ GETOBJECT
        /// </summary>
        public const int WM_GETOBJECT = 0x3d;

        /// <summary>
        /// 复制窗口的文本到缓冲区。
        /// </summary>
        public const int WM_GETTEXT = 13;

        /// <summary>
        /// 得到窗口的文本长度(不含结束符)。
        /// </summary>
        public const int WM_GETTEXTLENGTH = 14;

        /// <summary>
        /// The W m_ HANDHELDFIRST
        /// </summary>
        public const int WM_HANDHELDFIRST = 0x358;

        /// <summary>
        /// The W m_ HANDHELDLAST
        /// </summary>
        public const int WM_HANDHELDLAST = 0x35f;

        /// <summary>
        /// The W m_ HELP
        /// </summary>
        public const int WM_HELP = 0x53;

        /// <summary>
        /// 当用户按下由RegisterHotKey函数注册的热键时,发送本消息。
        /// </summary>
        public const int WM_HOTKEY = 0x312;

        /// <summary>
        /// 当窗口的标准水平滚动条产生一个滚动事件时,发送本消息给该窗口。
        /// </summary>
        public const int WM_HSCROLL = 0x114;

        /// <summary>
        /// 本消息通过一个剪贴板观察窗口发送给剪贴板的所有者,它发生在当剪贴板包含CFOWNERDISPALY格式的数据,并且有个事件在剪贴板观察窗的水平滚动条上,所有者应滚动剪贴板图像并更新滚动条的值。
        /// </summary>
        public const int WM_HSCROLLCLIPBOARD = 0x30e;

        /// <summary>
        /// 本消息发送给某个最小化的窗口,仅当它在画图标前它的背景必须被重画。
        /// </summary>
        public const int WM_ICONERASEBKGND = 0x27;

        /// <summary>
        /// 当打开输入法输入文字时,会发送WM_IME_CHAR消息。
        /// </summary>
        public const int WM_IME_CHAR = 0x286;

        /// <summary>
        /// 当用户改变了编码状态时,发送本消息,应用程序可通过调用ImmGetCompositionString函数获取新的编码状态。
        /// </summary>
        public const int WM_IME_COMPOSITION = 0x10f;

        /// <summary>
        /// 用户接口窗口不能增加编码窗口的尺寸时,IME用户接口窗口将发送WM_IME_COMPOSITIONFULL消息,可不处理,注:输入法相关。
        /// </summary>
        public const int WM_IME_COMPOSITIONFULL = 0x284;

        /// <summary>
        /// 可使用WM_IME_CONTROL消息来改变字母组合窗口的位置,注:输入法相关。
        /// </summary>
        public const int WM_IME_CONTROL = 0x283;

        /// <summary>
        /// 当编码结束时,IME发送本消息,用户程序可接受本消息,以便自己显示用户输入的编码,注:输入法相关。
        /// </summary>
        public const int WM_IME_ENDCOMPOSITION = 270;

        /// <summary>
        /// 在输入法录字窗口中按下按键时,触发发送本消息。
        /// </summary>
        public const int WM_IME_KEYDOWN = 0x290;

        /// <summary>
        /// 当用户改变了编码状态时,发送本消息,应用程序可通过调用ImmGetCompositionString函数获取新的编码状态。
        /// </summary>
        public const int WM_IME_KEYLAST = 0x10f;

        /// <summary>
        /// 在输入法录字窗口中释放按键时,触发发送本消息。
        /// </summary>
        public const int WM_IME_KEYUP = 0x291;

        /// <summary>
        /// 可使用WM_IME_NOTIFY消息来通知关于IME窗口状态的常规改变,注:输入法相关。
        /// </summary>
        public const int WM_IME_NOTIFY = 0x282;

        /// <summary>
        /// 应用程序请求输入法时,触发发送本消息。
        /// </summary>
        public const int WM_IME_REQUEST = 0x288;

        /// <summary>
        /// 系统发出WM_IME_SELECT以便选择一个新的IME输入法,注:输入法相关。
        /// </summary>
        public const int WM_IME_SELECT = 0x285;

        /// <summary>
        /// 应用程序的窗口激活时,系统将向应用程序发送WM_IME_SETCONTEXT消息,注:输入法相关。
        /// </summary>
        public const int WM_IME_SETCONTEXT = 0x281;

        /// <summary>
        /// 当用户开始输入编码时,系统立即发送该消息到IME中,IME打开编码窗口,注:输入法相关。
        /// </summary>
        public const int WM_IME_STARTCOMPOSITION = 0x10d;

        /// <summary>
        /// 在某对话框程序被显示前发送本消息给该对话框程序,通常用本消息对控件进行一些初始化工作和执行其它任务。
        /// </summary>
        public const int WM_INITDIALOG = 0x110;

        /// <summary>
        /// 当一个菜单将被激活时发送本消息,它发生在用户点击了某菜单项或按下某菜单键。它允许程序在显示前更改菜单。
        /// </summary>
        public const int WM_INITMENU = 0x116;

        /// <summary>
        /// 当一个下拉菜单或子菜单将被激活时发送本消息,它允许程序在它显示前更改菜单,却不更改全部菜单。
        /// </summary>
        public const int WM_INITMENUPOPUP = 0x117;

        /// <summary>
        /// The W m_ INPUT
        /// </summary>
        public const int WM_INPUT = 0xff;

        /// <summary>
        /// 切换输入法后,系统会发送本消息给受影响的顶层窗口。
        /// </summary>
        public const int WM_INPUTLANGCHANGE = 0x51;

        /// <summary>
        /// 当用户通过过单击任务栏上的语言指示符或某快捷键组合选择改变输入法时系统会向焦点窗口发送本消息。
        /// </summary>
        public const int WM_INPUTLANGCHANGEREQUEST = 80;

        /// <summary>
        /// The W m_ KEYDOWN
        /// </summary>
        public const int WM_KEYDOWN = 0x100;

        /// <summary>
        /// 用于WinCE系统,本消息在使用GetMessage和PeekMessage函数时,用于过滤键盘消息。
        /// </summary>
        public const int WM_KEYFIRST = 0x100;

        /// <summary>
        /// 用于WinCE系统,本消息在使用GetMessage和PeekMessage函数时,用于过滤键盘消息。
        /// </summary>
        public const int WM_KEYLAST = 0x108;

        /// <summary>
        /// The W m_ KEYUP
        /// </summary>
        public const int WM_KEYUP = 0x101;

        /// <summary>
        /// 使一个窗口失去焦点。
        /// </summary>
        public const int WM_KILLFOCUS = 8;

        /// <summary>
        /// 双击鼠标左键。
        /// </summary>
        public const int WM_LBUTTONDBLCLK = 0x203;

        /// <summary>
        /// 按下鼠标左键。
        /// </summary>
        public const int WM_LBUTTONDOWN = 0x201;

        /// <summary>
        /// 释放鼠标左键。
        /// </summary>
        public const int WM_LBUTTONUP = 0x202;

        /// <summary>
        /// 双击鼠标中键。
        /// </summary>
        public const int WM_MBUTTONDBLCLK = 0x209;

        /// <summary>
        /// 按下鼠标中键。
        /// </summary>
        public const int WM_MBUTTONDOWN = 0x207;

        /// <summary>
        /// 释放鼠标中键。
        /// </summary>
        public const int WM_MBUTTONUP = 520;

        /// <summary>
        /// 发送本消息给多文档应用程序的客户窗口通知客户窗口激活另一个MDI子窗口,当客户窗口收到本消息后,它发出WM_MDIACTIVE消息给MDI子窗口(未激活)来激活它。
        /// </summary>
        public const int WM_MDIACTIVATE = 0x222;

        /// <summary>
        /// 发送本消息给MDI客户窗口,以层叠方式重新排列所有MDI子窗口。
        /// </summary>
        public const int WM_MDICASCADE = 0x227;

        /// <summary>
        /// 发送本消息给多文档应用程序的客户窗口来创建一个MDI子窗口。
        /// </summary>
        public const int WM_MDICREATE = 0x220;

        /// <summary>
        /// 发送本消息给多文档应用程序的客户窗口来关闭一个MDI子窗口。
        /// </summary>
        public const int WM_MDIDESTROY = 0x221;

        /// <summary>
        /// 发送本消息给MDI客户窗口以找到激活的子窗口句柄。
        /// </summary>
        public const int WM_MDIGETACTIVE = 0x229;

        /// <summary>
        /// The W m_ MDIICON ar ANGE
        /// </summary>
        public const int WM_MDIICONArANGE = 0x228;

        /// <summary>
        /// 发送本消息给MDI客户窗口来最大化一个MDI子窗口。
        /// </summary>
        public const int WM_MDIMAXIMIZE = 0x225;

        /// <summary>
        /// 发送本消息给MDI客户窗口,激活下一个或前一个窗口。
        /// </summary>
        public const int WM_MDINEXT = 0x224;

        /// <summary>
        /// 发送本消息给多文档应用程序的客户窗口,根据当前MDI子窗口更新MDI框架窗口的菜单。
        /// </summary>
        public const int WM_MDIREFRESHMENU = 0x234;

        /// <summary>
        /// 发送本消息给MDI客户窗口,让子窗口从最大最小化恢复到原来的大小。
        /// </summary>
        public const int WM_MDIRESTORE = 0x223;

        /// <summary>
        /// 发送本消息给MDI客户窗口,用MDI菜单代替子窗口的菜单。
        /// </summary>
        public const int WM_MDISETMENU = 560;

        /// <summary>
        /// 发送本消息给MDI客户窗口,以平铺方式重新排列所有MDI子窗口。
        /// </summary>
        public const int WM_MDITILE = 550;

        /// <summary>
        /// 按钮、组合框、列表框、列表控件、菜单项被创建时会发送本消息给这些控件的所有者。
        /// </summary>
        public const int WM_MEASUREITEM = 0x2c;

        /// <summary>
        /// 当菜单已被激活且用户按下了某菜单字符键(菜单字符键用括号括着、带下划线,不同于快捷键),发送本消息给菜单的所有者。
        /// </summary>
        public const int WM_MENUCHAR = 0x120;

        /// <summary>
        /// 当用户在一个菜单上作出选择时,会发送本消息,菜单要具有MNS_NOTIFYBYPOS风格(在MENUINFO结构体中设置)。
        /// </summary>
        public const int WM_MENUCOMMAND = 0x126;

        /// <summary>
        /// 当用户拖动菜单项时,发送本消息给拖放菜单的拥有者,可让菜单支持拖拽,可使用OLE拖放传输协议启动拖放操作,注:菜单要具有MNS_DRAGDROP风格。
        /// </summary>
        public const int WM_MENUDRAG = 0x123;

        /// <summary>
        /// 当鼠标光标进入或离开菜单项时,本消息发送给支持拖放的菜单的拥有者,相关结构体:MENUGETOBJECTINFO,注:菜单要具有MNS_DRAGDROP风格。
        /// </summary>
        public const int WM_MENUGETOBJECT = 0x124;

        /// <summary>
        /// 本消息允许程序为菜单项提供一个感知上下文的菜单(即快捷菜单),要为菜单项显示一下上下文菜单,请使用TPM_RECURSE标识调用TrackPopupMenuEx函数。
        /// </summary>
        public const int WM_MENURBUTTONUP = 290;

        /// <summary>
        /// 当用户选择一条菜单项时,发送本消息给菜单的所有者(一般是窗口)。
        /// </summary>
        public const int WM_MENUSELECT = 0x11f;

        /// <summary>
        /// 当鼠标光标在某个未激活窗口内,而用户正按着鼠标的某个键时,会发送本消息给当前窗口。
        /// </summary>
        public const int WM_MOUSEACTIVATE = 0x21;

        /// <summary>
        /// The W m_ MOUSEFIRST
        /// </summary>
        public const int WM_MOUSEFIRST = 0x200;

        /// <summary>
        /// 鼠标移过控件时,触发发送本消息。
        /// </summary>
        public const int WM_MOUSEHOVER = 0x2a1;

        /// <summary>
        /// The W m_ MOUSELAST
        /// </summary>
        public const int WM_MOUSELAST = 0x20d;

        /// <summary>
        /// 鼠标离开控件时,触发发送本消息。
        /// </summary>
        public const int WM_MOUSELEAVE = 0x2a3;

        /// <summary>
        /// 移动鼠标。
        /// </summary>
        public const int WM_MOUSEMOVE = 0x200;

        /// <summary>
        /// 当鼠标轮子转动时,发送本消息给当前拥有焦点的控件。
        /// </summary>
        public const int WM_MOUSEWHEEL = 0x20a;

        /// <summary>
        /// 移动一个窗口。
        /// </summary>
        public const int WM_MOVE = 3;

        /// <summary>
        /// 当用户在移动窗口时发送本消息,通过本消息应用程序以监视窗口大小和位置,也可修改它们。
        /// </summary>
        public const int WM_MOVING = 0x216;

        /// <summary>
        /// 本消息发送给某窗口,在窗口的非客户区被激活时重绘窗口。
        /// </summary>
        public const int WM_NCACTIVATE = 0x86;

        /// <summary>
        /// 当某窗口的客户区的大小和位置须被计算时发送本消息。
        /// </summary>
        public const int WM_NCCALCSIZE = 0x83;

        /// <summary>
        /// 当某窗口首次被创建时,本消息在WM_CREATE消息发送前发送。
        /// </summary>
        public const int WM_NCCREATE = 0x81;

        /// <summary>
        /// 本消息通知某窗口,非客户区正在销毁。
        /// </summary>
        public const int WM_NCDESTROY = 130;

        /// <summary>
        /// 当用户在在非客户区移动鼠标、按住或释放鼠标时发送本消息(击中测试);若鼠标没有被捕获,则本消息在窗口得到光标之后发出,否则消息发送到捕获到鼠标的窗口。
        /// </summary>
        public const int WM_NCHITTEST = 0x84;

        /// <summary>
        /// 当用户双击鼠标左键的同时光标在某窗口的非客户区内时,会发送本消息。
        /// </summary>
        public const int WM_NCLBUTTONDBLCLK = 0xa3;

        /// <summary>
        /// 当光标在某窗口的非客户区内的同时按下鼠标左键,会发送本消息。
        /// </summary>
        public const int WM_NCLBUTTONDOWN = 0xa1;

        /// <summary>
        /// 当用户释放鼠标左键的同时光标在某窗口的非客户区内时,会发送本消息。
        /// </summary>
        public const int WM_NCLBUTTONUP = 0xa2;

        /// <summary>
        /// 当用户双击鼠标中键的同时光标在某窗口的非客户区内时,会发送本消息。
        /// </summary>
        public const int WM_NCMBUTTONDBLCLK = 0xa9;

        /// <summary>
        /// 当用户按下鼠标中键的同时光标在某窗口的非客户区内时,会发送本消息。
        /// </summary>
        public const int WM_NCMBUTTONDOWN = 0xa7;

        /// <summary>
        /// 当用户释放鼠标中键的同时光标在某窗口的非客户区内时,会发送本消息。
        /// </summary>
        public const int WM_NCMBUTTONUP = 0xa8;

        /// <summary>
        /// The W m_ NCMOUSELEAVE
        /// </summary>
        public const int WM_NCMOUSELEAVE = 0x2a2;

        /// <summary>
        /// 当光标在某窗口的非客户区内移动时,发送本消息给该窗口。
        /// </summary>
        public const int WM_NCMOUSEMOVE = 160;

        /// <summary>
        /// 当窗口框架(非客户区)必须被被重绘时,应用程序发送本消息给该窗口。
        /// </summary>
        public const int WM_NCPAINT = 0x85;

        /// <summary>
        /// 当用户双击鼠标右键的同时光标在某窗口的非客户区内时,会发送本消息。
        /// </summary>
        public const int WM_NCRBUTTONDBLCLK = 0xa6;

        /// <summary>
        /// 当用户按下鼠标右键的同时光标在某窗口的非客户区内时,会发送本消息。
        /// </summary>
        public const int WM_NCRBUTTONDOWN = 0xa4;

        /// <summary>
        /// 当用户释放鼠标右键的同时光标在某窗口的非客户区内时,会发送本消息。
        /// </summary>
        public const int WM_NCRBUTTONUP = 0xa5;

        /// <summary>
        /// The W m_ NCUAHDRAWCAPTION
        /// </summary>
        public const int WM_NCUAHDRAWCAPTION = 0xae;

        /// <summary>
        /// The W m_ NCUAHDRAWFRAME
        /// </summary>
        public const int WM_NCUAHDRAWFRAME = 0xaf;

        /// <summary>
        /// The W m_ NCXBUTTONDBLCLK
        /// </summary>
        public const int WM_NCXBUTTONDBLCLK = 0xad;

        /// <summary>
        /// The W m_ NCXBUTTONDOWN
        /// </summary>
        public const int WM_NCXBUTTONDOWN = 0xab;

        /// <summary>
        /// The W m_ NCXBUTTONUP
        /// </summary>
        public const int WM_NCXBUTTONUP = 0xac;

        /// <summary>
        /// 发送本消息给一个对话框程序窗口过程,以便在各控件间设置键盘焦点位置。
        /// </summary>
        public const int WM_NEXTDLGCTL = 40;

        /// <summary>
        /// 当使用左箭头光标键或右箭头光标键在菜单条与系统菜单之间切换时,会发送本消息给应用程序,相关结构体:MDINEXTMENU。
        /// </summary>
        public const int WM_NEXTMENU = 0x213;

        /// <summary>
        /// 当某控件的某事件已发生或该控件需得到一些信息时,发送本消息给其父窗。
        /// </summary>
        public const int WM_NOTIFY = 0x4e;

        /// <summary>
        /// 公用控件、自定义控件和其父窗通过本消息判断控件在WM_NOTIFY通知消息中是使用ANSI还是UNICODE,使用本消息能使某个控件与它的父控件间进行相互通信。
        /// </summary>
        public const int WM_NOTIFYFORMAT = 0x55;

        /// <summary>
        /// 空消息,可检测程序是否有响应等。
        /// </summary>
        public const int WM_NULL = 0;

        /// <summary>
        /// 窗口重绘。
        /// </summary>
        public const int WM_PAINT = 15;

        /// <summary>
        /// 当剪贴板包含CF_OWNERDIPLAY格式的数据,并且剪贴板观察窗口的客户区需要重画时,触发发送本消息。
        /// </summary>
        public const int WM_PAINTCLIPBOARD = 0x309;

        /// <summary>
        /// 当一个最小化的窗口图标将被重绘时发送本消息。
        /// </summary>
        public const int WM_PAINTICON = 0x26;

        /// <summary>
        /// 本消息在一个拥有焦点的窗口实现它的逻辑调色板后,发送本消息给所有顶级并重叠的窗口,以此来改变系统调色板。
        /// </summary>
        public const int WM_PALETTECHANGED = 0x311;

        /// <summary>
        /// 当一个应用程序正要实现它的逻辑调色板时,发本消息通知所有的应用程序。
        /// </summary>
        public const int WM_PALETTEISCHANGING = 0x310;

        /// <summary>
        /// 当MDI子窗口被创建或被销毁,或用户按了一下鼠标键而光标在子窗口上时,发送本消息给其父窗。
        /// </summary>
        public const int WM_PARENTNOTIFY = 0x210;

        /// <summary>
        /// 应用程序发送本消息给编辑框或组合框,以便从剪贴板中得到数据。
        /// </summary>
        public const int WM_PASTE = 770;

        /// <summary>
        /// 指定首个Pen Window消息,参见:PENWIN.H/WINUSER.H。
        /// </summary>
        public const int WM_PENWINFIRST = 0x380;

        /// <summary>
        /// 指定末个Pen Window消息,参见:PENWIN.H/WINUSER.H。
        /// </summary>
        public const int WM_PENWINLAST = 0x38f;

        /// <summary>
        /// 当系统将要进入暂停状态时发送本消息(适用于16位的windows)。
        /// </summary>
        public const int WM_POWER = 0x48;

        /// <summary>
        /// 本消息发送给应用程序来通知它有关电源管理事件,比如待机休眠时会发送本消息。
        /// </summary>
        public const int WM_POWERBROADCAST = 0x218;

        /// <summary>
        /// 发送本消息给一个窗口请求在指定的设备上下文中绘制自身,可用于窗口截图,但对子控件截图时得到的是与子控件等大的黑块。
        /// </summary>
        public const int WM_PRINT = 0x317;

        /// <summary>
        /// 发送本消息给一个窗口请求在指定的设备上下文中绘制其客户区(最通常是在一个打印机设备上下文中)。
        /// </summary>
        public const int WM_PRINTCLIENT = 0x318;

        /// <summary>
        /// 本消息发送给最小化的窗口(iconic),当该窗口将被拖放而其窗口类中没有定义图标,应用程序能返回一个图标或光标的句柄。当用户拖放图标时系统会显示这个图标或光标。
        /// </summary>
        public const int WM_QUERYDRAGICON = 0x37;

        /// <summary>
        /// 关机或注销时系统会按优先级给各进程发送WM_QUERYENDSESSION,告诉应用程序要关机或注销了。
        /// </summary>
        public const int WM_QUERYENDSESSION = 0x11;

        /// <summary>
        /// 本消息发送给将要收到焦点的窗口,本消息能使窗口在收到焦点时同时有机会实现逻辑调色板。
        /// </summary>
        public const int WM_QUERYNEWPALETTE = 0x30f;

        /// <summary>
        /// 最小化的窗口即将被恢复以前的大小位置。
        /// </summary>
        public const int WM_QUERYOPEN = 0x13;

        /// <summary>
        /// The W m_ QUERYUISTATE
        /// </summary>
        public const int WM_QUERYUISTATE = 0x129;

        /// <summary>
        /// 本消息由基于计算机的训练程序发送,通过WH_JOURNALPALYBACK的HOOK程序分离出用户输入消息。
        /// </summary>
        public const int WM_QUEUESYNC = 0x23;

        /// <summary>
        /// 关闭消息循环结束程序的运行。
        /// </summary>
        public const int WM_QUIT = 0x12;

        /// <summary>
        /// 双击鼠标右键。
        /// </summary>
        public const int WM_RBUTTONDBLCLK = 0x206;

        /// <summary>
        /// 按下鼠标右键。
        /// </summary>
        public const int WM_RBUTTONDOWN = 0x204;

        /// <summary>
        /// 释放鼠标右键。
        /// </summary>
        public const int WM_RBUTTONUP = 0x205;

        /// <summary>
        /// The W m_ REFLECT
        /// </summary>
        public const int WM_REFLECT = 0x2000;

        /// <summary>
        /// 应用程序退出时在程序退出时,系统会给当前程序发送该消息,要求提供所有格式的剪帖板数据,避免造成数据丢失。
        /// </summary>
        public const int WM_RENDERALLFORMATS = 0x306;

        /// <summary>
        /// 应用程序需要系统剪切板数据时,触发发送本消息。
        /// </summary>
        public const int WM_RENDERFORMAT = 0x305;

        /// <summary>
        /// 将焦点转向一个窗口。
        /// </summary>
        public const int WM_SETFOCUS = 7;

        /// <summary>
        /// 若鼠标光标在某窗口内移动且鼠标没被捕获时,就会发送本消息给某个窗口。
        /// </summary>
        public const int WM_SETCURSOR = 0x20;

        /// <summary>
        /// 指定控件所用字体。
        /// </summary>
        public const int WM_SETFONT = 0x30;

        /// <summary>
        /// 为某窗口关联一个热键。
        /// </summary>
        public const int WM_SETHOTKEY = 50;

        /// <summary>
        /// 应用程序发送本消息让一个新的大图标或小图标与某窗口相关联。
        /// </summary>
        public const int WM_SETICON = 0x80;

        /// <summary>
        /// 设置窗口是否能重绘。
        /// </summary>
        public const int WM_SETREDRAW = 11;

        /// <summary>
        /// 设置一个窗口的文本。
        /// </summary>
        public const int WM_SETTEXT = 12;

        /// <summary>
        /// The W m_ SETTINGCHANGE
        /// </summary>
        public const int WM_SETTINGCHANGE = 0x1a;

        /// <summary>
        /// 发送本消息给一个窗口,以便隐藏或显示该窗口。
        /// </summary>
        public const int WM_SHOWWINDOW = 0x18;

        /// <summary>
        /// 改变一个窗口的大小。
        /// </summary>
        public const int WM_SIZE = 5;

        /// <summary>
        /// 当剪贴板包含CF_OWNERDIPLAY格式的数据,并且剪贴板观察窗口的客户区域的大小已改变时,本消息通过剪贴板观察窗口发送给剪贴板的所有者。
        /// </summary>
        public const int WM_SIZECLIPBOARD = 0x30b;

        /// <summary>
        /// 当用户正在调整窗口大小时,发送本消息给窗口;通过本消息应用程序可监视窗口大小和位置,也可修改它们。
        /// </summary>
        public const int WM_SIZING = 0x214;

        /// <summary>
        /// 每当打印管理列队增加或减少一条作业时就会发出本消息。
        /// </summary>
        public const int WM_SPOOLERSTATUS = 0x2a;

        /// <summary>
        /// 当调用SetWindowLong函数改变一个或多个窗口的风格后,发送本消息给那个窗口。
        /// </summary>
        public const int WM_STYLECHANGED = 0x7d;

        /// <summary>
        /// 当调用SetWindowLong函数将要改变一个或多个窗口的风格时,发送本消息给那个窗口。
        /// </summary>
        public const int WM_STYLECHANGING = 0x7c;

        /// <summary>
        /// 当避免联系独立的GUI线程时,本消息用于同步刷新,本消息由系统确定是否发送。
        /// </summary>
        public const int WM_SYNCPAINT = 0x88;

        /// <summary>
        /// The W m_ SYSCHAR
        /// </summary>
        public const int WM_SYSCHAR = 0x106;

        /// <summary>
        /// 当系统颜色改变时,发送本消息给所有顶级窗口。
        /// </summary>
        public const int WM_SYSCOLORCHANGE = 0x15;

        /// <summary>
        /// 当用户选择一条系统菜单命令、用户最大化或最小化或还原或关闭时，窗口会收到本消息。
        /// </summary>
        public const int WM_SYSCOMMAND = 0x112;

        /// <summary>
        /// 当使用TranslateMessage函数翻译WM_SYSKEYDOWN消息时,发送本消息给拥有键盘焦点的窗口,注:德语键盘上,有些按键只是给字符添加音标的,并不产生字符,故称"死字符"。
        /// </summary>
        public const int WM_SYSDEADCHAR = 0x107;

        /// <summary>
        /// The W m_ SYSKEYDOWN
        /// </summary>
        public const int WM_SYSKEYDOWN = 260;

        /// <summary>
        /// The W m_ SYSKEYUP
        /// </summary>
        public const int WM_SYSKEYUP = 0x105;

        /// <summary>
        /// The W m_ TABLE t_ FIRST
        /// </summary>
        public const int WM_TABLET_FIRST = 0x2c0;

        /// <summary>
        /// The W m_ TABLE t_ LAST
        /// </summary>
        public const int WM_TABLET_LAST = 0x2df;

        /// <summary>
        /// 程序已初始化windows帮助例程时会发送本消息给应用程序。
        /// </summary>
        public const int WM_TCARD = 0x52;

        /// <summary>
        /// The W m_ THEMECHANGED
        /// </summary>
        public const int WM_THEMECHANGED = 0x31a;

        /// <summary>
        /// 当系统的时间变化时发送本消息给所有顶级窗口。
        /// </summary>
        public const int WM_TIMECHANGE = 30;

        /// <summary>
        /// 发生了定时器事件
        /// </summary>
        public const int WM_TIMER = 0x113;

        /// <summary>
        /// 应用程序发送本消息给编辑框或组合框,以撤消最后一次操作。
        /// </summary>
        public const int WM_UNDO = 0x304;

        /// <summary>
        /// The W m_ UNICHAR
        /// </summary>
        public const int WM_UNICHAR = 0x109;

        /// <summary>
        /// 当一条下拉菜单或子菜单被销毁时,发送本消息。
        /// </summary>
        public const int WM_UNINITMENUPOPUP = 0x125;

        /// <summary>
        /// The W m_ UPDATEUISTATE
        /// </summary>
        public const int WM_UPDATEUISTATE = 0x128;

        /// <summary>
        /// 用于帮助应用程序自定义私有消息,通常形式为:WM_USER + X。
        /// </summary>
        public const int WM_USER = 0x400;

        /// <summary>
        /// 当用户已登入或退出后发送本消息给所有窗口;当用户登入或退出时系统更新用户的具体设置信息,在用户更新设置时系统马上发送本消息。
        /// </summary>
        public const int WM_USERCHANGED = 0x54;

        /// <summary>
        /// LBS_WANTKEYBOARDINPUT风格的列表框会发出本消息给其所有者,以便响应WM_KEYDOWN消息。
        /// </summary>
        public const int WM_VKEYTOITEM = 0x2e;

        /// <summary>
        /// 当窗口的标准垂直滚动条产生一个滚动事件时,发送本消息给该窗口。
        /// </summary>
        public const int WM_VSCROLL = 0x115;

        /// <summary>
        /// 当剪贴板查看器的垂直滚动条被单击时,触发发送本消息。
        /// </summary>
        public const int WM_VSCROLLCLIPBOARD = 0x30a;

        /// <summary>
        /// 本消息会发送给那些大小和位置(Z_Order)已被改变的窗口,以调用SetWindowPos函数或其它窗口管理函数。
        /// </summary>
        public const int WM_WINDOWPOSCHANGED = 0x47;

        /// <summary>
        /// 本消息会发送给那些大小和位置(Z_Order)将被改变的窗口,以调用SetWindowPos函数或其它窗口管理函数。
        /// </summary>
        public const int WM_WINDOWPOSCHANGING = 70;

        /// <summary>
        /// 读写\"win.ini\"时会发送本消息给所有顶层窗口,通知其它进程该文件已被更改。
        /// </summary>
        public const int WM_WININICHANGE = 0x1a;

        /// <summary>
        /// The W m_ WTSSESSIO n_ CHANGE
        /// </summary>
        public const int WM_WTSSESSION_CHANGE = 0x2b1;

        /// <summary>
        /// The W m_ XBUTTONDBLCLK
        /// </summary>
        public const int WM_XBUTTONDBLCLK = 0x20d;

        /// <summary>
        /// The W m_ XBUTTONDOWN
        /// </summary>
        public const int WM_XBUTTONDOWN = 0x20b;

        /// <summary>
        /// The W m_ XBUTTONUP
        /// </summary>
        public const int WM_XBUTTONUP = 0x20c;
    }

    /// <summary>
    /// Class VIRTUALKEY
    /// </summary>
    public static class VIRTUALKEY
    {
        /// <summary>
        /// The V k_ LBUTTON
        /// </summary>
        public const int VK_LBUTTON = 0x01;
        /// <summary>
        /// The V k_ RBUTTON
        /// </summary>
        public const int VK_RBUTTON = 0x02;
        /// <summary>
        /// The V k_ CANCEL
        /// </summary>
        public const int VK_CANCEL = 0x03;
        /// <summary>
        /// The V k_ MBUTTON
        /// </summary>
        public const int VK_MBUTTON = 0x04;

        //
        /// <summary>
        /// The V k_ XBUTTO n1
        /// </summary>
        public const int VK_XBUTTON1 = 0x05;

        /// <summary>
        /// The V k_ XBUTTO n2
        /// </summary>
        public const int VK_XBUTTON2 = 0x06;

        //
        /// <summary>
        /// The V k_ BACK
        /// </summary>
        public const int VK_BACK = 0x08;

        /// <summary>
        /// The V k_ TAB
        /// </summary>
        public const int VK_TAB = 0x09;

        //
        /// <summary>
        /// The V k_ CLEAR
        /// </summary>
        public const int VK_CLEAR = 0x0C;

        /// <summary>
        /// The V k_ RETURN
        /// </summary>
        public const int VK_RETURN = 0x0D;

        //
        /// <summary>
        /// The V k_ SHIFT
        /// </summary>
        public const int VK_SHIFT = 0x10;

        /// <summary>
        /// The V k_ CONTROL
        /// </summary>
        public const int VK_CONTROL = 0x11;
        /// <summary>
        /// The V k_ MENU
        /// </summary>
        public const int VK_MENU = 0x12;
        /// <summary>
        /// The V k_ PAUSE
        /// </summary>
        public const int VK_PAUSE = 0x13;
        /// <summary>
        /// The V k_ CAPITAL
        /// </summary>
        public const int VK_CAPITAL = 0x14;

        //
        /// <summary>
        /// The V k_ KANA
        /// </summary>
        public const int VK_KANA = 0x15;

        /// <summary>
        /// The V k_ HANGEUL
        /// </summary>
        public const int VK_HANGEUL = 0x15;  /* old name - should be here for compatibility */
        /// <summary>
        /// The V k_ HANGUL
        /// </summary>
        public const int VK_HANGUL = 0x15;
        /// <summary>
        /// The V k_ JUNJA
        /// </summary>
        public const int VK_JUNJA = 0x17;
        /// <summary>
        /// The V k_ FINAL
        /// </summary>
        public const int VK_FINAL = 0x18;
        /// <summary>
        /// The V k_ HANJA
        /// </summary>
        public const int VK_HANJA = 0x19;
        /// <summary>
        /// The V k_ KANJI
        /// </summary>
        public const int VK_KANJI = 0x19;

        //
        /// <summary>
        /// The V k_ ESCAPE
        /// </summary>
        public const int VK_ESCAPE = 0x1B;

        //
        /// <summary>
        /// The V k_ CONVERT
        /// </summary>
        public const int VK_CONVERT = 0x1C;

        /// <summary>
        /// The V k_ NONCONVERT
        /// </summary>
        public const int VK_NONCONVERT = 0x1D;
        /// <summary>
        /// The V k_ ACCEPT
        /// </summary>
        public const int VK_ACCEPT = 0x1E;
        /// <summary>
        /// The V k_ MODECHANGE
        /// </summary>
        public const int VK_MODECHANGE = 0x1F;

        //
        /// <summary>
        /// The V k_ SPACE
        /// </summary>
        public const int VK_SPACE = 0x20;

        /// <summary>
        /// The V k_ PRIOR
        /// </summary>
        public const int VK_PRIOR = 0x21;
        /// <summary>
        /// The V k_ NEXT
        /// </summary>
        public const int VK_NEXT = 0x22;
        /// <summary>
        /// The V k_ END
        /// </summary>
        public const int VK_END = 0x23;
        /// <summary>
        /// The V k_ HOME
        /// </summary>
        public const int VK_HOME = 0x24;
        /// <summary>
        /// The V k_ LEFT
        /// </summary>
        public const int VK_LEFT = 0x25;
        /// <summary>
        /// The V k_ UP
        /// </summary>
        public const int VK_UP = 0x26;
        /// <summary>
        /// The V k_ RIGHT
        /// </summary>
        public const int VK_RIGHT = 0x27;
        /// <summary>
        /// The V k_ DOWN
        /// </summary>
        public const int VK_DOWN = 0x28;
        /// <summary>
        /// The V k_ SELECT
        /// </summary>
        public const int VK_SELECT = 0x29;
        /// <summary>
        /// The V k_ PRINT
        /// </summary>
        public const int VK_PRINT = 0x2A;
        /// <summary>
        /// The V k_ EXECUTE
        /// </summary>
        public const int VK_EXECUTE = 0x2B;
        /// <summary>
        /// The V k_ SNAPSHOT
        /// </summary>
        public const int VK_SNAPSHOT = 0x2C;
        /// <summary>
        /// The V k_ INSERT
        /// </summary>
        public const int VK_INSERT = 0x2D;
        /// <summary>
        /// The V k_ DELETE
        /// </summary>
        public const int VK_DELETE = 0x2E;
        /// <summary>
        /// The V k_ HELP
        /// </summary>
        public const int VK_HELP = 0x2F;

        //
        /// <summary>
        /// The V k_ LWIN
        /// </summary>
        public const int VK_LWIN = 0x5B;

        /// <summary>
        /// The V k_ RWIN
        /// </summary>
        public const int VK_RWIN = 0x5C;
        /// <summary>
        /// The V k_ APPS
        /// </summary>
        public const int VK_APPS = 0x5D;

        //
        /// <summary>
        /// The V k_ SLEEP
        /// </summary>
        public const int VK_SLEEP = 0x5F;

        //
        /// <summary>
        /// The V k_ NUMPA d0
        /// </summary>
        public const int VK_NUMPAD0 = 0x60;

        /// <summary>
        /// The V k_ NUMPA d1
        /// </summary>
        public const int VK_NUMPAD1 = 0x61;
        /// <summary>
        /// The V k_ NUMPA d2
        /// </summary>
        public const int VK_NUMPAD2 = 0x62;
        /// <summary>
        /// The V k_ NUMPA d3
        /// </summary>
        public const int VK_NUMPAD3 = 0x63;
        /// <summary>
        /// The V k_ NUMPA d4
        /// </summary>
        public const int VK_NUMPAD4 = 0x64;
        /// <summary>
        /// The V k_ NUMPA d5
        /// </summary>
        public const int VK_NUMPAD5 = 0x65;
        /// <summary>
        /// The V k_ NUMPA d6
        /// </summary>
        public const int VK_NUMPAD6 = 0x66;
        /// <summary>
        /// The V k_ NUMPA d7
        /// </summary>
        public const int VK_NUMPAD7 = 0x67;
        /// <summary>
        /// The V k_ NUMPA d8
        /// </summary>
        public const int VK_NUMPAD8 = 0x68;
        /// <summary>
        /// The V k_ NUMPA d9
        /// </summary>
        public const int VK_NUMPAD9 = 0x69;
        /// <summary>
        /// The V k_ MULTIPLY
        /// </summary>
        public const int VK_MULTIPLY = 0x6A;
        /// <summary>
        /// The V k_ ADD
        /// </summary>
        public const int VK_ADD = 0x6B;
        /// <summary>
        /// The V k_ SEPARATOR
        /// </summary>
        public const int VK_SEPARATOR = 0x6C;
        /// <summary>
        /// The V k_ SUBTRACT
        /// </summary>
        public const int VK_SUBTRACT = 0x6D;
        /// <summary>
        /// The V k_ DECIMAL
        /// </summary>
        public const int VK_DECIMAL = 0x6E;
        /// <summary>
        /// The V k_ DIVIDE
        /// </summary>
        public const int VK_DIVIDE = 0x6F;
        /// <summary>
        /// The V k_ f1
        /// </summary>
        public const int VK_F1 = 0x70;
        /// <summary>
        /// The V k_ f2
        /// </summary>
        public const int VK_F2 = 0x71;
        /// <summary>
        /// The V k_ f3
        /// </summary>
        public const int VK_F3 = 0x72;
        /// <summary>
        /// The V k_ f4
        /// </summary>
        public const int VK_F4 = 0x73;
        /// <summary>
        /// The V k_ f5
        /// </summary>
        public const int VK_F5 = 0x74;
        /// <summary>
        /// The V k_ f6
        /// </summary>
        public const int VK_F6 = 0x75;
        /// <summary>
        /// The V k_ f7
        /// </summary>
        public const int VK_F7 = 0x76;
        /// <summary>
        /// The V k_ f8
        /// </summary>
        public const int VK_F8 = 0x77;
        /// <summary>
        /// The V k_ f9
        /// </summary>
        public const int VK_F9 = 0x78;
        /// <summary>
        /// The V k_ F10
        /// </summary>
        public const int VK_F10 = 0x79;
        /// <summary>
        /// The V k_ F11
        /// </summary>
        public const int VK_F11 = 0x7A;
        /// <summary>
        /// The V k_ F12
        /// </summary>
        public const int VK_F12 = 0x7B;
        /// <summary>
        /// The V k_ F13
        /// </summary>
        public const int VK_F13 = 0x7C;
        /// <summary>
        /// The V k_ F14
        /// </summary>
        public const int VK_F14 = 0x7D;
        /// <summary>
        /// The V k_ F15
        /// </summary>
        public const int VK_F15 = 0x7E;
        /// <summary>
        /// The V k_ F16
        /// </summary>
        public const int VK_F16 = 0x7F;
        /// <summary>
        /// The V k_ F17
        /// </summary>
        public const int VK_F17 = 0x80;
        /// <summary>
        /// The V k_ F18
        /// </summary>
        public const int VK_F18 = 0x81;
        /// <summary>
        /// The V k_ F19
        /// </summary>
        public const int VK_F19 = 0x82;
        /// <summary>
        /// The V k_ F20
        /// </summary>
        public const int VK_F20 = 0x83;
        /// <summary>
        /// The V k_ F21
        /// </summary>
        public const int VK_F21 = 0x84;
        /// <summary>
        /// The V k_ F22
        /// </summary>
        public const int VK_F22 = 0x85;
        /// <summary>
        /// The V k_ F23
        /// </summary>
        public const int VK_F23 = 0x86;
        /// <summary>
        /// The V k_ F24
        /// </summary>
        public const int VK_F24 = 0x87;

        //
        /// <summary>
        /// The V k_ NUMLOCK
        /// </summary>
        public const int VK_NUMLOCK = 0x90;

        /// <summary>
        /// The V k_ SCROLL
        /// </summary>
        public const int VK_SCROLL = 0x91;

        //
        /// <summary>
        /// The V k_ OE m_ NE c_ EQUAL
        /// </summary>
        public const int VK_OEM_NEC_EQUAL = 0x92;   // '=' key on numpad

        //
        /// <summary>
        /// The V k_ OE m_ F j_ JISHO
        /// </summary>
        public const int VK_OEM_FJ_JISHO = 0x92;   // 'Dictionary' key

        /// <summary>
        /// The V k_ OE m_ F j_ MASSHOU
        /// </summary>
        public const int VK_OEM_FJ_MASSHOU = 0x93;   // 'Unregister word' key
        /// <summary>
        /// The V k_ OE m_ F j_ TOUROKU
        /// </summary>
        public const int VK_OEM_FJ_TOUROKU = 0x94;   // 'Register word' key
        /// <summary>
        /// The V k_ OE m_ F j_ LOYA
        /// </summary>
        public const int VK_OEM_FJ_LOYA = 0x95;   // 'Left OYAYUBI' key
        /// <summary>
        /// The V k_ OE m_ F j_ ROYA
        /// </summary>
        public const int VK_OEM_FJ_ROYA = 0x96;   // 'Right OYAYUBI' key

        //
        /// <summary>
        /// The V k_ LSHIFT
        /// </summary>
        public const int VK_LSHIFT = 0xA0;

        /// <summary>
        /// The V k_ RSHIFT
        /// </summary>
        public const int VK_RSHIFT = 0xA1;
        /// <summary>
        /// The V k_ LCONTROL
        /// </summary>
        public const int VK_LCONTROL = 0xA2;
        /// <summary>
        /// The V k_ RCONTROL
        /// </summary>
        public const int VK_RCONTROL = 0xA3;
        /// <summary>
        /// The V k_ LMENU
        /// </summary>
        public const int VK_LMENU = 0xA4;
        /// <summary>
        /// The V k_ RMENU
        /// </summary>
        public const int VK_RMENU = 0xA5;

        //
        /// <summary>
        /// The V k_ BROWSE r_ BACK
        /// </summary>
        public const int VK_BROWSER_BACK = 0xA6;

        /// <summary>
        /// The V k_ BROWSE r_ FORWARD
        /// </summary>
        public const int VK_BROWSER_FORWARD = 0xA7;
        /// <summary>
        /// The V k_ BROWSE r_ REFRESH
        /// </summary>
        public const int VK_BROWSER_REFRESH = 0xA8;
        /// <summary>
        /// The V k_ BROWSE r_ STOP
        /// </summary>
        public const int VK_BROWSER_STOP = 0xA9;
        /// <summary>
        /// The V k_ BROWSE r_ SEARCH
        /// </summary>
        public const int VK_BROWSER_SEARCH = 0xAA;
        /// <summary>
        /// The V k_ BROWSE r_ FAVORITES
        /// </summary>
        public const int VK_BROWSER_FAVORITES = 0xAB;
        /// <summary>
        /// The V k_ BROWSE r_ HOME
        /// </summary>
        public const int VK_BROWSER_HOME = 0xAC;

        //
        /// <summary>
        /// The V k_ VOLUM e_ MUTE
        /// </summary>
        public const int VK_VOLUME_MUTE = 0xAD;

        /// <summary>
        /// The V k_ VOLUM e_ DOWN
        /// </summary>
        public const int VK_VOLUME_DOWN = 0xAE;
        /// <summary>
        /// The V k_ VOLUM e_ UP
        /// </summary>
        public const int VK_VOLUME_UP = 0xAF;
        /// <summary>
        /// The V k_ MEDI a_ NEX t_ TRACK
        /// </summary>
        public const int VK_MEDIA_NEXT_TRACK = 0xB0;
        /// <summary>
        /// The V k_ MEDI a_ PRE v_ TRACK
        /// </summary>
        public const int VK_MEDIA_PREV_TRACK = 0xB1;
        /// <summary>
        /// The V k_ MEDI a_ STOP
        /// </summary>
        public const int VK_MEDIA_STOP = 0xB2;
        /// <summary>
        /// The V k_ MEDI a_ PLA y_ PAUSE
        /// </summary>
        public const int VK_MEDIA_PLAY_PAUSE = 0xB3;
        /// <summary>
        /// The V k_ LAUNC h_ MAIL
        /// </summary>
        public const int VK_LAUNCH_MAIL = 0xB4;
        /// <summary>
        /// The V k_ LAUNC h_ MEDI a_ SELECT
        /// </summary>
        public const int VK_LAUNCH_MEDIA_SELECT = 0xB5;
        /// <summary>
        /// The V k_ LAUNC h_ AP p1
        /// </summary>
        public const int VK_LAUNCH_APP1 = 0xB6;
        /// <summary>
        /// The V k_ LAUNC h_ AP p2
        /// </summary>
        public const int VK_LAUNCH_APP2 = 0xB7;

        //
        /// <summary>
        /// The V k_ OE M_1
        /// </summary>
        public const int VK_OEM_1 = 0xBA;   // ';:' for US

        /// <summary>
        /// The V k_ OE m_ PLUS
        /// </summary>
        public const int VK_OEM_PLUS = 0xBB;   // '+' any country
        /// <summary>
        /// The V k_ OE m_ COMMA
        /// </summary>
        public const int VK_OEM_COMMA = 0xBC;   // ',' any country
        /// <summary>
        /// The V k_ OE m_ MINUS
        /// </summary>
        public const int VK_OEM_MINUS = 0xBD;   // '-' any country
        /// <summary>
        /// The V k_ OE m_ PERIOD
        /// </summary>
        public const int VK_OEM_PERIOD = 0xBE;   // '.' any country
        /// <summary>
        /// The V k_ OE M_2
        /// </summary>
        public const int VK_OEM_2 = 0xBF;   // '/?' for US
        /// <summary>
        /// The V k_ OE M_3
        /// </summary>
        public const int VK_OEM_3 = 0xC0;   // '`~' for US

        //
        /// <summary>
        /// The V k_ OE M_4
        /// </summary>
        public const int VK_OEM_4 = 0xDB;  //  '[{' for US

        /// <summary>
        /// The V k_ OE M_5
        /// </summary>
        public const int VK_OEM_5 = 0xDC;  //  '\|' for US
        /// <summary>
        /// The V k_ OE M_6
        /// </summary>
        public const int VK_OEM_6 = 0xDD;  //  ']}' for US
        /// <summary>
        /// The V k_ OE M_7
        /// </summary>
        public const int VK_OEM_7 = 0xDE;  //  ''"' for US
        /// <summary>
        /// The V k_ OE M_8
        /// </summary>
        public const int VK_OEM_8 = 0xDF;

        //
        /// <summary>
        /// The V k_ OE m_ AX
        /// </summary>
        public const int VK_OEM_AX = 0xE1;  //  'AX' key on Japanese AX kbd

        /// <summary>
        /// The V k_ OE M_102
        /// </summary>
        public const int VK_OEM_102 = 0xE2;  //  "<>" or "\|" on RT 102-key kbd.
        /// <summary>
        /// The V k_ IC o_ HELP
        /// </summary>
        public const int VK_ICO_HELP = 0xE3;  //  Help key on ICO
        /// <summary>
        /// The V k_ IC o_00
        /// </summary>
        public const int VK_ICO_00 = 0xE4;  //  00 key on ICO

        //
        /// <summary>
        /// The V k_ PROCESSKEY
        /// </summary>
        public const int VK_PROCESSKEY = 0xE5;

        //
        /// <summary>
        /// The V k_ IC o_ CLEAR
        /// </summary>
        public const int VK_ICO_CLEAR = 0xE6;

        //
        /// <summary>
        /// The V k_ PACKET
        /// </summary>
        public const int VK_PACKET = 0xE7;

        //
        /// <summary>
        /// The V k_ OE m_ RESET
        /// </summary>
        public const int VK_OEM_RESET = 0xE9;

        /// <summary>
        /// The V k_ OE m_ JUMP
        /// </summary>
        public const int VK_OEM_JUMP = 0xEA;
        /// <summary>
        /// The V k_ OE m_ P a1
        /// </summary>
        public const int VK_OEM_PA1 = 0xEB;
        /// <summary>
        /// The V k_ OE m_ P a2
        /// </summary>
        public const int VK_OEM_PA2 = 0xEC;
        /// <summary>
        /// The V k_ OE m_ P a3
        /// </summary>
        public const int VK_OEM_PA3 = 0xED;
        /// <summary>
        /// The V k_ OE m_ WSCTRL
        /// </summary>
        public const int VK_OEM_WSCTRL = 0xEE;
        /// <summary>
        /// The V k_ OE m_ CUSEL
        /// </summary>
        public const int VK_OEM_CUSEL = 0xEF;
        /// <summary>
        /// The V k_ OE m_ ATTN
        /// </summary>
        public const int VK_OEM_ATTN = 0xF0;
        /// <summary>
        /// The V k_ OE m_ FINISH
        /// </summary>
        public const int VK_OEM_FINISH = 0xF1;
        /// <summary>
        /// The V k_ OE m_ COPY
        /// </summary>
        public const int VK_OEM_COPY = 0xF2;
        /// <summary>
        /// The V k_ OE m_ AUTO
        /// </summary>
        public const int VK_OEM_AUTO = 0xF3;
        /// <summary>
        /// The V k_ OE m_ ENLW
        /// </summary>
        public const int VK_OEM_ENLW = 0xF4;
        /// <summary>
        /// The V k_ OE m_ BACKTAB
        /// </summary>
        public const int VK_OEM_BACKTAB = 0xF5;

        //
        /// <summary>
        /// The V k_ ATTN
        /// </summary>
        public const int VK_ATTN = 0xF6;

        /// <summary>
        /// The V k_ CRSEL
        /// </summary>
        public const int VK_CRSEL = 0xF7;
        /// <summary>
        /// The V k_ EXSEL
        /// </summary>
        public const int VK_EXSEL = 0xF8;
        /// <summary>
        /// The V k_ EREOF
        /// </summary>
        public const int VK_EREOF = 0xF9;
        /// <summary>
        /// The V k_ PLAY
        /// </summary>
        public const int VK_PLAY = 0xFA;
        /// <summary>
        /// The V k_ ZOOM
        /// </summary>
        public const int VK_ZOOM = 0xFB;
        /// <summary>
        /// The V k_ NONAME
        /// </summary>
        public const int VK_NONAME = 0xFC;
        /// <summary>
        /// The V k_ P a1
        /// </summary>
        public const int VK_PA1 = 0xFD;
        /// <summary>
        /// The V k_ OE m_ CLEAR
        /// </summary>
        public const int VK_OEM_CLEAR = 0xFE;
    }

    /// <summary>
    /// Class SM
    /// </summary>
    public static class SM
    {
        /// <summary>
        /// The S m_ CXSCREEN
        /// </summary>
        public const int SM_CXSCREEN = 0;  // 0x00
        /// <summary>
        /// The S m_ CYSCREEN
        /// </summary>
        public const int SM_CYSCREEN = 1;  // 0x01
        /// <summary>
        /// The S m_ CXVSCROLL
        /// </summary>
        public const int SM_CXVSCROLL = 2;  // 0x02
        /// <summary>
        /// The S m_ CYHSCROLL
        /// </summary>
        public const int SM_CYHSCROLL = 3;  // 0x03
        /// <summary>
        /// The S m_ CYCAPTION
        /// </summary>
        public const int SM_CYCAPTION = 4;  // 0x04
        /// <summary>
        /// The S m_ CXBORDER
        /// </summary>
        public const int SM_CXBORDER = 5;  // 0x05
        /// <summary>
        /// The S m_ CYBORDER
        /// </summary>
        public const int SM_CYBORDER = 6;  // 0x06
        /// <summary>
        /// The S m_ CXDLGFRAME
        /// </summary>
        public const int SM_CXDLGFRAME = 7;  // 0x07
        /// <summary>
        /// The S m_ CXFIXEDFRAME
        /// </summary>
        public const int SM_CXFIXEDFRAME = 7;  // 0x07
        /// <summary>
        /// The S m_ CYDLGFRAME
        /// </summary>
        public const int SM_CYDLGFRAME = 8;  // 0x08
        /// <summary>
        /// The S m_ CYFIXEDFRAME
        /// </summary>
        public const int SM_CYFIXEDFRAME = 8;  // 0x08
        /// <summary>
        /// The S m_ CYVTHUMB
        /// </summary>
        public const int SM_CYVTHUMB = 9;  // 0x09
        /// <summary>
        /// The S m_ CXHTHUMB
        /// </summary>
        public const int SM_CXHTHUMB = 10; // 0x0A
        /// <summary>
        /// The S m_ CXICON
        /// </summary>
        public const int SM_CXICON = 11; // 0x0B
        /// <summary>
        /// The S m_ CYICON
        /// </summary>
        public const int SM_CYICON = 12; // 0x0C
        /// <summary>
        /// The S m_ CXCURSOR
        /// </summary>
        public const int SM_CXCURSOR = 13; // 0x0D
        /// <summary>
        /// The S m_ CYCURSOR
        /// </summary>
        public const int SM_CYCURSOR = 14; // 0x0E
        /// <summary>
        /// The S m_ CYMENU
        /// </summary>
        public const int SM_CYMENU = 15; // 0x0F
        /// <summary>
        /// The S m_ CXFULLSCREEN
        /// </summary>
        public const int SM_CXFULLSCREEN = 16; // 0x10
        /// <summary>
        /// The S m_ CYFULLSCREEN
        /// </summary>
        public const int SM_CYFULLSCREEN = 17; // 0x11
        /// <summary>
        /// The S m_ CYKANJIWINDOW
        /// </summary>
        public const int SM_CYKANJIWINDOW = 18; // 0x12
        /// <summary>
        /// The S m_ MOUSEPRESENT
        /// </summary>
        public const int SM_MOUSEPRESENT = 19; // 0x13
        /// <summary>
        /// The S m_ CYVSCROLL
        /// </summary>
        public const int SM_CYVSCROLL = 20; // 0x14
        /// <summary>
        /// The S m_ CXHSCROLL
        /// </summary>
        public const int SM_CXHSCROLL = 21; // 0x15
        /// <summary>
        /// The S m_ DEBUG
        /// </summary>
        public const int SM_DEBUG = 22; // 0x16
        /// <summary>
        /// The S m_ SWAPBUTTON
        /// </summary>
        public const int SM_SWAPBUTTON = 23; // 0x17
        /// <summary>
        /// The S m_ CXMIN
        /// </summary>
        public const int SM_CXMIN = 28; // 0x1C
        /// <summary>
        /// The S m_ CYMIN
        /// </summary>
        public const int SM_CYMIN = 29; // 0x1D
        /// <summary>
        /// The S m_ CXSIZE
        /// </summary>
        public const int SM_CXSIZE = 30; // 0x1E
        /// <summary>
        /// The S m_ CYSIZE
        /// </summary>
        public const int SM_CYSIZE = 31; // 0x1F
        /// <summary>
        /// The S m_ CXSIZEFRAME
        /// </summary>
        public const int SM_CXSIZEFRAME = 32; // 0x20
        /// <summary>
        /// The S m_ CXFRAME
        /// </summary>
        public const int SM_CXFRAME = 32; // 0x20
        /// <summary>
        /// The S m_ CYSIZEFRAME
        /// </summary>
        public const int SM_CYSIZEFRAME = 33; // 0x21
        /// <summary>
        /// The S m_ CYFRAME
        /// </summary>
        public const int SM_CYFRAME = 33; // 0x21
        /// <summary>
        /// The S m_ CXMINTRACK
        /// </summary>
        public const int SM_CXMINTRACK = 34; // 0x22
        /// <summary>
        /// The S m_ CYMINTRACK
        /// </summary>
        public const int SM_CYMINTRACK = 35; // 0x23
        /// <summary>
        /// The S m_ CXDOUBLECLK
        /// </summary>
        public const int SM_CXDOUBLECLK = 36; // 0x24
        /// <summary>
        /// The S m_ CYDOUBLECLK
        /// </summary>
        public const int SM_CYDOUBLECLK = 37; // 0x25
        /// <summary>
        /// The S m_ CXICONSPACING
        /// </summary>
        public const int SM_CXICONSPACING = 38; // 0x26
        /// <summary>
        /// The S m_ CYICONSPACING
        /// </summary>
        public const int SM_CYICONSPACING = 39; // 0x27

        /// <summary>
        /// 如果为TRUE或不为0的值下拉菜单是右对齐的否则是左对齐的。
        /// </summary>
        public const int SM_MENUDROPALIGNMENT = 40; // 0x28

        /// <summary>
        /// The S m_ PENWINDOWS
        /// </summary>
        public const int SM_PENWINDOWS = 41; // 0x29
        /// <summary>
        /// The S m_ DBCSENABLED
        /// </summary>
        public const int SM_DBCSENABLED = 42; // 0x2A
        /// <summary>
        /// The S m_ CMOUSEBUTTONS
        /// </summary>
        public const int SM_CMOUSEBUTTONS = 43; // 0x2B
        /// <summary>
        /// The S m_ SECURE
        /// </summary>
        public const int SM_SECURE = 44; // 0x2C
        /// <summary>
        /// The S m_ CXEDGE
        /// </summary>
        public const int SM_CXEDGE = 45; // 0x2D
        /// <summary>
        /// The S m_ CYEDGE
        /// </summary>
        public const int SM_CYEDGE = 46; // 0x2E
        /// <summary>
        /// The S m_ CXMINSPACING
        /// </summary>
        public const int SM_CXMINSPACING = 47; // 0x2F
        /// <summary>
        /// The S m_ CYMINSPACING
        /// </summary>
        public const int SM_CYMINSPACING = 48; // 0x30
        /// <summary>
        /// The S m_ CXSMICON
        /// </summary>
        public const int SM_CXSMICON = 49; // 0x31
        /// <summary>
        /// The S m_ CYSMICON
        /// </summary>
        public const int SM_CYSMICON = 50; // 0x32
        /// <summary>
        /// The S m_ CYSMCAPTION
        /// </summary>
        public const int SM_CYSMCAPTION = 51; // 0x33
        /// <summary>
        /// The S m_ CXSMSIZE
        /// </summary>
        public const int SM_CXSMSIZE = 52; // 0x34
        /// <summary>
        /// The S m_ CYSMSIZE
        /// </summary>
        public const int SM_CYSMSIZE = 53; // 0x35
        /// <summary>
        /// The S m_ CXMENUSIZE
        /// </summary>
        public const int SM_CXMENUSIZE = 54; // 0x36
        /// <summary>
        /// The S m_ CYMENUSIZE
        /// </summary>
        public const int SM_CYMENUSIZE = 55; // 0x37
        /// <summary>
        /// The S m_ ARRANGE
        /// </summary>
        public const int SM_ARRANGE = 56; // 0x38
        /// <summary>
        /// The S m_ CXMINIMIZED
        /// </summary>
        public const int SM_CXMINIMIZED = 57; // 0x39
        /// <summary>
        /// The S m_ CYMINIMIZED
        /// </summary>
        public const int SM_CYMINIMIZED = 58; // 0x3A
        /// <summary>
        /// The S m_ CXMAXTRACK
        /// </summary>
        public const int SM_CXMAXTRACK = 59; // 0x3B
        /// <summary>
        /// The S m_ CYMAXTRACK
        /// </summary>
        public const int SM_CYMAXTRACK = 60; // 0x3C
        /// <summary>
        /// The S m_ CXMAXIMIZED
        /// </summary>
        public const int SM_CXMAXIMIZED = 61; // 0x3D
        /// <summary>
        /// The S m_ CYMAXIMIZED
        /// </summary>
        public const int SM_CYMAXIMIZED = 62; // 0x3E
        /// <summary>
        /// The S m_ NETWORK
        /// </summary>
        public const int SM_NETWORK = 63; // 0x3F
        /// <summary>
        /// The S m_ CLEANBOOT
        /// </summary>
        public const int SM_CLEANBOOT = 67; // 0x43
        /// <summary>
        /// The S m_ CXDRAG
        /// </summary>
        public const int SM_CXDRAG = 68; // 0x44
        /// <summary>
        /// The S m_ CYDRAG
        /// </summary>
        public const int SM_CYDRAG = 69; // 0x45
        /// <summary>
        /// The S m_ SHOWSOUNDS
        /// </summary>
        public const int SM_SHOWSOUNDS = 70; // 0x46
        /// <summary>
        /// The S m_ CXMENUCHECK
        /// </summary>
        public const int SM_CXMENUCHECK = 71; // 0x47
        /// <summary>
        /// The S m_ CYMENUCHECK
        /// </summary>
        public const int SM_CYMENUCHECK = 72; // 0x48
        /// <summary>
        /// The S m_ SLOWMACHINE
        /// </summary>
        public const int SM_SLOWMACHINE = 73; // 0x49
        /// <summary>
        /// The S m_ MIDEASTENABLED
        /// </summary>
        public const int SM_MIDEASTENABLED = 74; // 0x4A
        /// <summary>
        /// The S m_ MOUSEWHEELPRESENT
        /// </summary>
        public const int SM_MOUSEWHEELPRESENT = 75; // 0x4B
        /// <summary>
        /// The S m_ XVIRTUALSCREEN
        /// </summary>
        public const int SM_XVIRTUALSCREEN = 76; // 0x4C
        /// <summary>
        /// The S m_ YVIRTUALSCREEN
        /// </summary>
        public const int SM_YVIRTUALSCREEN = 77; // 0x4D
        /// <summary>
        /// The S m_ CXVIRTUALSCREEN
        /// </summary>
        public const int SM_CXVIRTUALSCREEN = 78; // 0x4E
        /// <summary>
        /// The S m_ CYVIRTUALSCREEN
        /// </summary>
        public const int SM_CYVIRTUALSCREEN = 79; // 0x4F
        /// <summary>
        /// The S m_ CMONITORS
        /// </summary>
        public const int SM_CMONITORS = 80; // 0x50
        /// <summary>
        /// The S m_ SAMEDISPLAYFORMAT
        /// </summary>
        public const int SM_SAMEDISPLAYFORMAT = 81; // 0x51
        /// <summary>
        /// The S m_ IMMENABLED
        /// </summary>
        public const int SM_IMMENABLED = 82; // 0x52
        /// <summary>
        /// The S m_ CXFOCUSBORDER
        /// </summary>
        public const int SM_CXFOCUSBORDER = 83; // 0x53
        /// <summary>
        /// The S m_ CYFOCUSBORDER
        /// </summary>
        public const int SM_CYFOCUSBORDER = 84; // 0x54
        /// <summary>
        /// The S m_ TABLETPC
        /// </summary>
        public const int SM_TABLETPC = 86; // 0x56
        /// <summary>
        /// The S m_ MEDIACENTER
        /// </summary>
        public const int SM_MEDIACENTER = 87; // 0x57
        /// <summary>
        /// The S m_ STARTER
        /// </summary>
        public const int SM_STARTER = 88; // 0x58
        /// <summary>
        /// The S m_ SERVER r2
        /// </summary>
        public const int SM_SERVERR2 = 89; // 0x59
        /// <summary>
        /// The S m_ MOUSEHORIZONTALWHEELPRESENT
        /// </summary>
        public const int SM_MOUSEHORIZONTALWHEELPRESENT = 91; // 0x5B
        /// <summary>
        /// The S m_ CXPADDEDBORDER
        /// </summary>
        public const int SM_CXPADDEDBORDER = 92; // 0x5C
        /// <summary>
        /// The S m_ DIGITIZER
        /// </summary>
        public const int SM_DIGITIZER = 94; // 0x5E
        /// <summary>
        /// The S m_ MAXIMUMTOUCHES
        /// </summary>
        public const int SM_MAXIMUMTOUCHES = 95; // 0x5F

        /// <summary>
        /// The S m_ REMOTESESSION
        /// </summary>
        public const int SM_REMOTESESSION = 0x1000; // 0x1000
        /// <summary>
        /// The S m_ SHUTTINGDOWN
        /// </summary>
        public const int SM_SHUTTINGDOWN = 0x2000; // 0x2000
        /// <summary>
        /// The S m_ REMOTECONTROL
        /// </summary>
        public const int SM_REMOTECONTROL = 0x2001; // 0x2001
    }

    /// <summary>
    /// Class SC
    /// </summary>
    public static class SC
    {
        /// <summary>
        /// The S c_ SIZE
        /// </summary>
        public const int SC_SIZE = 0xF000;
        /// <summary>
        /// The S c_ MOVE
        /// </summary>
        public const int SC_MOVE = 0xF010;
        /// <summary>
        /// The S c_ MINIMIZE
        /// </summary>
        public const int SC_MINIMIZE = 0xF020;
        /// <summary>
        /// The S c_ MAXIMIZE
        /// </summary>
        public const int SC_MAXIMIZE = 0xF030;
        /// <summary>
        /// The S c_ NEXTWINDOW
        /// </summary>
        public const int SC_NEXTWINDOW = 0xF040;
        /// <summary>
        /// The S c_ PREVWINDOW
        /// </summary>
        public const int SC_PREVWINDOW = 0xF050;
        /// <summary>
        /// The S c_ CLOSE
        /// </summary>
        public const int SC_CLOSE = 0xF060;
        /// <summary>
        /// The S c_ VSCROLL
        /// </summary>
        public const int SC_VSCROLL = 0xF070;
        /// <summary>
        /// The S c_ HSCROLL
        /// </summary>
        public const int SC_HSCROLL = 0xF080;
        /// <summary>
        /// The S c_ MOUSEMENU
        /// </summary>
        public const int SC_MOUSEMENU = 0xF090;
        /// <summary>
        /// The S c_ KEYMENU
        /// </summary>
        public const int SC_KEYMENU = 0xF100;
        /// <summary>
        /// The S c_ ARRANGE
        /// </summary>
        public const int SC_ARRANGE = 0xF110;
        /// <summary>
        /// The S c_ RESTORE
        /// </summary>
        public const int SC_RESTORE = 0xF120;
        /// <summary>
        /// The S c_ TASKLIST
        /// </summary>
        public const int SC_TASKLIST = 0xF130;
        /// <summary>
        /// The S c_ SCREENSAVE
        /// </summary>
        public const int SC_SCREENSAVE = 0xF140;
        /// <summary>
        /// The S c_ HOTKEY
        /// </summary>
        public const int SC_HOTKEY = 0xF150;

        //#if(WINVER >= 0x0400) //Win95
        /// <summary>
        /// The S c_ DEFAULT
        /// </summary>
        public const int SC_DEFAULT = 0xF160;

        /// <summary>
        /// The S c_ MONITORPOWER
        /// </summary>
        public const int SC_MONITORPOWER = 0xF170;
        /// <summary>
        /// The S c_ CONTEXTHELP
        /// </summary>
        public const int SC_CONTEXTHELP = 0xF180;
        /// <summary>
        /// The S c_ SEPARATOR
        /// </summary>
        public const int SC_SEPARATOR = 0xF00F;

        //#endif /* WINVER >= 0x0400 */

        //#if(WINVER >= 0x0600) //Vista
        /// <summary>
        /// The SC f_ ISSECURE
        /// </summary>
        public const int SCF_ISSECURE = 0x00000001;

        //#endif /* WINVER >= 0x0600 */

        /*
          * Obsolete names
          */
        /// <summary>
        /// The S c_ ICON
        /// </summary>
        public const int SC_ICON = SC_MINIMIZE;
        /// <summary>
        /// The S c_ ZOOM
        /// </summary>
        public const int SC_ZOOM = SC_MAXIMIZE;
    }

    /// <summary>
    /// Class NOTIFYICONDATA
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class NOTIFYICONDATA
    {
        /// <summary>
        /// The cb size
        /// </summary>
        public int cbSize = Marshal.SizeOf(typeof(NOTIFYICONDATA));
        /// <summary>
        /// The h WND
        /// </summary>
        public IntPtr hWnd;
        /// <summary>
        /// The u ID
        /// </summary>
        public int uID;
        /// <summary>
        /// The u flags
        /// </summary>
        public NotifyIconFlags uFlags;
        /// <summary>
        /// The u callback message
        /// </summary>
        public int uCallbackMessage;
        /// <summary>
        /// The h icon
        /// </summary>
        public IntPtr hIcon;
        /// <summary>
        /// The sz tip
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x80)]
        public string szTip;
        /// <summary>
        /// The dw state
        /// </summary>
        public int dwState;
        /// <summary>
        /// The dw state mask
        /// </summary>
        public int dwStateMask;
        /// <summary>
        /// The sz info
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x100)]
        public string szInfo;
        /// <summary>
        /// The u timeout or version
        /// </summary>
        public int uTimeoutOrVersion;
        /// <summary>
        /// The sz info title
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x40)]
        public string szInfoTitle;
        /// <summary>
        /// The dw info flags
        /// </summary>
        public int dwInfoFlags;
    }

    /// <summary>
    /// Enum NotifyIconFlags
    /// </summary>
    [Flags]
    public enum NotifyIconFlags
    {
        /// <summary>
        /// The hIcon member is valid.
        /// </summary>
        Icon = 2,
        /// <summary>
        /// The uCallbackMessage member is valid.
        /// </summary>
        Message = 1,
        /// <summary>
        /// The szTip member is valid.
        /// </summary>
        ToolTip = 4,
        /// <summary>
        /// The dwState and dwStateMask members are valid.
        /// </summary>
        State = 8,
        /// <summary>
        /// Use a balloon ToolTip instead of a standard ToolTip. The szInfo, uTimeout, szInfoTitle, and dwInfoFlags members are valid.
        /// </summary>
        Balloon = 0x10,
    }

    /// <summary>
    /// Enum NotifyIconMessage
    /// </summary>
    public enum NotifyIconMessage
    {
        /// <summary>
        /// The balloon show
        /// </summary>
        BalloonShow = 0x402,
        /// <summary>
        /// The balloon hide
        /// </summary>
        BalloonHide = 0x403,
        /// <summary>
        /// The balloon timeout
        /// </summary>
        BalloonTimeout = 0x404,
        /// <summary>
        /// The balloon user click
        /// </summary>
        BalloonUserClick = 0x405,
        /// <summary>
        /// The popup open
        /// </summary>
        PopupOpen = 0x406,
        /// <summary>
        /// The popup close
        /// </summary>
        PopupClose = 0x407,
    }
    #endregion enum
}