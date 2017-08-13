using System.Runtime.InteropServices;

namespace Wintomaton.WinApis
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MousePoint
    {
        public int X;
        public int Y;
    }
}