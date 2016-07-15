using System;
using System.Collections.Generic;
using CodeMask.Attributes;

namespace CodeMask.Services
{
    internal sealed class FallbackServiceProvider : IServiceProvider
    {
        private readonly Dictionary<Type, object> fallbackServiceDict = new Dictionary<Type, object>();

        public object GetService(Type serviceType)
        {
            object instance;
            lock (fallbackServiceDict)
            {
                if (!fallbackServiceDict.TryGetValue(serviceType, out instance))
                {
                    var attrs = serviceType.GetCustomAttributes(typeof (SDServiceAttribute), false);
                    if (attrs.Length == 1)
                    {
                        var attr = (SDServiceAttribute) attrs[0];
                        if (attr.FallbackImplementation != null)
                        {
                            instance = Activator.CreateInstance(attr.FallbackImplementation);
                        }
                    }
                    // store null if no fallback implementation exists
                    fallbackServiceDict.Add(serviceType, instance);
                }
            }
            return instance;
        }
    }
}