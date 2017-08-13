using System;
using System.Runtime.InteropServices;
using Wintomaton.Inputs;

namespace Wintomaton.WinApis
{
    [StructLayout(LayoutKind.Sequential)]
    public struct KeyboardLowLevelHookStruct
    {
        public KeyCode vkCode;
        public uint scanCode;
        public KeyboardLowLevelHookStructFlags flags;
        public uint time;
        public UIntPtr dwExtraInfo;
    }
}