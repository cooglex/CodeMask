using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CodeMask.Windows.Extensions_
{
    /// <summary>
    ///     <see cref="System.Collections.Generic.IEnumerable&lt;T&gt;" />扩展类。
    /// </summary>
    public static partial class IEnumerableExtension
    {
        /// <summary>
        ///     将<see cref="System.Collections.Generic.IEnumerable&lt;T&gt;" />集合转换成
        ///     <see cref="System.Collections.ObjectModel.ObservableCollection&lt;T&gt;" />。
        /// </summary>
        /// <typeparam name="T">泛型类型参数。</typeparam>
        /// <param name="this"><see cref="System.Collections.Generic.IEnumerable&lt;T&gt;" />集合对象。</param>
        /// <example>
        ///     <code>
        ///          <![CDATA[
        ///          ]]>
        ///     </code>
        /// </example>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> @this)
        {
            var dest = new ObservableCollection<T>();

            foreach (var item in @this)
                dest.Add(item);

            return dest;
        }
    }
}