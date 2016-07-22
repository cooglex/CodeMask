using System.Windows.Media.Media3D;

namespace CodeMask.WPF.Extensions
{
    /// <summary>
    ///     <see cref="System.Windows.Media.Media3D.Vector3D" />扩展。
    /// </summary>
    public static partial class Vector3DExtensions
    {
        /// <summary>
        ///     将三维向量<see cref="System.Windows.Media.Media3D.Vector3D" />转为三维点<see cref="System.Windows.Media.Media3D.Point3D" />。
        /// </summary>
        /// <param name="this">三维向量<see cref="System.Windows.Media.Media3D.Vector3D" />。</param>
        /// <returns>三维点<see cref="System.Windows.Media.Media3D.Point3D" />。</returns>
        /// <example>
        ///     <code>
        ///          <![CDATA[
        ///          ]]>
        ///     </code>
        /// </example>
        public static Point3D ToPoint3D(this Vector3D @this)
        {
            return new Point3D(@this.X, @this.Y, @this.Z);
        }
    }
}