using System;
using System.Collections.Generic;

namespace Clipt.KeyboardHooks
{
    public struct KeySequenceHandlerNode : IKeySequenceNode
    {
        private readonly KeySequenceHandler handler;

        public KeySequenceHandlerNode(KeySequenceHandler handler)
        {
            this.handler = handler;
        }

        public void Fire(IReadOnlyList<KeyData> keys)
        {
            handler(keys);
        }
    }
}