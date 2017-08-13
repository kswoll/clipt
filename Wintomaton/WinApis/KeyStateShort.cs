using Wintomaton.Utils;

namespace Wintomaton.WinApis
{
    public enum KeyStateShort : ushort
    {
        None = 0,
        Toggled = 1,
        Pressed = BitUtils.HighOrderBitMaskUShort
    }
}