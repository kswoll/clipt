using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Clipt.Keyboards
{
    public class KeySequence : IEnumerable<KeyTrigger>
    {
        public int Count => keys.Count;
        public KeyTrigger this[int index] => keys[index];
        public KeySequenceSpan Start => new KeySequenceSpan(this, 0, Count);

        private IReadOnlyList<KeyTrigger> keys;

        public KeySequence(params KeyTrigger[] keys) : this((IEnumerable<KeyTrigger>)keys)
        {
        }

        public KeySequence(IEnumerable<KeyTrigger> keys)
        {
            this.keys = keys.ToArray();
            if (this.keys.Count == 0)
                throw new ArgumentException(nameof(keys));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<KeyTrigger> GetEnumerator()
        {
            return keys.GetEnumerator();
        }
    }
}