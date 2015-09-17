using System;
using System.Windows.Markup;
using System.Windows.Media;

namespace CodeMask.Windows.Extensions.MarkupExtensions
{
    /// <summary>
    ///     ImageSource 标记扩展类。
    /// </summary>
    /// <example>
    ///     <code>
    ///          <![CDATA[
    ///          ]]>
    ///     </code>
    /// </example>
    public class ImageSourceExtension : MarkupExtension
    {
        #region 私有变量

        private readonly ImageSource _source;

        #endregion

        #region 构造

        /// <summary>
        ///     构造。
        /// </summary>
        /// <param name="relativePath">图片相对路径。</param>
        public ImageSourceExtension(string relativePath)
        {
            _source =
                (ImageSource) new ImageSourceConverter().ConvertFromString("pack://siteOfOrigin:,,,/" + relativePath);
        }

        #endregion

        #region 重写方法

        /// <summary>
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _source;
        }

        #endregion
    }
}