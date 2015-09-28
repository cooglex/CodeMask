using System;
using System.Windows.Markup;

namespace CodeMask.UWP.Converters
{
    /// <summary>
    ///     支持标记扩展的转换器基类。
    /// </summary>
    /// <typeparam name="T">类型参数。</typeparam>
    public class CoreConverter<T> : MarkupExtension
    {
        /// <summary>
        ///     实例对象。
        /// </summary>
        private static Lazy<T> _instance;

        /// <summary>
        ///     访问锁。
        /// </summary>
        private static readonly object Syslock = new object();

        /// <summary>
        ///     获取相应类型的实例对象。
        /// </summary>
        /// <value>实例对象。</value>
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Syslock)
                    {
                        if (_instance == null)
                        {
                            _instance = new Lazy<T>(true);
                        }
                    }
                }
                return _instance.Value;
            }
        }

        /// <summary>
        ///     返回默认的转换器对象。
        /// </summary>
        /// <param name="serviceProvider">服务提供者。</param>
        /// <returns>转换器对象。</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Instance;
        }
    }
}