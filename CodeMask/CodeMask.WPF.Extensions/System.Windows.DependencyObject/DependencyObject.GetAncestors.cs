using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace CodeMask.WPF.Extensions
{
    /// <summary>
    ///     DependencyObject扩展方法类。
    /// </summary>
    public static partial class DependencyObjectExtensions
    {
        /// <summary>
        ///     查找可视对象的所有迭代父类。
        /// </summary>
        /// <param name="this">要查找父对象的元素。</param>
        /// <returns>可视父类集合。</returns>
        /// <remarks>返回的集合包括传入的参数element。</remarks>
        /// <example>
        ///     <code>
        ///          <![CDATA[
        ///          ]]>
        ///     </code>
        /// </example>
        public static IEnumerable<DependencyObject> GetAncestors(this DependencyObject @this)
        {
            do
            {
                yield return @this;
                @this = VisualTreeHelper.GetParent(@this);
            } while (@this != null);
        }
    }
}