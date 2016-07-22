using System;
using System.Windows.Controls;
using System.Windows.Markup;
using CodeMask.WPF.Converters;

namespace CodeMask.WPF.MarkupExtensions
{
    /// <summary>
    /// </summary>
    public sealed class CaseConverterExtension : MarkupExtension
    {
        /// <summary>
        /// </summary>
        public CaseConverterExtension()
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="casing"></param>
        public CaseConverterExtension(CharacterCasing casing)
        {
            Casing = casing;
        }

        /// <summary>
        /// </summary>
        /// <param name="sourceCasing"></param>
        /// <param name="targetCasing"></param>
        public CaseConverterExtension(CharacterCasing sourceCasing, CharacterCasing targetCasing)
        {
            SourceCasing = sourceCasing;
            TargetCasing = targetCasing;
        }

        /// <summary>
        /// </summary>
#if !SILVERLIGHT
        [ConstructorArgument("sourceCasing")]
#endif
            public CharacterCasing SourceCasing { get; set; }

        /// <summary>
        /// </summary>
#if !SILVERLIGHT
        [ConstructorArgument("targetCasing")]
#endif
            public CharacterCasing TargetCasing { get; set; }

        /// <summary>
        /// </summary>
        public CharacterCasing Casing
        {
            set
            {
                SourceCasing = value;
                TargetCasing = value;
            }
        }

        /// <summary>
        ///     返回默认的转换器对象。
        /// </summary>
        /// <param name="serviceProvider">服务提供者。</param>
        /// <returns>转换器对象。</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new CaseConverter(SourceCasing, TargetCasing);
        }
    }
}