using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Clipt.Inputs
{
    public struct KeyStroke : IEnumerable<KeyCode>
    {
        public int Count => keys.Count;
        public KeyCode this[int index] => keys[index];
        public bool Contains(KeyCode key) => keySet.Contains(key);

        private readonly int hashCode;
        private readonly ImmutableList<KeyCode> keys;
        private readonly ImmutableHashSet<KeyCode> keySet;

        public KeyStroke(params KeyCode[] keys)
        {
            this.keys = keys.OrderBy(x => (byte)x).ToImmutableList();
            keySet = keys.ToImmutableHashSet();

            unchecked
            {
                var hash = 17;
                foreach (KeyCode key in keys)
                {
                    hash = hash * 31 + key.GetHashCode();
                }
                hashCode = hash;
            }
        }

        public bool ProcessKey(ImmutableHashSet<KeyCode> activeKeys)
        {
            return keySet.Except(activeKeys).Count == 0;
        }

        public override int GetHashCode()
        {
            return hashCode;
        }

        public override bool Equals(Object obj)
        {
            return obj is KeyStroke && this == (KeyStroke)obj;
        }

        public static bool operator ==(KeyStroke x, KeyStroke y)
        {
            if (x.keys.Count != y.keys.Count)
                return false;

            return x.keys.SequenceEqual(y.keys);
        }

        public static bool operator !=(KeyStroke x, KeyStroke y)
        {
            return !(x == y);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<KeyCode> GetEnumerator()
        {
            return keys.GetEnumerator();
        }
    }
}