using Clipt.Utils;

namespace Clipt.WinApi
{
    public enum KeyStateShort : ushort
    {
        None = 0,
        Toggled = 1,
        Pressed = BitUtils.HighOrderBitMaskUShort
    }
}