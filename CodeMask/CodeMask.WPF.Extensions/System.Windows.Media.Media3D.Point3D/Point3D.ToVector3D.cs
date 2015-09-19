using System.Windows.Media.Media3D;

namespace CodeMask.WPF.Extensions
{
    /// <summary>
    ///     <see cref="System.Windows.Media.Media3D.Point3D" />扩展。
    /// </summary>
    public static partial class Point3DExtensions
    {
        /// <summary>
        ///     将三维点<see cref="System.Windows.Media.Media3D.Point3D" />转成三维向量<see cref="System.Windows.Media.Media3D.Vector3D" />。
        /// </summary>
        /// <param name="this">三维点<see cref="System.Windows.Media.Media3D.Point3D" />。</param>
        /// <returns>三维向量<see cref="System.Windows.Media.Media3D.Vector3D" />。</returns>
        /// <example>
        ///     <code>
        ///          <![CDATA[
        ///          ]]>
        ///     </code>
        /// </example>
        public static Vector3D ToVector3D(this Point3D @this)
        {
            return new Vector3D(@this.X, @this.Y, @this.Z);
        }
    }
}