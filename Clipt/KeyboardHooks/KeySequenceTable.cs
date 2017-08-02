using System;
using System.Collections.Generic;

namespace Clipt.KeyboardHooks
{
    public class KeySequenceTable : KeySequenceBranch
    {
        public static KeySequenceTable Instance { get; } = new KeySequenceTable();

    }
}