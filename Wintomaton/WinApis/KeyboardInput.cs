using System;
using System.Runtime.InteropServices;

namespace Wintomaton.WinApis
{
    [StructLayout(LayoutKind.Sequential)]
    public struct KeyboardInput
    {
        public ushort Vk;
        public ushort Scan;
        public KeyInputEvent Flags;
        public uint Time;
        public IntPtr ExtraInfo;
    }
}