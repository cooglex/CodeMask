namespace CodeMask.Windows.Services
{
    /// <summary>
    ///     提供服务管理。
    /// </summary>
    public interface IServicesFactory
    {
        /// <summary>
        ///     获取类型参数<typeparamref name="T" /> name="T"/>指定的服务。
        /// </summary>
        /// <typeparam name="T">服务类型。</typeparam>
        /// <param name="context">服务对象。</param>
        /// <returns>服务。</returns>
        /// <exception cref="System.ArgumentException">
        ///     获取的服务必须是接口类型。
        /// </exception>
        T GetService<T>(object context = null);

        /// <summary>
        ///     判断是否已经注册类型参数<typeparamref name="T" />指定的服务。
        /// </summary>
        /// <typeparam name="T">服务类型。</typeparam>
        /// <returns>包含此服务，为<c>true</c> ; 否则, <c>false</c>.</returns>
        bool HasService<T>();

        /// <summary>
        ///     初始化服务。
        /// </summary>
        void Initialize();

        /// <summary>
        ///     注册类型参数<typeparamref name="T" />指定的服务。
        /// </summary>
        /// <typeparam name="T">服务类型。</typeparam>
        /// <param name="serviceProvider">要注册的服务。</param>
        /// <exception cref="System.ArgumentException">注册的服务必须是接口类型</exception>
        void RegisterService<T>(ServiceProvider serviceProvider);
    }
}