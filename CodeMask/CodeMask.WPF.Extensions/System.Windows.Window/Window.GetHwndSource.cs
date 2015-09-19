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
        ///     获取WPF窗体的HwndSource。
        /// </summary>
        /// <param name="this">WPF窗体实例。</param>
        /// <returns>HwndSource。</returns>
        /// <example>
        ///     <code>
        ///          <![CDATA[
        ///          ]]>
        ///     </code>
        /// </example>
        public static HwndSource GetHwndSource(this Window @this)
        {
            return HwndSource.FromHwnd(@this.GetHandle());
        }
    }
}