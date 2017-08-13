using System.Runtime.InteropServices;

namespace Wintomaton.WinApis
{
    [StructLayout(LayoutKind.Sequential)]
    public struct HardwareInput
    {
        public uint Msg;
        public ushort ParamL;
        public ushort ParamH;
    }
}