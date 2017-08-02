using System;

namespace Clipt.KeyboardHooks
{
    public class SequenceAlreadyRegisteredException : Exception
    {
        public SequenceAlreadyRegisteredException(KeySequenceSpan collision) : base($"The following sequence was already registered (possibly to a longer sequence that started off with this sequence):\r\n{collision.Prefix}")
        {
        }
    }
}