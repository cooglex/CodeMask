using System;

namespace CodeMask.Windows.Core
{
    /// <summary>
    ///     核心异常类，表示在应用程序执行过程中发生的错误。
    /// </summary>
    public class CoreException : Exception
    {
        /// <summary>
        ///     初始化 <see cref="CodeMask.Windows.Core.CoreException" /> 类的新实例。
        /// </summary>
        public CoreException()
        {
        }

        /// <summary>
        ///     使用指定的错误信息初始化 <see cref="CodeMask.Windows.Core.CoreException" />  类的新实例。
        /// </summary>
        /// <param name="message">描述错误的消息。</param>
        public CoreException(string message) : base(message)
        {
        }

        /// <summary>
        ///     使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="CodeMask.Windows.Core.CoreException" /> 类的新实例。
        /// </summary>
        /// <param name="message">解释异常原因的错误信息。</param>
        /// <param name="innerException">导致当前异常的异常；如果未指定内部异常，则是一个 null 引用（在 Visual Basic 中为 Nothing）。</param>
        public CoreException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}