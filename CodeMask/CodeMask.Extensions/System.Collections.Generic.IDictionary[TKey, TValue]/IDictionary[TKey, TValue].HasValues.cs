using System.Collections.Generic;
using System.Linq;

namespace CodeMask.Extensions
{
    /// <summary>
    ///     <see cref="System.Collections.Generic.IDictionary&lt;TKey, TValue&gt;" />扩展类。
    /// </summary>
    public static class IDictionaryExtension
    {
        /// <summary>
        ///     检查<see cref="System.Collections.Generic.IDictionary&lt;TKey, TValue&gt;" />类型的字典是否为空或长度是为0；
        ///     若<see cref="System.Collections.Generic.IDictionary&lt;TKey, TValue&gt;" />为空或者长度为0，返回false；
        ///     若<see cref="System.Collections.Generic.IDictionary&lt;TKey, TValue&gt;" />不为空且长度大于0，返回true。
        /// </summary>
        /// <typeparam name="TKey">键</typeparam>
        /// <typeparam name="TValue">值</typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        /// <example>
        ///     <code>
        ///           <![CDATA[
        /// 				Dictionary<string, string>names=new Dictionary<string, string>();
        ///                 if(!names.HasValues())
        /// 					Console.WriteLine("names为空或者长度为0");
        ///                 else
        /// 					Console.WriteLine("names不为空且长度大于0");
        ///           ]]>
        ///      </code>
        /// </example>
        public static bool HasValues<TKey, TValue>(this IDictionary<TKey, TValue> @this)
        {
            return @this != null && @this.Any();
        }
    }
}