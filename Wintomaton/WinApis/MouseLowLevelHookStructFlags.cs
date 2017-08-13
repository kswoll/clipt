using System;

namespace Wintomaton.WinApis
{
    [Flags]
    public enum MouseLowLevelHookStructFlags
    {
        LLMHF_INJECTED = 0x0001,
        LLMHF_LOWER_IL_INJECTED = 0x0002
    }
}