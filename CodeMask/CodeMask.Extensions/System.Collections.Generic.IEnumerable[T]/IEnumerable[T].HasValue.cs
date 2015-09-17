using System.Collections.Generic;
using System.Linq;

namespace CodeMask.Extensions
{
    /// <summary>
    ///     <see cref="System.Collections.Generic.IEnumerable&lt;T&gt;" />扩展类。
    /// </summary>
    public static partial class IEnumerableExtension
    {
        /// <summary>
        ///     检查<see cref="System.Collections.Generic.IEnumerable&lt;T&gt;" />类型的集合是否为空或长度是为0;
        ///     若<see cref="System.Collections.Generic.IEnumerable&lt;T&gt;" />为空或者长度为0，返回false;
        ///     若<see cref="System.Collections.Generic.IEnumerable&lt;T&gt;" />不为空且长度大于0，返回true。
        /// </summary>
        /// <typeparam name="T">类型参数。</typeparam>
        /// <param name="this">当前对象。</param>
        /// <returns></returns>
        /// <example>
        ///     <code>
        ///            <![CDATA[
        /// 				List<string> names = new List<string>();
        ///  				if (!names.HasValues())
        ///  					Console.WriteLine("names为空或者长度为0");
        ///  				else
        ///  					Console.WriteLine("names不为空且长度大于0");
        ///            ]]>
        ///       </code>
        /// </example>
        public static bool HasValues<T>(this IEnumerable<T> @this)
        {
            return @this != null && @this.Any();
        }
    }
}