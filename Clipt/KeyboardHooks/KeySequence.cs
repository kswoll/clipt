using System.Collections.Generic;
using System.Linq;

namespace Clipt.KeyboardHooks
{
    public class KeySequence
    {
        public IReadOnlyList<KeyTrigger> Keys { get; }

        public KeySequence(params KeyTrigger[] keys)
        {
            Keys = keys.ToArray();
        }

        public KeySequence(IEnumerable<KeyTrigger> keys)
        {
            Keys = keys.ToArray();
        }
    }
}