using System;
using System.Collections.Generic;

namespace CodeMask.Extensions
{
    /// <summary>
    ///     <see cref="System.Collections.Generic.IEnumerable&lt;T&gt;" />扩展类。
    /// </summary>
    public static partial class IEnumerableExtension
    {
        /// <summary>
        ///     对<see cref="System.Collections.Generic.IEnumerable&lt;T&gt;" />的每个元素执行指定操作。
        /// </summary>
        /// <typeparam name="T">泛型类型参数。</typeparam>
        /// <param name="this"><see cref="System.Collections.Generic.IEnumerable&lt;T&gt;" />集合对象。</param>
        /// <param name="action">
        ///     要对<see cref="System.Collections.Generic.IEnumerable&lt;T&gt;" />的每个元素执行的
        ///     <see cref="System.Action&lt;T&gt;" />委托。
        /// </param>
        /// <example>
        ///     <code>
        ///          <![CDATA[
        ///          ]]>
        ///     </code>
        /// </example>
        public static void ForEach<T>(this IEnumerable<T> @this, Action<T> action)
        {
            foreach (var t in @this)
            {
                action(t);
            }
        }
    }
}