using System.Windows;
using System.Windows.Interop;

namespace CodeMask.WPF.Extensions
{
    public static partial class WindowExtensions
    {
        /// <summary>
        ///     给指定WPF窗体添加消息钩子。
        /// </summary>
        /// <param name="this">窗体实例。</param>
        /// <param name="hook"><code>HwndSourceHook</code> 实例。</param>
        /// <example>
        ///     <code>
        ///          <![CDATA[
        ///          ]]>
        ///     </code>
        /// </example>
        public static void RegisterHook(this Window @this, HwndSourceHook hook)
        {
            @this.GetHwndSource().AddHook(hook);
        }
    }
}