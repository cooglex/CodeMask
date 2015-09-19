using System.Windows.Media.Media3D;

namespace CodeMask.WPF.Extensions
{
    /// <summary>
    ///     <see cref="System.Windows.Media.Media3D.Vector3D" />扩展。
    /// </summary>
    public static partial class Vector3DExtensions
    {
        /// <summary>
        ///     查找给定三维向量<see cref="System.Windows.Media.Media3D.Vector3D" />的垂直向量
        ///     <see cref="System.Windows.Media.Media3D.Vector3D" />。
        /// </summary>
        /// <param name="this">三维向量<see cref="System.Windows.Media.Media3D.Vector3D" />。</param>
        /// <returns>三维向量<see cref="System.Windows.Media.Media3D.Vector3D" />。</returns>
        /// <example>
        ///     <code>
        ///          <![CDATA[
        ///          ]]>
        ///     </code>
        /// </example>
        public static Vector3D FindAnyPerpendicular(this Vector3D @this)
        {
            var u = Vector3D.CrossProduct(new Vector3D(0, 1, 0), @this);
            if (u.LengthSquared < 1e-3)
            {
                u = Vector3D.CrossProduct(new Vector3D(1, 0, 0), @this);
            }

            return u;
        }
    }
}