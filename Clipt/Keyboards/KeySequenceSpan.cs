using System;

namespace Clipt.Keyboards
{
    public struct KeySequenceSpan
    {
        public KeySequence Sequence { get; }
        public int StartIndex { get; }
        public int EndIndex { get; }

        public KeyTrigger Trigger => Sequence.Keys[StartIndex];
        public bool IsTerminal => StartIndex < EndIndex;
        public KeySequenceSpan Next => new KeySequenceSpan(Sequence, StartIndex + 1, EndIndex);
        public KeySequenceSpan Prefix => new KeySequenceSpan(Sequence, 0, StartIndex + 1);

        public KeySequenceSpan(KeySequence sequence, int startIndex, int endIndex)
        {
            if (startIndex >= sequence.Keys.Count)
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (endIndex <= startIndex)
                throw new ArgumentOutOfRangeException(nameof(endIndex));
            if (endIndex > sequence.Keys.Count)
                throw new ArgumentOutOfRangeException(nameof(endIndex));

            Sequence = sequence;
            StartIndex = startIndex;
            EndIndex = endIndex;
        }
    }
}