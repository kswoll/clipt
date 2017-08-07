namespace Clipt.Keyboards
{
    public struct KeySequenceHandlerNode : IKeySequenceNode
    {
        public KeySequence Sequence { get; }

        private readonly KeySequenceHandler handler;

        public KeySequenceHandlerNode(KeySequence sequence, KeySequenceHandler handler)
        {
            Sequence = sequence;

            this.handler = handler;
        }

        public void Fire(KeySequence sequence)
        {
            handler(sequence);
        }
    }
}