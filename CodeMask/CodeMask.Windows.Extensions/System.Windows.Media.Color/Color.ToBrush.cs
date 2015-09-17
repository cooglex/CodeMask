using System.Windows.Media;

namespace CodeMask.Windows.Extensions
{
    /// <summary>
    ///     Color扩展方法类
    /// </summary>
    public static class ColorExtensions
    {
        /// <summary>
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        /// <example>
        ///     <code>
        ///          <![CDATA[
        ///          ]]>
        ///     </code>
        /// </example>
        public static SolidColorBrush ToBrush(this Color @this)
        {
            return new SolidColorBrush(@this);
        }
    }
}