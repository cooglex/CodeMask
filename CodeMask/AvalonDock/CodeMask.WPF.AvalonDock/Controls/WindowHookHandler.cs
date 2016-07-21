/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System;

namespace CodeMask.WPF.AvalonDock.Controls
{
    internal class FocusChangeEventArgs : EventArgs
    {
        public FocusChangeEventArgs(IntPtr gotFocusWinHandle, IntPtr lostFocusWinHandle)
        {
            GotFocusWinHandle = gotFocusWinHandle;
            LostFocusWinHandle = lostFocusWinHandle;
        }

        public IntPtr GotFocusWinHandle { get; private set; }

        public IntPtr LostFocusWinHandle { get; private set; }
    }

    internal class WindowHookHandler
    {
        //public event EventHandler<WindowActivateEventArgs> Activate;

        private readonly ReentrantFlag _insideActivateEvent = new ReentrantFlag();
        private Win32Helper.HookProc _hookProc;

        private IntPtr _windowHook;

        public void Attach()
        {
            _hookProc = HookProc;
            _windowHook = Win32Helper.SetWindowsHookEx(
                Win32Helper.HookType.WH_CBT,
                _hookProc,
                IntPtr.Zero,
                (int) Win32Helper.GetCurrentThreadId());
        }


        public void Detach()
        {
            Win32Helper.UnhookWindowsHookEx(_windowHook);
        }

        public int HookProc(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code == Win32Helper.HCBT_SETFOCUS)
            {
                if (FocusChanged != null)
                    FocusChanged(this, new FocusChangeEventArgs(wParam, lParam));
            }
            else if (code == Win32Helper.HCBT_ACTIVATE)
            {
                if (_insideActivateEvent.CanEnter)
                {
                    using (_insideActivateEvent.Enter())
                    {
                        //if (Activate != null)
                        //    Activate(this, new WindowActivateEventArgs(wParam));
                    }
                }
            }


            return Win32Helper.CallNextHookEx(_windowHook, code, wParam, lParam);
        }

        public event EventHandler<FocusChangeEventArgs> FocusChanged;
    }
}