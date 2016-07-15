using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using NativeMethodsPack;

namespace CodeMask.WPF.Controls.CustomWindow
{
    /// <summary>
    /// Class Screen
    /// </summary>
    public static class Screen
    {
        /// <summary>
        /// Finds the maximum single monitor rectangle.
        /// </summary>
        /// <param name="windowRect">The window rect.</param>
        /// <param name="screenSubRect">The screen sub rect.</param>
        /// <param name="monitorRect">The monitor rect.</param>
        public static void FindMaximumSingleMonitorRectangle(RECT windowRect, out RECT screenSubRect, out RECT monitorRect)
        {
            List<RECT> rects = new List<RECT>();
            NativeMethodsPack.NativeMethods.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, delegate(IntPtr hMonitor, IntPtr hdcMonitor, ref RECT rect, IntPtr lpData)
            {
                MONITORINFO monitorInfo = new MONITORINFO
                {
                    cbSize = (uint)Marshal.SizeOf(typeof(MONITORINFO))
                };
                NativeMethodsPack.NativeMethods.GetMonitorInfo(hMonitor, ref monitorInfo);
                rects.Add(monitorInfo.rcWork);
                return true;
            }, IntPtr.Zero);
            screenSubRect = new RECT
            {
                Left = 0,
                Right = 0,
                Top = 0,
                Bottom = 0
            };
            monitorRect = new RECT
            {
                Left = 0,
                Right = 0,
                Top = 0,
                Bottom = 0
            };
            long num = 0L;
            foreach (RECT rect in rects)
            {
                RECT lprcDst;
                RECT lprcSrc1 = rect;
                NativeMethodsPack.NativeMethods.IntersectRect(out lprcDst, ref lprcSrc1, ref windowRect);
                long num2 = lprcDst.Width * lprcDst.Height;
                if (num2 > num)
                {
                    screenSubRect = lprcDst;
                    monitorRect = rect;
                    num = num2;
                }
            }
        }

        /// <summary>
        /// Finds the maximum single monitor rectangle.
        /// </summary>
        /// <param name="windowRect">The window rect.</param>
        /// <param name="screenSubRect">The screen sub rect.</param>
        /// <param name="monitorRect">The monitor rect.</param>
        public static void FindMaximumSingleMonitorRectangle(Rect windowRect, out Rect screenSubRect, out Rect monitorRect)
        {
            RECT _screenSubRect;
            RECT _monitorRect;
            Screen.FindMaximumSingleMonitorRectangle(new RECT(windowRect), out _screenSubRect, out _monitorRect);
            screenSubRect = new Rect(_screenSubRect.Position, _screenSubRect.Size);
            monitorRect = new Rect(_monitorRect.Position, _monitorRect.Size);
        }

        /// <summary>
        /// Finds the monitor rects from point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="monitorRect">The monitor rect.</param>
        /// <param name="workAreaRect">The work area rect.</param>
        public static void FindMonitorRectsFromPoint(Point point, out Rect monitorRect, out Rect workAreaRect)
        {
            POINT pt = new POINT
            {
                x = (int)point.X,
                y = (int)point.Y
            };
            IntPtr hMonitor = NativeMethodsPack.NativeMethods.MonitorFromPoint(pt, 2);
            monitorRect = new Rect(0.0, 0.0, 0.0, 0.0);
            workAreaRect = new Rect(0.0, 0.0, 0.0, 0.0);
            if (hMonitor != IntPtr.Zero)
            {
                MONITORINFO monitorInfo = new MONITORINFO
                {
                    cbSize = (uint)Marshal.SizeOf(typeof(MONITORINFO))
                };
                NativeMethodsPack.NativeMethods.GetMonitorInfo(hMonitor, ref monitorInfo);
                monitorRect = new Rect(monitorInfo.rcMonitor.Position, monitorInfo.rcMonitor.Size);
                workAreaRect = new Rect(monitorInfo.rcWork.Position, monitorInfo.rcWork.Size);
            }
        }

        /// <summary>
        /// Screens to work area.
        /// </summary>
        /// <param name="pt">The pt.</param>
        /// <returns>Point.</returns>
        public static Point ScreenToWorkArea(Point pt)
        {
            Rect workAreaRect;
            Rect monitorRect;
            Screen.FindMonitorRectsFromPoint(pt, out monitorRect, out workAreaRect);
            return new Point(pt.X - workAreaRect.Left + monitorRect.Left, pt.Y - workAreaRect.Top + monitorRect.Top);
        }

        /// <summary>
        /// Works the area to screen.
        /// </summary>
        /// <param name="pt">The pt.</param>
        /// <returns>Point.</returns>
        public static Point WorkAreaToScreen(Point pt)
        {
            Rect workAreaRect;
            Rect monitorRect;
            Screen.FindMonitorRectsFromPoint(pt, out monitorRect, out workAreaRect);
            return new Point(pt.X - monitorRect.Left + workAreaRect.Left, pt.Y - monitorRect.Top + workAreaRect.Top);
        }
    }
}