using System.Windows.Media;

namespace CodeMask.WPF.Extensions
{
    /// <summary>
    ///     <see cref="System.Windows.Media.Color" />扩展。
    /// </summary>
    public static partial class ColorExtensions
    {
        /// <summary>
        ///     反转颜色。
        /// </summary>
        /// <param name="color"><see cref="System.Windows.Media.Color" />实例。</param>
        /// <returns>反转后的颜色。</returns>
        /// <example>
        ///     <code>
        ///          <![CDATA[
        ///          ]]>
        ///     </code>
        /// </example>
        public static Color Invert(this Color color)
        {
            return Color.FromArgb(color.A, (byte) ~color.R, (byte) ~color.G, (byte) ~color.B);
        }
    }
}