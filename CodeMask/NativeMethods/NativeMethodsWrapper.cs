using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace NativeMethodsPack
{
    /// <summary>
    /// Class NativeMethods
    /// </summary>
    public partial class NativeMethods
    {
        /// <summary>
        /// The VSM notify owner activate
        /// </summary>
        private static int vsmNotifyOwnerActivate;
        /// <summary>
        /// The HRG n_ NONE
        /// </summary>
        public readonly static IntPtr HRGN_NONE = new IntPtr(-1);

        /// <summary>
        /// Gets the name of the window class.
        /// </summary>
        /// <param name="windowHandle">The window handle.</param>
        /// <returns>System.String.</returns>
        public static string GetWindowClassName(IntPtr windowHandle)
        {
            StringBuilder lpClassName = new StringBuilder(0x100, 0x100);
            if (NativeMethodsPack.NativeMethods.GetClassName(windowHandle, lpClassName, 0x100) == 0)
            {
                return null;
            }
            return lpClassName.ToString();
        }

        /// <summary>
        /// Gets the window long PTR.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="nIndex">Index of the n.</param>
        /// <returns>IntPtr.</returns>
        public static IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex)
        {
            if (IntPtr.Size == 8)
            {
                return IntPtr.Zero;
            }
            return (IntPtr)new IntPtr(NativeMethodsPack.NativeMethods.GetWindowLongPtr32(hWnd, nIndex));
        }

        /// <summary>
        /// Removes the window system menu.
        /// </summary>
        /// <param name="windowHandle">The window handle.</param>
        public static void RemoveWindowSystemMenu(IntPtr windowHandle)
        {
            SetWindowLong(windowHandle, GWL_INDEX.GWL_STYLE, new IntPtr((int)(GetWindowLongPtr(windowHandle, -16).ToInt32() & -524289)));
        }

        /// <summary>
        /// Sets the window long.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="nIndex">Index of the n.</param>
        /// <param name="dwNewLong">The dw new long.</param>
        /// <returns>IntPtr.</returns>
        public static IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size == 8)
                return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
            else
                return new IntPtr(SetWindowLong32(hWnd, nIndex, dwNewLong.ToInt32()));
        }

        /// <summary>
        /// Windows the has system menu.
        /// </summary>
        /// <param name="windowHandle">The window handle.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public static bool WindowHasSystemMenu(IntPtr windowHandle)
        {
            return (bool)((GetWindowLongPtr(windowHandle, GWL_INDEX.GWL_STYLE).ToInt32() & 0x80000) != 0);
        }

        /// <summary>
        /// Gets the window placement.
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        /// <returns>WINDOWPLACEMENT.</returns>
        /// <exception cref="System.ComponentModel.Win32Exception"></exception>
        public static WINDOWPLACEMENT GetWindowPlacement(IntPtr hwnd)
        {
            WINDOWPLACEMENT wINDOWPLACEMENT = new WINDOWPLACEMENT();
            if (!NativeMethods.GetWindowPlacement(hwnd, out wINDOWPLACEMENT))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            else
            {
                return wINDOWPLACEMENT;
            }
        }

        /// <summary>
        /// Gets the cursor pos.
        /// </summary>
        /// <returns>Point.</returns>
        public static Point GetCursorPos()
        {
            POINT pOINT = new POINT();
            pOINT.x = 0;
            pOINT.y = 0;
            POINT pOINT1 = pOINT;
            Point point = new Point();
            if (NativeMethods.GetCursorPos(ref pOINT1))
            {
                point.X = (double)pOINT1.x;
                point.Y = (double)pOINT1.y;
            }
            return point;
        }

        /// <summary>
        /// Gets the window pos flags.
        /// </summary>
        /// <param name="lParam">The l param.</param>
        /// <returns>System.UInt32.</returns>
        public static uint GetWindowPosFlags(IntPtr lParam)
        {
            WINDOWPOS windowPos = NativeMethods.LParamToWindowPos(lParam);
            return windowPos.flags;
        }

        /// <summary>
        /// Creates the compatible DC.
        /// </summary>
        /// <param name="hdc">The HDC.</param>
        /// <returns>IntPtr.</returns>
        public static IntPtr CreateCompatibleDC(HandleRef hdc)
        {
            return NativeMethods.CreateCompatibleDC(hdc.Handle);
        }

        /// <summary>
        /// Deletes the DC.
        /// </summary>
        /// <param name="hdc">The HDC.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public static bool DeleteDC(HandleRef hdc)
        {
            return NativeMethods.DeleteDC(hdc.Handle);
        }

        /// <summary>
        /// Deletes the object.
        /// </summary>
        /// <param name="hObject">The h object.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public static bool DeleteObject(HandleRef hObject)
        {
            return NativeMethods.DeleteObject(hObject.Handle);
        }

        /// <summary>
        /// Gets the XL param.
        /// </summary>
        /// <param name="lParam">The l param.</param>
        /// <returns>System.Int32.</returns>
        public static int GetXLParam(int lParam)
        {
            return NativeMethods.LoWord(lParam);
        }

        /// <summary>
        /// Loes the word.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Int32.</returns>
        public static int LoWord(int value)
        {
            return (short)(value & 65535);
        }

        /// <summary>
        /// Gets the YL param.
        /// </summary>
        /// <param name="lParam">The l param.</param>
        /// <returns>System.Int32.</returns>
        public static int GetYLParam(int lParam)
        {
            return NativeMethods.HiWord(lParam);
        }

        #region Key Relative
        /// <summary>
        /// Determines whether [is key pressed] [the specified v key].
        /// </summary>
        /// <param name="vKey">The v key.</param>
        /// <returns><c>true</c> if [is key pressed] [the specified v key]; otherwise, <c>false</c>.</returns>
        public static bool IsKeyPressed(int vKey)
        {
            return NativeMethods.GetKeyState(vKey) < 0;
        }

        /// <summary>
        /// Determines whether [is left button pressed].
        /// </summary>
        /// <returns><c>true</c> if [is left button pressed]; otherwise, <c>false</c>.</returns>
        public static bool IsLeftButtonPressed()
        {
            return NativeMethods.IsKeyPressed(VIRTUALKEY.VK_LBUTTON);
        }

        /// <summary>
        /// Determines whether [is right button pressed].
        /// </summary>
        /// <returns><c>true</c> if [is right button pressed]; otherwise, <c>false</c>.</returns>
        public static bool IsRightButtonPressed()
        {
            return NativeMethods.IsKeyPressed(VIRTUALKEY.VK_RBUTTON);
        }

        /// <summary>
        /// Determines whether [is shift pressed].
        /// </summary>
        /// <returns><c>true</c> if [is shift pressed]; otherwise, <c>false</c>.</returns>
        public static bool IsShiftPressed()
        {
            return NativeMethods.IsKeyPressed(VIRTUALKEY.VK_SHIFT);
        }

        /// <summary>
        /// Determines whether [is alt pressed].
        /// </summary>
        /// <returns><c>true</c> if [is alt pressed]; otherwise, <c>false</c>.</returns>
        public static bool IsAltPressed()
        {
            return NativeMethods.IsKeyPressed(VIRTUALKEY.VK_MENU);
        }

        /// <summary>
        /// Determines whether [is control pressed].
        /// </summary>
        /// <returns><c>true</c> if [is control pressed]; otherwise, <c>false</c>.</returns>
        public static bool IsControlPressed()
        {
            return NativeMethods.IsKeyPressed(VIRTUALKEY.VK_CONTROL);
        }
        #endregion

        /// <summary>
        /// His the word.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Int32.</returns>
        public static int HiWord(int value)
        {
            return (short)(value >> 16);
        }

        /// <summary>
        /// Makes the param.
        /// </summary>
        /// <param name="lowWord">The low word.</param>
        /// <param name="highWord">The high word.</param>
        /// <returns>IntPtr.</returns>
        public static IntPtr MakeParam(int lowWord, int highWord)
        {
            return new IntPtr(lowWord & 65535 | highWord << 16);
        }

        /// <summary>
        /// Gets the NOTIFYOWNERACTIVATE.
        /// </summary>
        /// <value>The NOTIFYOWNERACTIVATE.</value>
        public static int NOTIFYOWNERACTIVATE
        {
            get
            {
                if (NativeMethods.vsmNotifyOwnerActivate == 0)
                {
                    NativeMethods.vsmNotifyOwnerActivate = NativeMethods.RegisterWindowMessage("NOTIFYOWNERACTIVATE{A982313C-756C-4da9-8BD0-0C375A45784B}");
                }
                return NativeMethods.vsmNotifyOwnerActivate;
            }
        }

        /// <summary>
        /// Gets the modifier keys.
        /// </summary>
        /// <value>The modifier keys.</value>
        public static ModifierKeys ModifierKeys
        {
            get
            {
                byte[] numArray = new byte[256];
                NativeMethods.GetKeyboardState(numArray);
                ModifierKeys modifierKey = ModifierKeys.None;
                if ((numArray[16] & 128) == 128)
                {
                    modifierKey = modifierKey | ModifierKeys.Shift;
                }
                if ((numArray[17] & 128) == 128)
                {
                    modifierKey = modifierKey | ModifierKeys.Control;
                }
                if ((numArray[18] & 128) == 128)
                {
                    modifierKey = modifierKey | ModifierKeys.Alt;
                }
                if ((numArray[91] & 128) == 128 || (numArray[92] & 128) == 128)
                {
                    modifierKey = modifierKey | ModifierKeys.Windows;
                }
                return modifierKey;
            }
        }

        /// <summary>
        /// Ls the param to window pos.
        /// </summary>
        /// <param name="lParam">The l param.</param>
        /// <returns>WINDOWPOS.</returns>
        private static WINDOWPOS LParamToWindowPos(IntPtr lParam)
        {
            WINDOWPOS wINDOWPO = new WINDOWPOS();
            Marshal.PtrToStructure(lParam, wINDOWPO);
            return wINDOWPO;
        }

        /// <summary>
        /// Modifies the style.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="styleToRemove">The style to remove.</param>
        /// <param name="styleToAdd">The style to add.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public static bool ModifyStyle(IntPtr hWnd, int styleToRemove, int styleToAdd)
        {
            int windowLong = NativeMethods.GetWindowLong(hWnd, GWL_INDEX.GWL_STYLE);
            int num = windowLong & ~styleToRemove | styleToAdd;
            if (num != windowLong)
            {
                NativeMethods.SetWindowLong(hWnd, GWL_INDEX.GWL_STYLE, new IntPtr(num));
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}