using System;

namespace CodeMask.WPF.Commands
{
    /// <summary>
    ///     Interface IActiveAware
    /// </summary>
    public interface IActiveAware
    {
        /// <summary>
        ///     Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>
        bool IsActive { get; set; }

        /// <summary>
        ///     Occurs when [is active changed].
        /// </summary>
        event EventHandler IsActiveChanged;
    }
}