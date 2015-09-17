namespace CodeMask.Services
{
    /// <summary>
    ///     提供服务委托。
    /// </summary>
    /// <param name="context">服务对象。</param>
    /// <returns>服务对象。</returns>
    public delegate object ServiceProvider(object context = null);
}