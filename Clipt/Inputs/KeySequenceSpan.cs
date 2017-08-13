using System;
using System.Collections;
using System.Collections.Generic;

namespace Clipt.Inputs
{
    public struct KeySequenceSpan : IEnumerable<KeyData>
    {
        public KeySequence Sequence { get; }
        public int StartIndex { get; }
        public int EndIndex { get; }

        public KeyData Trigger => Sequence[StartIndex];
        public bool IsTerminal => StartIndex == EndIndex - 1;
        public KeySequenceSpan Next => new KeySequenceSpan(Sequence, StartIndex + 1, EndIndex);
        public KeySequenceSpan Prefix => new KeySequenceSpan(Sequence, 0, StartIndex + 1);

        public KeySequenceSpan(KeySequence sequence, int startIndex, int endIndex)
        {
            if (startIndex >= sequence.Count)
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (endIndex <= startIndex)
                throw new ArgumentOutOfRangeException(nameof(endIndex));
            if (endIndex > sequence.Count)
                throw new ArgumentOutOfRangeException(nameof(endIndex));

            Sequence = sequence;
            StartIndex = startIndex;
            EndIndex = endIndex;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<KeyData> GetEnumerator()
        {
            for (var i = StartIndex; i < EndIndex; i++)
            {
                yield return Sequence[i];
            }
        }
    }
}