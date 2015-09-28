using System;
using System.Windows.Input;

namespace CodeMask.UWP.Commands
{
    /// <summary>
    ///     <see cref="RelayCommand{T}" />类。
    /// </summary>
    /// <typeparam name="T">类型参数。</typeparam>
    public class RelayCommand<T> : ICommand
    {
        /// <summary>
        ///     执行委托的条件。
        /// </summary>
        private readonly Predicate<T> canExecute;

        /// <summary>
        ///     执行的委托。
        /// </summary>
        private readonly Action<T> execute;

        /// <summary>
        ///     构造。
        /// </summary>
        /// <param name="execute">执行的委托。</param>
        /// <exception cref="System.ArgumentNullException">空异常。</exception>
        public RelayCommand(Action<T> execute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            this.execute = execute;
        }

        /// <summary>
        ///     构造。
        /// </summary>
        /// <param name="execute">执行的委托。</param>
        /// <param name="canExecute">执行委托的条件。</param>
        /// <exception cref="System.ArgumentNullException">空异常。</exception>
        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        ///     当出现影响是否应执行该命令的更改时发生。
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        /// <summary>
        ///     定义用于确定此命令是否可以在其当前状态下执行的方法。
        /// </summary>
        /// <param name="parameter">此命令使用的数据。 如果此命令不需要传递数据，则该对象可以设置为 null。</param>
        /// <returns>如果可以执行此命令，则为 true；否则为 false。</returns>
        public bool CanExecute(object parameter)
        {
            if (parameter != null && !(parameter is T))
                return false;
            return canExecute == null ? true : canExecute((T) parameter);
        }

        /// <summary>
        ///     定义在调用此命令时调用的方法。
        /// </summary>
        /// <param name="parameter">此命令使用的数据。 如果此命令不需要传递数据，则该对象可以设置为 null。</param>
        public void Execute(object parameter)
        {
            execute((T) parameter);
        }
    }

    /// <summary>
    ///     RelayCommand类。
    /// </summary>
    public class RelayCommand : ICommand
    {
        /// <summary>
        ///     执行委托的条件。
        /// </summary>
        private readonly Func<bool> canExecute;

        /// <summary>
        ///     执行的委托。
        /// </summary>
        private readonly Action execute;

        /// <summary>
        ///     执行的委托。
        /// </summary>
        private readonly Action<object> execute2;

        /// <summary>
        ///     构造。
        /// </summary>
        /// <param name="execute">执行的委托</param>
        /// <exception cref="System.ArgumentNullException">空异常。</exception>
        public RelayCommand(Action execute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            this.execute = execute;
        }

        /// <summary>
        ///     构造。
        /// </summary>
        /// <param name="execute">执行的委托。</param>
        /// <param name="canExecute">执行委托的条件。</param>
        /// <exception cref="System.ArgumentNullException">空异常。</exception>
        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        ///     构造。
        /// </summary>
        /// <param name="execute">执行的委托。</param>
        /// <exception cref="System.ArgumentNullException">空异常。</exception>
        public RelayCommand(Action<object> execute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            execute2 = execute;
        }

        /// <summary>
        ///     当出现影响是否应执行该命令的更改时发生。
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        /// <summary>
        ///     定义用于确定此命令是否可以在其当前状态下执行的方法。
        /// </summary>
        /// <param name="parameter">此命令使用的数据。 如果此命令不需要传递数据，则该对象可以设置为 null。</param>
        /// <returns>如果可以执行此命令，则为 true；否则为 false。</returns>
        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute();
        }

        /// <summary>
        ///     定义在调用此命令时调用的方法。
        /// </summary>
        /// <param name="parameter">此命令使用的数据。 如果此命令不需要传递数据，则该对象可以设置为 null。</param>
        public void Execute(object parameter)
        {
            if (parameter == null)
                execute();
            else
                execute2(parameter);
        }
    }
}