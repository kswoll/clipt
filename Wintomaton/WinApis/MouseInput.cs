using System;
using System.Runtime.InteropServices;

namespace Wintomaton.WinApis
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct MouseInput
    {
        public int X;
        public int Y;
        public uint MouseData;
        public MouseInputEvent Flags;
        public uint Time;
        public IntPtr ExtraInfo;
    }
}