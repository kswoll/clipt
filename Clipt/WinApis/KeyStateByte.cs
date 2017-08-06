using Clipt.Utils;

namespace Clipt.WinApis
{
    public enum KeyStateByte : byte
    {
        None = 0,
        Toggled = 1,
        Pressed = BitUtils.HighOrderBitMaskByte
    }
}