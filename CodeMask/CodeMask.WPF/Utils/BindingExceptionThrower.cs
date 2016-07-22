using System;
using System.Diagnostics;
using System.Text;

namespace CodeMask.WPF.Utils
{
    /// <summary>
    ///     WPF绑定异常捕捉。
    /// </summary>
    public static class BindingExceptionThrower
    {
        private static BindingErrorListener _errorListener;

        /// <summary>
        /// </summary>
        public static bool IsAttached
        {
            get { return _errorListener != null; }
        }

        /// <summary>
        ///     
        /// </summary>
        public static void Attach()
        {
            _errorListener = new BindingErrorListener();
            _errorListener.ErrorCatched += OnErrorCatched;
        }

        /// <summary>
        ///     
        /// </summary>
        public static void Detach()
        {
            _errorListener.ErrorCatched -= OnErrorCatched;
            _errorListener.Dispose();
            _errorListener = null;
        }

        [DebuggerStepThrough]
        private static void OnErrorCatched(string message)
        {
            throw new BindingException(message);
        }
    }

    /// <summary>
    /// </summary>
    public sealed class BindingErrorListener : IDisposable
    {
        private readonly ObservableTraceListener _traceListener;

        static BindingErrorListener()
        {
            PresentationTraceSources.Refresh();
        }

        /// <summary>
        /// </summary>
        public BindingErrorListener()
        {
            _traceListener = new ObservableTraceListener();
            PresentationTraceSources.DataBindingSource.Switch.Level = SourceLevels.Error;
            PresentationTraceSources.DataBindingSource.Listeners.Add(_traceListener);
        }

        public void Dispose()
        {
            PresentationTraceSources.DataBindingSource.Listeners.Remove(_traceListener);
            _traceListener.Dispose();
        }

        /// <summary>
        ///     错误捕捉。
        /// </summary>
        public event Action<string> ErrorCatched
        {
            add { _traceListener.TraceCatched += value; }
            remove { _traceListener.TraceCatched -= value; }
        }
    }

    /// <summary>
    /// </summary>
    internal sealed class ObservableTraceListener : TraceListener
    {
        internal StringBuilder Buffer = new StringBuilder();

        public override void Write(string message)
        {
            Buffer.Append(message);
        }

        [DebuggerStepThrough]
        public override void WriteLine(string message)
        {
            Buffer.Append(message);

            if (TraceCatched != null)
            {
                TraceCatched(Buffer.ToString());
            }

            Buffer.Clear();
        }

        public event Action<string> TraceCatched;
    }

    /// <summary>
    ///     绑定异常。
    /// </summary>
    [Serializable]
    public class BindingException : Exception
    {
        /// <summary>
        /// </summary>
        /// <param name="message"></param>
        public BindingException(string message)
            : base(message)
        {
        }
    }
}