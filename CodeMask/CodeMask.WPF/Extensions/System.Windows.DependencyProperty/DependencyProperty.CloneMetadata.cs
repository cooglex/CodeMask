using System.Windows;

namespace CodeMask.WPF.Extensions
{
    /// <summary>
    ///     <see cref="System.Windows.DependencyProperty" />扩展。
    /// </summary>
    public static partial class DependencyPropertyExtensions
    {
        /// <summary>
        ///     克隆指定对象上的依赖属性元数据。
        /// </summary>
        /// <param name="this"><see cref="System.Windows.DependencyProperty" />实例。</param>
        /// <param name="dependencyObject">依赖属性所在的对象。</param>
        /// <returns>克隆后的依赖属性元数据。</returns>
        /// <example>
        ///     <code>
        ///          <![CDATA[
        ///          ]]>
        ///     </code>
        /// </example>
        public static FrameworkPropertyMetadata CloneMetadata(this DependencyProperty @this,
            DependencyObject dependencyObject)
        {
            var metadata = @this.GetMetadata(dependencyObject) as FrameworkPropertyMetadata;
            if (metadata != null)
            {
                return metadata.Clone();
            }
            return null;
        }
    }
}