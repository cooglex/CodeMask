using System;
using System.Runtime.InteropServices;
using NativeMethodsPack;

namespace CodeMask.WPF.Controls.CustomWindow
{
    /// <summary>
    /// Class HwndWrapper
    /// </summary>
    public abstract class HwndWrapper : DisposableObject
    {
        /// <summary>
        /// The handle
        /// </summary>
        private IntPtr handle;

        /// <summary>
        /// The is creating handle
        /// </summary>
        private bool isCreatingHandle;

        /// <summary>
        /// The WND class atom
        /// </summary>
        private ushort wndClassAtom;

        /// <summary>
        /// The WND proc
        /// </summary>
        private Delegate wndProc;

        /// <summary>
        /// Gets the handle.
        /// </summary>
        /// <value>The handle.</value>
        public IntPtr Handle
        {
            get
            {
                this.EnsureHandle();
                return this.handle;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is window subclassed.
        /// </summary>
        /// <value><c>true</c> if this instance is window subclassed; otherwise, <c>false</c>.</value>
        protected virtual bool IsWindowSubclassed
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the window class atom.
        /// </summary>
        /// <value>The window class atom.</value>
        [CLSCompliant(false)]
        protected ushort WindowClassAtom
        {
            get
            {
                if (this.wndClassAtom == 0)
                {
                    this.wndClassAtom = this.CreateWindowClassCore();
                }
                return this.wndClassAtom;
            }
        }

        /// <summary>
        /// Creates the window class core.
        /// </summary>
        /// <returns>System.UInt16.</returns>
        [CLSCompliant(false)]
        protected virtual ushort CreateWindowClassCore()
        {
            return this.RegisterClass(Guid.NewGuid().ToString());
        }

        /// <summary>
        /// Creates the window core.
        /// </summary>
        /// <returns>IntPtr.</returns>
        protected abstract IntPtr CreateWindowCore();

        /// <summary>
        /// Destroys the window class core.
        /// </summary>
        protected virtual void DestroyWindowClassCore()
        {
            if (this.wndClassAtom != 0)
            {
                IntPtr moduleHandle = NativeMethodsPack.NativeMethods.GetModuleHandle(null);
                NativeMethodsPack.NativeMethods.UnregisterClass(new IntPtr(this.wndClassAtom), moduleHandle);
                this.wndClassAtom = 0;
            }
        }

        /// <summary>
        /// Destroys the window core.
        /// </summary>
        protected virtual void DestroyWindowCore()
        {
            if (this.handle != IntPtr.Zero)
            {
                NativeMethodsPack.NativeMethods.DestroyWindow(this.handle);
                this.handle = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Disposes the native resources.
        /// </summary>
        protected override void DisposeNativeResources()
        {
            this.DestroyWindowCore();
            this.DestroyWindowClassCore();
        }

        /// <summary>
        /// Ensures the handle.
        /// </summary>
        public void EnsureHandle()
        {
            if ((this.handle == IntPtr.Zero) && !this.isCreatingHandle)
            {
                this.isCreatingHandle = true;
                try
                {
                    this.handle = this.CreateWindowCore();
                    if (this.IsWindowSubclassed)
                    {
                        this.SubclassWndProc();
                    }
                }
                finally
                {
                    this.isCreatingHandle = false;
                }
            }
        }

        /// <summary>
        /// Registers the class.
        /// </summary>
        /// <param name="className">Name of the class.</param>
        /// <returns>System.UInt16.</returns>
        [CLSCompliant(false)]
        protected ushort RegisterClass(string className)
        {
            WNDCLASS lpWndClass = new WNDCLASS
            {
                cbClsExtra = 0,
                cbWndExtra = 0,
                hbrBackground = IntPtr.Zero,
                hCursor = IntPtr.Zero,
                hIcon = IntPtr.Zero,
                lpfnWndProc = this.wndProc = new WndProc(this.WndProc),
                lpszClassName = className,
                lpszMenuName = null,
                style = 0
            };
            return NativeMethodsPack.NativeMethods.RegisterClass(ref lpWndClass);
        }

        /// <summary>
        /// Subclasses the WND proc.
        /// </summary>
        private void SubclassWndProc()
        {
            this.wndProc = new WndProc(this.WndProc);
            NativeMethodsPack.NativeMethods.SetWindowLong(this.handle, GWL_INDEX.GWL_WNDPROC, Marshal.GetFunctionPointerForDelegate(this.wndProc));
        }

        /// <summary>
        /// WNDs the proc.
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        /// <param name="msg">The MSG.</param>
        /// <param name="wParam">The w param.</param>
        /// <param name="lParam">The l param.</param>
        /// <returns>IntPtr.</returns>
        protected virtual IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam)
        {
            return NativeMethodsPack.NativeMethods.DefWindowProc(hwnd, msg, wParam, lParam);
        }
    }
}