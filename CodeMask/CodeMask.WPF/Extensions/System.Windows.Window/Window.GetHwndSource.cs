using System.Windows;
using System.Windows.Interop;

namespace CodeMask.WPF.Extensions
{
    public static partial class WindowExtensions
    {
        /// <summary>
        ///     获取WPF窗体的HwndSource。
        /// </summary>
        /// <param name="this">窗体实例。</param>
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