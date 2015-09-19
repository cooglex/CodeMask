using System.Windows;

namespace CodeMask.WPF.Extensions
{
    /// <summary>
    ///     <see cref="System.Windows.Vector" />扩展。
    /// </summary>
    public static partial class VectorExtensions
    {
        /// <summary>
        ///     将平面向量<see cref="System.Windows.Vector" />转为平面点<see cref="System.Windows.Point" />。
        /// </summary>
        /// <param name="this">平面向量<see cref="System.Windows.Vector" />。</param>
        /// <returns>平面点<see cref="System.Windows.Point" />。</returns>
        /// <example>
        ///     <code>
        ///          <![CDATA[
        ///          ]]>
        ///     </code>
        /// </example>
        public static Point ToPoint(this Vector @this)
        {
            return new Point(@this.X, @this.Y);
        }
    }
}