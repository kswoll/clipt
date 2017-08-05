using System.Collections.Generic;

namespace Clipt.Keyboards
{
    public delegate void KeySequenceHandler(IReadOnlyList<KeyTrigger> keys);
}