using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using NativeMethodsPack;

namespace CodeMask.WPF.Controls.CustomWindow
{
    /// <summary>
    /// Class DpiHelper
    /// </summary>
    public static class DpiHelper
    {
        /// <summary>
        /// The logical dpi
        /// </summary>
        private const double LogicalDpi = 96;

        /// <summary>
        /// The transform from device
        /// </summary>
        private static MatrixTransform transformFromDevice;

        /// <summary>
        /// The transform to device
        /// </summary>
        private static MatrixTransform transformToDevice;

        /// <summary>
        /// Gets or sets the device dpi X.
        /// </summary>
        /// <value>The device dpi X.</value>
        public static double DeviceDpiX { get; set; }

        /// <summary>
        /// Gets or sets the device dpi Y.
        /// </summary>
        /// <value>The device dpi Y.</value>
        public static double DeviceDpiY { get; set; }

        /// <summary>
        /// Gets the device to logical units scaling factor X.
        /// </summary>
        /// <value>The device to logical units scaling factor X.</value>
        public static double DeviceToLogicalUnitsScalingFactorX
        {
            get { return TransformFromDevice.Matrix.M11; }
        }

        /// <summary>
        /// Gets the device to logical units scaling factor Y.
        /// </summary>
        /// <value>The device to logical units scaling factor Y.</value>
        public static double DeviceToLogicalUnitsScalingFactorY
        {
            get { return TransformFromDevice.Matrix.M22; }
        }

        /// <summary>
        /// Gets the logical to device units scaling factor X.
        /// </summary>
        /// <value>The logical to device units scaling factor X.</value>
        public static double LogicalToDeviceUnitsScalingFactorX
        {
            get { return TransformFromDevice.Matrix.M11; }
        }

        /// <summary>
        /// Gets the logical to device units scaling factor Y.
        /// </summary>
        /// <value>The logical to device units scaling factor Y.</value>
        public static double LogicalToDeviceUnitsScalingFactorY
        {
            get { return TransformFromDevice.Matrix.M22; }
        }

        /// <summary>
        /// Gets the transform from device.
        /// </summary>
        /// <value>The transform from device.</value>
        public static MatrixTransform TransformFromDevice
        {
            get { return DpiHelper.transformFromDevice; }
        }

        /// <summary>
        /// Gets the transform to device.
        /// </summary>
        /// <value>The transform to device.</value>
        public static MatrixTransform TransformToDevice
        {
            get { return DpiHelper.transformToDevice; }
        }

        /// <summary>
        /// Initializes static members of the <see cref="DpiHelper"/> class.
        /// </summary>
        static DpiHelper()
        {
            IntPtr dC = NativeMethodsPack.NativeMethods.GetDC(IntPtr.Zero);
            if (dC == IntPtr.Zero)
            {
                DpiHelper.DeviceDpiX = 96;
                DpiHelper.DeviceDpiY = 96;
            }
            else
            {
                DpiHelper.DeviceDpiX = (double)NativeMethodsPack.NativeMethods.GetDeviceCaps(dC, 88);
                DpiHelper.DeviceDpiY = (double)NativeMethodsPack.NativeMethods.GetDeviceCaps(dC, 90);
                NativeMethodsPack.NativeMethods.ReleaseDC(IntPtr.Zero, dC);
            }
            Matrix identity = Matrix.Identity;
            Matrix matrix = Matrix.Identity;
            identity.Scale(DpiHelper.DeviceDpiX / 96, DpiHelper.DeviceDpiY / 96);
            matrix.Scale(96 / DpiHelper.DeviceDpiX, 96 / DpiHelper.DeviceDpiY);
            DpiHelper.transformFromDevice = new MatrixTransform(matrix);
            DpiHelper.transformFromDevice.Freeze();
            DpiHelper.transformToDevice = new MatrixTransform(identity);
            DpiHelper.transformToDevice.Freeze();
        }

        /// <summary>
        /// Devices to logical units.
        /// </summary>
        /// <param name="devicePoint">The device point.</param>
        /// <returns>Point.</returns>
        public static Point DeviceToLogicalUnits(this Point devicePoint)
        {
            return DpiHelper.TransformFromDevice.Transform(devicePoint);
        }

        /// <summary>
        /// Devices to logical units.
        /// </summary>
        /// <param name="deviceRect">The device rect.</param>
        /// <returns>Rect.</returns>
        public static Rect DeviceToLogicalUnits(this Rect deviceRect)
        {
            Rect rect = deviceRect;
            rect.Transform(DpiHelper.TransformFromDevice.Matrix);
            return rect;
        }

