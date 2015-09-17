using System;
using System.Windows.Input;

namespace CodeMask.WPF.Commands
{
    /// <summary>
    ///     一个简单命令类。
    /// </summary>
    public abstract class SimpleCommand : ICommand
    {
        /// <summary>
        ///     当出现影响是否应执行该命令的更改时发生。
        /// </summary>
        public virtual event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        /// <summary>
        ///     定义用于确定此命令是否可以在其当前状态下执行的方法。
        /// </summary>
        /// <param name="parameter">此命令使用的数据。 如果此命令不需要传递数据，则该对象可以设置为 null。</param>
        /// <returns>如果可以执行此命令，则为 true；否则为 false。</returns>
        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        ///     定义在调用此命令时调用的方法。
        /// </summary>
        /// <param name="parameter">此命令使用的数据。 如果此命令不需要传递数据，则该对象可以设置为 null。</param>
        public abstract void Execute(object parameter);
    }
}