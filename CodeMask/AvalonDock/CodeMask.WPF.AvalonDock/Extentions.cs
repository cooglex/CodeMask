﻿/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;

namespace CodeMask.WPF.AvalonDock
{
    internal static class Extensions
    {
        public static bool Contains(this IEnumerable collection, object item)
        {
            foreach (var o in collection)
                if (o == item)
                    return true;

            return false;
        }


        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var v in collection)
                action(v);
        }


        public static int IndexOf<T>(this T[] array, T value) where T : class
        {
            for (var i = 0; i < array.Length; i++)
                if (array[i] == value)
                    return i;

            return -1;
        }

        public static V GetValueOrDefault<V>(this WeakReference wr)
        {
            if (wr == null || !wr.IsAlive)
                return default(V);
            return (V) wr.Target;
        }
    }
}