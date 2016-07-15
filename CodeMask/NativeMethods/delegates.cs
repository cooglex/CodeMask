using System;

namespace NativeMethodsPack
{
    /// <summary>
    /// Delegate WndProc
    /// </summary>
    /// <param name="hWnd">The h WND.</param>
    /// <param name="msg">The MSG.</param>
    /// <param name="wParam">The w param.</param>
    /// <param name="lParam">The l param.</param>
    /// <returns>IntPtr.</returns>
    public delegate IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

    /// <summary>
    /// Delegate EnumWindowsProc
    /// </summary>
    /// <param name="hWnd">The h WND.</param>
    /// <param name="lParam">The l param.</param>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
    public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

    /// <summary>
    /// Delegate EnumMonitorsDelegate
    /// </summary>
    /// <param name="hMonitor">The h monitor.</param>
    /// <param name="hdcMonitor">The HDC monitor.</param>
    /// <param name="lprcMonitor">The LPRC monitor.</param>
    /// <param name="dwData">The dw data.</param>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
    public delegate bool EnumMonitorsDelegate(IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData);

    /// <summary>
    /// Delegate WindowsHookProc
    /// </summary>
    /// <param name="code">The code.</param>
    /// <param name="wParam">The w param.</param>
    /// <param name="lParam">The l param.</param>
    /// <returns>IntPtr.</returns>
    public delegate IntPtr WindowsHookProc(int code, IntPtr wParam, IntPtr lParam);
}