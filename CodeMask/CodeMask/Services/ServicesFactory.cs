using System;
using System.Collections.Generic;
using CodeMask.Design;

namespace CodeMask.Services
{
    /// <summary>
    ///     服务工厂类。
    /// </summary>
    public class ServicesFactory : Singleton<ServicesFactory>, IServicesFactory
    {
        /// <summary>
        ///     存储服务字典。
        /// </summary>
        private readonly Dictionary<Type, ServiceProvider> _serviceDictionary = new Dictionary<Type, ServiceProvider>();

        /// <summary>
        ///     获取类型参数<typeparamref name="T" /> name="T"/>指定的服务。
        /// </summary>
        /// <typeparam name="T">服务类型。</typeparam>
        /// <param name="context">服务对象。</param>
        /// <returns>服务。</returns>
        /// <exception cref="System.ArgumentException">
        ///     获取的服务必须是接口类型。
        /// </exception>
        public T GetService<T>(object context = null)
        {
            T service;
            var type = typeof (T);
            if (!type.IsInterface)
            {
                throw new ArgumentException("获取的服务必须是接口类型");
            }
            lock (_serviceDictionary)
            {
                if (!_serviceDictionary.ContainsKey(type))
                {
                    throw new ArgumentException(string.Format("服务{0}未注册", type.FullName));
                }
                service = (T) _serviceDictionary[type](context);
            }
            return service;
        }

        /// <summary>
        ///     判断是否已经注册类型参数<typeparamref name="T" />指定的服务。
        /// </summary>
        /// <typeparam name="T">服务类型。</typeparam>
        /// <returns>包含此服务，为<c>true</c> ; 否则, <c>false</c>.</returns>
        public bool HasService<T>()
        {
            bool hasService;
            lock (_serviceDictionary)
            {
                hasService = _serviceDictionary.ContainsKey(typeof (T));
            }
            return hasService;
        }

        /// <summary>
        ///     注册类型参数<typeparamref name="T" />指定的服务。
        /// </summary>
        /// <typeparam name="T">服务类型。</typeparam>
        /// <param name="serviceProvider">要注册的服务。</param>
        /// <exception cref="System.ArgumentException">注册的服务必须是接口类型</exception>
        public void RegisterService<T>(ServiceProvider serviceProvider)
        {
            var type = typeof (T);
            if (!type.IsInterface)
            {
                throw new ArgumentException("注册的服务必须是接口类型");
            }
            lock (_serviceDictionary)
            {
                if (serviceProvider != null || !_serviceDictionary.ContainsKey(type))
                {
                    _serviceDictionary[type] = serviceProvider;
                }
                else
                {
                    _serviceDictionary.Remove(type);
                }
            }
        }

        /// <summary>
        ///     初始化服务。
        /// </summary>
        public virtual void Initialize()
        {
        }
    }
}