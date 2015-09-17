using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace CodeMask.Windows.Extensions
{
    /// <summary>
    ///     DependencyObject扩展方法类。
    /// </summary>
    public static partial class DependencyObjectExtensions
    {
        /// <summary>
        ///     查找给定元素指定类型的父类。
        /// </summary>
        /// <typeparam name="T">父类型。</typeparam>
        /// <param name="this"></param>
        /// <returns>父类。</returns>
        /// <example>
        ///     <code>
        ///          <![CDATA[
        ///          ]]>
        ///     </code>
        /// </example>
        public static T FindParent<T>(this DependencyObject @this) where T : DependencyObject
        {
            return @this.GetAncestors().OfType<T>().FirstOrDefault();
        }


        /// <summary>
        ///     返回指定条件的父类。
        /// </summary>
        /// <param name="this"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        /// <example>
        ///     <code>
        ///          <![CDATA[
        ///          ]]>
        ///     </code>
        /// </example>
        public static DependencyObject FindParent(this DependencyObject @this, Predicate<DependencyObject> condition)
        {
            while (@this != null)
            {
                if (condition(@this))
                {
                    return @this;
                }
                @this = VisualTreeHelper.GetParent(@this);
            }
            return null;
        }
    }
}