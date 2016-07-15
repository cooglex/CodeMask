using System.Windows;

namespace CodeMask.WPF.Controls.CustomWindow
{
    /// <summary>
    /// Interface INonClientArea
    /// </summary>
    public interface INonClientArea
    {
        /// <summary>
        /// Hits the test.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>System.Int32.</returns>
        int HitTest(Point point);
    }
}