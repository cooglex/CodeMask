using System;
using System.Windows;
using System.Windows.Interop;

namespace CodeMask.WPF.Extensions
{
    public static partial class WindowExtensions
    {
        /// <summary>
        ///     获取WPF窗体句柄。
        /// </summary>
        /// <param name="this">窗体实例。</param>
        /// <returns>WPF窗体句柄。</returns>
        /// <example>
        ///     <code>
        ///          <![CDATA[
        ///          ]]>
        ///     </code>
        /// </example>
        public static IntPtr GetHandle(this Window @this)
        {
            return new WindowInteropHelper(@this).Handle;
        }
    }
}