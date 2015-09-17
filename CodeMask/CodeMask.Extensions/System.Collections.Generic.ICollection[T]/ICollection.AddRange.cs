using System.Collections.Generic;

namespace CodeMask.Extensions
{
    /// <summary>
    ///     <see cref="System.Collections.Generic.ICollection&lt;T&gt;" />扩展类。
    /// </summary>
    public static class ICollectionExtension
    {
        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="input"></param>
        /// <example>
        ///     <code>
        ///            <![CDATA[
        ///            ]]>
        ///       </code>
        /// </example>
        public static void AddRange<T>(this ICollection<T> @this, IEnumerable<T> input)
        {
            foreach (var item in input)
                @this.Add(item);
        }
    }
}