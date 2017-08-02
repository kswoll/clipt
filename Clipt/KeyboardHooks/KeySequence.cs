using System;
using System.Collections.Generic;
using System.Linq;

namespace Clipt.KeyboardHooks
{
    public class KeySequence
    {
        public IReadOnlyList<KeyTrigger> Keys { get; }

        public KeySequenceSpan Start => new KeySequenceSpan(this, 0, Keys.Count);

        public KeySequence(params KeyTrigger[] keys) : this((IEnumerable<KeyTrigger>)keys)
        {
        }

        public KeySequence(IEnumerable<KeyTrigger> keys)
        {
            Keys = keys.ToArray();
            if (Keys.Count == 0)
                throw new ArgumentException(nameof(keys));
        }
    }
}