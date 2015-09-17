using System;

namespace CodeMask.Windows.Design
{
    /// <summary>
    ///     单例模式类。
    /// </summary>
    /// <typeparam name="T">类型参数。</typeparam>
    public class Singleton<T> where T : class, new()
    {
        /// <summary>
        ///     实例对象。
        /// </summary>
        private static Lazy<T> _instance;

        /// <summary>
        ///     访问锁。
        /// </summary>
        private static readonly object syslock = new object();

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
                    lock (syslock)
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
    }
}