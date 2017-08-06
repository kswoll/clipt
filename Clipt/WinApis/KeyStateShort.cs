using Clipt.Utils;

namespace Clipt.WinApis
{
    public enum KeyStateShort : ushort
    {
        None = 0,
        Toggled = 1,
        Pressed = BitUtils.HighOrderBitMaskUShort
    }
}