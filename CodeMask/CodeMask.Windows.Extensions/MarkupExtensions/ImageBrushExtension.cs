using System;
using System.Windows.Markup;
using System.Windows.Media;

namespace CodeMask.Windows.Extensions.MarkupExtensions
{
    /// <summary>
    ///     ImageBrush标记扩展。
    /// </summary>
    /// <example>
    ///     <code>
    ///          <![CDATA[
    ///          ]]>
    ///     </code>
    /// </example>
    public class ImageBrushExtension : MarkupExtension
    {
        #region 私有变量

        /// <summary>
        ///     图片相对路径。
        /// </summary>
        private readonly object _relativePath;

        #endregion

        #region 构造

        /// <summary>
        ///     构造。
        /// </summary>
        /// <param name="relativePath">图片相对路径。</param>
        public ImageBrushExtension(object relativePath)
        {
            _relativePath = relativePath;
        }

        #endregion

        #region 重写方法

        /// <summary>
        ///     返回值。
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_relativePath.GetType() == typeof (ImageSource))
                return new ImageBrush((ImageSource) _relativePath);
            return
                new ImageBrush(
                    (ImageSource)
                        new ImageSourceConverter().ConvertFromString("pack://siteOfOrigin:,,,/" + _relativePath));
        }

        #endregion
    }
}