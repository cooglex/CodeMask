/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System;
using System.Collections.Generic;

namespace CodeMask.WPF.AvalonDock.Controls
{
    internal class WeakDictionary<K, V> where K : class
    {
        private readonly List<WeakReference> _keys = new List<WeakReference>();
        private readonly List<V> _values = new List<V>();

        public V this[K key]
        {
            get
            {
                V valueToReturn;
                if (!GetValue(key, out valueToReturn))
                    throw new ArgumentException();
                return valueToReturn;
            }
            set { SetValue(key, value); }
        }

        public bool ContainsKey(K key)
        {
            CollectGarbage();
            return -1 != _keys.FindIndex(k => k.GetValueOrDefault<K>() == key);
        }

        public void SetValue(K key, V value)
        {
            CollectGarbage();
            var vIndex = _keys.FindIndex(k => k.GetValueOrDefault<K>() == key);
            if (vIndex > -1)
                _values[vIndex] = value;
            else
            {
                _values.Add(value);
                _keys.Add(new WeakReference(key));
            }
        }

        public bool GetValue(K key, out V value)
        {
            CollectGarbage();
            var vIndex = _keys.FindIndex(k => k.GetValueOrDefault<K>() == key);
            value = default(V);
            if (vIndex == -1)
                return false;
            value = _values[vIndex];
            return true;
        }


        private void CollectGarbage()
        {
            var vIndex = 0;

            do
            {
                vIndex = _keys.FindIndex(vIndex, k => !k.IsAlive);
                if (vIndex >= 0)
                {
                    _keys.RemoveAt(vIndex);
                    _values.RemoveAt(vIndex);
                }
            } while (vIndex >= 0);
        }
    }
}