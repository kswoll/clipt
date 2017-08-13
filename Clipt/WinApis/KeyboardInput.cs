using System;
using System.Runtime.InteropServices;

namespace Clipt.WinApis
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct KeyboardInput
    {
        public ushort Vk;
        public ushort Scan;
        public KeyInputEvent Flags;
        public uint Time;
        public IntPtr ExtraInfo;
    }
}