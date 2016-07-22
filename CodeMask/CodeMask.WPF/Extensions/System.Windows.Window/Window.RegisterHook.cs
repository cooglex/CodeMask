using System.Windows;
using System.Windows.Interop;

namespace CodeMask.WPF.Extensions
{
    /// <summary>
    ///     <see cref="System.Windows.Window" />扩展。
    /// </summary>
    public static partial class WindowExtensions
    {
        /// <summary>
        ///     给指定WPF窗体添加消息钩子。
        /// </summary>
        /// <param name="this">WPF窗体实例。</param>
        /// <param name="hook"><see cref="System.Windows.Interop.HwndSourceHook" /> 实例。</param>
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