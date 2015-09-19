using System.Windows;

namespace CodeMask.WPF.Extensions
{
    /// <summary>
    ///     <see cref="System.Windows.FrameworkPropertyMetadata" />扩展。
    /// </summary>
    public static partial class FrameworkPropertyMetadataExtensions
    {
        /// <summary>
        ///     对<see cref="System.Windows.FrameworkPropertyMetadata" />元数据进行克隆。
        /// </summary>
        /// <param name="this"><see cref="System.Windows.FrameworkPropertyMetadata" />元数据。</param>
        /// <returns>克隆后的<see cref="System.Windows.FrameworkPropertyMetadata" />元数据。</returns>
        /// <example>
        ///     <code>
        ///          <![CDATA[
        ///          ]]>
        ///     </code>
        /// </example>
        public static FrameworkPropertyMetadata Clone(this FrameworkPropertyMetadata @this)
        {
            var clone = new FrameworkPropertyMetadata();
            clone.AffectsArrange = @this.AffectsArrange;
            clone.AffectsMeasure = @this.AffectsMeasure;
            clone.AffectsParentArrange = @this.AffectsParentArrange;
            clone.AffectsParentMeasure = @this.AffectsParentMeasure;
            clone.AffectsRender = @this.AffectsRender;
            clone.BindsTwoWayByDefault = @this.BindsTwoWayByDefault;
            clone.CoerceValueCallback = @this.CoerceValueCallback;
            clone.DefaultUpdateSourceTrigger = @this.DefaultUpdateSourceTrigger;
            clone.DefaultValue = @this.DefaultValue;
            clone.Inherits = @this.Inherits;
            clone.IsAnimationProhibited = @this.IsAnimationProhibited;
            clone.IsNotDataBindable = @this.IsNotDataBindable;
            clone.Journal = @this.Journal;
            clone.OverridesInheritanceBehavior = @this.OverridesInheritanceBehavior;
            clone.PropertyChangedCallback = @this.PropertyChangedCallback;
            clone.SubPropertiesDoNotAffectRender = @this.SubPropertiesDoNotAffectRender;
            return clone;
        }
    }
}