        /// <summary>
        /// Devices to logical units.
        /// </summary>
        /// <param name="deviceSize">Size of the device.</param>
        /// <returns>Size.</returns>
        public static Size DeviceToLogicalUnits(this Size deviceSize)
        {
            return new Size(deviceSize.Width * DpiHelper.DeviceToLogicalUnitsScalingFactorX, deviceSize.Height * DpiHelper.DeviceToLogicalUnitsScalingFactorY);
        }

        /// <summary>
        /// Gets the actual size of the device.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>Size.</returns>
        public static Size GetDeviceActualSize(this FrameworkElement element)
        {
            Size size = new Size(element.ActualWidth, element.ActualHeight);
            return size.LogicalToDeviceUnits();
        }

        /// <summary>
        /// Gets the height of the device.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <returns>System.Double.</returns>
        public static double GetDeviceHeight(this System.Windows.Window window)
        {
            return window.Height * DpiHelper.LogicalToDeviceUnitsScalingFactorY;
        }

        /// <summary>
        /// Gets the device left.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <returns>System.Double.</returns>
        public static double GetDeviceLeft(this System.Windows.Window window)
        {
            return window.Left * DpiHelper.LogicalToDeviceUnitsScalingFactorX;
        }

        /// <summary>
        /// Gets the device rect.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <returns>Rect.</returns>
        public static Rect GetDeviceRect(this System.Windows.Window window)
        {
            RECT rECT;
            NativeMethodsPack.NativeMethods.GetWindowRect((new WindowInteropHelper(window)).Handle, out rECT);
            return new Rect(new Point((double)rECT.Left, (double)rECT.Top), new Size((double)rECT.Width, (double)rECT.Height));
        }

        /// <summary>
        /// Gets the device top.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <returns>System.Double.</returns>
        public static double GetDeviceTop(this System.Windows.Window window)
        {
            return window.Top * DpiHelper.LogicalToDeviceUnitsScalingFactorY;
        }

        /// <summary>
        /// Gets the width of the device.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <returns>System.Double.</returns>
        public static double GetDeviceWidth(this System.Windows.Window window)
        {
            return window.Width * DpiHelper.LogicalToDeviceUnitsScalingFactorX;
        }

        /// <summary>
        /// Logicals to device units.
        /// </summary>
        /// <param name="logicalPoint">The logical point.</param>
        /// <returns>Point.</returns>
        public static Point LogicalToDeviceUnits(this Point logicalPoint)
        {
            return DpiHelper.TransformToDevice.Transform(logicalPoint);
        }

        /// <summary>
        /// Logicals to device units.
        /// </summary>
        /// <param name="logicalRect">The logical rect.</param>
        /// <returns>Rect.</returns>
        public static Rect LogicalToDeviceUnits(this Rect logicalRect)
        {
            Rect rect = logicalRect;
            rect.Transform(DpiHelper.TransformToDevice.Matrix);
            return rect;
        }

        /// <summary>
        /// Logicals to device units.
        /// </summary>
        /// <param name="logicalSize">Size of the logical.</param>
        /// <returns>Size.</returns>
        public static Size LogicalToDeviceUnits(this Size logicalSize)
        {
            return new Size(logicalSize.Width * DpiHelper.LogicalToDeviceUnitsScalingFactorX, logicalSize.Height * DpiHelper.LogicalToDeviceUnitsScalingFactorY);
        }

        /// <summary>
        /// Sets the height of the device.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <param name="deviceHeight">Height of the device.</param>
        public static void SetDeviceHeight(this System.Windows.Window window, double deviceHeight)
        {
            window.Height = deviceHeight * DpiHelper.DeviceToLogicalUnitsScalingFactorY;
        }

        /// <summary>
        /// Sets the device left.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <param name="deviceLeft">The device left.</param>
        public static void SetDeviceLeft(this System.Windows.Window window, double deviceLeft)
        {
            window.Left = deviceLeft * DpiHelper.DeviceToLogicalUnitsScalingFactorX;
        }

        /// <summary>
        /// Sets the device top.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <param name="deviceTop">The device top.</param>
        public static void SetDeviceTop(this System.Windows.Window window, double deviceTop)
        {
            window.Top = deviceTop * DpiHelper.DeviceToLogicalUnitsScalingFactorY;
        }

        /// <summary>
        /// Sets the width of the device.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <param name="deviceWidth">Width of the device.</param>
        public static void SetDeviceWidth(this System.Windows.Window window, double deviceWidth)
        {
            window.Width = deviceWidth * DpiHelper.DeviceToLogicalUnitsScalingFactorX;
        }
    }
}