using System;
using System.Runtime.InteropServices;

namespace CodeMask.WPF.Controls.CustomWindow
{
    /// <summary>
    /// Class DisposableObject
    /// </summary>
    [ComVisible(true)]
    public class DisposableObject : IDisposable
    {
        /// <summary>
        /// The _disposing
        /// </summary>
        private EventHandler _disposing;

        /// <summary>
        /// Occurs when [disposing].
        /// </summary>
        public event EventHandler Disposing
        {
            add
            {
                this.ThrowIfDisposed();
                _disposing += value;
            }
            remove
            {
                this.ThrowIfDisposed();
                _disposing -= value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is disposed.
        /// </summary>
        /// <value><c>true</c> if this instance is disposed; otherwise, <c>false</c>.</value>
        public bool IsDisposed
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DisposableObject"/> class.
        /// </summary>
        public DisposableObject()
        {
        }

        /// <summary>
        /// 执行与释放或重置非托管资源相关的应用程序定义的任务。
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected void Dispose(bool disposing)
        {
            if (!this.IsDisposed)
            {
                try
                {
                    if (this._disposing != null)
                        this._disposing(this, EventArgs.Empty);
                    this._disposing = null;
                    if (disposing)
                    {
                        this.DisposeManagedResources();
                    }
                    this.DisposeNativeResources();
                }
                finally
                {
                    this.IsDisposed = true;
                }
            }
        }

        /// <summary>
        /// Disposes the managed resources.
        /// </summary>
        protected virtual void DisposeManagedResources()
        {
        }

        /// <summary>
        /// Disposes the native resources.
        /// </summary>
        protected virtual void DisposeNativeResources()
        {
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="DisposableObject"/> class.
        /// </summary>
        ~DisposableObject()
        {
            Console.WriteLine("I am descontructor");
        }

        /// <summary>
        /// Throws if disposed.
        /// </summary>
        /// <exception cref="System.ObjectDisposedException"></exception>
        protected void ThrowIfDisposed()
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }
        }
    }
}