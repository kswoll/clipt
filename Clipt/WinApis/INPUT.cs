using System.Runtime.InteropServices;

namespace Clipt.WinApis
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct INPUT
    {
        public uint Type;
        public MOUSEKEYBDHARDWAREINPUT Data;
    }
}