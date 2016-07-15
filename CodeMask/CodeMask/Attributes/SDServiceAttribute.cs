using System;

namespace CodeMask.Attributes
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, Inherited = false)]
    public class SDServiceAttribute : Attribute
    {
        /// <summary>
        ///     Creates a new SDServiceAttribute instance.
        /// </summary>
        public SDServiceAttribute()
        {
        }

        /// <summary>
        ///     Creates a new SDServiceAttribute instance.
        /// </summary>
        /// <param name="staticPropertyPath">
        ///     Documents the suggested way to access this service using a static property.
        ///     Example: <c>SD.WinForms.ResourceService</c>
        /// </param>
        public SDServiceAttribute(string staticPropertyPath)
        {
            StaticPropertyPath = staticPropertyPath;
        }

        /// <summary>
        ///     A string that documents the suggested way to access this service using a static property.
        ///     Example: <c>SD.WinForms.ResourceService</c>
        /// </summary>
        public string StaticPropertyPath { get; set; }

        /// <summary>
        ///     The class that implements the interface and serves as a fallback service
        ///     in case no real implementation is registered.
        /// </summary>
        /// <remarks>
        ///     This property is also useful for unit tests, as there usually is no real service instance when testing.
        ///     Fallback services must not maintain any state, as that would be preserved between runs
        ///     even if <c>SD.TearDownForUnitTests()</c> or <c>SD.InitializeForUnitTests()</c> is called.
        /// </remarks>
        public Type FallbackImplementation { get; set; }
    }
}