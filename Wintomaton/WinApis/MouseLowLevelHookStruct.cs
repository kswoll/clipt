using System;
using System.Runtime.InteropServices;

namespace Wintomaton.WinApis
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MouseLowLevelHookStruct
    {
        public MousePoint Point;
        public MouseData MouseData;
        public MouseLowLevelHookStructFlags Flags;
        public uint Time;
        public IntPtr ExtraInfo;
    }
}