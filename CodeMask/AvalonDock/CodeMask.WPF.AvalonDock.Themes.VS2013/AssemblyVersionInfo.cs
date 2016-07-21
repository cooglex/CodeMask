/************************************************************************

   AvalonDock







 

  

  **********************************************************************/

using System.Diagnostics.CodeAnalysis;
using System.Reflection;

#pragma warning disable 0436

[assembly: AssemblyVersion(VersionInfo.Version)]
#pragma warning restore 0436

internal static class VersionInfo
{
    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")] public const string BaseVersion =
        "2.0";

    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")] public const string Version =
        BaseVersion +
        ".0.0";

    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")] public const string PublicKeyToken =
        "ba83ff368b7563c6";
}