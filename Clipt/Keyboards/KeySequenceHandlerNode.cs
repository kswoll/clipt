using System.Collections.Generic;

namespace Clipt.Keyboards
{
    public struct KeySequenceHandlerNode : IKeySequenceNode
    {
        private readonly KeySequenceHandler handler;

        public KeySequenceHandlerNode(KeySequenceHandler handler)
        {
            this.handler = handler;
        }

        public void Fire(IReadOnlyList<KeyTrigger> keys)
        {
            handler(keys);
        }
    }
}