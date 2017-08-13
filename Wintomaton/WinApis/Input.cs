using System.Runtime.InteropServices;

namespace Wintomaton.WinApis
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Input
    {
        public InputType Type;
        public MouseKeyboardHardwareInput Data;
    }
}