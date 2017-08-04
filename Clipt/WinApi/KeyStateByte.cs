using Clipt.Utils;

namespace Clipt.WinApi
{
    public enum KeyStateByte : byte
    {
        None = 0,
        Toggled = 1,
        Pressed = BitUtils.HighOrderBitMaskByte
    }
}