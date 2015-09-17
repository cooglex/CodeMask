using System;

namespace CodeMask.Extensions
{
    /// <summary>
    ///     EventHandler扩展类。
    /// </summary>
    public static class EventHandlerExtension
    {
        /// <summary>
        ///     引发事件执行。
        /// </summary>
        /// <param name="this">要引发的事件。</param>
        /// <param name="sender">触发事件的来源。</param>
        /// <example>
        ///     <code>
        ///          <![CDATA[
        ///          ]]>
        ///     </code>
        /// </example>
        public static void RaiseEvent(this EventHandler @this, object sender)
        {
            if (@this != null)
                @this(sender, null);
        }

        /// <summary>
        ///     引发事件执行。
        /// </summary>
        /// <param name="this">要引发的事件。</param>
        /// <param name="sender">触发事件的来源。</param>
        /// <param name="e">事件参数。</param>
        /// <example>
        ///     <code>
        ///          <![CDATA[
        ///          ]]>
        ///     </code>
        /// </example>
        public static void RaiseEvent(this EventHandler @this, object sender, EventArgs e)
        {
            if (@this != null)
                @this(sender, e);
        }

        /// <summary>
        ///     引发事件执行。
        /// </summary>
        /// <typeparam name="TEventArgs">事件泛型参数。</typeparam>
        /// <param name="this">要引发的事件。</param>
        /// <param name="sender">触发事件的来源。</param>
        /// <example>
        ///     <code>
        ///          <![CDATA[
        ///          ]]>
        ///     </code>
        /// </example>
        public static void RaiseEvent<TEventArgs>(this EventHandler<TEventArgs> @this, object sender)
            where TEventArgs : EventArgs
        {
            if (@this != null)
                @this(sender, Activator.CreateInstance<TEventArgs>());
        }

        /// <summary>
        ///     引发事件执行。
        /// </summary>
        /// <typeparam name="TEventArgs">事件参数类型。</typeparam>
        /// <param name="this">要引发的事件。</param>
        /// <param name="sender">触发事件的来源。</param>
        /// <param name="e">事件参数对象。</param>
        /// <example>
        ///     <code>
        ///          <![CDATA[
        ///          ]]>
        ///     </code>
        /// </example>
        public static void RaiseEvent<TEventArgs>(this EventHandler<TEventArgs> @this, object sender, TEventArgs e)
            where TEventArgs : EventArgs
        {
            if (@this != null)
                @this(sender, e);
        }
    }
}