using Microsoft.Win32;

namespace CodeMask.Windows.Core
{
    /// <summary>
    ///     检测.NET是否安装。
    /// </summary>
    public static class DotnetDetection
    {
        /// <summary>
        ///     获取 .NET 3.5  SP1 是否安装。
        /// </summary>
        public static bool IsDotnet35SP1Installed()
        {
            using (var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.5"))
            {
                return key != null && (key.GetValue("SP") as int?) >= 1;
            }
        }

        /// <summary>
        ///     获取 .NET 4.x 是否安装。
        /// </summary>
        public static bool IsDotnet40Installed()
        {
            return true;
        }

        /// <summary>
        ///     获取.NET 4.5 是否安装。
        /// </summary>
        public static bool IsDotnet45Installed()
        {
            return GetDotnet4Release() >= 378389;
        }

        /// <summary>
        ///     获取 .NET 4.5.1 是否安装。
        /// </summary>
        public static bool IsDotnet451Installed()
        {
            // According to: http://blogs.msdn.com/b/astebner/archive/2013/11/11/10466402.aspx
            // 378675 is .NET 4.5.1 on Win8
            // 378758 is .NET 4.5.1 on Win7
            return GetDotnet4Release() >= 378675;
        }

        /// <summary>
        ///     获取 .NET 4.5.2 是否安装。
        /// </summary>
        /// <returns></returns>
        public static bool IsDotnet452Installed()
        {
            // 379893 is .NET 4.5.2 on my Win7 machine
            return GetDotnet4Release() >= 379893;
        }

        /// <summary>
        ///     获取 .NET 4.6 是否安装。
        /// </summary>
        /// <returns></returns>
        public static bool IsDotnet46Installed()
        {
            // 393273 is .NET 4.6 on my Win7 machine with VS 2015 RC installed
            return GetDotnet4Release() >= 393273;
        }

        /// <summary>
        ///     获取 .NET 4.x 发布版本号。<see cref="http://msdn.microsoft.com/en-us/library/hh925568.aspx" />。
        /// </summary>
        private static int? GetDotnet4Release()
        {
            using (var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full"))
            {
                if (key != null)
                    return key.GetValue("Release") as int?;
            }
            return null;
        }
    }
}