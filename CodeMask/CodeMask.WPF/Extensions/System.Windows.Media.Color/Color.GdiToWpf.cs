using System.Windows.Media;

namespace CodeMask.WPF.Extensions
{
    /// <summary>
    ///     <see cref="System.Windows.Media.Color" />扩展。
    /// </summary>
    public static partial class ColorExtensions
    {
        /// <summary>
        ///     转换<see cref="System.Drawing.Color" />为<see cref="System.Windows.Media.Color" />。
        /// </summary>
        /// <param name="color"><see cref="System.Drawing.Color" />实例。</param>
        /// <returns><see cref="System.Windows.Media.Color" />实例。</returns>
        /// <example>
        ///     <code>
        ///          <![CDATA[
        ///          ]]>
        ///     </code>
        /// </example>
        public static Color GdiToWpf(this System.Drawing.Color color)
        {
            return Color.FromArgb(color.A, color.R, color.G, color.B);
        }
    }
}