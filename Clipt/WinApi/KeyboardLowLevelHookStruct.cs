using System;
using System.Runtime.InteropServices;
using Clipt.Keyboards;

namespace Clipt.WinApi
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