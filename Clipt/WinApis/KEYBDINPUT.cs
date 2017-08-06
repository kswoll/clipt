using System;
using System.Runtime.InteropServices;

namespace Clipt.WinApis
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct KEYBDINPUT
    {
        public ushort Vk;
        public ushort Scan;
        public KEYEVENTF Flags;
        public uint Time;
        public IntPtr ExtraInfo;
    }
}