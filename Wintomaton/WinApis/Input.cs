using System.Runtime.InteropServices;

namespace Wintomaton.WinApis
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct Input
    {
        public InputType Type;
        public MouseKeyboardHardwareInput Data;
    }
}